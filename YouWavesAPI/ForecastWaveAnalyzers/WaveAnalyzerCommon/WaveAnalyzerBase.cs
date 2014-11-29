using ForecastAnalysisModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveAnalyzerCommon.Model;

namespace WaveAnalyzerCommon
{
    public interface IReportCreator
    {    
        Task<BaseReportModel> Create();
        bool ShouldRun();
    }

    public abstract class WaveAnalyzerBase : IReportCreator
    {
        private readonly IImageDownloader mImageDownloader;
        private readonly IImageAnalyzer mImageAnalyzer;

        protected DateTime mLastRunTime;

        protected WaveAnalyzerBase(IImageDownloader imageDownloader, IImageAnalyzer imageAnalyzer)
        {
            mImageDownloader = imageDownloader;
            mImageAnalyzer = imageAnalyzer;
        }

        public async Task<BaseReportModel> Create()
        {
            mLastRunTime = DateTime.Now;

            var waveForecastReportModel = new WaveForecastReportModel();
            waveForecastReportModel.ForecastStartDate = DateTime.Today.AddDays(1);

            string imageFolder = await mImageDownloader.DownloadImages(waveAnalysisModel);

            var imagesPaths = Directory.GetFiles(imageFolder);
            foreach (var imagePath in imagesPaths)
            {
                if (new FileInfo(imagePath).Length == 0) continue;

                float analysisValue = mImageAnalyzer.AnalyzeImage(imagePath);
                waveAnalysisModel.Update(analysisValue, imagePath);
            }

            Directory.Delete(imageFolder, true);

            return waveAnalysisModel;
        }

        protected abstract void SetForecastEndDate(WaveForecastReportModel waveForecastReportModel);

        public abstract bool ShouldRun();
    }
}
