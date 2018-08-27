using InterviewTest.DriverData.DataFileReaders;
using NUnit.Framework;
using System.IO;

namespace InterviewTest.DriverData.UnitTests.DataReaders
{
    [TestFixture]
    public class FileDataReaderTests
    {
        IDataFileReader dataFileReader;

        [SetUp]
        public void Initialize()
        {
            dataFileReader = new FileDataReader();
        }

        [TearDown]
        public void TearDown()
        {
            dataFileReader = null;
        }

        [Test]
        public void ShouldReadFile_ForValidInputPath()
        {
            // Arrange
            string filePath = @"D:\\InterviewTest\InterviewTest.DriverData\DataFiles\HistoryData.json";

            // Act
            var actualResult = dataFileReader.ReadFileData(filePath);

            // Assert
            Assert.IsNotNull(actualResult);
        }

        [Test]
        public void ShouldThrowException_ForInvalidInputPath()
        {
            // Arrange
            string filePath = "invalidPath";

            // Act and Assert
            Assert.Throws<FileNotFoundException>(() => dataFileReader.ReadFileData(filePath));
        }
    }
}
