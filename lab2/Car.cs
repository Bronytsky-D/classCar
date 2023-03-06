using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Car: IComparable
    {
        private string model { get; set; }
        private double initialCost { get; set; }
        private uint serviceYear { get; set; }

        public Car() 
        {
            model = string.Empty; initialCost = 0.0; serviceYear = 0; 
        }
        public Car(string _model, double _initialCost, uint _serviceYear)
        {
            this.model = _model;
            this.initialCost = _initialCost;
            this.serviceYear = _serviceYear;
        }
        public override string ToString()
        {
            return $"Car model {model},cost {appraisedCost}-usd,{serviceYear} year(s)";
        }
        public double appraisedCost 
        {
            get
            {
                double temp = initialCost;
                for (int i = 0; i < serviceYear; i++)
                    temp-=(temp * 0.1);
                return temp;
            }
        }
        public override bool Equals(object obj)
        {
            Car other = obj as Car;
            return this.initialCost == other.initialCost;
        }
        public int CompareTo(object obj) 
        { 
            Car other = obj as Car;
            double triger = this.initialCost - other.initialCost;
            if (triger > 0.0)
                return 1;
            else if (triger < 0.0)
                return -1;
            else 
                return -1;
        }
        public static bool operator>(Car first, Car other)
        {
            return first.CompareTo(other) >0;
        }
        public static bool operator <(Car first, Car other)
        {
            return first.CompareTo(other) < 0;
        }

    }
}
