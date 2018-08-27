using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.DataFileReaders
{
    /// <summary>
    /// Reads data from given data source
    /// </summary>
    public interface IDataFileReader
    {
        /// <summary>
        /// Reads data from given data source and returns as a string
        /// </summary>
        /// <param name="source">Data source URL or path</param>
        /// <returns>Data as a string</returns>
        string ReadFileData(string source);
    }
}
