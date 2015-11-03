using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using MenuCounter.Data_Contracts;

namespace MenuCounter
{
    public static class Program
    {
        /// <summary>
        /// Problem
        /// Given a JSON string which describes a menu, calculate the SUM of the IDs of all "items", as long as a "label" exists for that item.
        /// Your program should somehow take a file (or path to a file) as input.
        /// All IDs are integers between 0 and 100. The menu can be of any length.
        /// </summary>
        /// <param name="args">The path to a file containing a JSON string.</param>
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("A file path must be provided as the sole command line argument when running this program. Please see README.txt for details.");

                return;
            }

            IEnumerable<MenuRoot> fileContents;
            IEnumerable<int> idSums;

            try
            {
                fileContents = LoadFileFromPath(args[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while trying to deserialize the file. Exception details are below.");
                Console.WriteLine(ex);

                return;
            }

            try
            {
                idSums = CalculateSums(fileContents);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while trying to calculate sums. Exception details are below.");
                Console.WriteLine(ex);

                return;
            }

            PrintResults(idSums);

            Console.ReadLine();
        }

        /// <summary>
        /// Deserializes and returns a collection of menus from a JSON text file at the given file path.
        /// </summary>
        /// <param name="filePath">The path to a JSON text file. Can be relative or absolute.</param>
        /// <returns>A deserialized JSON object consisting of a collection of menus.</returns>
        public static IEnumerable<MenuRoot> LoadFileFromPath(string filePath)
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                var jsonSerializer = new DataContractJsonSerializer(typeof(IEnumerable<MenuRoot>));
                var jsonObject = jsonSerializer.ReadObject(fileStream);

                return jsonObject as IEnumerable<MenuRoot>;
            }
        }

        /// <summary>
        /// Given a deserialized JSON object consisting of one or more menus, this function calculates
        /// a sum for each menu. A menu's sum is defined as the sum of all ID values in children items
        /// that have a label.
        /// </summary>
        /// <param name="fileContents">The deserialized collection of menus to be summed.</param>
        /// <returns>A collection of sums - one per menu - of the IDs of labeled items.</returns>
        public static IEnumerable<int> CalculateSums(IEnumerable<MenuRoot> fileContents)
        {
            return from menuRoot in fileContents
                where menuRoot.Menu?.Items != null
                select menuRoot.Menu.Items.Where(item => item?.Label != null).Sum(item => item.ID);
        }

        /// <summary>
        /// Prints a collection of sums to the Console, one per line.
        /// Prints error messages if the collection is null or empty.
        /// </summary>
        /// <param name="idSums">Collection of sums to be printed.</param>
        public static void PrintResults(IEnumerable<int> idSums)
        {
            if (idSums == null)
            {
                Console.WriteLine("An unforeseen error occurred. Your results could not be calculated.");

                return;
            }

            if (!idSums.Any())
            {
                Console.WriteLine("No countable items were found. The sum of nothing is 0.");

                return;
            }

            foreach (var idSum in idSums)
            {
                Console.WriteLine(idSum);
            }
        }
    }
}
