using System;
using System.Collections.Generic;
using System.Text;

namespace Rockyfi.Expr
{
    class REPL
    {
        public static void RunConsoleREPL()
        {
            Type type = typeof(string);

            Parser ep = new Parser();
            Evaluator evaluater = new Evaluator();
            ParameterVariableHolder pvh = new ParameterVariableHolder();

            pvh.Parameters["char"] = new Parameter(typeof(char));
            pvh.Parameters["sbyte"] = new Parameter(typeof(sbyte));
            pvh.Parameters["byte"] = new Parameter(typeof(byte));
            pvh.Parameters["short"] = new Parameter(typeof(short));
            pvh.Parameters["ushort"] = new Parameter(typeof(ushort));
            pvh.Parameters["int"] = new Parameter(typeof(int));
            pvh.Parameters["uint"] = new Parameter(typeof(uint));
            pvh.Parameters["long"] = new Parameter(typeof(string));
            pvh.Parameters["ulong"] = new Parameter(typeof(ulong));
            pvh.Parameters["float"] = new Parameter(typeof(float));
            pvh.Parameters["double"] = new Parameter(typeof(double));
            pvh.Parameters["decimal"] = new Parameter(typeof(decimal));
            pvh.Parameters["DateTime"] = new Parameter(typeof(DateTime));
            pvh.Parameters["string"] = new Parameter(typeof(string));

            pvh.Parameters["Guid"] = new Parameter(typeof(Guid));

            pvh.Parameters["Convert"] = new Parameter(typeof(Convert));
            pvh.Parameters["Math"] = new Parameter(typeof(Math));
            pvh.Parameters["Array "] = new Parameter(typeof(Array));
            pvh.Parameters["Random"] = new Parameter(typeof(Random));
            pvh.Parameters["TimeZone"] = new Parameter(typeof(TimeZone));
            pvh.Parameters["AppDomain "] = new Parameter(typeof(AppDomain));

            pvh.Parameters["Console"] = new Parameter(typeof(Console));

            pvh.Parameters["evaluater"] = new Parameter(evaluater);

            evaluater.VariableHolder = pvh;

            while (true)
            {
                System.Console.WriteLine("Input line,press Return to Eval:");
                string line = System.Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(line))
                    break;
                try
                {

                    Tree tree = ep.Parse(line);

                    tree.Print(System.Console.Out);

                    object result = evaluater.Eval(tree);

                    if( result != null )
                        System.Console.WriteLine("Resut:{0}", result);
                    else
                        System.Console.WriteLine("Resut is null");
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Exception:" + e.GetType().Name +"->"+ e.Message);
                }
            }
        }
    }
}
