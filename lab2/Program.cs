using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Car c = new Car("bmw",2000,5);
            Car w = new Car("mercedes", 2000, 4);
            Console.WriteLine(c);
            Console.WriteLine(w);
            bool triger = c==w;
            Console.WriteLine(triger);
        }
    }
}
