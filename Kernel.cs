using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Sys = Cosmos.System;

namespace BoufOS
{
    public class Kernel : Sys.Kernel
    {
        [ManifestResourceStream(ResourceName = "BoufOS.data.info")]
        static byte[] file;

        List<string> commands = new List<string>
            {"shutdown","reboot","info","fileman","clear","time","ram","help","add","sub","mult","div","mod","print-file","root","exit","check-root"};

        bool root = false;
        string password = "boufOS";

        void Execute(string command)
        {
            string[] words = command.ToLower().Split(' ');

            switch (words[0])
            {
                case "shutdown":
                    Cosmos.System.Power.Shutdown();
                    break;
                case "reboot":
                    Cosmos.System.Power.Reboot();
                    break;
                case "info":
                    Console.WriteLine("BoufOS 0.2");
                    break;
                case "fileman":
                    if (root)
                    {
                        Console.WriteLine("Fileman not working because cosmos VFS is dumb");
                    }
                    else
                    {
                        Console.WriteLine("Access denied");
                    }
                    break;
                case "clear":
                    Console.Clear();
                    break;
                case "time":
                    Console.WriteLine(Cosmos.HAL.RTC.DayOfTheMonth + "/" + Cosmos.HAL.RTC.Month + "/" + Cosmos.HAL.RTC.Year);
                    Console.WriteLine(Cosmos.HAL.RTC.Hour + ":" + Cosmos.HAL.RTC.Minute + ":" + Cosmos.HAL.RTC.Second);
                    break;
                case "ram":
                    Console.WriteLine(Cosmos.Core.CPU.GetAmountOfRAM() + "Mb");
                    break;
                case "help":
                    Console.WriteLine("Commands:");
                    foreach(string cmd in commands)
                    {
                        Console.WriteLine("  " + cmd);
                    }
                    Console.WriteLine("");
                    break;
                case "print-file":
                    string fileContent = System.Text.Encoding.UTF8.GetString(file); //convert the byte array to string (assuming text data)
                    Console.WriteLine(fileContent); //write it out
                    break;
                case "root":
                    Console.Write("Enter root password: ");
                    if(Console.ReadLine() == password)
                    {
                        root = true;
                    }
                    else
                    {
                        Console.WriteLine("\nWrong password");
                    }
                    break;
                case "exit":
                    root = false;
                    break;
                case "check-root":
                    Console.Write("Is root: ");
                    Console.WriteLine(root);
                    break;
                case "add":
                    try
                    {
                        int a;
                        int b;
                        if(Int32.TryParse(words[1], out a) && Int32.TryParse(words[2], out b))
                        {
                            Console.WriteLine(a + b);
                        }
                        else
                        {
                            Console.WriteLine("Can add only numbers!");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Correct syntax: add <num1> <num2>");
                    }
                    break;
                case "sub":
                    try
                    {
                        int a;
                        int b;
                        if (Int32.TryParse(words[1], out a) && Int32.TryParse(words[2], out b))
                        {
                            Console.WriteLine(a - b);
                        }
                        else
                        {
                            Console.WriteLine("Can subtract only numbers!");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Correct syntax: sub <num1> <num2>");
                    }
                    break;
                case "mult":
                    try
                    {
                        int a;
                        int b;
                        if (Int32.TryParse(words[1], out a) && Int32.TryParse(words[2], out b))
                        {
                            Console.WriteLine(a * b);
                        }
                        else
                        {
                            Console.WriteLine("Can multiply only numbers!");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Correct syntax: mult <num1> <num2>");
                    }
                    break;
                case "div":
                    try
                    {
                        int a;
                        int b;
                        if (Int32.TryParse(words[1], out a) && Int32.TryParse(words[2], out b))
                        {
                            Console.WriteLine(a / b);
                        }
                        else
                        {
                            Console.WriteLine("Can divide only numbers!");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Correct syntax: div <num1> <num2>");
                    }
                    break;
                case "mod":
                    try
                    {
                        int a;
                        int b;
                        if (Int32.TryParse(words[1], out a) && Int32.TryParse(words[2], out b))
                        {
                            Console.WriteLine(a % b);
                        }
                        else
                        {
                            Console.WriteLine("Can mod only numbers!");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Correct syntax: mod <num1> <num2>");
                    }
                    break;

                default:
                    Console.WriteLine("Command not found");
                    break;
            }
        }

        protected override void BeforeRun()
        {
            Console.WriteLine("Loading BoufOS...");
            for (int i = 0; i < 50; i++)
            {
                Console.Write(".");
            }
            Console.WriteLine("\nBoufOS started successfully!");
        }

        protected override void Run()
        {
            Console.Write(">>");
            try
            {
                Execute(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Something went wrong while executing the command");
            }
            Console.WriteLine();
        }
    }
}
