using System;
using System.Collections.Generic;
using System.Text;
using PropertyDictionary = System.Collections.Generic.Dictionary<string, object>;

namespace Rockyfi
{
    class VirtualDom
    {
        class VirtualNode
        {
            readonly internal string tagName;
            readonly internal string key;
            readonly internal List<VirtualNode> Children = new List<VirtualNode>();
            readonly internal PropertyDictionary Properties = new PropertyDictionary();
        }

        class PatchOperate
        {
            public const int Change = 0;
            public const int Remove = 1;
            public const int Insert = 2;
            public const int Properties = 3;
            public const int Reorder = 4;
            readonly internal int Type;
            readonly internal VirtualNode Original;
            readonly internal VirtualNode Newborn;
            readonly internal PropertyDictionary Diff;

            public PatchOperate(VirtualNode original, PropertyDictionary diff)
            {
                Original = original;
                Type = Properties;
                Diff = diff;
            }
            public PatchOperate(int Type, VirtualNode original, VirtualNode newborn)
            {
                Original = original;
                Newborn = newborn;
            }
        }

        class PatchApply
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
        }

        class Patch
        {
            readonly VirtualNode Original;
            readonly VirtualNode Newborn;
            readonly internal List<PatchApply> Applys = new List<PatchApply>();
            public Patch(VirtualNode original)
            {
                Original = original;
            }

            static Patch Diff(VirtualNode left, VirtualNode right)
            {
                var patch = new Patch(left);
                patch.Walk(left, right, 0);
                return patch;
            }
            void Walk(VirtualNode a, VirtualNode b, int index)
            {
                if (a == b)
                {
                    return;
                }
                var apply = this.Applys[index];

                if (b == null)
                {
                    apply.Append(new PatchOperate(PatchOperate.Remove, a, b));
                }
                else if (a.tagName == b.tagName && a.key == b.key)
                {
                    var propsPatch = DiffProps(a.Properties, b.Properties);
                    if (propsPatch.Count > 0)
                    {
                        apply.Append(new PatchOperate(a, propsPatch));
                    }
                    DiffChildren(a, b, apply, index);
                }
                else
                {
                    apply.Append(new PatchOperate(PatchOperate.Change, a, b));
                }
            }

            void DiffChildren(VirtualNode a, VirtualNode b, PatchApply apply, int index)
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

                    index += 1;

                    if (leftNode == null)
                    {
                        if (rightNode != null)
                        {
                            // Excess nodes in b need to be added
                            apply.Append(new PatchOperate(PatchOperate.Insert, null, rightNode));
                        }
                    }
                    else
                    {
                        Walk(leftNode, rightNode, index);
                    }
                }
            }
        }



        static PropertyDictionary DiffProps(PropertyDictionary original, PropertyDictionary newborn)
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

            return diff;
        }
    }
}
