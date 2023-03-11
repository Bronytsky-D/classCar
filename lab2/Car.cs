using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Car: IComparable
    {
        private string model;
        private double initialCost;
        private uint serviceYear; 
        public string Model { get { return model; } set { model = value; } }
        public double InitialCost { get { return initialCost; } set { initialCost = value; } }
        public uint ServiceYear { get { return serviceYear; } set { serviceYear = value; } }
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
        public Car(Car c)
        {
            model = c.model;
            initialCost = c.initialCost;
            serviceYear = c.serviceYear;
        }
        public override string ToString()
        {
            return $"Car model {model},{serviceYear} year(s),cost {appraisedCost}-usd";
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
            double temp = this.initialCost - other.initialCost;
            if (temp > 0.0)
                return 1;
            else if (temp < 0.0)
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
        public static bool operator ==(Car first, Car other)
        {
            return first.Equals(other); 
        }

        public static bool operator !=(Car first, Car other)
        {
            return !(first == other); 
        }
    }
    public class Taxi: Car
    {
        private string firm;
        private uint k;
        public Taxi(): base()
        {
            firm= string.Empty; k = 1;
        }
        public Taxi(string _model, double _initialCost, uint _serviceYear, string _firm,uint _k):base(_model, _initialCost, _serviceYear)
        {
            firm = _firm; k = _k;
        }
        public Taxi(Car c,string _firm,uint _k):base(c)
        {
            firm = _firm;
            k = _k;
        }  
        public override string ToString()
        {
            return base.ToString() + $" ,taxi appraised cost-{appraisedCost},taxi firma - {firm}";
        }
        public new double appraisedCost
        {
            get
            {
                double temp = base.InitialCost;
                for (int i = 0; i < base.ServiceYear; i++)
                    temp -= (temp * k * 0.1);
                return temp;
            }
        }

    }
}
