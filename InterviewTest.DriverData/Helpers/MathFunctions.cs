using InterviewTest.DriverData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Helpers
{
    /// <summary>
    /// Math functions
    /// </summary>
    internal static class MathFunctions
    {
        /// <summary>
        /// Calculates Weighted Average
        /// </summary>
        /// <param name="periodAnalysis">PeriodAnalysis List</param>
        /// <returns>Calculated Weighted Average</returns>
        public static decimal CalculateWeightedAverage(IEnumerable<PeriodAnalysis> periodAnalysis)
        {
            if (periodAnalysis == null && !periodAnalysis.Any())
                return 0;

            decimal weightedAnalysisSum = periodAnalysis.Sum(x => x.DurationInSeconds * x.Rating);
            decimal weightedDurationSum = periodAnalysis.Sum(x => x.DurationInSeconds);
            if (weightedDurationSum > 0)
                return Math.Round(weightedAnalysisSum / weightedDurationSum, 4);
            return 0;
        }
    }
}
