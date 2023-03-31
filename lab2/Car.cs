using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public enum modelCar
    {
        nonExistent,
        Lada,
        Camri,
        Sedan,
        SUV,
        Hatchback,
        Coupe,
        Convertible
    }
    public enum firmTaxi
    {
        None ,
        Uber,
        Uklon,
        Bolt 
    }

    public class Car : IComparable, IFormattable
    {

        private modelCar model;
        private double initialCost;
        private uint serviceYear;
        public modelCar Model { get { return model; } set { model = value; } }
        public double InitialCost { get { return initialCost; } set { initialCost = value; } }
        public uint ServiceYear
        {
            get { return serviceYear; }
            set
            {
                serviceYear = value;
                checkAgeForInspection();
            }
        }
        public Car()
        {
            model = 0; initialCost = 0.0; serviceYear = 0;
        }
        public Car(modelCar _model, double _initialCost, uint _serviceYear)
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
            return $"Car model: {Model}, price: {appraisedCost:C}, service year: {serviceYear}";
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return ToString();
            }
            switch (format.ToUpper())
            {
                case "C":
                    return $"Car model {model},{serviceYear} year(s),cost {appraisedCost:C}";
                case "N":
                    return $"Car model {model},{serviceYear} year(s),cost {appraisedCost:N2}-usd";
                default:
                    throw new FormatException($"The '{format}' format string is not supported.");
            }
        }
        public double appraisedCost
        {
            get
            {
                double temp = initialCost;
                for (int i = 0; i < serviceYear; i++)
                    temp -= (temp * 0.1);
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
        public static Car operator +(Car first, uint _year)
        {// ???? 
            Car c = new Car(first.model, first.initialCost, first.serviceYear + _year);
            c.checkAgeForInspection();
            return c;
        }
        public static Car operator -(Car first, uint _year)
        {
            return new Car(first.model, first.initialCost, first.serviceYear - _year);
        }
        public static Car operator *(Car first, uint _year)
        {
            return new Car(first.model, first.initialCost, first.serviceYear * _year);
        }
        public static Car operator /(Car first, uint _year)
        {
            return new Car(first.model, first.initialCost, first.serviceYear / _year);
        }
        public static bool operator >(Car first, Car other)
        {
            return first.CompareTo(other) > 0;
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

        public delegate void ServiceYearEventHandler(object sender, ServiceYearEventArgs e);
        public event ServiceYearEventHandler ServiceYearChanged;

        private void checkAgeForInspection()
        {
            if (serviceYear % 3 == 0)
            {
                OnServiceYearChanged(new ServiceYearEventArgs(serviceYear));
            }
        }

        protected virtual void OnServiceYearChanged(ServiceYearEventArgs e)
        {
            ServiceYearChanged?.Invoke(this, e);
        }
    }

    public class Taxi: Car
    {
        public static Dictionary<string, double> coefficients = new Dictionary<string, double>{{ "Low", 1.2 },{ "Midl", 1.5 },{ "High", 1.8 }};
        private firmTaxi firm;
        private double k;
        public Taxi(): base()
        {
            firm= 0; k = 1;
        }
        public Taxi(modelCar _model, double _initialCost, uint _serviceYear, firmTaxi _firm, string _coef):base(_model, _initialCost, _serviceYear)
        {
            firm = _firm; k = coefficients[_coef] ;
        }
        public Taxi(Car c,firmTaxi _firm, string _coef) :base(c)
        {
            firm = _firm;
            k = coefficients[_coef];
        }
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format)) format = "G";
            switch (format.ToUpperInvariant())
            {
                case "G":
                    return $"Car model {Model}, {ServiceYear} year(s), taxi appraised cost - {appraisedCost:F2}, taxi firma - {firm}";
                case "C":
                    return $"Taxi company: {firm}";
                case "N":
                    return $"Taxi appraised cost: {appraisedCost:F2}";
                default:
                    throw new FormatException($"The '{format}' format specifier is not supported.");
            }
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
    public class ServiceYearEventArgs : EventArgs
    {
        public uint ServiceYear { get; set; }

        public ServiceYearEventArgs(uint serviceYear)
        {
            ServiceYear = serviceYear;
        }
    }
}
