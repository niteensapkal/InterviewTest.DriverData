using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
    [TestFixture]
    public class FormulaOneAnalyserTests
    {
        private FormulaOneAnalyser formulaOneAnalyser;

        [SetUp]
        public void Initialize()
        {
            formulaOneAnalyser = new FormulaOneAnalyser(new DriverAnalysisCriteria
            {
                AllowedStartTime = new TimeSpan(0, 0, 0),
                AllowedEndTime = new TimeSpan(0, 0, 0),
                AllowedMaxSpeed = 200m,
                RatingForExceedingSpeedLimit = 1,
            });


        }

        [TearDown]
        public void TearDown()
        {
            formulaOneAnalyser = null;
        }

        [Test]
        public void ShouldYieldCorrectValues()
        {
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(10, 3, 0),
                DriverRating = 0.1231m
            };

            var actualResult = formulaOneAnalyser.Analyse(CannedDrivingData.History);

            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
    }
}
