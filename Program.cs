using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void setPromptColor()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        static void setInfoColor()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        static void setDataColor()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void setErrorColor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }


        static void ShowCurrentDirectory()
        {
            setInfoColor();
            Console.WriteLine("Files in current directory:");

            //string dir = Directory.GetCurrentDirectory();
            DirectoryInfo d = new DirectoryInfo(Directory.GetCurrentDirectory());

            Console.WriteLine("Directories:");
            setDataColor();
            foreach (DirectoryInfo dir in d.GetDirectories())
            {
                Console.WriteLine(dir.FullName);
            }

            setInfoColor();
            Console.WriteLine("Files:");
            setDataColor();
            foreach (FileInfo file in d.GetFiles())
            {
                Console.WriteLine(file.FullName);
            }
        }

        static void ChangeDirectory()
        {
            setInfoColor();
            Console.Write("Change to directory: ");
            setDataColor();
            string path = Console.ReadLine();

            try
            {
                Directory.SetCurrentDirectory(path);
            }
            catch (DirectoryNotFoundException)
            {
                setErrorColor();
                Console.WriteLine(path + " not found.");
            }
        }

        static void ReadFileOrDirectory()
        {
            setInfoColor();
            Console.Write("What do I have to read? ");
            setDataColor();
            string obj = Console.ReadLine();

            if (Directory.Exists(obj))
            {
                setInfoColor();
                Console.WriteLine("Contents of directory [{0}]:", obj);
                setDataColor();
                foreach (string fileEntry in Directory.GetFileSystemEntries(obj))
                {
                    Console.WriteLine(fileEntry);
                }
            }
            else if (File.Exists(obj))
            {
                setInfoColor();
                Console.WriteLine("Contents of file [{0}]:", obj);
                setDataColor();
                Console.WriteLine(File.ReadAllText(obj));
            }
            else
            {
                setErrorColor();
                Console.WriteLine("Cannot read [{0}], not a file or directory!", obj);
            }
        }

        static void Main(string[] args)
        {
            Console.CancelKeyPress += Console_CancelKeyPress;

            var cmds = new Dictionary<ConsoleKey, Action>() {
                { ConsoleKey.L, ShowCurrentDirectory },
                { ConsoleKey.C, ChangeDirectory },
                { ConsoleKey.R, ReadFileOrDirectory },
                { ConsoleKey.Q, () => Environment.Exit(0) }
            };

            while (true)
            {
                setPromptColor();
                Console.Write("Enter command : ");
                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();
                Console.WriteLine();
                if (cmds.ContainsKey(key.Key))
                {
                    cmds[key.Key]();
                }
                else
                {
                    setErrorColor();
                    Console.WriteLine("Invalid command");
                }
            }

        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            setErrorColor();
            Console.WriteLine("Exit requested. Exiting.");
            Environment.Exit(0);
        }
    }
}
