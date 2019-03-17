using System;
using System.Collections.Generic;
using Rockyfi;
using Love;

namespace RockyfiFactory
{
    class Util
    {
        static public float WatchTime(Action action)
        {
            var stopWatch = new System.Diagnostics.Stopwatch();
            double startTime = Timer.GetTime();
            var now = System.DateTime.Now; // <-- Value is copied into local
            stopWatch.Start();
            action();
            stopWatch.Stop();
            long delta = stopWatch.ElapsedMilliseconds;
            System.Console.WriteLine("" + stopWatch.ElapsedMilliseconds / 1000.0f
                + "   \t" + (Timer.GetTime() - startTime)
                + "   \t" + (System.DateTime.Now.Ticks - now.Ticks) / (float)System.TimeSpan.TicksPerSecond
                );
            return (System.DateTime.Now.Ticks - now.Ticks) / (float)System.TimeSpan.TicksPerSecond;
        }
    }

    internal class LovePrinterElement :Rockyfi.ShadowPlay.Element
    {
        public List<Rockyfi.ShadowPlay.Element> Children { get; } = new List<Rockyfi.ShadowPlay.Element>();
        public Dictionary<string, object> Attributes { get; } = new Dictionary<string, object>();

        public LovePrinterElement Parent { private set; get; } = null;
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

        public override void OnInsertChild(int index, Rockyfi.ShadowPlay.Element child)
        {
            Children.Insert(index, child);
            (child as LovePrinterElement).Parent = this;
        }

        public override void OnRemove(Rockyfi.ShadowPlay.Element child)
        {
            if(Children.Remove(child))
            {
                (child as LovePrinterElement).Parent = null;
            }
        }

        public override void OnRemoveAt(int index)
        {
            if (0 <= index && index < Children.Count)
            {
                (Children[index] as LovePrinterElement).Parent = null;
            }
            Children.RemoveAt(index);
        }

        public override void OnChangeText(string text)
        {
        }

        public override void OnAddChild(Rockyfi.ShadowPlay.Element child)
        {
            Children.Add(child);
            (child as LovePrinterElement).Parent = this;
        }

        public override void OnReplaceChild(ShadowPlay.Element oldChild, ShadowPlay.Element child)
        {
            var index = Children.IndexOf(oldChild);
            if (0 <= index && index < Children.Count)
            {
                Children[index] = child;
                (child as LovePrinterElement).Parent = this;
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
            foreach (var c in Children)
            {
                sb.Append(tab);
                sb.AppendLine((c as LovePrinterElement).ToString(deep + 1));
            }
            sb.Append(tab);
            sb.AppendLine("</div>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString(0);
        }
    }

    internal class LovePrinter : Rockyfi.ShadowPlay.ElementFactory
    {
        public override Rockyfi.ShadowPlay.Element CreateElement(string tagName, Dictionary<string, object> attr)
        {
            var ele = new LovePrinterElement();
            foreach (var kv in attr)
            {
                ele.Attributes[kv.Key] = kv.Value;
            }
            return ele;
        }

        LovePrinterElement root;
        public override void SetRootElement(Rockyfi.ShadowPlay.Element root)
        {
            this.root = (LovePrinterElement)root;
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

        public void GetLeftTop(LovePrinterElement e, out float x, out float y)
        {
            x = e.LayoutGetLeft();
            y = e.LayoutGetTop();
            LovePrinterElement ele = e.Parent;
            while (ele != null)
            {
                x += ele.LayoutGetLeft();
                y += ele.LayoutGetTop();
                ele = ele.Parent;
            }
        }

        public void Draw(float x, float y)
        {
            Queue<LovePrinterElement> queue = new Queue<LovePrinterElement>();
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
                        queue.Enqueue(child as LovePrinterElement);
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
