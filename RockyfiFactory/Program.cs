using System.Collections.Generic;
using Love;
using Rockyfi;

namespace RockyfiFactory
{
    class LovePrinterElement : Rockyfi.ShadowPlay.Element<LovePrinterElement>
    {
        Dictionary<string, object> eleAttr = new Dictionary<string, object>();
        List<LovePrinterElement> children = new List<LovePrinterElement>();
        LovePrinterElement parent = null;
        public override void ChangeAttributes(Dictionary<string, object> attributes)
        {
            foreach (var attr in attributes)
            {
                eleAttr[attr.Key] = attr.Value;
            }
        }

        public override void Destory(LovePrinterElement element)
        {
            if (parent != null)
            {
                parent.children.Remove(this);
            }
        }

        public override void InsertChild(int index, LovePrinterElement child)
        {
            children.Insert(index, child);
        }

        public override void Remove(LovePrinterElement child)
        {
            children.Remove(child);
        }

        public override void RemoveAt(int index)
        {
            children.RemoveAt(index);
        }
    }

    class LovePrinter : Rockyfi.ShadowPlay.ElementFactory<LovePrinterElement>
    {
        public override LovePrinterElement CreateElement(string tagName, Dictionary<string, object> attr)
        {
            var ele = new LovePrinterElement();
            ele.ChangeAttributes(attr);
            return ele;
        }
    }

    class Program : Scene
    {
        Rockyfi.ShadowPlay userInterface = new Rockyfi.ShadowPlay();

        public override void Load()
        {
            string tmpXML3 = @"
<div el-bind:padding-top=""pt"" 
    el-bind:width=""w"" 
    el-bind:height=""styleObj.StyleHeight""
    id=""root""
    flex-wrap=""wrap"" justify-content=""center"" flex-direction=""row"" >
    <div el-for=""item in list"" el-if=""item != '2' "" el-bind:margin=""mt"" el-bind:id=""item"" width=""100px"" height=""100px"">
        my id is {{item}}
        <div el-for=""item in list"" el-if=""item != '2' "" el-bind:margin=""mt"" el-bind:id=""item"" width=""50px"" height=""50px"">
            my id is {{item}}
            <div el-for=""item in list"" el-if=""item != '2' "" el-bind:margin=""mt"" el-bind:id=""item"" width=""20px"" height=""20px"">
                my id is {{item}}
                <div el-for=""item in list"" el-if=""item != '2' "" el-bind:margin=""mt"" el-bind:id=""item"" width=""10px"" height=""10px"">
                    my id is {{item}}
                    <div el-for=""item in list"" el-if=""item != '2' "" el-bind:margin=""mt"" el-bind:id=""item"" width=""10px"" height=""10px"">
                        my id is {{item}}
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
";
            string tmpXML4 = @"
<div el-bind:padding-top=""pt"" 
    el-bind:width=""w"" 
    el-bind:height=""styleObj.StyleHeight""
    id=""root""
    flex-wrap=""wrap"" justify-content=""center"" flex-direction=""row"" >
    <div el-for=""item in list"" el-if=""item != '2' "" el-bind:margin=""mt"" el-bind:id=""item"" width=""100px"" height=""100px"">
        my id is {{item}}
    </div>
</div>
";

            var list = new List<string> { "1", "2", "3", "4", "5", "6" };
            //for (int i = 0; i < 10000; i++)
            //{
            //    list.Add("xx" + i);
            //}

            userInterface.Load(tmpXML3, new Dictionary<string, object>()
            {
                { "styleObj", this},
                { "w", "620px" },
                { "mt", "5px 5px 2px 2px" },
                { "pt", "15px" },
                { "list", list },
            });
        }

        public string StyleHeight => "200px";

        public override void Update(float dt)
        {
            Love.Misc.FPSGraph.FPSGraph.Update(dt);

            var stopWatch = new System.Diagnostics.Stopwatch();
            double startTime = Timer.GetTime();
            var now = System.DateTime.Now; // <-- Value is copied into local
            stopWatch.Start();

            //if (((int)(Timer.GetTime() * 1000)) % 1000 > 500)
            //{
            //    userInterface.SetData("list", new List<string> { "1", "2", "3", "4", "5" });
            //}
            //else
            //{
            //    userInterface.SetData("list", new List<string> { "3", "4", "5" });
            //}

            //userInterface.SetData("list", new List<string> { "1", "2", "3", "4", "5" });


            for (int i = 0; i < 1; i++)
            {
                //card.SetData("list", new List<object> { 2222 });
                //card.ReRender();
                userInterface.Update();
            }
            stopWatch.Stop();
            long delta = stopWatch.ElapsedMilliseconds;
            System.Console.WriteLine("" + stopWatch.ElapsedMilliseconds / 1000.0f
                + "   \t" + (Timer.GetTime() - startTime)
                + "   \t" + (System.DateTime.Now.Ticks - now.Ticks) / (float)System.TimeSpan.TicksPerSecond
                );
        }

        public override void Draw()
        {
            Graphics.SetBackgroundColor(85 / 255f, 77 / 255f, 216 / 255f);
            Love.Misc.FPSGraph.FPSGraph.Draw();

            Graphics.SetColor(Color.White);
            Graphics.Translate(100, 100);
            //userInterface.Draw((x, y, w, h, text, attr) =>
            //{
            //    Graphics.Rectangle(DrawMode.Line, x, y, w, h);
            //    Graphics.Print($"{(attr.TryGetValue("id", out object id) ? id : "")}", x, h + y - Graphics.GetFont().GetHeight());

            //    if (text != null)
            //    {
            //        Graphics.Printf(text.Trim(), x, y, w, AlignMode.Center);
            //    }
            //});
        }

        static void Main(string[] args)
        {
            Boot.Init(new BootConfig
            {

            });
            Boot.Run(new Program());
        }
    }
}
