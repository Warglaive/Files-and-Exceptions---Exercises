using System;
using System.IO;
namespace _01.Filter_Extensions
{
    public class Program
    {
       public static void Main()
        {
            var extension = Console.ReadLine();
            string[] entry = Directory.GetFiles("input");

            foreach (var file in entry)
            {
                if (file.Contains(extension))
                {
                    Console.WriteLine(file);
                }
            }
        }
    }
}