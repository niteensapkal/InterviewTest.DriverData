using System;

namespace InterviewTest.DriverData.Entities
{

    /// <summary>
    /// Hold period analysis data
    /// </summary>
    internal class PeriodAnalysis
    {
        /// <summary>
        /// Start Time
        /// </summary>
        public DateTimeOffset Start { get; set; }

        /// <summary>
        /// End Time
        /// </summary>
        public DateTimeOffset End { get; set; }

        /// <summary>
        /// Average Speed
        /// </summary>
        public decimal AverageSpeed { get; set; }

        /// <summary>
        /// Is Undocumented period
        /// </summary>
        public bool IsUndcoumentedPeriod { get; set; }

        /// <summary>
        /// Rating
        /// </summary>
        public decimal Rating { get; set; }

        /// <summary>
        /// Duration in seconds
        /// </summary>
        public decimal DurationInSeconds { get; set; }
    }
}
