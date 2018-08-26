using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class DeliveryDriverAnalyserTests
	{
        private DeliveryDriverAnalyser deliveryDriverAnalyser;
        private DeliveryDriverAnalyser _deliveryDriverAnalyserWithPenaltyApplicable;

        [SetUp]
        public void Initialize()
        {
            deliveryDriverAnalyser = new DeliveryDriverAnalyser(new DriverAnalysisCriteria 
            {
                AllowedStartTime = new TimeSpan(9, 0, 0),
                AllowedEndTime = new TimeSpan(17, 0, 0),
                AllowedMaxSpeed = 30m,
                RatingForExceedingSpeedLimit = 0,
            });

            
        }

        [TearDown]
        public void TearDown()
        {
            deliveryDriverAnalyser = null;
        }

        [Test]
		public void ShouldYieldCorrectValues()
		{
			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = new TimeSpan(7, 45, 0),
				DriverRating = 0.7638m
			};

			var actualResult = deliveryDriverAnalyser.Analyse(CannedDrivingData.History);

			Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
		}
	}
}
