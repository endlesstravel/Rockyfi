using Love;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rockyfi;
using Love.Misc;

namespace LoveBridge
{
    public class Bridge : Bridge<Element>
    {
        public delegate ElementController CreateUIElementDelegate(string tagName, Dictionary<string, object> attr);
        CreateUIElementDelegate createUIFunc;
        public delegate void SetRootParentDelegate(ElementController root);
        SetRootParentDelegate setRootParentUIFunc;

        ShadowPlay<Element> shadowPlay;

        public Bridge(ShadowPlay<Element> shadowPlay, CreateUIElementDelegate createUIFunc, SetRootParentDelegate setRootParentUIFunc)
        {
            this.createUIFunc = createUIFunc;
            this.setRootParentUIFunc = setRootParentUIFunc;
            this.shadowPlay = shadowPlay;
        }

        public override Element CreateElement(string tagName, Dictionary<string, object> attr)
        {
            var ele = new Element();
            ele.flashElement = createUIFunc(tagName, attr);
            ele.flashElement.Element = ele;
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
            if (setRootParentUIFunc != null)
                setRootParentUIFunc(root.flashElement);
        }

        public delegate void DrawNodeDelegate(float x, float y, float w, float h, string text, Dictionary<string, object> attr);

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

        public void Draw(float x, float y, DrawNodeDelegate drawNodeFunc)
        {
            if (drawNodeFunc == null)
                throw new System.ArgumentNullException("drawNodeFunc");

            Queue<Element> queue = new Queue<Element>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                var ele = queue.Dequeue();
                if (ele != null)
                {
                    GetLeftTop(ele, out var l, out var t);
                    drawNodeFunc(l + x, t + y,
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

        public void Recursive(Element element)
        {

        }

        public void Update()
        {
            Queue<Element> queue = new Queue<Element>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                var ele = queue.Dequeue();
                if (ele != null)
                {
                    ele.UpdateLayout();
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

    public class Element : BridgeElement<Element>
    {
        public ElementController flashElement;

        public override object OnGetThisValue()
        {
            return flashElement;
        }

        public Element Parent { private set; get; } = null;
        public List<Element> Children { get; } = new List<Element>();
        public Dictionary<string, object> Attributes { get; } = new Dictionary<string, object>();

        public override void OnAfterCreated()
        {
            foreach (var attr in Attributes)
            {
                flashElement.OnChangeAttributes(attr.Key, attr.Value);
            }

            UpdateScroll();
            if (GetText() != null)
                OnChangeText(GetText());
        }

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
            flashElement.OnChangeAttributes(key, value);
        }

        public void UpdateLayout()
        {
            UpdateScroll();
            Vector2 offset = Vector2.Zero;
            if (Parent != null && Parent.flashElement.HasScrollOffset)
            {
                offset = Parent.flashElement.ScrollOffset;
            }

            flashElement.OnUpdateLayout(
                LayoutGetLeft() + offset.X, LayoutGetTop() + offset.Y,
                LayoutGetWidth(), LayoutGetHeight());
        }

        public RectangleF GetChildrenBound()
        {
            RectangleF bound = new RectangleF();
            bound.X = LayoutGetLeft();
            bound.Y = LayoutGetTop();

            if (Children.Count > 1) // 初始化...
            {
                var c = Children[0];
                bound.X = c.LayoutGetLeft();
                bound.Y = c.LayoutGetTop();
                bound.Width = c.LayoutGetWidth();
                bound.Height = c.LayoutGetHeight();
            }
            for (int i = 1; i < Children.Count; i++)
            {
                var c = Children[i];
                bound.Left = c.LayoutGetLeft() < bound.Left ? c.LayoutGetLeft() : bound.Left;
                bound.Top = c.LayoutGetTop() < bound.Top ? c.LayoutGetTop() : bound.Top;

                var right = c.LayoutGetLeft() + c.LayoutGetWidth();
                var bottom = c.LayoutGetTop() + c.LayoutGetHeight();
                bound.Right = right > bound.Right ? right : bound.Right;
                bound.Bottom = bottom > bound.Bottom ? bottom : bound.Bottom;
            }
            return bound;
        }

        public void UpdateScroll()
        {
            flashElement.OnUpdateOverflow(StyleGetOverflow(), GetChildrenBound());
        }

        public override void OnInsertChild(int index, Element child)
        {
            Children.Insert(index, child);
            child.Parent = this;
            //--------------- split ------------------------
            child.flashElement.OnSetParent(flashElement);
            //child.rectTransform.SetParent(rectTransform);
            //child.rectTransform.SetSiblingIndex(index); // no need to adjust child index
        }

        public override void OnRemove(Element child)
        {
            if (Children.Remove(child))
            {
                child.Parent = null;

                //--------------- split ------------------------
                child.flashElement.OnRemoved();
            }

        }

        public override void OnRemoveAt(int index)
        {
            if (0 <= index && index < Children.Count)
            {
                OnRemove(Children[index] as Element);
                //child.Parent = null;
                //Children.RemoveAt(index);
                //child.flashElement.OnRemoved();
            }
        }

        public override void OnChangeText(string text)
        {
            flashElement.OnChangeText(text);
        }

        public override void OnAddChild(Element child)
        {
            Children.Add(child);
            child.Parent = this;
            //--------------- split ------------------------
            child.flashElement.OnSetParent(flashElement);
            //child.rectTransform.SetParent(rectTransform);
        }

        public override void OnReplaceChild(Element oldChild, Element child)
        {
            var index = Children.IndexOf(oldChild);
            if (0 <= index && index < Children.Count)
            {
                Children[index] = child;
                child.Parent = this;

                oldChild.flashElement.OnRemoved();
                child.flashElement.OnSetParent(flashElement);
            }
            //--------------- split ------------------------
            //var sindex = oldChild.rectTransform.GetSiblingIndex();
            //Object.Destroy(oldChild.rectTransform.gameObject);
            //child.rectTransform.SetSiblingIndex(sindex); // no need to adjust child index
        }

        public string ToString(int deep)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string tab = "";
            for (int i = 0; i < deep; i++) tab += "....";
            sb.Append(tab);
            sb.Append("<div");

            sb.Append($" pos:\"{LayoutGetLeft()} {LayoutGetTop()} {LayoutGetWidth()} {LayoutGetHeight()}\" ");

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

}
