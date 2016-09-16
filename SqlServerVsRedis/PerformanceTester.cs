using System;
using System.Collections.Generic;

namespace SqlServerVsRedis
{
    public class PerformanceTester<T>
        where T : class, new()
    {
        private readonly IDataManager<T> dataManager;
        private readonly List<Guid> idPool;

        public PerformanceTester(IDataManager<T> dataManager, int iterationCount)
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
                dataManager.Delete(id);
            }
        }

        public void DoLoad()
        {
            foreach (Guid id in idPool)
            {
                T data = dataManager.Load(id);
            }
        }

        public void DoSave(T data)
        {
            foreach (Guid id in idPool)
            {
                dataManager.Save(data, id);
            }
        }
    }
}
