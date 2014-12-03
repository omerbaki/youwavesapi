using System;
using System.IO;
using System.Threading.Tasks;

namespace Framework
{
    public interface IStorageAccessor
    {
        Task WriteReport(object obj);
    }

    class StorageAccessor : IStorageAccessor
    {
        public const string IMAGES_FOLDER = @"Images\{0}\{1}";

        private readonly IJsonSerializer mJsonSerializer;

        public StorageAccessor(IJsonSerializer jsonSerializer)
        {
            mJsonSerializer = jsonSerializer;
        }

        public async Task WriteReport(object obj)
        {
            string directory = CreateReportDirectory();
            string reportFileName = Path.Combine(directory, obj.GetType().Name + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".json");
            await mJsonSerializer.Export(reportFileName, obj);
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
