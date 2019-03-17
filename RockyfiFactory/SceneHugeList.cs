using System.Collections.Generic;
using Love;

namespace RockyfiFactory
{
    class SceneHugeList : Scene
    {
        Rockyfi.ShadowPlay stage = new Rockyfi.ShadowPlay();
        public override void Load()
        {
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
            for (int i = 0; i < 10000; i++)
            {
                list.Add("xx" + i);
            }
            stage = new Rockyfi.ShadowPlay();
            stage.Build(tmpXML4, "styleObj", "w", "mt", "pt", "list");
            stage.SetData(new Dictionary<string, object>()
            {
                { "styleObj", this},
                { "w", "620px" },
                { "mt", "5px" },
                { "pt", "15px" },
                { "list", list },
            });
        }

        public string StyleHeight => "200px";

        public override void Update(float dt)
        {
            stage.Update();
        }

        public override void Draw()
        {
            Graphics.SetColor(Color.White);
            Graphics.Translate(100, 100);
            stage.DrawTraversely(0, 0, (x, y, w, h, text, attr) =>
            {
                Graphics.Rectangle(DrawMode.Line, x, y, w, h);
                Graphics.Print($"{(attr.TryGetValue("id", out object id) ? id : "")}" + text, x, y);
            });
        }
    }
}
