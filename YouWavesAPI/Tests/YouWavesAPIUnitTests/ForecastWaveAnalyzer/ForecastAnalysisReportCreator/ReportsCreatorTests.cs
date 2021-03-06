﻿using FakeItEasy.ExtensionSyntax.Full;
using NUnit.Framework;
using System.Collections.Generic;
using ForecastAnalysisReportCreator;
using FakeItEasy;
using LoggerFramework;
using WaveAnalyzerCommon;
using Framework;
using System.Threading.Tasks;
using System;

namespace YouWavesAPIUnitTests.ForecastWaveAnalyzer.ForecastAnalysisReportCreator
{
    [TestFixture]
    class ReportsCreatorTests
    {
        private ILogger mLogger;
        private IReportCreator mReportCreator;       

        private ReportsCreator mTarget;
        
        [TestFixtureSetUp]
        public void Setup()
        {
            mLogger = A.Fake<ILogger>();
            mReportCreator = A.Fake<IReportCreator>();         

            var reportCreators = new List<IReportCreator> {mReportCreator};

            mTarget = new ReportsCreator(mLogger, reportCreators);
        }

        [Test]
        public async Task CreateReports_ReportCreatorShouldNotRun_NoReportCreated()
        {
            A.CallTo(() => mReportCreator.ShouldRun(DateTime.MinValue)).WithAnyArguments().Returns(false);

            await mTarget.CreateReports();

            A.CallTo(() => mReportCreator.Create()).MustNotHaveHappened();
        }

        [Test]
        public async Task CreateReports_ReportCreatorShouldRun_ReportCreatedAndSaved()
        {
            A.CallTo(() => mReportCreator.ShouldRun(DateTime.MinValue)).WithAnyArguments().Returns(true);

            await mTarget.CreateReports();

            A.CallTo(() => mReportCreator.Create()).MustHaveHappened();           
        }
    }  
}
