using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsramarWaveAnalyzer;
using NUnit.Framework;

namespace YouWavesAPIUnitTests.ForecastWaveAnalyzer.IsramarWaveAnalyzer
{
    [TestFixture]
    class IsramarImageAnalyzerTests
    {
        private IsramarImageAnalyzer mTarget;

        [TestFixtureSetUp]
        public void Setup()
        {
            mTarget = new IsramarImageAnalyzer();
        }

        [Test]
        public void TestMethod1()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void TestMethod2()
        {
            Assert.IsTrue(true);
        }
    }
}
