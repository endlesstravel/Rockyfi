using System.Collections.Generic;
using Love;
using Rockyfi;

namespace RockyfiFactory
{
    class SceneExopnentXML : Scene
    {
        ShadowPlay<LoveBridge.Element> userInterface = new ShadowPlay<LoveBridge.Element>();
        LoveBridge printer = new LoveBridge();

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
            userInterface.Build(tmpXML3);
            userInterface.SetBridge(printer);
        }

        public string StyleHeight => "200px";

        public override void Update(float dt)
        {
            Love.Misc.FPSGraph.Update(dt);

            Util.WatchTime(() =>
            {

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
                    var list = new List<string> { "1", "2", "3", "4", "5", "6" };
                    userInterface.SetData(new Dictionary<string, object>
                    {
                        { "styleObj", this},
                        { "w", "620px" },
                        { "mt", "5px 5px 2px 2px" },
                        { "pt", "15px" },
                        { "list", list },
                    });

                    //card.SetData("list", new List<object> { 2222 });
                    //card.ReRender();
                    userInterface.Update();
                }
            });
        }

        public override void Draw()
        {
            Graphics.SetBackgroundColor(85 / 255f, 77 / 255f, 216 / 255f);
            Love.Misc.FPSGraph.Draw();

            Graphics.SetColor(Color.White);
            Graphics.Translate(100, 100);
            userInterface.Draw(0, 0, (x, y, w, h, text, attr) =>
            {
                Graphics.Rectangle(DrawMode.Line, x, y, w, h);
                Graphics.Print($"{(attr.TryGetValue("id", out object id) ? id : "")}", x, h + y - Graphics.GetFont().GetHeight());

                if (text != null)
                {
                    Graphics.Printf(text.Trim(), x, y, w, AlignMode.Center);
                }
            });
        }
    }
}
