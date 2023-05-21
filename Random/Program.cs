using System;
using System.IO;

namespace GenerateRandomIntegers
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            string path = @"C:\Temporal\numeros_random.txt";
            using (var writer = File.CreateText(path))
            {
                for (int i = 0; i <= 10000000; i++)
                {
                    writer.WriteLine(random.Next(Int32.MinValue, Int32.MaxValue));
                }
            }

            Console.WriteLine("Datos generados correctamente.");

        }
    }
}
