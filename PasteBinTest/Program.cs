using System;

namespace PasteBinSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = PasteBin.Create("Hello World, from PasteBinSample!");

            Console.WriteLine(url);
            Console.ReadKey();
        }
    }
}
