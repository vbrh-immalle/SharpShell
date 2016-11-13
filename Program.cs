using System;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Hello World!");

            for(var i=0; i<args.Length; i++) {
                Console.WriteLine(args[i]);
            }
        }
    }
}
