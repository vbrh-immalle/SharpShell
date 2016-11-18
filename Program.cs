using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication
{
    public class Program
    {
        private enum Result {
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

        private static void DisplayState(Result status, string dirnaam = "") {          
            Console.ForegroundColor = kleur[status];
            Console.WriteLine(melding[status], dirnaam);
            Console.ResetColor();
        }

        public static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Hello World!");

            if(args.Length <= 1) {
                DisplayState(Result.NoNameGiven);
            } else {
                string naam = args[0];
                if(Directory.Exists(naam)) {
                    DisplayState(Result.DirExists, naam);
                } else {
                    Directory.CreateDirectory(naam);
                    DisplayState(Result.DirMade, naam);
                }
            }
        }
    }
}
