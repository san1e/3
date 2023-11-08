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
        static void CreateFileIfNotExists(string fileName)
        {
            if (!File.Exists(fileName))
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                }
            }
        }
        static void Main(string[] args)
        {
            for (int i = 10; i <= 29; i++)
            {
                string fileName = i + ".txt";
                CreateFileIfNotExists(fileName);
            }

            int totalProduct = 0;
            int validFileCount = 0;
            string noFile = "no_file.txt";
            string badData = "bad_data.txt";
            string overflow = "overflow.txt";

            using (StreamWriter noFileWriter = new StreamWriter(noFile))
            using (StreamWriter badDataWriter = new StreamWriter(badData))
            using (StreamWriter overflowWriter = new StreamWriter(overflow))
            {
                for (int i = 10; i <= 29; i++)
                {
                    string file = i + ".txt";
                    try
                    {
                        using (StreamReader reader = new StreamReader(file))
                        {
                            int product = 1;
                            for (int j = 1; j <= 2; j++)
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
            Console.ReadKey();
        }
    }
    
}
