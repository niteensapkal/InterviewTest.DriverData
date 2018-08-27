using System.IO;

namespace InterviewTest.DriverData.DataFileReaders
{
    public class FileDataReader : IDataFileReader
    {
        /// <summary>
        /// Reads file content in a string
        /// </summary>
        /// <param name="filePath">Input file path to read</param>
        /// <returns>File data as a string</returns>
        public string ReadFileData(string filePath)
        {
            if (File.Exists(filePath))
                return File.ReadAllText(filePath);

            throw new FileNotFoundException($"File not found exception at source location: {filePath}");
        }
    }
}
