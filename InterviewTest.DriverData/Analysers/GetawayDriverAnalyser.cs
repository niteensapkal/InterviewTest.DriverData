using InterviewTest.DriverData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
    // BONUS: Why internal?
    internal class GetawayDriverAnalyser : BaseDriverAnalyser, IAnalyser
    {
        public GetawayDriverAnalyser(DriverAnalysisCriteria driverAnalysisCriteria) : base(driverAnalysisCriteria) { }
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
        /// Analyses valid period list for Getway driver
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

            if (!periodAnalysisList.Any())
                return periodAnalysisList;


            //remove if analysed end time is before the allowed start time
            periodAnalysisList.RemoveAll(x => x.End.Hour < driverAnalysisCriteria.AllowedStartTime.Hours);

            if (!periodAnalysisList.Any())
                return periodAnalysisList;

            //remove if analysed start time is after the allowed end time
            periodAnalysisList.RemoveAll(x => x.Start.Hour > driverAnalysisCriteria.AllowedEndTime.Hours);

            if (!periodAnalysisList.Any())
                return periodAnalysisList;

            foreach (var period in periodAnalysisList)
            {
                //reset start and end time
                if (period.Start.Hour < driverAnalysisCriteria.AllowedStartTime.Hours)
                {
                    //if analysed start time is before allowed start time reset it to allowed start time
                    period.Start = new DateTimeOffset(period.Start.Year, period.Start.Month,
                        period.Start.Day, driverAnalysisCriteria.AllowedStartTime.Hours, driverAnalysisCriteria.AllowedStartTime.Minutes, driverAnalysisCriteria.AllowedStartTime.Seconds, 0, TimeSpan.Zero);
                }
                if (period.End.Hour > driverAnalysisCriteria.AllowedEndTime.Hours)
                {
                    //if analysed end time is after allowed end time reset it to allowed end time
                    period.End = new DateTimeOffset(period.End.Year, period.End.Month,
                        period.End.Day, driverAnalysisCriteria.AllowedEndTime.Hours, driverAnalysisCriteria.AllowedEndTime.Minutes, driverAnalysisCriteria.AllowedEndTime.Seconds, 0, TimeSpan.Zero);
                }
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