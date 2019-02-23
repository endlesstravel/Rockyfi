using System;
using Love;

namespace FlexCs
{
    class Program : Scene
    {
        Rockyfi.Factory factory;

        int p
        {
            get
            {
                return 1;
            }
        }

        int p2;
        public int p3
        {
            get
            {
                return 1;
            }
        }
        public int ppp3
        {
            set
            {
                value = 1;
            }
        }

        public int p4;

        public static int p5
        {
            get
            {
                return 1;
            }
        }

        public static int p6;

        public override void Load()
        {

            Type t = this.GetType();
            System.Reflection.MemberInfo[] members = t.GetMembers();
            System.Reflection.PropertyInfo[] properties = t.GetProperties();
            System.Reflection.FieldInfo[] fieldInfos = t.GetFields();

            Console.WriteLine("--------------mem");
            foreach (var m in members)
            {
                Console.WriteLine(m.Name);
            }
            Console.WriteLine("--------------p");
            foreach (var p in properties)
            {
                Console.WriteLine(p.Name + "  " + (p.GetGetMethod().Name));
            }
            Console.WriteLine("--------------f");
            foreach (var f in fieldInfos)
            {
                Console.WriteLine(f.Name);
            }
            Console.WriteLine("--------------");

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
            factory.Draw((x, y, w, h, node) =>
            {
                Graphics.Rectangle(DrawMode.Line, x, y, w, h);
                Graphics.Print($"{(node.Atrribute.TryGetValue("id", out object id) ? id : "")}", x, y);
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
