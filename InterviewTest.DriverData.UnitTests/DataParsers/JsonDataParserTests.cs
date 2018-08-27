using InterviewTest.DriverData.Parsers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.UnitTests.DataParsers
{
    public class JsonDataParserTests
    {
        IDataParser dataParser;

        [SetUp]
        public void Initialize()
        {
            dataParser = new JsonDataParser();
        }

        [TearDown]
        public void TearDown()
        {
            dataParser = null;
        }

        [Test]
        public void ShouldParseJsonData()
        {
            // Arrange
            var jsonData = "[{\"Start\": \"10/13/2016 12:00:00 AM +00:00\",\"End\": \"10/13/2016 8:54:00 AM +00:00\",\"AverageSpeed\": \"0\"}]";

            // Act
            var actualResult = dataParser.ParseData<IReadOnlyCollection<Period>>(jsonData);

            // Assert
            Assert.IsInstanceOf(typeof(IReadOnlyCollection<Period>), actualResult);
            Assert.IsTrue(actualResult.Any());
            Assert.AreEqual(0, actualResult.First().AverageSpeed);
        }

        [Test]
        public void ShouldThrowException_ForInvalidJson()
        {
            // Arrange
            var jsonData = "[{\"Start\": \"10/13/2016 12:00:00 AM +00:00\",]";

            // Act and Assert
            Assert.Throws<Newtonsoft.Json.JsonReaderException>(() => dataParser.ParseData<IReadOnlyCollection<Period>>(jsonData));
        }

        [Test]
        public void ShouldReturnNull_ForEmptyJson()
        {
            // Arrange
            var jsonData = "";

            // Act
            var actualResult = dataParser.ParseData<IReadOnlyCollection<Period>>(jsonData);

            // Assert
            Assert.IsNull(actualResult);
        }

        [Test]
        public void ShouldReturnNull_ForNullInput()
        {
            // Arrange
            string jsonData = null;

            // Act
            var actualResult = dataParser.ParseData<IReadOnlyCollection<Period>>(jsonData);

            // Assert
            Assert.IsNull(actualResult);
        }
    }
}
