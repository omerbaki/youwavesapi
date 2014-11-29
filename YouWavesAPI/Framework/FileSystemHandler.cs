using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public interface IStorageAccessor
    {
        void Save(string directoryName);
        string[] GetFiles(string directoryName);

        Task<string> WriteReport(object obj);
    }

    class StorageAccessor : IStorageAccessor
    {
        public const string IMAGES_FOLDER = @"Images\{0}\{1}";

        public async Task<string> WriteReport(object obj)
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
