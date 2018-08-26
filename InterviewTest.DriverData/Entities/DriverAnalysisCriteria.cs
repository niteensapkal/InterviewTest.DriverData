using System;

namespace InterviewTest.DriverData
{
    /// <summary>
    /// Criteria for driver analysis
    /// </summary>
    internal class DriverAnalysisCriteria
    {
        /// <summary>
        /// Allowed Start Time
        /// </summary>
        public TimeSpan AllowedStartTime { get; set; }

        /// <summary>
        /// Allowed End Time
        /// </summary>
        public TimeSpan AllowedEndTime { get; set; }

        /// <summary>
        /// Allowed Max Speed
        /// </summary>
        public decimal AllowedMaxSpeed { get; set; }

    }
}
