using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class GetawayDriverAnalyserTests
	{
        private GetawayDriverAnalyser getawayDriverAnalyser;

        [SetUp]
        public void Initialize()
        {
            getawayDriverAnalyser = new GetawayDriverAnalyser(new DriverAnalysisCriteria
            {
                AllowedStartTime = new TimeSpan(13, 0, 0),
                AllowedEndTime = new TimeSpan(14, 0, 0),
                AllowedMaxSpeed = 80m,
                RatingForExceedingSpeedLimit = 1,
            });


        }

        [TearDown]
        public void TearDown()
        {
            getawayDriverAnalyser = null;
        }
        [Test]
		public void ShouldYieldCorrectValues()
		{
			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = TimeSpan.FromHours(1),
				DriverRating = 0.1813m
			};

			var actualResult = getawayDriverAnalyser.Analyse(CannedDrivingData.History);

			Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
		}
	}
}
