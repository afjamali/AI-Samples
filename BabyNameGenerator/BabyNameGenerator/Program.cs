using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyNameGenerator
{
    class Program
    {
        //Main method
        static void Main(string[] args)
        {
            //Ask user input questions
            Console.WriteLine("\"M\" ORDER MARKOV MODEL\n");

            Console.Write("1. Male (m) or Female (f) ");
            string gender = Console.ReadLine();

            if (gender.Substring(0, 1).ToLower().Equals("m"))
                gender = "Male";
            else if (gender.Substring(0, 1).ToLower().Equals("f"))
                gender = "Female";
            else
                Environment.Exit(1);

            Console.Write("2. MINIMUM name length ");
            int minLength = Convert.ToInt32(Console.ReadLine());

            Console.Write("2. MAXIMUM name length ");
            int maxLength = Convert.ToInt32(Console.ReadLine());

            Console.Write("2. Order of the model ");
            int order = Convert.ToInt32(Console.ReadLine());

            Console.Write("2. Number of names to generate ");
            int numOfNames = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            //Generate Markov model
            Markov model = new Markov(gender.Substring(0, 1).ToLower(), minLength, maxLength, order, numOfNames);
            List<string> names = model.run();

            Console.WriteLine("List of " + gender + " names:");

            //Display names
            foreach (string name in names)
                Console.WriteLine(name);

            while (true)
            {
                //Ask user for input for finding probability
                Console.Write("\nTo get the probability of a letter, \ntype the \"next letter\" \nfollowed by \"space\" \nand the \"m order letter/s\" : ");
                string input = Console.ReadLine();

                //Display probability
                Console.WriteLine("\n" + model.getProbability(input));
                Console.WriteLine();
            }
        }
    }
}
