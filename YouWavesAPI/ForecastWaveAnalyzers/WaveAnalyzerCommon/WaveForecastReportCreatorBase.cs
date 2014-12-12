using ForecastAnalysisEntities;
using Framework;
using System;
using System.Threading.Tasks;


namespace WaveAnalyzerCommon
{
    public interface IReportCreator
    {    
        Task Create();
        bool ShouldRun(DateTime now);
    }

    public abstract class WaveForecastReportCreatorBase : IReportCreator
    {
        protected DateTime mLastRunTime;
        private readonly IStorageAccessor<WaveForecastReportModel> mStorageAccessor;
        
        public WaveForecastReportCreatorBase(IStorageAccessor<WaveForecastReportModel> aStorageAccessor)
        {
            mStorageAccessor = aStorageAccessor;
        }

        public async Task Create()
        {
            mLastRunTime = DateTime.Now;

            var waveForecastReportModel = new WaveForecastReportModel(this.GetType().Name);
            SetForecastTimeFrame(waveForecastReportModel);

            var forecastDates = GetForecastDates(waveForecastReportModel);
            foreach (var forecastDate in forecastDates)
            {
                var waveHeight = await GetWaveHeight(forecastDate);
                waveForecastReportModel.AddWaveTiming(forecastDate, waveHeight);
            }

            await mStorageAccessor.Write(waveForecastReportModel);
        }

        protected abstract void SetForecastTimeFrame(WaveForecastReportModel model);
        protected abstract DateTime[] GetForecastDates(WaveForecastReportModel model);
        protected abstract Task<WaveHeight> GetWaveHeight(DateTime forecastDate);

        public abstract bool ShouldRun(DateTime now);
    }
}
