using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private IEnumerable<IReportCreator> mReportCreators;
        private IStorageAccessor mStorageAccessor;

        private ReportsCreator mTarget;

        [TestFixtureSetUp]
        public void Setup()
        {
            mLogger = A.Fake<ILogger>();
            mReportCreators = A.Fake<IEnumerable<IReportCreator>>();
            mStorageAccessor = A.Fake<IStorageAccessor>();

            //var fake = new Fake<IFoo>();
            //fake

            mTarget = new ReportsCreator(mLogger, mReportCreators, mStorageAccessor);
        }

        [Test]
        public void CreateReports_ReportCreatorShouldNotRun_NoReportCreated()
        {
            mReportCreators.CallsTo(x => x.Bar("some argument")).Returns("some return value");

            Assert.IsTrue(true);
        }

        [Test]
        public void TestMethod2()
        {
            Assert.IsTrue(true);
        }
    }  
}
