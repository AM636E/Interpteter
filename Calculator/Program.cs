using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Interpreter("a=sin(PI/180 * 30); out a * 2;").Interpter();

            while (true)
            {
                Console.WriteLine(new Interpreter(Console.ReadLine()).Interpter());
            }

            Console.WriteLine("Hello World!");
        }
    }
}
