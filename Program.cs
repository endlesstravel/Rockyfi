using System;

namespace FlexCs
{
    enum Color
    {
        A,
        B,
        C,
    }

    class Program
    {
        static void Main(string[] args)
        {
            int a = 111;
            change(ref a);
            Console.WriteLine($"{Color.A > Color.B}");
            Console.WriteLine($"{Color.A < Color.B}");
        }

        static void change(ref int v)
        {
            v = 12;
        }
    }
}
