using System.Collections.Generic;
using Love;

namespace RockyfiFactory
{
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

            var list = new List<string> { "1", "2", "3", "4", "5" };
            for (int i = 0; i < 1; i++)
            {
                // list.Add("xx" + i);
            }

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
            userInterface.Draw((x, y, w, h, text, attr) =>
            {
                Graphics.Rectangle(DrawMode.Line, x, y, w, h);
                Graphics.Print($"{(attr.TryGetValue("id", out object id) ? id : "")}", x, h + y - Graphics.GetFont().GetHeight());

                if (text != null)
                {
                    Graphics.Printf(text.Trim(), x, y, w, AlignMode.Center);
                }
            });
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
