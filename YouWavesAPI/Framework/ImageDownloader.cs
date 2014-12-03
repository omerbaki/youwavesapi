using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public interface IImageDownloader
    {
        Task<byte[]> DownloadImage(DownloadImageModel model);
    }

    public class ImageDownloader : IImageDownloader
    {
        public async Task<byte[]> DownloadImage(DownloadImageModel imageModel)
        {
            using (var client = new WebClient())
            {
                try
                {
                    return await client.DownloadDataTaskAsync(imageModel.ImageUrl);
                }
                catch (WebException webEx)
                {
                    if (!Is404Error(webEx))
                    {
                        throw;
                    }
                }
            }

            return null;
        }

        private bool Is404Error(WebException webEx)
        {
            var errorResponse = webEx.Response as HttpWebResponse;
            return errorResponse != null && errorResponse.StatusCode == HttpStatusCode.NotFound;
        }
    }

    public class DownloadImageModel
    {
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
    }
}
