using Microsoft.Extensions.Logging;
using Spy.Entities;
using System;
using System.IO;

namespace Spy
{
    class Program
    {
        private static Watch _watch;
        static void Main(string[] args)
        {
            Whatcher();
            Console.ReadKey();
        }

        private static void Whatcher()
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //directory pode ser alterado esse é apenas para teste
            var directory = System.IO.Path.GetDirectoryName(path);

            _watch = new Watch(directory, "txt", "Spy", "Log", "Gravar");
        }
    }
}
