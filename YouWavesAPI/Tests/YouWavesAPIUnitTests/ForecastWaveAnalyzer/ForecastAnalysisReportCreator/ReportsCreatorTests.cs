using FakeItEasy.ExtensionSyntax.Full;
using NUnit.Framework;
using System.Collections.Generic;
using ForecastAnalysisReportCreator;
using FakeItEasy;
using Logger;
using WaveAnalyzerCommon;
using Framework;

namespace YouWavesAPIUnitTests.ForecastWaveAnalyzer.ForecastAnalysisReportCreator
{
    [TestFixture]
    class ReportsCreatorTests
    {
        private ILogger mLogger;
        private IReportCreator mReportCreator;
        private IStorageAccessor mStorageAccessor;

        private ReportsCreator mTarget;

        [TestFixtureSetUp]
        public void Setup()
        {
            mLogger = A.Fake<ILogger>();
            mReportCreator = A.Fake<IReportCreator>();
            mStorageAccessor = A.Fake<IStorageAccessor>();

            var reportCreators = new List<IReportCreator> {mReportCreator};

            mTarget = new ReportsCreator(mLogger, reportCreators, mStorageAccessor);
        }

        [Test]
        public void CreateReports_ReportCreatorShouldNotRun_NoReportCreated()
        {
            A.CallTo(() => mReportCreator.ShouldRun()).Returns(false);
            A.CallTo(() => mReportCreator.Create()).MustNotHaveHappened());
        }

        [Test]
        public void TestMethod2()
        {
            Assert.IsTrue(true);
        }
    }  
}
