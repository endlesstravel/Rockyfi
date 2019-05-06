using Love;
using System.Collections.Generic;
using Rockyfi;

namespace RockyfiFactory
{
    class SceneNormalXML: Scene
    {
        ShadowPlaySimple stage = new ShadowPlaySimple();
        public override void Load()
        {
            string tmpXML = @"
<div el-bind:width=""w"" el-bind:height=""h"" flex-wrap=""wrap"" justify-content=""center"" flex-direction=""row"" >
    <div el-for=""itemId in listData"" width=""150px"" height=""100px"" el-bind:id=""itemId""/>
</div>
";
            stage.Build(tmpXML, "listData", "w", "h");
            stage.SetData("listData", new List<string>
            {
                "child-0", "child-1", "child-2", "child-3", "child-4",
            });
            stage.SetData("w", "320px");
            stage.SetData("h", "320px");
            stage.Update();
            System.Console.WriteLine(stage.ToString());
        }

        public override void Update(float dt)
        {
            stage.SetData("w", $"{Graphics.GetWidth() - 200}px");
            stage.SetData("h", $"{Graphics.GetHeight() - 200}px");
            stage.Update();
        }

        public override void Draw()
        {
            Graphics.Translate(100, 100);
            Graphics.SetColor(Color.White);
            stage.Draw(0, 0, (x, y, w, h, text, attr) =>
            {
                Graphics.Rectangle(DrawMode.Line, x, y, w, h);
                Graphics.Print($"{(attr.TryGetValue("id", out object id) ? id : "")}", x, y);
            });
        }
    }
}
