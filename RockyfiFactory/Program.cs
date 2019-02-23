using System;
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
            factory = new Rockyfi.Factory();
            factory.LoadFromString(tmpXML2);
        }

        public override void Update(float dt)
        {
            factory.CalculateLayout(Rockyfi.Direction.LTR);
            Love.Misc.FPSGraph.FPSGraph.Update(dt);
        }

        public override void Draw()
        {

            Love.Misc.FPSGraph.FPSGraph.Draw();
            Graphics.SetColor(Color.White);
            Graphics.Translate(100, 100);
            //Console.WriteLine("----------------------");
            factory.Draw((x, y, w, h, node) =>
            {
                Graphics.Rectangle(DrawMode.Line, x, y, w, h);
                Graphics.Print($"{(node.Atrribute.TryGetValue("id", out object id) ? id : "")}", x, y);
                //Console.WriteLine(string.Format("{0} {1} {2} {3}", node.LayoutGetLeft(), node.LayoutGetRight(),
                //    node.LayoutGetWidth(), node.LayoutGetHeight()));
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
