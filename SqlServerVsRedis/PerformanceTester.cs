using System;
using System.Collections.Generic;

namespace SqlServerVsRedis
{
    public class PerformanceTester

    {
        private readonly IDataManager<byte[]> dataManager;
        private readonly List<Guid> idPool;

        public PerformanceTester(IDataManager<byte[]> dataManager, int iterationCount)
        {
            this.dataManager = dataManager;
            idPool = new List<Guid>(iterationCount);
            for (var i = 0; i < iterationCount; i++)
            {
                idPool.Add(Guid.NewGuid());
            }
        }

        public void DoDelete()
        {
            foreach (Guid id in idPool)
            {
                using (new StopwatchTimer((time) => { Console.WriteLine($"Delete\tlength:\t?\tTime:\t{time}"); }))
                {
                    dataManager.Delete(id);
                }
            }
        }

        public void DoLoad()
        {
            foreach (Guid id in idPool)
            {
                byte[] data = new byte[0];
                using (new StopwatchTimer((time) => { Console.WriteLine($"Load\tlength:\t{data.Length}\tTime:\t{time}"); }))
                {
                    data = dataManager.Load(id);
                }
            }
        }

        public void DoSave(byte[] data)
        {
            foreach (Guid id in idPool)
            {
                using (new StopwatchTimer((time) => { Console.WriteLine($"Save\tlength:\t{data.Length}\tTime:\t{time}"); }))
                {
                    dataManager.Save(data, id);
                }
            }
        }
    }
}
