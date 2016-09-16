using System;
using System.IO;
using System.Linq;

namespace SqlServerVsRedis
{
    class Program
    {
        private const int kb = 1024;
        private const int Mb = 1024 * kb;
        private static readonly int[] itemsSize = new[] { kb, 10 * kb, 100 * kb, 1 * Mb, 10 * Mb, 100 * Mb };
        private static readonly int[] iterationsCount = new[] { 1, 10, 100, 1000 };
        //private static readonly int[] itemsSize = new[] {  10 * kb };
        //private static readonly int[] iterationsCount = new[] { 1000 };
        private static readonly IDataManager<byte[]>[] savers = {new RedisDataManager(),  new SqlServerDataManager() };

        static void Main(string[] args)
        {
            try
            {
                foreach (IDataManager<byte[]> dataManager in savers)
                {
                    foreach (int size in itemsSize)
                    {
                        foreach (int iterations in iterationsCount)
                        {
                            using (FileStream filestream = new FileStream(fileName(dataManager.Type, size, iterations), FileMode.Append))
                            {
                                var streamwriter = new StreamWriter(filestream) { AutoFlush = true };
                                Console.SetOut(streamwriter);


                                PerformanceTester tester = new PerformanceTester(dataManager, iterations);
                                tester.DoSave(CreateArray(size));
                                tester.DoLoad();
                                tester.DoDelete();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                File.WriteAllLines("errors.txt",new string[] { ex.ToString() });
               

            }
            


        }

        private static string fileName(string type, int size, int count)
        {
            return $"{type}_{size}_of_{count}.txt";
        }

        private static byte[] CreateArray(int arrSize)
        {
            int i = 0;
            return Enumerable.Repeat((byte)(++i), arrSize).ToArray();
        }
    }
}
