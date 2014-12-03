using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForecastAnalysisModel;
using WaveAnalyzerCommon;
using WaveAnalyzerCommon.Model;

namespace IsramarWaveAnalyzer
{
    public class IsramarImageAnalyzer : IImageAnalyzer
    {
        public WaveHeight GetWaveHeight(byte[] imageBytes)
        {
            var waveHeightCounter = new Dictionary<WaveHeight, int>();

            using (var ms = new MemoryStream(imageBytes))
            {
                using (var bitmap = new Bitmap(ms))
                {
                    var relevantArea = GetRelevantArea();
                    for (int x = relevantArea.X; x < relevantArea.Right; x++)
                    {
                        for (int y = relevantArea.Y; y < relevantArea.Bottom; y++)
                        {
                            var pixelColor = bitmap.GetPixel(x, y);
                            if(ShouldIgnoreColor(pixelColor)) continue;

                            var waveHeightByColor = getWaveHeightByColor(pixelColor);
                            if (!waveHeightCounter.ContainsKey(waveHeightByColor))
                            {
                                waveHeightCounter[waveHeightByColor] = 1;
                            }
                            else
                            {
                                waveHeightCounter[waveHeightByColor]++;
                            }

                        }
                    }
                }
            }

            var maxCount = waveHeightCounter.Values.Max();
            var waveHeight = waveHeightCounter.Single(x => x.Value == maxCount).Key;

            return waveHeight;
        }

        protected Rectangle GetRelevantArea()
        {
            return new Rectangle(777, 315, 14, 35);
        }

        private bool ShouldIgnoreColor(Color color)
        {
            return ((color.R == 0 && color.G == 1 && color.B == 0) ||
                    (color.R == 255 && color.G == 255 && color.B == 254) ||
                    (color.R == 202 && color.G == 204 && color.B == 201));
        }

        private WaveHeight getWaveHeightByColor(Color color)
        {
            if ((color.R == 204 && color.G == 255 && color.B == 255) ||
                (color.R == 164 && color.G == 236 && color.B == 239) ||
                (color.R == 134 && color.G == 216 && color.B == 217) ||
                (color.R == 168 && color.G == 235 && color.B == 235) ||
                (color.R == 106 && color.G == 194 && color.B == 209))
            {
                return WaveHeight.None;
            }
            else if ((color.R == 134 && color.G == 216 && color.B == 217) ||
                     (color.R == 148 && color.G == 255 && color.B == 152))
            {
                return WaveHeight.Small;
            }
            else if ((color.R == 123 && color.G == 241 && color.B == 123) ||
                     (color.R == 93 && color.G == 230 && color.B == 97) ||
                     (color.R == 55 && color.G == 217 && color.B == 67))
            {
                return WaveHeight.Medium;
            }
            else
            {
                return WaveHeight.Big;
            }
        }
    }
}
