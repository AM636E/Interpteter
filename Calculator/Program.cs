using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write(">>> ");
                Console.WriteLine(new Interpreter(Console.ReadLine()).Interpter());
            }

            Console.WriteLine("Hello World!");
        }
    }
}
