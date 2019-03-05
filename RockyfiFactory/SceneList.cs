using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Love;

namespace RockyfiFactory
{
    class SceneList: Scene
    {
        Rockyfi.Factory factory = new Rockyfi.Factory();
        public override void Load()
        {
            string tmpXML3List = @"<root>
<div el-bind:padding-top=""pt"" el-bind:width=""w"" el-bind:height=""styleObj.StyleHeight"" flex-wrap=""wrap"" justify-content=""center"" flex-direction=""row"" >
    <div el-for=""item in list"" el-if=""item != '2' "" el-bind:margin-top=""mt"" el-bind:id=""item"" width=""100px"" height=""100px""/>
</div>
</root>";
            factory = new Rockyfi.Factory();
            factory.Load(tmpXML3List, new Dictionary<string, object>()
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
            factory.CalculateLayout();
        }

        public override void Draw()
        {
            Graphics.SetColor(Color.White);
            Graphics.Translate(100, 100);
            factory.Draw((x, y, w, h, text, attr) =>
            {
                Graphics.Rectangle(DrawMode.Line, x, y, w, h);
                Graphics.Print($"{(attr.TryGetValue("id", out object id) ? id : "")}", x, y);
            });
        }
    }
}
