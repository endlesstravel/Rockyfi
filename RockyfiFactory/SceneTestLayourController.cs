using System;
using System.Collections.Generic;
using Love;

namespace RockyfiFactory
{
    class ExampleLayourController : LayoutController
    {
        public override ElementController CreateElement(string tagName, Dictionary<string, object> attr)
        {
            return null;
        }

        public override Dictionary<string, object> DefineInitialData()
        {
            return new Dictionary<string, object>
            {

                { "listData", new List<string>{
                        "child-0", "child-1", "child-2", "child-3", "child-4",
                }},
                { "w", "320px" },
                { "h", "320px" },
            };
        }

        public override string DefineLayoutXml()
        {
            return @"
<div el-bind:width=""w"" el-bind:height=""h"" flex-wrap=""wrap"" justify-content=""center"" flex-direction=""row"" >
    <div el-for=""itemId in listData"" width=""150px"" height=""100px"" el-bind:id=""itemId""/>
</div>
";
        }
    }

    class SceneTestLayourController: Scene
    {
        ExampleLayourController elc = new ExampleLayourController();

        public override void Load()
        {
            elc.Start();
        }

        public override void Update(float dt)
        {
            elc.Update();
        }

        public override void Draw()
        {
            elc.Draw(0, 0, (x, y, width, height, text, attribute)=>{
                Graphics.Rectangle(DrawMode.Line, x, y, width, height);
            });
        }
    }
}
