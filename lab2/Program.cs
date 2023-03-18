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
            Car c = new Car(modelCar.Camri,200000,5);
            Car w = new Car();
            Taxi t = new Taxi(modelCar.SUV, 2000, 5, firmTaxi.Uber);
            Taxi d = new Taxi(modelCar.Sedan, 150, 5,firmTaxi.Bolt);
            Taxi x = new Taxi(w,firmTaxi.Uklon);
            Car[] arrCar = new Car[5] { c, w, t, d, x };
            Console.WriteLine(c);
            Console.WriteLine(w);
            bool triger = c==w;
            Console.WriteLine(triger);
            Console.WriteLine(d);
            Console.WriteLine(t);
            bool trig = t > d;
            Console.WriteLine(trig);
            Car a = cheapestCar(arrCar);
            WriteArrCar(arrCar);
            Car a1 = ReadCar();
            Console.WriteLine(a1);
            if(a1 is Taxi)
                Console.WriteLine("yes");
            else
                Console.WriteLine("no");
            increaseServiceYearInARR(arrCar, 3);
            WriteArrCar(arrCar);
        }
        public static Car cheapestCar(Car[] arrCar)
        {
            Car min = arrCar[0];
            for (int i = 1; i < arrCar.Length; i++)
            {
                if (min > arrCar[i])
                    min = arrCar[i];
            }
            return min;
        }
        public static void WriteArrCar(Car[] car)
        {
            for (int i = 0; i < car.Length; i++)
                Console.WriteLine(car[i]);
            
        }
        public static Car ReadCar()
        {
            Console.WriteLine($"you want a taxi?(Enter 'Taxi'): ");
            string inputChoice = Console.ReadLine();
            Console.Write("Enter a model(1-Lada,2-Camri,3-Sedan,4-SUV,5-Hatchback,6-Coupe,7-Convertible): ");
            string inputModel = Console.ReadLine();
            modelCar _model = 0;
            if (int.TryParse(inputModel, out int numericValue))
            {
                _model = (modelCar)Enum.ToObject(typeof(modelCar), numericValue);
                Console.WriteLine("You chose " + _model);
            }
            else
                Console.WriteLine("Invalid input");
            Console.Write("Write initial cost: ");
            double _initialCost = double.Parse(Console.ReadLine());
            Console.Write("Write service year: ");
            uint _serviceYear = uint.Parse(Console.ReadLine());
            if (inputChoice == "Taxi") 
            {
                Console.Write("Enter a taxi firm(Uber,Uklon or Bolt): ");
                string input = Console.ReadLine();
                firmTaxi _firm;
                if (Enum.TryParse(input, out _firm))
                    Console.WriteLine("You chose " + _firm);
                else
                    Console.WriteLine("Invalid input");
                Car resultTaxi = new Taxi(_model, _initialCost, _serviceYear,_firm);
                return resultTaxi;
            }
            Car resultCar = new Car( _model, _initialCost, _serviceYear);
            return resultCar;
        }
        public static void increaseServiceYearInARR(Car[] arrCar,uint _year)
        {
            for(int i =0;i<arrCar.Length;i++)
                arrCar[i] += _year;
        }

    }
}
