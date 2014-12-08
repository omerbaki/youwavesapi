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

    class StorageAccessor<T> : IStorageAccessor<T>
    {
        private readonly IJsonSerializer mJsonSerializer;

        public StorageAccessor(IJsonSerializer jsonSerializer)
        {
            mJsonSerializer = jsonSerializer;
        }

        public async Task Write(T obj)
        {
            string directory = CreateReportDirectory();
            string reportFileName = Path.Combine(directory, obj.GetType().Name + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".json");
            await mJsonSerializer.Export(reportFileName, obj);
        }

        public Task<T> Read()
        {
            throw new NotImplementedException();
        }

        private string CreateReportDirectory()
        {
            string directory = Path.Combine("Reports", DateTime.Now.ToString("yyyyMMdd_HHmm"));

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }
    }
}
