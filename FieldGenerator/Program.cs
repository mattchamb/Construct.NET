using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FieldGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i <= 1000; i++)
            {
                Console.WriteLine("[PrimitiveElement({0}, typeof(int))]", i);
                Console.WriteLine("public int I{0} {{ get; set; }}", i);
            }
        }
    }
}
