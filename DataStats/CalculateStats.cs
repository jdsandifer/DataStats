// Copyright 2016 J.D. Sandifer - MIT License
 
// Solution to the code problem

// Because of the relatively small size of the task and its disconnected nature, I chose not 
// to segment the code into objects, structs, single-purpose functions, etc. to keep the code 
// easy to read without jumping around. Had this been a component in a larger code base that 
// might have benefited from more code reuse or been difficult to test without more segmentation,
// I would have chosen to do more to separate concerns and use OOP.

// I tested this project using .data files with descriptive names that held edge cases, error values,
// bad data, and large data sets to verify the file reading portion. I also used standard VS unit 
// tests to to verify the functionality of the calculation function. 

using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace JDSandifer.Interview
{
    public class DataStats
    {
        static int Main(string[] args)
        {
            // Define return values
            const int Success = 0;
            const int ErrorNoDataFiles = 1;
            
            // Create an array of all .data files in the this folder
            string[] fileNames = null;

            try
            {
                fileNames = Directory.GetFiles(".", "*.data");
            }
            catch
            {
                // We could potentially log specific exceptions here.
            }

            if (fileNames == null || fileNames.Length == 0)
            {
                Console.WriteLine("No .data files were found in this folder.");
                // Keep command line window open til user is done
                Console.ReadKey();
                return ErrorNoDataFiles;
            }
            
            // Check for illegal characters, bad formatting, and then
            // print the stats if everything's ok
            foreach (string fileName in fileNames)
            {
                // Print filename we're processing, but trim off the ".\" prefix
                Console.WriteLine(fileName.Substring(2));

                // Read in the file and check for errors
                string[] linesFromFile = null;

                try
                {
                    linesFromFile = File.ReadAllLines(fileName);
                }
                catch
                {
                    // Another spot to potentially log specific errors.
                }

                const string IllegalCharacters = "[^0-9. -]";
                const string CorrectNumberList = "^[ ]?(-?[0-9]*[.]?[0-9]+)([ ]-?[0-9]*[.]?[0-9]+)*[ ]?";

                if (linesFromFile == null || linesFromFile.Length == 0 || linesFromFile[0] == "")
                {
                    Console.WriteLine("   Error reading file: no data");
                }
                else if (linesFromFile.Length != 1 || Regex.IsMatch(linesFromFile[0], IllegalCharacters))
                {
                    Console.WriteLine("   Error reading file: unexpected character");
                }
                else if (!Regex.IsMatch(linesFromFile[0], CorrectNumberList))
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

                    Console.WriteLine(CalculateAndFormatFileStats(numbers));
                }
            }
            
            // Keep command line window open til user is done 
            // in case this isn't run from the command line
            Console.ReadKey();
            return Success;
        }
        
        // Prints out stats based on a given array of numbers.
        // Expects at least one number in the array.
        public static string CalculateAndFormatFileStats(double[] numbers)
        {
            // Ensure we have at least one number
            if (numbers.Length == 0)
            {
                Console.WriteLine();
                return "   Error reading file: no data";
            }

            // At least one number is expected so initialize sum, min, and max with it
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
                squaresOfDifferences += (decimal) Math.Pow((double)(accurateNumber - average), 2.0);
            }

            standardDeviation = 
                (decimal) Math.Pow((double) (squaresOfDifferences / (decimal) quantityOfNumbers), 0.5);
            
            // Print out the stats
            const string NumberFormat = "0.##";
            return  "   Sum: " + sum.ToString(NumberFormat)
                + "\n   Min: " + min.ToString(NumberFormat)
                + "\n   Max: " + max.ToString(NumberFormat)
                + "\n   Average: " + average.ToString(NumberFormat)
                + "\n   Standard Deviation: " + standardDeviation.ToString(NumberFormat);
        }
    }
}
