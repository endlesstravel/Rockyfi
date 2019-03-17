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

        LovePrinterElement parent = null;
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
        }

        public override void OnRemove(Rockyfi.ShadowPlay.Element child)
        {
            Children.Remove(child);
        }

        public override void OnRemoveAt(int index)
        {
            Children.RemoveAt(index);
        }

        public override void OnChangeText(string text)
        {
        }

        public override void OnAddChild(Rockyfi.ShadowPlay.Element child)
        {
            Children.Add(child);
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

        public void Draw()
        {
            Queue<LovePrinterElement> queue = new Queue<LovePrinterElement>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                var ele = queue.Dequeue();
                if (ele != null)
                {
                    DrawNode(ele.LayoutGetLeft(), ele.LayoutGetTop(),
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
    }

}
