﻿using System;
using System.Collections.Generic;
using System.IO;
using Love;

namespace FlexCs
{
    class Program : Scene
    {
        Rockyfi.Factory factory;

        public override void Load()
        {
            string tmpXML1 = @"
<div width=""520px"" height=""300px"" id=""root"">
    <div width=""50px"" height=""30%"" id=""child-1"">
    </div>
    <div width=""20%"" height=""30px"" padding-top=""50px"" id=""child-2"">
    </div>
</div>
";
            string tmpXML2 = @"
<div width=""320px"" height=""300px"" flex-wrap=""wrap"" justify-content=""center"" flex-direction=""row"" >
    <div width=""150px"" height=""100px"" id=""child-0""/>
    <div width=""150px"" height=""100px"" id=""child-1""/>
    <div width=""150px"" height=""100px"" id=""child-2""/>
    <div width=""150px"" height=""100px"" id=""child-3""/>
    <div width=""150px"" height=""100px"" id=""child-4""/>
</div>
";
            string tmpXML3 = @"<root>
<div el-bind:padding-top=""pt"" el-bind:width=""w"" el-bind:height=""styleObj.StyleHeight"" flex-wrap=""wrap"" justify-content=""center"" flex-direction=""row"" >
    <div el-for=""item in list"" el-if=""item != '2' "" el-bind:margin-top=""mt"" el-bind:id=""item"" width=""100px"" height=""100px""/>
</div>
</root>";
            factory = new Rockyfi.Factory();
            factory.LoadFromString(tmpXML3, new Dictionary<string, object>()
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
            Love.Misc.FPSGraph.FPSGraph.Update(dt);
        }

        public override void Draw()
        {
            Love.Misc.FPSGraph.FPSGraph.Draw();
            Graphics.SetColor(Color.White);
            Graphics.Translate(100, 100);
            factory.Draw((x, y, w, h, node) =>
            {
                Graphics.Rectangle(DrawMode.Line, x, y, w, h);
                //Graphics.Print($"{(node.Atrribute.TryGetValue("id", out object id) ? id : "")}", x, y);
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
