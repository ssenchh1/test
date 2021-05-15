using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FlowersGallery
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Hello in our flowers wiki. Here you can add a new flower or check the existing one");
            Console.WriteLine("You can use one of the following commands:");
            Console.WriteLine("\t* add - to add a new flower");
            Console.WriteLine("\t* show - to show a flower with details");
            Console.WriteLine("\t* list - to list all flowers on our portal");
            Console.WriteLine("\t* exit - to finish session");
            Console.ForegroundColor = ConsoleColor.White;


            List<Flower> flowers = new List<Flower>();
            List<string> rawData = File.ReadLines("flowers.csv").ToList();
            flowers.AddRange(rawData.Select(d => d.Split(","))
                .Select(flowerData =>
                    new Flower
                    {
                        Id = int.Parse(flowerData[0]),
                        Name = flowerData[1],
                        Description = flowerData[2],
                        Type = flowerData[3],
                        ImagePath = flowerData[4]
                    }));

            string command;
            do
            {
                command = Console.ReadLine();
                switch (command)
                {
                    case "add":
                    {
                        Flower newFlower = new Flower {Id = flowers.Count + 1};
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter flower name");
                        Console.ForegroundColor = ConsoleColor.White;
                        newFlower.Name = Console.ReadLine();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter flower description");
                        Console.ForegroundColor = ConsoleColor.White;
                        newFlower.Description = Console.ReadLine();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter flower type");
                        Console.ForegroundColor = ConsoleColor.White;
                        newFlower.Type = Console.ReadLine();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter flower image path");
                        Console.ForegroundColor = ConsoleColor.White;
                        newFlower.ImagePath = Console.ReadLine();

                        flowers.Add(newFlower);

                        string csvFlowerMapping =
                            $"{newFlower.Id},{newFlower.Name},{newFlower.Description},{newFlower.Type},{newFlower.ImagePath}";
                        using StreamWriter writer = File.AppendText("flowers.csv");
                        writer.WriteLine(csvFlowerMapping);
                        break;
                    }
                    case "show":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter flower id");
                        bool isInt = int.TryParse(Console.ReadLine(), out int id);
                        if (isInt)
                        {
                            Flower flower = flowers.Find(f => f.Id == id);
                            if(flower == null)
                            {
                                Console.WriteLine("No such flower found, please add one");
                                break;
                            }
                            Console.WriteLine($"Flower {flower.Name}");
                        }
                        else
                        {
                            Console.WriteLine("You entered incorrect number");
                        }
                        break;
                    case "list":
                        foreach (var flower in flowers)
                        {
                            Console.WriteLine($"Flower {flower.Name}");
                        }
                        break;
                    case "exit":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Goodbye");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You entered unsupported command. Please try again");
                        break;
                }
            } while (command != "exit");
        }
    }
}