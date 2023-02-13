using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    class Program
    {

       /*1.  Write a program and continuously ask the user to enter a number or "ok" to exit.
        Calculate the sum of all the previously entered numbers and display it on the console.*/
        public static void SumOfPreviouslyEnterNo()
        {
            var total = 0;
            while (true)
            {
                var input = Console.ReadLine();
                //error handeling
                if (String.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Enter valid Input");
                    break;
                }

                if (input == "ok")
                {
                    break;
                }
                else
                {
                    var number = Convert.ToInt32(input);
                    total = total + number;
                }
            }
            Console.WriteLine(total);
        }
        /* 2. Write a program and ask the user to enter a series of numbers separated by comma.
         Find the maximum of the numbers and display it on the console. For example,
        if the user enters “5, 3, 8, 1, 4", the program should display 8. */
        public static void MaximumOfNo()
        {
            Console.WriteLine("enter numbers separated by comma. ex - 5, 3, 8, 1, 4.... ");
            var input = Console.ReadLine().Split(',');
            var maxi = Convert.ToInt32(input[0]);
            foreach(var ele in input)
            {
                var tempNo = Convert.ToInt32(ele);
                if (maxi < tempNo)
                {
                    maxi = tempNo;
                }
            }
            Console.WriteLine(maxi);
        }

        /* 
        3. Write a program and ask the user to supply a list of comma separated numbers (e.g 5, 1, 9, 2, 10).
        If the list is empty or includes less than 5 numbers, display "Invalid List" and ask the user
        to re-try; otherwise, display the 3 smallest numbers in the list. */
        
        public static void ValidInValidList()
        {
            Console.WriteLine("enter numbers separated by comma. ex - 5, 3, 8, 1, 4.... ");
            var input = Console.ReadLine();

            //error handeling
            if (String.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Enter valid Input");
                return;
            }
            
            var numbers = input.Split(',');
            var list = new List<int>();

            //converting each string number into int and storing into list 
            foreach(var ele in numbers)
            {
                list.Add(Convert.ToInt32(ele));
            }

            if (list.Count == 0 || list.Count < 5)
            {
                Console.WriteLine("Invalid List");  
            }

            list.Sort();

            for(var i = 0;i < 3; i++)
            {
                Console.WriteLine("{0} ", list[i]);
            }

        }

        /* 4. Take comma-separated list of numbers as input. Print them in descending order*/
        public static void descendingOrder()
        {
            Console.WriteLine("enter numbers separated by comma. ex - 5, 3, 8, 1, 4.... ");
            var input = Console.ReadLine();

            //error handeling
            if (String.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Enter valid Input");
                return;
            }
            var number = input.Split(',');
            var list = new List<int>();
            foreach(var ele in number)
            {
                list.Add(Convert.ToInt32(ele));
            }
            list.Sort();
            list.Reverse();

        Console.WriteLine(String.Join(", ", list));
        }
        static void Main(string[] args)
        {
            //  SumOfPreviouslyEnterNo();
            //  MaximumOfNo();
            //  ValidInValidList();
               descendingOrder();
        }
    }
}
