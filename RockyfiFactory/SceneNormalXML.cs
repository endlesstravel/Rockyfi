using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Love;

namespace RockyfiFactory
{
    class SceneNormalXML: Scene
    {
        Rockyfi.Factory factory = new Rockyfi.Factory();
        public override void Load()
        {
            string tmpXML = @"
<div width=""320px"" height=""300px"" flex-wrap=""wrap"" justify-content=""center"" flex-direction=""row"" >
    <div width=""150px"" height=""100px"" id=""child-0""/>
    <div width=""150px"" height=""100px"" id=""child-1""/>
    <div width=""150px"" height=""100px"" id=""child-2""/>
    <div width=""150px"" height=""100px"" id=""child-3""/>
    <div width=""150px"" height=""100px"" id=""child-4""/>
</div>
";
            factory.LoadFromString(tmpXML);
        }

        public override void Update(float dt)
        {
            factory.CalculateLayout();
        }

        public override void Draw()
        {
            Graphics.Translate(100, 100);
            Graphics.SetColor(Color.White);
            factory.Draw((x, y, w, h, text, attr) =>
            {
                Graphics.Rectangle(DrawMode.Line, x, y, w, h);
                Graphics.Print($"{(attr.TryGetValue("id", out object id) ? id : "")}", x, y);
            });
        }
    }
}
