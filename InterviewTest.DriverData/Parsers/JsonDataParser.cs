using Newtonsoft.Json;

namespace InterviewTest.DriverData.Parsers
{
    /// <summary>
    /// Parses json data into given object
    /// </summary>
    public class JsonDataParser : IDataParser
    {
        /// <summary>
        /// Parses json data into given object
        /// </summary>
        /// <typeparam name="T">Type to which input data should be parsed</typeparam>
        /// <param name="data">Data input to be parse</param>
        /// <returns>Parsed data object</returns>
        public T ParseData<T>(string data)
        {
            if (string.IsNullOrEmpty(data))
                return default(T);

            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}