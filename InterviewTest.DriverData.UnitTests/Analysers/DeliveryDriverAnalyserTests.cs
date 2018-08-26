using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class DeliveryDriverAnalyserTests
	{
        private DeliveryDriverAnalyser deliveryDriverAnalyser;
        private DeliveryDriverAnalyser deliveryDriverAnalyserWithPenaltyApplicable;

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

            deliveryDriverAnalyserWithPenaltyApplicable = new DeliveryDriverAnalyser(new DriverAnalysisCriteria
            {
                AllowedStartTime = new TimeSpan(9, 0, 0),
                AllowedEndTime = new TimeSpan(17, 0, 0),
                AllowedMaxSpeed = 30m,
                RatingForExceedingSpeedLimit = 0,
                IsPenaltyApplicable = true,
                Penalty = 0.5m
            });


        }

        [TearDown]
        public void TearDown()
        {
            deliveryDriverAnalyser = null;
            deliveryDriverAnalyserWithPenaltyApplicable = null;
        }

        [Test]
		public void ShouldYieldCorrectValues()
		{
            //Arrange
			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = new TimeSpan(7, 45, 0),
				DriverRating = 0.7638m
			};

            //Act
			var actualResult = deliveryDriverAnalyser.Analyse(CannedDrivingData.History);

			Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
		}

        [Test]
        public void ShouldYieldCorrectValues_WhenPenaltyIsApplicable()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(7, 45, 0),
                DriverRating = 0.7638m * 0.5m
            };

            // Act
            var actualResult = deliveryDriverAnalyserWithPenaltyApplicable.Analyse(CannedDrivingData.History);

            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
    }
}
