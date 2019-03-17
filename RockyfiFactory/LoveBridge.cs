using System;
using System.Collections.Generic;
using Rockyfi;
using Love;

namespace RockyfiFactory
{

    public class LoveBridge: Bridge<LoveBridge.Element>
    {
        public class Element : BridgeElement<Element>
        {
            public List<Element> Children { get; } = new List<Element>();
            public Dictionary<string, object> Attributes { get; } = new Dictionary<string, object>();

            public Element Parent { private set; get; } = null;
            public override void OnChangeAttributes(string key, object value)
            {
                if (key == null)
                {
                    return;
                }
                if (value == null)
                {
                    Attributes.Remove(key);
                }
                else
                {
                    Attributes[key] = value;
                }
            }

            public override void OnInsertChild(int index, Element child)
            {
                Children.Insert(index, child);
                child.Parent = this;
            }

            public override void OnRemove(Element child)
            {
                if (Children.Remove(child))
                {
                    child.Parent = null;
                }
            }

            public override void OnRemoveAt(int index)
            {
                if (0 <= index && index < Children.Count)
                {
                    (Children[index] as Element).Parent = null;
                }
                Children.RemoveAt(index);
            }

            public override void OnChangeText(string text)
            {
            }

            public override void OnAddChild(Element child)
            {
                Children.Add(child);
                child.Parent = this;
            }

            public override void OnReplaceChild(Element oldChild, Element child)
            {
                var index = Children.IndexOf(oldChild);
                if (0 <= index && index < Children.Count)
                {
                    Children[index] = child;
                    child.Parent = this;
                }
            }

            public string ToString(int deep)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string tab = "";
                for (int i = 0; i < deep; i++) tab += "....";
                sb.Append(tab);
                sb.Append("<div");

                foreach (var attr in Attributes)
                {
                    sb.Append(" " + attr.ToString());
                }
                sb.AppendLine(">");
                System.Text.StringBuilder csb = new System.Text.StringBuilder();
                foreach (var c in Children)
                {
                    csb.Append((c as Element).ToString(deep + 1));
                }
                sb.Append(string.Join("\n", csb));
                sb.Append(tab);
                sb.AppendLine("</div>");
                return sb.ToString();
            }

            public override string ToString()
            {
                return ToString(0);
            }
        }

        public override Element CreateElement(string tagName, Dictionary<string, object> attr)
        {
            var ele = new Element();
            foreach (var kv in attr)
            {
                ele.Attributes[kv.Key] = kv.Value;
            }
            return ele;
        }

        Element root;
        public override void OnSetRoot(Element root)
        {
            this.root = root;
        }

        public void DrawNode(float x, float y, float w, float h, string text, Dictionary<string, object> attr)
        {
            Graphics.Rectangle(DrawMode.Line, x, y, w, h);
            Graphics.Print($"{(attr.TryGetValue("id", out object id) ? id : "")}", x, h + y - Graphics.GetFont().GetHeight());

            if (text != null)
            {
                Graphics.Printf(text.Trim(), x, y, w, AlignMode.Center);
            }
        }

        public void GetLeftTop(Element e, out float x, out float y)
        {
            x = e.LayoutGetLeft();
            y = e.LayoutGetTop();
            Element ele = e.Parent;
            while (ele != null)
            {
                x += ele.LayoutGetLeft();
                y += ele.LayoutGetTop();
                ele = ele.Parent;
            }
        }

        public void Draw(float x, float y)
        {
            Queue<Element> queue = new Queue<Element>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                var ele = queue.Dequeue();
                if (ele != null)
                {
                    GetLeftTop(ele, out var l, out var t);
                    DrawNode(l + x, t + y,
                        ele.LayoutGetWidth(), ele.LayoutGetHeight(),
                        ele.GetText(),
                        ele.Attributes // contextAttr.attributes ???
                        );
                    foreach (var child in ele.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }

        public override string ToString()
        {
            return root.ToString();
        }
    }

}
