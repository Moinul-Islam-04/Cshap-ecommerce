using System;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Components.Forms;


namespace Amazon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine("C. Create new inventory item");
            Console.WriteLine("R. Read inventory item");
            Console.WriteLine("U. Update inventory item");
            Console.WriteLine("D. Delete inventory item");
            Console.WriteLine("Q. Quit");
            char choice;
            List<string?> list = new List<string?>();
            do
            {
                string? input = Console.ReadLine().ToUpper();
                choice = input[0];
                switch (choice)
                {
                case 'C':
                    var newproduct = Console.ReadLine();
                    break;
                case 'R':
                    Console.WriteLine("Read inventory item");
                    break;
                case 'U':
                    Console.WriteLine("Update inventory item");
                    break;
                case 'D':
                    Console.WriteLine("Delete inventory item");
                    break;
                case 'Q':
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
                }
            } while (choice != 'Q' && choice != 'q');
            Console.ReadLine();

        }

    static void Createproduct(List<string?> list)
    {
        
    }



    }



}



