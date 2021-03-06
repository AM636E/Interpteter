﻿using System;
using Calculator.Core;

namespace Calculator.Ui.Text
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write(">>> ");
                var interpreter = new Interpreter(Console.ReadLine());
                try
                {
                    Console.WriteLine(interpreter.Interpret());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
