using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
    // BONUS: Why internal? 
    // Internal class is accessible within same assembly only. Accesses to other assemblies is restricted.
    // As a part of dependency inversion principle, we should program to interface rather than implementation.
    // So clients of this feature can use IAnalyser interface and need not worry about what is actual concrete implementation.
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