using ForecastAnalysisEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Framework
{
    public interface IStorageAccessor<T>
    {
        Task Write(T obj);
        Task<IEnumerable<T>> ReadAllReports(DateTime day);
    }

    public class StorageAccessor<T> : IStorageAccessor<T>
    {
        private readonly IJsonSerializer mJsonSerializer;

        public StorageAccessor(IJsonSerializer jsonSerializer)
        {
            mJsonSerializer = jsonSerializer;
        }

        public async Task Write(T t)
        {
            string directory = CreateReportDirectory(t);
            string reportFileName = Path.Combine(directory, DateTime.Now.ToString("yyyyMMdd_HHmm") + ".json");
            await mJsonSerializer.Export(reportFileName, t);
        }

        public async Task<IEnumerable<T>> ReadAllReports(DateTime day)
        {
            var list = new List<T>();
            string directory = GetDirectoryName(day);
            foreach(string fileName in Directory.GetFiles(directory))
            {
                T t = (T) await mJsonSerializer.Import(fileName, typeof(T));
                list.Add(t);
            }

            return list;
        }

        private string CreateReportDirectory(T t)
        {
            string directory = GetDirectoryName(DateTime.Now);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }

        private string GetDirectoryName(DateTime aDay)
        {            
            var storageAttribute = (StorageAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(StorageAttribute));
            return Path.Combine("Reports", storageAttribute.Name, aDay.ToString("yyyyMMdd"));
        }
    }
}
