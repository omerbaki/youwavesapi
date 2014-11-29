using ForecastAnalysisModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WaveAnalyzerCommon.Model;

namespace WaveAnalyzerCommon
{
    public interface IImageDownloader
    {
        Task<string> DownloadImages(BaseReportModel model);
    }

    public abstract class ImageDownloader : IImageDownloader
    {
        

        public async Task<string> DownloadImages(BaseReportModel model)
        {
            string imageFolder = CreateImageFolder();

            var downloadTasks = new List<Task>();
            foreach (var imageModel in GetImageModels(model))
            {
                using (var client = new WebClient())
                {
                    downloadTasks.Add(
                        //byte[] imageData = await client.DownloadDataTaskAsync(new Uri(imageModel.ImageUrl));

                        client.DownloadFileTaskAsync(
                            ,
                            Path.Combine(imageFolder, imageModel.ImageName)));
                }
            }

            try
            {
                await Task.WhenAll(downloadTasks);
            }
            catch (WebException webEx)
            {
                if (!Is404Error(webEx))
                {
                    throw;
                }
            }

            return imageFolder;
        }

        private bool Is404Error(WebException webEx)
        {
            var errorResponse = webEx.Response as HttpWebResponse;
            return errorResponse != null && errorResponse.StatusCode == HttpStatusCode.NotFound;
        }

        private string CreateImageFolder()
        {
            string dateForFolder = DateTime.Now.ToString("yyyy_MM_dd_HH_mm");
            string imageFolder = string.Format(IMAGES_FOLDER, GetDownloaderName(), dateForFolder);

            Directory.CreateDirectory(imageFolder);

            return imageFolder;
        }

        protected abstract DownloadImageModel[] GetImageModels(BaseReportModel model);
        protected abstract string GetDownloaderName();
    }
}
