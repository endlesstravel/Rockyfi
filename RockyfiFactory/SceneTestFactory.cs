using System.Collections.Generic;
using Love;
using Rockyfi;


namespace RockyfiFactory
{
    class SceneTestFactory : Scene
    {
        ShadowPlay<LoveBridge.Element> userInterface = new ShadowPlay<LoveBridge.Element>();
        LoveBridge fac = new LoveBridge();

        bool useFactory = true;

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
        </div>
    </div>
</div>
";
            string tmpXML = @"
<div el-bind:padding-top=""pt""  el-bind:width=""w""  el-bind:height=""styleObj.StyleHeight"" id=""root""
    flex-wrap=""wrap"" justify-content=""center"" flex-direction=""row"" >
    <div el-for=""item in list"" flex-wrap=""wrap"" el-if=""item != '2' "" el-bind:margin=""mt"" el-bind:id=""item"" width=""150px"" height=""150px"">
        my id is {{item}}
        <div el-for=""item in list"" flex-wrap=""wrap"" el-if=""item != '2' "" el-bind:id=""item"" width=""50px"" height=""50px"">
            my id is {{item}}
        </div>
    </div>
</div>
";
            userInterface.Build(tmpXML);
            userInterface.SetData(new Dictionary<string, object>()
            {
                { "styleObj", this},
                { "w", "620px" },
                { "mt", "5px" },
                { "pt", "15px" },
            });

            if (useFactory)
            {
                userInterface.SetBridge(fac);
            }

            userInterface.SetData("list", new List<string> { "a3" });
            userInterface.Update();
            System.Console.WriteLine("--------------------------------");
            System.Console.WriteLine(userInterface.ToString());
            System.Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            System.Console.WriteLine(fac.ToString());
            System.Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");

            userInterface.SetData("list", new List<string> { "b3", "b5" });
            userInterface.Update();
            System.Console.WriteLine("--------------------------------");
            System.Console.WriteLine(userInterface.ToString());

            userInterface.SetData("list", new List<string> { "c1", "c2", "c3" });
            userInterface.Update();
            System.Console.WriteLine("--------------------------------");
            System.Console.WriteLine(userInterface.ToString());


            userInterface.SetData("list", new List<string> { "a3" });
            userInterface.Update();
            System.Console.WriteLine("--------------------------------");
            System.Console.WriteLine(userInterface.ToString());

            userInterface.SetData("list", new List<string> { "c1", "c2", "c3" });
            userInterface.Update();
            System.Console.WriteLine("--------------------------------");
            System.Console.WriteLine(userInterface.ToString());


            userInterface.SetData("list", new List<string> { "b3", "b5" });
            userInterface.Update();
            System.Console.WriteLine("--------------------------------");
            System.Console.WriteLine(userInterface.ToString());
        }

        public string StyleHeight => "200px";

        public override void Update(float dt)
        {
            Love.Misc.FPSGraph.Update(dt);

            if (((int)(Timer.GetTime() * 1000)) % 2000 > 1000)
            {
                userInterface.SetData("list", new List<string> { "b1", "b2", "b3", "b4", "b5" });
            }
            else
            {
                userInterface.SetData("list", new List<string> { "x1", "x2", "x3" });
            }

            userInterface.Update();
        }

        public override void Draw()
        {
            Graphics.SetBackgroundColor(85 / 255f, 77 / 255f, 216 / 255f);
            Love.Misc.FPSGraph.Draw();

            Graphics.SetColor(Color.White);

            if (useFactory)
            {
                fac.Draw(100, 100);
            }
            else
            {
                userInterface.Draw(100, 100, (float x, float y, float w, float h, string text, Dictionary<string, object> attr) =>
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
}
