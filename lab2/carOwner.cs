using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class CarOwner
    {
        public void InspectCar(object sender, ServiceYearEventArgs arg)
        {
            Car car = sender as Car;
            Console.WriteLine($"I need to inspect the car - {car}");
            if ( car.ServiceYear > 10) 
            {
                double possible_appCost = car.appraisedCost / 2;
                Console.WriteLine($"The market value of the car can drop up to{possible_appCost:F2} , I should think about selling this car\n");
            }
        }
    }
}
