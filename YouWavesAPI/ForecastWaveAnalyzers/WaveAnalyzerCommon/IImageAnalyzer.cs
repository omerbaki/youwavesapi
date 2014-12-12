using ForecastAnalysisEntities;

namespace WaveAnalyzerCommon
{
    public interface IImageAnalyzer
    {
        WaveHeight GetWaveHeight(byte[] imageBytes);
    }
}
