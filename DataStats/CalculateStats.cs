using System;
using System.Globalization;
using System.IO;

namespace JDSandifer.DataStats
{
    class CalculateStats
    {
        static void Main(string[] args)
        {
            /* TODO: Find all .data files in the current directory */


            /* TODO: Check for bad files: 
             *         illegal characters, no data, bad formats */


            /* Read in a file (shortdata1.data) and print out the stats
             * or an error */
            string path = "shortdata1.data";
            string numbersString = "   Error reading file: no data";
            string[] numberStrings = null;
            double[] numbers = new double[0];

            if (File.Exists(path))
            {
                // Read all lines but only take the first one - line 0
                // (.data files should only contain one line) - and
                // split it into an array of floats
                numbersString = File.ReadAllLines(path)[0];
                numberStrings = numbersString.Split(null as char[]);

                numbers = new double[numberStrings.Length];

                for (int i = 0; i < numberStrings.Length; i++)
                {
                    numbers[i] = double.Parse(numberStrings[i],
                        CultureInfo.InvariantCulture.NumberFormat);
                }
            }

            /* Calculate the statistics on the numbers array */
            decimal sum = 0m;
            double min = 0.0;
            double max = 0.0;
            decimal average = 0m;
            decimal standardDeviation = 0m;

            if (numbers.Length > 0)
            {
                sum = (decimal) numbers[0];
                min = numbers[0];
                max = numbers[0];

                for (int i = 1; i < numbers.Length; i++)
                {
                    sum += (decimal) numbers[i];
                    min = Math.Min(min, numbers[i]);
                    max = Math.Max(max, numbers[i]);
                }
            }

            /* Print out the filename followed by the stats or an error */
            Console.WriteLine(path);

            if (numbers.Length > 0)
            {
                Console.WriteLine("   Sum: " + sum.ToString("#0.0###"));
                Console.WriteLine("   Min: " + min.ToString("#0.0###"));
                Console.WriteLine("   Max: " + max.ToString("#0.0###"));
                Console.WriteLine("   Average: " + "not calculated yet");
                Console.WriteLine("   Standard Deviation: " 
                    + "not calculated yet");
            }
            else
            {
                Console.WriteLine(numbersString);
            }
            Console.ReadKey();
        }
    }
}
