using System;
using Love;

namespace FlexCs
{
    class Program: Scene
    {
        Rockyfi.Factory factory;
        public override void Load()
        {
            string tmpXML1 = @"
<div width=""100px"" height=""100px"" id=""root"">
    <div width=""50px"" height=""30%"" id=""child-1"">
    </div>
</div>
";
            factory = new Rockyfi.Factory();
            factory.LoadFromString(tmpXML1);
            factory.Update(Rockyfi.Direction.LTR);
        }

        public override void Draw()
        {
            Graphics.SetColor(Color.White);

            Graphics.Translate(100, 100);

            factory.Draw((x, y, w, h, node) =>
            {
                //Console.WriteLine($"{node.Atrribute["id"]}    {x}  {y}  {w}  {h}");
                Graphics.Rectangle(DrawMode.Line, x, y, w, h);
                Graphics.Print($"{node.Atrribute["id"]}", x, y - 10);
            });
        }

        static void Main(string[] args)
        {
            var t = new Rockyfi.RociyfiTest();
            t.XTestAll();
            Boot.Init(new BootConfig
            {

            });
            Boot.Run(new Program());
        }
    }
}
