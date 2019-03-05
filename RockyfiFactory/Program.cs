using System;
using System.Collections.Generic;
using System.IO;
using Love;

namespace FlexCs
{
    class Program : Scene
    {
        Rockyfi.Factory factory = new Rockyfi.Factory();

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
    </div>
</div>
";
            factory.Load(tmpXML3, new Dictionary<string, object>()
            {
                { "styleObj", this},
                { "w", "620px" },
                { "mt", "5px" },
                { "pt", "15px" },
                { "list", new List<string>{ "1", "2", "3", "4", "5" } },
            });
        }

        public string StyleHeight => "200px";

        public override void Update(float dt)
        {
            Love.Misc.FPSGraph.FPSGraph.Update(dt);
            for (int i = 0; i < 100; i++)
            {
                factory.SetData("list", new List<object> { 2222 });
                factory.ReRender();
            }
        }

        public override void Draw()
        {
            Graphics.SetBackgroundColor(85 / 255f, 77 / 255f, 216 / 255f);
            Love.Misc.FPSGraph.FPSGraph.Draw();

            Graphics.SetColor(Color.White);
            Graphics.Translate(100, 100);
            factory.Draw((x, y, w, h, text, attr) =>
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
