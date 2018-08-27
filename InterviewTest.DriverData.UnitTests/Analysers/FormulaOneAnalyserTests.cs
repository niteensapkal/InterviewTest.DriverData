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

        [Test]
        public void ShouldYieldZeroRating_ForNullHistory()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            // Act
            var actualResult = formulaOneAnalyser.Analyse(null);

            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldZeroRating_ForZeroHistoryRecords()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            // Act
            var actualResult = formulaOneAnalyser.Analyse(CannedDrivingData.NoHistoryRecords);

            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldZeroRating_ForPeriodsWithSameStartAndEndTime()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            // Act
            var actualResult = formulaOneAnalyser.Analyse(CannedDrivingData.SameStartAndEndTimeHistory);

            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYield1Rating_WhenSpeedIsGreaterThanAllowedMaxSpeed_ForSingleValidRecords()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(1, 0, 0),
                DriverRating = 1.0m
            };

            // Act
            var actualResult = formulaOneAnalyser.Analyse(CannedDrivingData.FormulaOneHistoryBeyondAllowedMaxSpeed);

            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
        }

        [Test]
        public void ShouldYieldCorrectValues_WhenHistoryLoadedFromDataFile()
        {
            // Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(10, 3, 0),
                DriverRating = 0.1231m
            };


            // Act
            var actualResult = formulaOneAnalyser.Analyse(CannedDrivingData.GetHistoryFromFile());

            // Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
    }
}
