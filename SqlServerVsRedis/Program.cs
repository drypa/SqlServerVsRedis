using System;
using System.IO;
using System.Linq;

namespace SqlServerVsRedis
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileStream filestream = new FileStream("stat.txt", FileMode.Append))
            {
                var streamwriter = new StreamWriter(filestream) { AutoFlush = true };
                Console.SetOut(streamwriter);


                SqlServerDataManager dataManager = new SqlServerDataManager();
                PerformanceTester tester = new PerformanceTester(dataManager, 1000);
                tester.DoSave(CreateArray(100));
                tester.DoLoad();
                tester.DoDelete();
            }
            
        }

        private static byte[] CreateArray(int arrSize)
        {
            int i = 0;
            return Enumerable.Repeat((byte)(++i), arrSize).ToArray();
        }
    }
}
