using InterviewTest.DriverData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
    // BONUS: Why internal?
    internal class FormulaOneAnalyser : BaseDriverAnalyser, IAnalyser
    {
        public FormulaOneAnalyser(DriverAnalysisCriteria driverAnalysisCriteria) : base(driverAnalysisCriteria) { }
        public HistoryAnalysis Analyse(IReadOnlyCollection<Period> history)
        {
            var historyAnalysisResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0
            };

            if (history != null && history.Any())
            {
                var analysisList = InitializePeriodAnalysisData(history);
                var analysedList = AnalyseValidPeriodList(analysisList);
                analysedList = AnalysisUndocumentedPeriodList(analysedList);
                historyAnalysisResult = ComputeHistoryAnalysis(analysedList);
            }
            return historyAnalysisResult;
        }

        /// <summary>
        /// Analyses valid period list for formula one driver
        /// </summary>
        /// <param name="periodAnalysisList">Period Analysis List</param>
        /// <returns>Period Analysis List after analysis</returns>
        protected override List<PeriodAnalysis> AnalyseValidPeriodList(List<PeriodAnalysis> periodAnalysisList)
        {
            if (periodAnalysisList == null && !periodAnalysisList.Any())
                return periodAnalysisList;

            //skip anything befoe first non-zero speed.
            periodAnalysisList = periodAnalysisList.SkipWhile(period => period.AverageSpeed <= 0).ToList();

            if (!periodAnalysisList.Any())
                return periodAnalysisList;

            //get index of last non zero analysis
            var lastNonZeroPeriodIndex = periodAnalysisList.IndexOf(periodAnalysisList.LastOrDefault(p => p.AverageSpeed > 0));

            periodAnalysisList = periodAnalysisList.Take(lastNonZeroPeriodIndex + 1).ToList();

            foreach (var period in periodAnalysisList)
            {
                //duration in seconds
                period.DurationInSeconds = (decimal)(period.End - period.Start).TotalSeconds;
                //this is documented period. set flag to false
                period.IsUndcoumentedPeriod = false;

                //calculate rating
                period.Rating = period.AverageSpeed > driverAnalysisCriteria.AllowedMaxSpeed ?
                                        driverAnalysisCriteria.RatingForExceedingSpeedLimit : period.AverageSpeed / driverAnalysisCriteria.AllowedMaxSpeed;
            }
            return periodAnalysisList;

        }
    }
}