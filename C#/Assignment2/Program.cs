using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var cal = new AdvancedCalculator();
            Console.WriteLine("Sum for two interger: " + cal.Add(1, 2));
            Console.WriteLine("Sum for three interger: " + cal.Add(1, 2, 4));
            Console.WriteLine("Sum for two float: " + cal.Add(1.2, 2.2));
            Console.WriteLine("Latest result: " + cal.GetResult());

            Console.WriteLine("Pow of two No: " + cal.Power(2, 4));
            Console.WriteLine("Latest result: " + cal.GetResult());
        }
    }
}
