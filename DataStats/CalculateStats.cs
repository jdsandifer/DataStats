using System;
using System.IO;

namespace JDSandifer.DataStats
{
    class CalculateStats
    {
        static void Main(string[] args)
        {
            /* Read in shortdata1.data and print out the line
             * or an error */
            string path = "shortdata1.data";
            string numbers = "   Error reading file: no data";

            if (File.Exists(path))
            {
                numbers = File.ReadAllLines(path)[0];
            }

            // Split the line by spaces and assign it to an array


            // Print out the first line
            Console.WriteLine(path);
            Console.WriteLine(numbers);
        }
    }
}
