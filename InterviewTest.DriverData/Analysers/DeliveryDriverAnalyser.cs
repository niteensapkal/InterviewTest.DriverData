using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
    // BONUS: Why internal?
    internal class DeliveryDriverAnalyser : BaseDriverAnalyser, IAnalyser
    {
        public DeliveryDriverAnalyser(DriverAnalysisCriteria driverAnalysisCriteria) : base(driverAnalysisCriteria) { }
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
    }
}