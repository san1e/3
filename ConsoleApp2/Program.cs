using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] files =
               {
            "10.txt", "11.txt", "12.txt", "13.txt", "14.txt", "15.txt",
            "16.txt", "17.txt", "18.txt", "19.txt", "20.txt", "21.txt",
            "22.txt", "23.txt", "24.txt", "25.txt", "26.txt", "27.txt",
            "28.txt", "29.txt"
        };

            int totalProduct = 0;
            int validFileCount = 0;
            string noFile = "no_file.txt";
            string badData = "bad_data.txt";
            string overflow = "overflow.txt";

            using (StreamWriter noFileWriter = new StreamWriter(noFile))
            using (StreamWriter badDataWriter = new StreamWriter(badData))
            using (StreamWriter overflowWriter = new StreamWriter(overflow))
            {
                foreach (string file in files)
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(file))
                        {
                            int product = 1;
                            for (int i = 1; i <= 2; i++)
                            {
                                string line = reader.ReadLine();
                                if (line == null)
                                {
                                    noFileWriter.WriteLine(file);
                                    throw new FileNotFoundException("File not found: " + file);
                                }

                                if (!int.TryParse(line, out int number))
                                {
                                    badDataWriter.WriteLine(file);
                                    throw new FormatException("Bad data in file: " + file);
                                }

                                checked
                                {
                                    product *= number;
                                }
                            }

                            totalProduct += product;
                            validFileCount++;
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        continue;
                    }
                    catch (FormatException)
                    {
                        continue;
                    }
                    catch (OverflowException)
                    {
                        overflowWriter.WriteLine(file);
                    }
                }
            }


            if (validFileCount > 0)
            {
                int average = totalProduct / validFileCount;
                Console.WriteLine("Average: " + average);
            }
            else
            {
                Console.WriteLine("No valid files found.");
            }
        }
    }
    }
}
