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


            /* Read in a file (shortdata1.data) and get the numbers */
            string path = "shortdata1.data";
            string numbersString = "   Error reading file: no data";
            string[] numberStrings = null;
            double[] numbers = new double[0];

            if (File.Exists(path))
            {
                // Read all lines but only take the first one - line 0
                // (.data files should only contain one line) - and
                // split it into an array of doubles
                numbersString = File.ReadAllLines(path)[0];
                numberStrings = numbersString.Split(null as char[]);

                numbers = new double[numberStrings.Length];

                for (int i = 0; i < numberStrings.Length; i++)
                {
                    numbers[i] = double.Parse(numberStrings[i],
                        CultureInfo.InvariantCulture.NumberFormat);
                }
            }

            /* Calculate the statistics on the numbers */
            decimal sum = 0.0m;
            double min = 0.0;
            double max = 0.0;
            decimal average = 0.0m;
            decimal standardDeviation = 0.0m;
            int quantityOfNumbers = numbers.Length;

            if (quantityOfNumbers > 0)
            {
                // If at least one number, initialize sum, min, and max
                sum = (decimal) numbers[0];
                min = numbers[0];
                max = numbers[0];

                // If more numbers, compute sum, min, and max
                for (int i = 1; i < quantityOfNumbers; i++)
                {
                    sum += (decimal) numbers[i];
                    min = Math.Min(min, numbers[i]);
                    max = Math.Max(max, numbers[i]);
                }

                average = sum / quantityOfNumbers;

                // Compute standard deviation
                decimal squaresOfDifferences = 0.0m;
                decimal accurateNumber = 0.0m;

                foreach (double number in numbers)
                {
                    accurateNumber = (decimal) number;
                    squaresOfDifferences += (decimal) Math.Pow(
                        (double)(accurateNumber - average),
                        2.0);
                }

                standardDeviation = (decimal) Math.Pow(
                    (double) (squaresOfDifferences 
                                / (decimal) quantityOfNumbers),
                    0.5);
            }

            /* Print out the filename followed by the stats or an error */
            const string numberFormat = "0.####";
            Console.WriteLine(path);
            
            if (quantityOfNumbers > 0)
            {
                Console.WriteLine("   Sum: " + sum.ToString(numberFormat));
                Console.WriteLine("   Min: " + min.ToString(numberFormat));
                Console.WriteLine("   Max: " + max.ToString(numberFormat));
                Console.WriteLine("   Average: " 
                    + average.ToString(numberFormat));
                Console.WriteLine("   Standard Deviation: "
                    + standardDeviation.ToString(numberFormat));
            }
            else
            {
                Console.WriteLine(numbersString);
            }

            // Debug Only - Keeps command line window open
            Console.ReadKey();
        }
    }
}
