using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace JDSandifer.DataStats
{
    class CalculateStats
    {
        static void Main(string[] args)
        {
            /* Find all .data files in the this folder */
            string[] fileNames = Directory.GetFiles(".", "*.data");

            /* TODO: Check for bad files: 
                *         illegal characters, no data, bad formats 
            foreach (string fileName in fileNames)
            {

            } */

            /* Extract all numbers from the file as an array */
            foreach (string fileName in fileNames)
            {

                // Read in the file and get the numbers as an array
                string numbersString = "";
                string[] numberStrings = null;
                double[] numbers = new double[0];

                if (File.Exists(fileName))
                {
                    // Read all lines but only take the first one - line 0
                    // (.data files should only contain one line) - and
                    // split it into an array of doubles
                    numbersString = File.ReadAllLines(fileName)[0].Trim(' ');
                    numberStrings = numbersString.Split(null as char[]);
                    numbers = new double[numberStrings.Length];

                    for (int i = 0; i < numberStrings.Length; i++)
                    {
                        numbers[i] = double.Parse(numberStrings[i],
                            CultureInfo.InvariantCulture.NumberFormat);
                    }

                    // Print filename, but trim off ".\" at start
                    Console.WriteLine(fileName.Substring(2));

                    if (numbers.Length > 0)
                    {
                        CalculateAndPrintFileStats(numbers);
                    }
                    else
                    {
                        Console.WriteLine("   Error reading file: no data");
                    }
                }
            }

            // Keep command line window open til user is done 
            // in case this isn't run from the command line
            Console.ReadKey();
        }

        /* Prints out stats based on a given array of numbers.
         * Expects at least one number in the array. */
        private static void CalculateAndPrintFileStats(double[] numbers)
        {
            // At least one number is expected so initialize sum, min, and max
            decimal sum = (decimal) numbers[0];
            double min = numbers[0];
            double max = numbers[0];

            decimal average = 0.0m;
            decimal standardDeviation = 0.0m;
            int quantityOfNumbers = numbers.Length;

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
                    (double)(accurateNumber - average), 2.0);
            }

            standardDeviation = (decimal) Math.Pow(
                (double) (squaresOfDifferences / (decimal) quantityOfNumbers),
                0.5);
            
            /* Print out the stats */
            const string numberFormat = "0.####";
            
            Console.WriteLine("   Sum: " + sum.ToString(numberFormat));
            Console.WriteLine("   Min: " + min.ToString(numberFormat));
            Console.WriteLine("   Max: " + max.ToString(numberFormat));
            Console.WriteLine("   Average: " + average.ToString(numberFormat));
            Console.WriteLine("   Standard Deviation: "
                + standardDeviation.ToString(numberFormat));
            
        }
    }
}
