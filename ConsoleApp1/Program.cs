using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            // Отримуємо список файлів у поточній папці.
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(currentDirectory);

            // Регулярний вираз для визначення графічних файлів.
            Regex regexExtForImage = new Regex(@"\.(bmp|gif|tiff?|jpe?g|png)$", RegexOptions.IgnoreCase);

            foreach (string fileName in files)
            {
                // Перевіряємо, чи відповідає файл регулярному виразу графічних файлів.
                if (regexExtForImage.IsMatch(Path.GetExtension(fileName)))
                {
                    try
                    {
                        // Завантажуємо зображення.
                        using (Bitmap original = new Bitmap(fileName))
                        {
                            // Виконуємо дзеркальне відбивання.
                            original.RotateFlip(RotateFlipType.RotateNoneFlipY);

                            // Генеруємо нове ім'я файлу.
                            string newFileName = Path.GetFileNameWithoutExtension(fileName) + "-mirrored.gif";

                            // Зберігаємо зображення у форматі GIF.
                            original.Save(newFileName, ImageFormat.Gif);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Помилка при обробці файлу {fileName}: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Файл {fileName} не містить картинки.");
                }
            }

            Console.WriteLine("Завершено.");
        }
    }
}
