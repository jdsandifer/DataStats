﻿/* Copyright 2016 J.D. Sandifer - MIT License
 * 
 * Solution to the interview code problem
 * 
 * Because of the relatively small size of the task and its disconnected nature,
 * I chose not to use a bunch of objects, structs, helper classes, etc. so I
 * could reduce overhead as I would when on the job. Had this been a
 * component in a larger code base that might have benefited from more 
 * code reuse or separation of concerns, then I would have chosen a more
 * segmented and object oriented approach to help with that.
 * 
 * However, the stats calculations were getting nested pretty deeply so
 * I separated them into a different function. To me, fewer indents
 * means more readable code so I avoided several indents on some of the more
 * "meaty" code that way. */

using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace JDSandifer.DataStats
{
    class CalculateStats
    {
        static int Main(string[] args)
        {
            // Define return values
            const int Success = 0;
            const int ErrorNoDataFiles = 1;
            

            /* Create an array of all .data files in the this folder */
            string[] fileNames = null;

            try
            {
                fileNames = Directory.GetFiles(".", "*.data");
            }
            catch (Exception e)
            {
            }

            if (fileNames == null || fileNames.Length == 0)
            {
                Console.WriteLine("No .data files were found in this folder.");
                // Keep command line window open til user is done
                Console.ReadKey();
                return ErrorNoDataFiles;
            }


            /* Check for illegal characters, bad formatting, and then
             * print the stats if everything's ok */
            foreach (string fileName in fileNames)
            {
                // Print filename we're processing, but trim off the ".\" prefix
                Console.WriteLine(fileName.Substring(2));

                // Read in the file and check for errors
                string[] linesFromFile = File.ReadAllLines(fileName);
                const string illegalCharacters = "[^0-9. ]";
                const string correctNumberList =
                    "^[ ]?([0-9]+([.][0-9]+)?)+([ ][0-9]+([.][0-9]+)?)*[ ]?";

                if (linesFromFile == null 
                    || linesFromFile.Length == 0
                    || linesFromFile[0] == "")
                {
                    Console.WriteLine("   Error reading file: no data");
                }
                else if (linesFromFile.Length != 1
                    || Regex.IsMatch(linesFromFile[0], illegalCharacters))
                {
                    Console.WriteLine("   Error reading file: unexpected character");
                }
                else if (!Regex.IsMatch(linesFromFile[0], correctNumberList))
                {
                    Console.WriteLine("   Error reading file: incorrect format");
                }
                else
                {
                    // Convert line of numbers into an array
                    string firstLine = linesFromFile[0].Trim(' ');
                    string[] numberStrings = firstLine.Split(null as char[]);
                    double[] numbers = new double[numberStrings.Length];

                    for (int i = 0; i < numberStrings.Length; i++)
                    {
                        numbers[i] = double.Parse(numberStrings[i],
                            CultureInfo.InvariantCulture.NumberFormat);
                    }

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
            return Success;
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
