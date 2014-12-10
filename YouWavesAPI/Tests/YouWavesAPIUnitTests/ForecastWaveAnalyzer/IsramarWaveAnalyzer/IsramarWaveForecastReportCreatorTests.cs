using System;
using System.Threading.Tasks;
using FakeItEasy;
using ForecastAnalysisModel;
using Framework;
using IsramarWaveAnalyzer;
using NUnit.Framework;
using WaveAnalyzerCommon;

namespace YouWavesAPIUnitTests.ForecastWaveAnalyzer.IsramarWaveAnalyzer
{
    [TestFixture]
    class IsramarWaveForecastReportCreatorTests
    {
        private IsramarWaveForecastReportCreator mTarget;
        private IImageDownloader mImageDownloader;
        private IImageAnalyzer mImageAnalyzer;
        private IStorageAccessor<WaveForecastReportModel> mStorageAccessor;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            mImageDownloader = A.Fake<IImageDownloader>();
            mImageAnalyzer = A.Fake<IImageAnalyzer>();
            mStorageAccessor = A.Fake<IStorageAccessor<WaveForecastReportModel>>();
        }

        [SetUp]
        public void TestSetup()
        {
            // Create new target for every test
            mTarget = new IsramarWaveForecastReportCreator(mImageDownloader, mImageAnalyzer, mStorageAccessor);            
        }

        [Test]
        public void ShouldRun_RuntimeIsNot8am_RetrunFalse()
        {
            var dateTimeWith8am = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 1, 0, 0, 0);
            Assert.IsFalse(mTarget.ShouldRun(dateTimeWith8am));
        }

        [Test]
        public void ShouldRun_RuntimeIs8am_RetrunTrue()
        {
            var dateTimeWith8am = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0, 0);
            Assert.IsTrue(mTarget.ShouldRun(dateTimeWith8am));
        }

        [Test]
        public async Task ShouldRun_RuntimeIs8amButAlreadyRan_RetrunFalse()
        {
            var dateTimeWith8am = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0, 0);
            
            await mTarget.Create();
            Assert.IsFalse(mTarget.ShouldRun(dateTimeWith8am));
        }

        [Test]
        public async Task Create_DefaultForecastTimeIsSet_DurationIs4DaysFromToday()
        {
            await mTarget.Create();

            A.CallTo(() => mStorageAccessor.Write(null)).WithAnyArguments().MustHaveHappened();     
        }      
    }
}
