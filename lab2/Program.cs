using System;
using System.CodeDom.Compiler;
using System.Collections;
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
            Car w = new Car(modelCar.SUV,20001,2);
            Taxi t = new Taxi(modelCar.Sedan, 2000, 5, firmTaxi.Uber,"Midl");
            Taxi d = new Taxi(modelCar.Camri, 150, 5,firmTaxi.Bolt,"Low");
            Taxi x = new Taxi(w,firmTaxi.Uklon,"Low");
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
            Console.WriteLine($"найдешевше авто --{a}");
            WriteArrCar(arrCar);
            Car a1 = ReadCar();
            Console.WriteLine(a1);
            if(a1 is Taxi)
                Console.WriteLine("yes");
            else
                Console.WriteLine("no");
            increaseServiceYearInARR(arrCar, 3);
            WriteArrCar(arrCar);
            Console.WriteLine("\n");
            //вивід нової колекйії 
            Dictionary<modelCar,Car> myDictionary = arrayWithoutRepetitions(arrCar);
            foreach (KeyValuePair<modelCar, Car> kvp in myDictionary)
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
        }
        //  Знаходження найдешевшого авто
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
        // Вивід всіх елементів масиву 
        public static void WriteArrCar(Car[] car)
        {
            for (int i = 0; i < car.Length; i++)
                Console.WriteLine(car[i]);
            
        }
        // Ввід даних про новий автомобіль в режимі діалоку    
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
                string input_firm = Console.ReadLine();
                firmTaxi _firm;
                if (Enum.TryParse(input_firm, out _firm))
                    Console.WriteLine("You chose " + _firm);
                else
                    Console.WriteLine("Invalid input");
                Console.Write("Enter a taxi coef(Low,Midl or High): ");
                string input_coef = Console.ReadLine();

                if (Taxi.coefficients.ContainsKey(input_coef))
                    Console.WriteLine("You chose " + input_coef);
                else
                    input_coef = "Midl";
                Car resultTaxi = new Taxi(_model, _initialCost, _serviceYear,_firm, input_coef);
                return resultTaxi;
            }
            Car resultCar = new Car( _model, _initialCost, _serviceYear);
            return resultCar;
        }
        // збільшення поля serviceYear для кожного елементу arrCar
        public static void increaseServiceYearInARR(Car[] arrCar,uint _year)
        {
            for(int i =0;i<arrCar.Length;i++)
                arrCar[i] += _year;
        }
        // створення нової колекції, що містить по одному автомобілю кожної марки з попередньої колекції
        public static Dictionary<modelCar, Car> arrayWithoutRepetitions(Car[] arrCar)
        {
            Dictionary<modelCar,Car>  result = new Dictionary<modelCar, Car>();
            for (int i =0; i < arrCar.Length; i++)
            {
                if (!result.ContainsKey(arrCar[i].Model))
                {
                    result[arrCar[i].Model] = arrCar[i];
                }
            }
            return result;
        }
    }
}
