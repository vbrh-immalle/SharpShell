using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication
{
    public class Program
    {
        enum Status {
            NoNameGiven,
            DirMade,
            DirExists
        }

        static Dictionary<Status, string> melding = new Dictionary<Status, string>() {
                { Status.NoNameGiven, "Kan directory niet maken: naam niet opgegeven." },
                { Status.DirMade, "Directory [{0}] gemaakt." },
                { Status.DirExists, "Directory [{0}] bestaat al." }
            };

        static Dictionary<Status, ConsoleColor> kleur = new Dictionary<Status, ConsoleColor>() {
                { Status.NoNameGiven, ConsoleColor.Red },
                { Status.DirMade, ConsoleColor.Green },
                { Status.DirExists, ConsoleColor.Yellow }
            };

        private static void HandleState(Status status, string dirnaam = "") {          
            Console.ForegroundColor = kleur[status];
            Console.WriteLine(melding[status], dirnaam);
            Console.ResetColor();
        }

        public static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Hello World!");

            if(args.Length <= 1) {
                HandleState(Status.NoNameGiven);
            } else {
                string naam = args[0];
                if(Directory.Exists(naam)) {
                    HandleState(Status.DirExists, naam);
                } else {
                    Directory.CreateDirectory(naam);
                    HandleState(Status.DirMade, naam);
                }
            }
        }
    }
}
