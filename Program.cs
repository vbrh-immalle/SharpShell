using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication
{
    public class MakeDir
    {
        private enum Result
        {
            NoNameGiven,
            DirMade,
            DirExists
        }

        private static Dictionary<Result, string> melding = new Dictionary<Result, string>() {
                { Result.NoNameGiven, "Kan directory niet maken: naam niet opgegeven." },
                { Result.DirMade, "Directory [{0}] gemaakt." },
                { Result.DirExists, "Directory [{0}] bestaat al." }
            };

        private static Dictionary<Result, ConsoleColor> kleur = new Dictionary<Result, ConsoleColor>() {
                { Result.NoNameGiven, ConsoleColor.Red },
                { Result.DirMade, ConsoleColor.Green },
                { Result.DirExists, ConsoleColor.Yellow }
            };

        private static void DisplayState(Result status, string dirnaam = "")
        {
            Console.ForegroundColor = kleur[status];
            Console.WriteLine(melding[status], dirnaam);
            Console.ResetColor();
        }

        public static void Run(string[] args)
        {
            if (args.Length <= 0)
            {
                DisplayState(Result.NoNameGiven);
            }
            else
            {
                string naam = args[0];
                if (Directory.Exists(naam))
                {
                    DisplayState(Result.DirExists, naam);
                }
                else
                {
                    Directory.CreateDirectory(naam);
                    DisplayState(Result.DirMade, naam);
                }
            }
        }

    }


    public class RemoveDir
    {
        public enum Result
        {
            NoNameGiven,
            DirRemoved,
            DirDoesntExist,
            DirNotEmpty
        }

        static Dictionary<Result, string> melding = new Dictionary<Result, string>() {
                { Result.NoNameGiven, "Kan directory niet verwijderen: naam niet opgegeven." },
                { Result.DirRemoved, "Directory [{0}] verwijderd." },
                { Result.DirDoesntExist, "Kan directory niet verwijderen: directory [{0}] bestaat niet." },
                { Result.DirNotEmpty, "Directory [{0}] niet leeg, bevat [{1}] items." }
            };

        static Dictionary<Result, ConsoleColor> kleur = new Dictionary<Result, ConsoleColor>() {
                { Result.NoNameGiven, ConsoleColor.Red },
                { Result.DirRemoved, ConsoleColor.Green },
                { Result.DirDoesntExist, ConsoleColor.Yellow },
                { Result.DirNotEmpty, ConsoleColor.Yellow }
            };

        private static void DisplayState(Result status, string dirnaam = "", int aantalDirItems = 0)
        {
            Console.ForegroundColor = kleur[status];
            Console.WriteLine(melding[status], dirnaam, aantalDirItems);
            Console.ResetColor();
        }

        public static void Run(string[] args)
        {
            if (args.Length <= 0)
            {
                DisplayState(Result.NoNameGiven);
            }
            else
            {
                string naam = args[0];
                if (!Directory.Exists(naam))
                {
                    DisplayState(Result.DirNotEmpty, naam);
                }
                else {
                    int aantalDirItems = Directory.GetFileSystemEntries(naam).Length;
                    if( aantalDirItems > 0) {
                        DisplayState(Result.DirNotEmpty, naam, aantalDirItems);
                    } else {
                        Directory.CreateDirectory(naam);
                        Directory.Delete(naam);
                        DisplayState(Result.DirRemoved, naam);
                    }
                }
            }
        }
    }

    public class Program
    {
        enum Status
        {
            InvalidCommand,
            MakeDirCommand,
            RemoveDirCommand
        }

        public static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Hello World!");

            if(args.Length <= 0) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not a command!");
                System.Environment.Exit(1);
            }
        
            switch (args[0])
            {
                case "makedir":
                    MakeDir.Run(args.Skip(1).ToArray());
                    break;
                case "removedir":
                    RemoveDir.Run(args.Skip(1).ToArray());
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unknown command [{0}].", args[0]);
                    break;
            }

        }
    }
}
