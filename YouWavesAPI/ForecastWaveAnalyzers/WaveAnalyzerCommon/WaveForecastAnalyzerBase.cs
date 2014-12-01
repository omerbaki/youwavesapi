using ForecastAnalysisModel;
using System;
using System.Collections.Generic;
using System.Drawing;
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

    public abstract class WaveForecastAnalyzerBase : IReportCreator
    {
        private readonly IImageDownloader mImageDownloader;
        private readonly IImageAnalyzer mImageAnalyzer;

        protected DateTime mLastRunTime;

        protected WaveForecastAnalyzerBase(IImageDownloader imageDownloader, IImageAnalyzer imageAnalyzer)
        {
            mImageDownloader = imageDownloader;
            mImageAnalyzer = imageAnalyzer;
        }

        public async Task<BaseReportModel> Create()
        {
            mLastRunTime = DateTime.Now;

            var waveForecastReportModel = new WaveForecastReportModel();
            SetForecastTimeFrame(waveForecastReportModel);

            var forecastDates = GetForecastDates(waveForecastReportModel);
            foreach (var forecastDate in forecastDates)
            {
                float waveHeight = await GetWaveHeight(forecastDate);
                waveForecastReportModel.AddWaveTiming(forecastDate, waveHeight);
            }

            return waveForecastReportModel;
        }

        protected abstract void SetForecastTimeFrame(WaveForecastReportModel model);
        protected abstract DateTime[] GetForecastDates(WaveForecastReportModel model);
        protected abstract Task<float> GetWaveHeight(DateTime forecastDate);

        public abstract bool ShouldRun();
    }
}
