using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyNameGenerator
{
    class Markov
    {        
        //Declare variables
        private string gender;
        private int min;
        private int max;
        private int order;
        private int numOfNames;
        private static Dictionary<string, int> zeroOrder;
        private static Dictionary<string, Dictionary<string, int>> mOrder;
        private static Dictionary<string, Dictionary<string, double>> probabilities;
        private static List<string> origNames;

        public Markov(string gender, int min, int max, int order, int numOfNames)
        {
            mOrder = new Dictionary<string, Dictionary<string, int>>();
            probabilities = new Dictionary<string, Dictionary<string, double>>();
            zeroOrder = new Dictionary<string, int>();
            origNames = new List<string>();
            this.gender = gender;
            this.min = min;
            this.max = max;
            this.order = order;
            this.numOfNames = numOfNames;
        }

        //Gets new names until max number of names
        public List<string> run()
        {
            List<string> names = new List<string>();

            //read file and parse names
            readFile();

            while (names.Count < numOfNames)
            {
                string name = "";

                //Check constraints
                while (name.Length < min || name.Length > max || names.Contains(name.ToUpper()) || origNames.Contains(name))
                {
                    name = generateName();
                }

                names.Add(name.ToUpper());
            }

            return names;
        }

        //Reads baby name file
        private void readFile()
        {
            string file = "";

            if (gender.Equals("m"))
                file = "namesBoys.txt";
            else if (gender.Equals("f"))
                file = "namesGirls.txt";

            StreamReader r = new StreamReader(file);

            string name = "";

            while ((name = r.ReadLine()) != null)
            {
                name = name.ToLower();

                //If order is 0, use different method
                if (order == 0)
                    parseName_zeroOrder(name);
                else
                    parseName_mOrder(name);

                origNames.Add(name);
            }
        }

        //Generates a random name using Markov model
        private string generateName()
        {
            string name = "";

            //Add dashes at end of name for m order
            for (int i = 0; i < order; i++)
            {
                name = name + "_";
            }

            if (order == 0)
            {
                //use to find a name between min and max length of name for zero order
                Random randNum = new Random();

                //Gets next random letter
                string nextLetter = pickNextLetter(name);

                while (name.Length < randNum.Next(max))
                {
                    name = name + nextLetter;
                    nextLetter = pickNextLetter(name);
                }
            }
            else
            {
                //use to find name for m order
                string nextLetter = pickNextLetter(name);

                while (!nextLetter.Equals("_"))
                {
                    name = name + nextLetter;
                    nextLetter = pickNextLetter(name);
                }

                name = name.Trim(new char[] { '_' });
            }
            return name;
        }

        //Sets ranges for probabilities, generates a random number between >= than 0 and less than 1. 
        //If rand num is within range of letter, letter is returned.
        private string pickNextLetter(string name)
        {
            Dictionary<string, Range> probRange = new Dictionary<string, Range>();
            string randLetter = "-1";
            double currRange = 0;
            int arraySize = 0;

            //case for m order
            if (order != 0)
            {
                string key = name.Substring(name.Length - order, name.Length - (name.Length - order));

                //sum of all occurances following m order key.
                arraySize = mOrder[key].Sum(x => x.Value);

                List<string> nextLetters = mOrder[key].Keys.ToList();

                //calculate probability, add range beginning from 0
                foreach (string letter in nextLetters)
                {
                    double prob = (double)mOrder[key][letter] / (double)arraySize;
                    probRange.Add(letter, new Range(currRange, currRange + prob));
                    currRange = probRange[letter].getEndRange();
                }
            }
            else
            {
                // case for zero order
                //sum of all occurances of key.
                arraySize = zeroOrder.Sum(x => x.Value);
                List<string> nextLetters = zeroOrder.Keys.ToList();

                //calculate probability, add range beginning from 0
                foreach (string letter in nextLetters)
                {
                    double prob = (double)zeroOrder[letter] / (double)arraySize;
                    probRange.Add(letter, new Range(currRange, currRange + prob));
                    currRange = probRange[letter].getEndRange();
                }
            }

            //generate random number >= 0 and less than 1
            Random rand = new Random();
            double randNum = rand.NextDouble();

            //check if rand number is within range of probability
            foreach (string letter in probRange.Keys)
            {
                if (randNum >= probRange[letter].getBegRange() && randNum <= probRange[letter].getEndRange())
                    randLetter = letter;
            }

            //Return random letter
            return randLetter;
        }

        //Add zero order name to dictionary
        private void parseName_zeroOrder(string name)
        {
            for (int i = 0; i < name.Length; i++)
            {
                string letter = name.Substring(i, 1);
                if (zeroOrder.ContainsKey(letter) == false)
                    zeroOrder.Add(letter, 1);
                else
                    zeroOrder[letter] += 1;

            }
        }

        //Add m order name to dictionary
        private void parseName_mOrder(string name)
        {
            for (int i = 0; i < order; i++)
                name = "_" + name + "_";
                        
            for (int i = 0; i + order < name.Length; i++)
            {
                string key = name.Substring(i, order);
                string next = name.Substring(i + order, 1);

                //populate mOrder dictionary
                if (mOrder.ContainsKey(key) == false)
                    mOrder.Add(key, new Dictionary<string, int>());
                if (mOrder[key].ContainsKey(next) == false)
                    mOrder[key].Add(next, 1);
                else
                    mOrder[key][next]++;

                //populate probability dictionary, used only for querying to get the probability of letter following m order. 
                key = key.Trim(new char[] { '_' }); //get rid of dashes
                next = next.Trim(new char[] { '_' });

                if (next.Equals("")) continue;

                if (probabilities.ContainsKey(key) == false)
                    probabilities.Add(key, new Dictionary<string, double>());
                if (probabilities[key].ContainsKey(next) == false)
                    probabilities[key].Add(next, 1);
                else
                    probabilities[key][next]++;
            }
        }

        //Get probability and return back to user
        public double getProbability(string input)
        {
            double prob = 0;

            // if zero order
            if (order == 0)
            {
                if (zeroOrder.ContainsKey(input))
                    prob = (double)zeroOrder[input] / (double)zeroOrder.Sum(x => x.Value);
            }
            else
            {
                string next = input.Substring(0, 1);

                if (input.Length > 2)
                    input = input.Substring(2);
                else
                    input = "";

                if (probabilities.ContainsKey(input))
                    if (probabilities[input].ContainsKey(next))
                        prob = (double)probabilities[input][next] / (double)probabilities[input].Sum(x => x.Value);
            }
            return prob;
        }
    }
}
