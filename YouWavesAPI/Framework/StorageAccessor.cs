using ForecastAnalysisEntities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Framework
{
    public interface IStorageAccessor<T>
    {
        Task Write(T obj);
        Task<T> Read();
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

        public Task<T> Read()
        {
            throw new NotImplementedException();
        }

        private string CreateReportDirectory(T t)
        {
            var storageAttribute = (StorageAttribute)Attribute.GetCustomAttribute(t.GetType(), typeof(StorageAttribute));
            string directory = Path.Combine("Reports", storageAttribute.Name, DateTime.Now.ToString("yyyyMMdd"));

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }
    }
}
