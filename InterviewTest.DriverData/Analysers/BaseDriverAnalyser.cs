using InterviewTest.DriverData.Entities;
using InterviewTest.DriverData.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
    internal class BaseDriverAnalyser
    {
        protected readonly DriverAnalysisCriteria driverAnalysisCriteria;

        public BaseDriverAnalyser(DriverAnalysisCriteria _driverAnalysisCriteria)
        {
            driverAnalysisCriteria = _driverAnalysisCriteria;
        }

        /// <summary>
        /// Initialzes period analysis data.
        /// </summary>
        /// <param name="history"></param>
        /// <returns>List of PeriodAnalysis which is input for further calculations.</returns>
        protected virtual List<PeriodAnalysis> InitializePeriodAnalysisData(IEnumerable<Period> history)
        {
            List<PeriodAnalysis> periodAnalysisList = new List<PeriodAnalysis>();
            if (history == null || !history.Any())
                return periodAnalysisList;
            foreach (var period in history)
            {
                PeriodAnalysis periodAnalysis = new PeriodAnalysis();
                periodAnalysis.Start = period.Start;
                periodAnalysis.End = period.End;
                periodAnalysis.AverageSpeed = period.AverageSpeed;
                //consider all periods as undocumented. Reset it on next calculations.
                periodAnalysis.IsUndcoumentedPeriod = true;
                periodAnalysisList.Add(periodAnalysis);
            }
            periodAnalysisList.OrderBy(x => x.Start);
            return periodAnalysisList;
        }

        /// <summary>
        /// Analyses valid period list. Recalculates start and end time as per analysis criteria. Calculates Duration & Rating
        /// </summary>
        /// <param name="periodAnalysisList">PeriodAnalysis List</param>
        /// <returns>Post calculation PeriodAnalysis List</returns>
        protected virtual List<PeriodAnalysis> AnalyseValidPeriodList(List<PeriodAnalysis> periodAnalysisList)
        {
            if (periodAnalysisList != null && periodAnalysisList.Any())
            {
                //remove if analysed end time is before the allowed start time
                periodAnalysisList.RemoveAll(x => x.End.Hour < driverAnalysisCriteria.AllowedStartTime.Hours);

                //remove if analysed start time is after the allowed end time
                periodAnalysisList.RemoveAll(x => x.Start.Hour > driverAnalysisCriteria.AllowedEndTime.Hours);

                //list have valid periods
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

                    //calculate rating. set rating to 0 if speed is greater than allowed max speed. 
                    period.Rating = period.AverageSpeed > driverAnalysisCriteria.AllowedMaxSpeed ?
                                            driverAnalysisCriteria.RatingForExceedingSpeedLimit : period.AverageSpeed / driverAnalysisCriteria.AllowedMaxSpeed;
                }
            }

            return periodAnalysisList;
        }

        /// <summary>
        /// Analyses Undocumented period
        /// Evaluates undocumented period and adds to PeriodAnalysis List
        /// </summary>
        /// <param name="analysedData">PeriodAnalysis List</param>
        /// <returns>PeriodAnalysis List</returns>
        protected virtual List<PeriodAnalysis> AnalysisUndocumentedPeriodList(List<PeriodAnalysis> analysedData)
        {
            if (analysedData != null && analysedData.Any())
            {
                List<PeriodAnalysis> undocumentedPeriods = new List<PeriodAnalysis>();
                //calculations for undocumented period
                for (int i = 1; i < analysedData.Count; i++)
                {
                    if (analysedData[i - 1].End != analysedData[i].Start)
                    {
                        undocumentedPeriods.Add(new PeriodAnalysis
                        {
                            Start = analysedData[i - 1].End,
                            End = analysedData[i].Start,
                            AverageSpeed = 0,
                            DurationInSeconds = (decimal)(analysedData[i].Start - analysedData[i - 1].End).TotalSeconds,
                            IsUndcoumentedPeriod = true, //undocumented period
                            Rating = 0,
                        });
                    }
                }
                if (undocumentedPeriods != null && undocumentedPeriods.Any())
                {
                    analysedData.AddRange(undocumentedPeriods);
                }
            }
            return analysedData;
        }

        /// <summary>
        /// Computes History Analysis. Evaluates Analysed Duration And Driver Rating     
        /// </summary>
        /// <param name="periodAnalysisList"></param>
        /// <returns></returns>
        protected virtual HistoryAnalysis ComputeHistoryAnalysis(IEnumerable<PeriodAnalysis> periodAnalysisList)
        {
            var historyAnalysis = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0
            };

            if (periodAnalysisList != null && periodAnalysisList.Any())
            {
                var documentedPeriodList = periodAnalysisList.Where(x => !x.IsUndcoumentedPeriod);
                TimeSpan analysedDuration = new TimeSpan(0, 0, 0);
                foreach (var period in documentedPeriodList)
                {
                    var timeDifference = period.End - period.Start;
                    analysedDuration = analysedDuration.Add(timeDifference);
                }
                historyAnalysis.AnalysedDuration = analysedDuration;
                historyAnalysis.DriverRating = MathFunctions.CalculateWeightedAverage(periodAnalysisList);
            }
            return historyAnalysis;
        }


    }
}
