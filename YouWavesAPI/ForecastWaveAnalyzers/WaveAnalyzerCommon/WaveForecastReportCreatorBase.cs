using ForecastAnalysisModel;
using System;
using System.Threading.Tasks;


namespace WaveAnalyzerCommon
{
    public interface IReportCreator
    {    
        Task<BaseReportModel> Create();
        bool ShouldRun(DateTime now);
    }

    public abstract class WaveForecastReportCreatorBase : IReportCreator
    {
        protected DateTime mLastRunTime;

        public async Task<BaseReportModel> Create()
        {
            mLastRunTime = DateTime.Now;

            var waveForecastReportModel = new WaveForecastReportModel();
            SetForecastTimeFrame(waveForecastReportModel);

            var forecastDates = GetForecastDates(waveForecastReportModel);
            foreach (var forecastDate in forecastDates)
            {
                var waveHeight = await GetWaveHeight(forecastDate);
                waveForecastReportModel.AddWaveTiming(forecastDate, waveHeight);
            }

            return waveForecastReportModel;
        }

        protected abstract void SetForecastTimeFrame(WaveForecastReportModel model);
        protected abstract DateTime[] GetForecastDates(WaveForecastReportModel model);
        protected abstract Task<WaveHeight> GetWaveHeight(DateTime forecastDate);

        public abstract bool ShouldRun(DateTime now);
    }
}
