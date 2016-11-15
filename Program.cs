using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication
{
    public class MakeDir {
        public enum Status {
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

        public static void HandleState(Status status, string dirnaam = "") {          
            Console.ForegroundColor = kleur[status];
            Console.WriteLine(melding[status], dirnaam);
            Console.ResetColor();
        }
    }

    public class RemoveDir {
        public enum Status {
                    NoNameGiven,
                    DirRemoved,
                    DirDoesntExist
                }

        static Dictionary<Status, string> melding = new Dictionary<Status, string>() {
                { Status.NoNameGiven, "Kan directory niet verwijderen: naam niet opgegeven." },
                { Status.DirRemoved, "Directory [{0}] verwijderd." },
                { Status.DirDoesntExist, "Kan directory niet verwijderen: directory [{0}] bestaat niet." }
            };

        static Dictionary<Status, ConsoleColor> kleur = new Dictionary<Status, ConsoleColor>() {
                { Status.NoNameGiven, ConsoleColor.Red },
                { Status.DirRemoved, ConsoleColor.Green },
                { Status.DirDoesntExist, ConsoleColor.Yellow }
            };
    }

    public class Program
    {
        enum Status {
            InvalidCommand,
            MakeDirCommand,
            RemoveDirCommand
        }

        public static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Hello World!");

            if(args.Length <= 1) {
                MakeDir.HandleState(MakeDir.Status.NoNameGiven);
            } else {
                string naam = args[0];
                if(Directory.Exists(naam)) {
                    MakeDir.HandleState(MakeDir.Status.DirExists, naam);
                } else {
                    Directory.CreateDirectory(naam);
                    MakeDir.HandleState(MakeDir.Status.DirMade, naam);
                }
            }
        }
    }
}
