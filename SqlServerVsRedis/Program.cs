using System;
using System.IO;

namespace SqlServerVsRedis
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream filestream = new FileStream("stat.txt", FileMode.OpenOrCreate);
            var streamwriter = new StreamWriter(filestream) { AutoFlush = true };
            Console.SetOut(streamwriter);
        }
    }
}
