using System;
using System.Collections.Generic;
using System.IO;
using Love;

namespace RockyfiFactory
{
    class TestProgram
    {
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


        void Test()
        {
            var lex = new Rockyfi.Lex();

            foreach (string lexStr in new string[] {
                //" a.b.c == 'ba' ",
                //" a.b.c == 22 ",
                //" a.b.c == -22",
                //" a.b.c == -2.2",
                //" !abe == -2.2",
                " !abe == true || false + 1 && 1223.4 && !'3f23f'  == fwe.a232",
                " 2.2",
                " a2.2",
                " .2a2.2",
                " a+b ",
                " a == !!!!!b ",
            })
            {
                Console.WriteLine($"-----------{lexStr}-----------");
                MemoryStream ms = new MemoryStream(System.Text.UTF8Encoding.Default.GetBytes(lexStr));
                lex.reader = new StreamReader(ms);

                while (true)
                {
                    try
                    {
                        lex.NextToken();
                        Console.WriteLine($"({lex.currentType},\t\t{lex.GetValueString()})");
                        if (lex.currentType == Rockyfi.Lex.TokenType.EOF)
                            break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                        break;
                    }
                }
                Console.WriteLine($"-----------------------------------");
            }

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
        }

    }
}
