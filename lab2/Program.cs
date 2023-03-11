using System;
using System.CodeDom.Compiler;
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
            Car c = new Car("bmw",200000,5);
            Car w = new Car("mercedes", 2000, 4);
            Taxi t = new Taxi("bmw", 2000, 5, "dou" , 2);
            Taxi d = new Taxi("bmw", 150, 5, "dou", 2);
            Taxi x = new Taxi(w, "dou", 3);
            Car[] arrCar = new Car[5] { c, w, t, d, x };
            Console.WriteLine(c);
            Console.WriteLine(w);
            bool triger = c==w;
            Console.WriteLine(triger);
            Console.WriteLine(d);
            Console.WriteLine(t);
            bool trig = t > d;
            Console.WriteLine(trig);
            Console.WriteLine(cheapestCar(arrCar));
        }
        public static Car cheapestCar(Car[] arrCar) 
        {
            Car min = arrCar[0];
            for(int i =1 ; i < arrCar.Length; i++)
            {
                if (min > arrCar[i])
                {
                    min = arrCar[i];
                }
            }
            return min;
        }
    }
}
