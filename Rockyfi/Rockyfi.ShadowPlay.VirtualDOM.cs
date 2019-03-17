using System;
using System.Collections.Generic;
using System.Text;
using static Rockyfi.ShadowPlay;
using PropertyDictionary = System.Collections.Generic.Dictionary<Rockyfi.ShadowPlay.AttributeDataBindExpress, object>;

namespace Rockyfi
{
    class VirtualDom
    {
        static void PatchInsertChildRecursive(RuntimeAttribute attr, ElementFactory elementFactory)
        {
            Queue<RuntimeAttribute> queue = new Queue<RuntimeAttribute>();

            foreach (var child in attr.Children)
            {
                queue.Enqueue(child);
            }

            while (queue.Count > 0)
            {
                var ra = queue.Dequeue();
                var pra = ra.Parent;
                ra.element = elementFactory.CreateElement(ra.node, ra.template.TagName, ra.StringAttr);
                pra.element?.OnAddChild(ra.element);

                foreach (var child in ra.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        public abstract class PatchOperate
        {
            public abstract void DoPatch(ShadowPlay shadowPlay, ElementFactory elementFactory);
        }

        public class PatchOperateChange: PatchOperate
        {
            readonly internal RuntimeAttribute vNode;
            readonly internal RuntimeAttribute vPatch;
            public PatchOperateChange(RuntimeAttribute original, RuntimeAttribute newborn)
            {
                this.vNode = original;
                this.vPatch = newborn;
            }

            public override void DoPatch(ShadowPlay shadowPlay, ElementFactory elementFactory)
            {
                if (vNode.node.Parent != null)
                {
                    var pchildren = vNode.node.Parent.Children;
                    int index = pchildren.IndexOf(vNode.node);
                    vPatch.node.Parent = vNode.node.Parent; // change parent
                    pchildren[index] = vPatch.node; // change node
                    vNode.node.Parent.MarkAsDirty();

                    // template
                    vNode.Parent.Children[index] = vPatch;

                    // change child
                    if (elementFactory != null)
                    {
                        var pra = GetNodeRuntimeAttribute(vNode.node.Parent);
                        vPatch.element = elementFactory.CreateElement(vPatch.node, vPatch.template.TagName, vPatch.StringAttr);
                        pra.element?.OnReplaceChild(vNode.element, vPatch.element);

                        PatchInsertChildRecursive(vPatch, elementFactory);
                    }
                }
            }
        }

        public class PatchOperateRemove : PatchOperate
        {
            readonly internal RuntimeAttribute vNode;
            public PatchOperateRemove(RuntimeAttribute original)
            {
                this.vNode = original;
            }

            public override void DoPatch(ShadowPlay shadowPlay, ElementFactory elementFactory)
            {
                if (vNode.node.Parent != null)
                {
                    // flex remove
                    vNode.node.Parent.RemoveChild(vNode.node);

                    // runtime remove
                    vNode.Parent.Children.Remove(vNode);

                    // factory remove
                    if (elementFactory != null)
                    {
                        var pra = vNode.Parent;
                        pra.element?.OnRemove(vNode.element);
                    }
                }
            }
        }

        public class PatchOperateInsert : PatchOperate
        {
            readonly internal RuntimeAttribute vNodePrent;
            readonly internal RuntimeAttribute vPatch;
            public PatchOperateInsert(RuntimeAttribute parent, RuntimeAttribute newborn)
            {
                this.vNodePrent = parent;
                this.vPatch = newborn;
            }

            public override void DoPatch(ShadowPlay shadowPlay, ElementFactory elementFactory)
            {
                // flex add child
                vPatch.node.Parent = null;
                vNodePrent.node.AddChild(vPatch.node);

                // runtime insert
                vNodePrent.AppendChild(vPatch);

                // factory add child
                if (elementFactory != null)
                {
                    vPatch.element = elementFactory.CreateElement(vPatch.node, vPatch.template.TagName, vPatch.StringAttr);
                    vNodePrent.element?.OnAddChild(vPatch.element);

                    PatchInsertChildRecursive(vPatch, elementFactory);
                }
            }
        }

        public class PatchOperateProperties : PatchOperate
        {
            readonly internal RuntimeAttribute vNode;
            readonly internal PropertyDictionary PropertiesDiff;

            public PatchOperateProperties(RuntimeAttribute original, PropertyDictionary pd)
            {
                this.vNode = original;
                this.PropertiesDiff = pd;
            }

            public override void DoPatch(ShadowPlay shadowPlay, ElementFactory elementFactory)
            {
                var props = vNode.attributes;
                foreach (var kv in PropertiesDiff)
                {
                    props[kv.Key] = kv.Value;
                }

                // chaneg attr
                if (elementFactory != null)
                {
                    if (vNode.element != null)
                    {
                        foreach (var kv in PropertiesDiff)
                        {
                            vNode.element.OnChangeAttributes(kv.Key.TargetName, kv.Value);
                            props[kv.Key] = kv.Value;

                            // change style
                            shadowPlay.ProcessNodeStyle(vNode.node, kv.Key.TargetName, kv.Value != null ? kv.ToString() : "");
                        }
                    }
                }
            }
        }

        public class PatchOperateReorder : PatchOperate
        {
            readonly internal RuntimeAttribute vNode;
            readonly internal RuntimeAttribute vPatch;
            public PatchOperateReorder(RuntimeAttribute original, RuntimeAttribute newborn)
            {
                this.vNode = original;
                this.vPatch = newborn;
            }

            public override void DoPatch(ShadowPlay shadowPlay, ElementFactory elementFactory)
            {
                // TODO: ..................
            }
        }

        public class PatchOperateThunk : PatchOperate
        {
            readonly internal Patch vThunk;

            public PatchOperateThunk(Patch patch)
            {
                this.vThunk = patch;
            }

            public override void DoPatch(ShadowPlay shadowPlay, ElementFactory elementFactory)
            {
                // TODO: ..................
            }
        }



        class PatchGroup
        {
            internal List<PatchOperate> list = null;
            internal PatchOperate monoPatch = null;
            public void Append(PatchOperate vPatch)
            {
                if (list != null)
                {
                    list.Add(vPatch);
                }
                else if (monoPatch != null)
                {
                    list = new List<PatchOperate>(2);
                    list.Add(monoPatch);
                    list.Add(vPatch);
                    monoPatch = null;
                }
                else
                {
                    monoPatch = vPatch;
                }
            }

            public bool HasChange { get { return list != null || monoPatch != null; } }
        }

        public class Patch
        {
            readonly RuntimeAttribute Original;
            readonly LinkedList<PatchGroup> groupList = new LinkedList<PatchGroup>();
            public Patch(RuntimeAttribute original)
            {
                Original = original;
            }

            public void DoPatch(ShadowPlay shadowPlay, ElementFactory elementFactory)
            {
                foreach (var group in groupList)
                {
                    if (group.list != null)
                    {
                        foreach (var p in group.list)
                        {
                            p.DoPatch(shadowPlay, elementFactory);
                        }
                    }
                    else if(group.monoPatch != null)
                    {
                        group.monoPatch.DoPatch(shadowPlay, elementFactory);
                    }
                }
            }

            public static Patch Diff(RuntimeAttribute left, RuntimeAttribute right)
            {
                var patch = new Patch(left);
                patch.Walk(left, right);
                return patch;
            }

            void Walk(RuntimeAttribute a, RuntimeAttribute b)
            {
                if (a == b)
                {
                    return;
                }

                var group = new PatchGroup();

                if (a != null && a.IsThunk && b != null && b.IsThunk) // diff thunk
                {
                    // TODO: ....
                    //var patchDiff = Diff(a, b);
                    //if (patchDiff.HasPatch)
                    //{
                    //    group.Append(new PatchOperateThunk(patchDiff));
                    //}
                }
                else if (b == null)
                {
                    group.Append(new PatchOperateRemove(a));
                }
                else if (a.template.TagName == b.template.TagName && a.template.Key == b.template.Key)
                {
                    var propsPatch = DiffProperties(a.attributes, b.attributes);
                    if (propsPatch != null && propsPatch.Count > 0)
                    {
                        group.Append(new PatchOperateProperties(a, propsPatch));
                    }
                    DiffChildren(a, b, group);
                }
                else
                {
                    group.Append(new PatchOperateChange(a, b));
                }

                if (group.HasChange)
                    groupList.AddLast(group);

            }

            bool HasPatch { get { return groupList.Count > 0; } }


            void DiffChildren(RuntimeAttribute a, RuntimeAttribute b, PatchGroup group)
            {
                var aChildren = a.Children;
                var bChildren = b.Children;
                var aLen = aChildren.Count;
                var bLen = bChildren.Count;
                var len = aLen > bLen ? aLen : bLen;

                var leftIter = aChildren.GetEnumerator();
                var rightIter = bChildren.GetEnumerator();
                for (int i = 0; i < len; i++)
                {
                    var leftNode = leftIter.MoveNext() ? leftIter.Current : null;
                    var rightNode = rightIter.MoveNext() ? rightIter.Current : null;

                    if (leftNode == null)
                    {
                        if (rightNode != null)
                        {
                            // Excess nodes in b need to be added
                            group.Append(new PatchOperateInsert(a, rightNode));
                        }
                    }
                    else
                    {
                        Walk(leftNode, rightNode); // change it or remove it.
                    }
                }
            }
        }

        /// <summary>
        /// return null when there no different on vnode.
        /// [key] = null means remove it property
        /// [key] = value means change it property
        /// </summary>
        /// <param name="original"></param>
        /// <param name="newborn"></param>
        /// <returns></returns>
        static PropertyDictionary DiffProperties(PropertyDictionary original, PropertyDictionary newborn)
        {
            var diff = new PropertyDictionary();

            // try find property need remove and changed
            foreach (var kv in original)
            {
                if (!newborn.TryGetValue(kv.Key, out object newbornValue)) // remove it
                {
                    diff[kv.Key] = null;
                }
                else if (newbornValue != kv.Value) // change it
                {
                    diff[kv.Key] = newbornValue;
                }
            }

            // try find property need to add
            foreach (var kv in newborn)
            {
                if (!original.ContainsKey(kv.Key))
                {
                    diff[kv.Key] = kv.Value;
                }
            }

            return diff.Count > 0 ? diff : null;
        }
    }
}
