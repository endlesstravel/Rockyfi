using System;

namespace FlexCs
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 111;
            change(ref a);
            Console.WriteLine("Hello World!" + a);
        }

        static void change(ref int v)
        {
            v = 12;
        }
    }
}
