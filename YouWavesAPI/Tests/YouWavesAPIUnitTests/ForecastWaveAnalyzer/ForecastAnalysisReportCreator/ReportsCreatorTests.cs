using FakeItEasy.ExtensionSyntax.Full;
using NUnit.Framework;
using System.Collections.Generic;
using ForecastAnalysisReportCreator;
using FakeItEasy;
using Logger;
using WaveAnalyzerCommon;
using Framework;
using System.Threading.Tasks;

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
        public async Task CreateReports_ReportCreatorShouldNotRun_NoReportCreated()
        {
            A.CallTo(() => mReportCreator.ShouldRun()).Returns(false);

            await mTarget.CreateReports();

            A.CallTo(() => mReportCreator.Create()).MustNotHaveHappened();
        }

        [Test]
        public async Task CreateReports_ReportCreatorShouldRun_ReportCreatedAndSaved()
        {
            A.CallTo(() => mReportCreator.ShouldRun()).Returns(true);

            await mTarget.CreateReports();

            A.CallTo(() => mReportCreator.Create()).MustHaveHappened();
            A.CallTo(() => mStorageAccessor.Save(null)).WithAnyArguments().MustHaveHappened();
        }
    }  
}
