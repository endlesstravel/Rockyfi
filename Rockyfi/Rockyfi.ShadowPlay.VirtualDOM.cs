using System;
using System.Collections.Generic;
using System.Text;
using PropertyDictionary = System.Collections.Generic.Dictionary<string, object>;

namespace Rockyfi
{
    class VirtualDom
    {
        class VNode
        {
            readonly internal string tagName;
            readonly internal string key;
            readonly internal List<VNode> Children = new List<VNode>();
            readonly internal PropertyDictionary Properties = new PropertyDictionary();

            bool thunk = false;
            public static bool IsThunk(VNode vNode)
            {
                return vNode != null && vNode.thunk;
            }
        }

        class PatchOperate
        {
            public const int ChangeNode = 0;
            public const int RemoveNode = 1;
            public const int InsertNode = 2;
            public const int PropertiesChange = 3;
            public const int ReorderNode = 4;
            public const int ThunkChangeNode = 5;

            readonly internal int Type;
            readonly internal VNode vNode;
            readonly internal VNode vPatch;
            readonly internal PropertyDictionary PropertiesDiff;
            readonly internal Patch vThunk;

            private PatchOperate(int type, VNode original, VNode newborn)
            {
                Type = type;
                vNode = original;
                vPatch = newborn;
            }
            private PatchOperate(int type, VNode original, VNode newborn, PropertyDictionary pd)
            {
                Type = type;
                vNode = original;
                PropertiesDiff = pd;
            }
            private PatchOperate(int type, VNode original, VNode newborn, PropertyDictionary pd, Patch patch)
            {
                Type = type;
                vNode = original;
                PropertiesDiff = pd;
                vThunk = patch;
            }

            public static PatchOperate Change(VNode original, VNode newborn)
            {
                return new PatchOperate(ChangeNode, original, newborn);
            }

            public static PatchOperate Remove(VNode original)
            {
                return new PatchOperate(RemoveNode, original, null);
            }

            public static PatchOperate Insert(VNode newborn)
            {
                return new PatchOperate(InsertNode, null, newborn);
            }

            public static PatchOperate Properties(VNode original, PropertyDictionary pd)
            {
                return new PatchOperate(PropertiesChange, original, null, pd);
            }

            public static PatchOperate Reorder(VNode original, VNode newborn)
            {
                return new PatchOperate(ReorderNode, original, newborn);
            }

            public static PatchOperate Thunk(Patch patch)
            {
                return new PatchOperate(ThunkChangeNode, null, null, null, patch);
            }
        }

        class PatchGroup
        {
            List<PatchOperate> list = null;
            PatchOperate monoPatch = null;
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

        class Patch
        {
            readonly VNode Original;
            readonly VNode Newborn;
            readonly internal LinkedList<PatchGroup> groupList = new LinkedList<PatchGroup>();
            public Patch(VNode original)
            {
                Original = original;
            }

            public static Patch Diff(VNode left, VNode right)
            {
                var patch = new Patch(left);
                patch.Walk(left, right);
                return patch;
            }

            void Walk(VNode a, VNode b)
            {
                if (a == b)
                {
                    return;
                }

                var group = new PatchGroup();

                if (VNode.IsThunk(a) && VNode.IsThunk(b)) // diff thunk
                {
                    var patchDiff = Diff(a, b);
                    if (patchDiff.HasPatch)
                    {
                        group.Append(PatchOperate.Thunk(patchDiff));
                    }
                }
                else if (b == null)
                {
                    group.Append(PatchOperate.Remove(a));
                }
                else if (a.tagName == b.tagName && a.key == b.key)
                {
                    var propsPatch = DiffProperties(a.Properties, b.Properties);
                    if (propsPatch != null && propsPatch.Count > 0)
                    {
                        group.Append(PatchOperate.Properties(a, propsPatch));
                    }
                    DiffChildren(a, b, group);
                }
                else
                {
                    group.Append(PatchOperate.Change(a, b));
                }

                if (group.HasChange)
                    groupList.AddLast(group);

            }

            bool HasPatch { get { return groupList.Count > 0; } }


            void DiffChildren(VNode a, VNode b, PatchGroup group)
            {
                var aChildren = a.Children;
                var bChildren = b.Children;
                var aLen = aChildren.Count;
                var bLen = bChildren.Count;
                var len = aLen > bLen ? aLen : bLen;

                for (int i = 0; i < len; i++)
                {
                    var leftNode = len < aChildren.Count ? aChildren[i] : null;
                    var rightNode = len < bChildren.Count ? bChildren[i] : null;

                    if (leftNode == null)
                    {
                        if (rightNode != null)
                        {
                            // Excess nodes in b need to be added
                            group.Append(PatchOperate.Insert(rightNode));
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
                else if (newborn != kv.Value) // change it
                {
                    diff[kv.Key] = newborn;
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
