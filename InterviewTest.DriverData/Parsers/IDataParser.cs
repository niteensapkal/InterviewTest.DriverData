namespace InterviewTest.DriverData.Parsers
{
    /// <summary>
    /// Parses input data
    /// </summary>
    public interface IDataParser
    {
        /// <summary>
        /// Parses input data into expected type
        /// </summary>
        /// <typeparam name="T">Type to which data will be parsed</typeparam>
        /// <param name="data">Input data to parse</param>
        /// <returns>Mentioned type</returns>
        T ParseData<T>(string data);
    }
}
