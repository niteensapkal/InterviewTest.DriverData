using System;
using InterviewTest.DriverData.Analysers;
using System.Collections.Generic;

namespace InterviewTest.DriverData
{
    public static class AnalyserLookup
    {
        /// <summary>
        /// Get Analyser
        /// </summary>
        /// <param name="analyserType">AnalyserType</param>
        /// <returns>IAnalyser</returns>
        public static IAnalyser GetAnalyser(string analyserType)
        {
            if (string.IsNullOrEmpty(analyserType))
                throw new ArgumentNullException(nameof(analyserType), "Null or Empty analyser exception");

            if (!analyserList.ContainsKey(analyserType))
                throw new ArgumentException(nameof(analyserType),  "Invalid analyser exception");

            return analyserList[analyserType]();
        }

        /// <summary>
        /// Readonly collection of available analysers
        /// </summary>
        private static IReadOnlyDictionary<string, Func<IAnalyser>> analyserList =
                    new Dictionary<string, Func<IAnalyser>>
                {
                    { "delivery", DeliveryDriverAnalyser },
                    { "friendly", FriendlyAnalyser },
                    { "formulaone", FormulaOneAnalyser },
                    { "getaway", GetawayDriverAnalyser }
                };

        private static DeliveryDriverAnalyser DeliveryDriverAnalyser()
        {
            var driverConfiguration = new DriverAnalysisCriteria()
            {
                AllowedStartTime = new TimeSpan(9, 0, 0),
                AllowedEndTime = new TimeSpan(17, 0, 0),
                AllowedMaxSpeed = 30m,
                RatingForExceedingSpeedLimit = 0.0m,
                IsPenaltyApplicable = true,
                Penalty = 0.5m
            };
            return new DeliveryDriverAnalyser(driverConfiguration);
        }

        private static FriendlyAnalyser FriendlyAnalyser()
        {
            return new FriendlyAnalyser();
        }

        private static FormulaOneAnalyser FormulaOneAnalyser()
        {
            var driverConfiguration = new DriverAnalysisCriteria()
            {
                AllowedStartTime = TimeSpan.Zero,
                AllowedEndTime = TimeSpan.Zero,
                AllowedMaxSpeed = 200m,
                RatingForExceedingSpeedLimit = 1.0m,
                IsPenaltyApplicable = true,
                Penalty = 0.5m
            };

            return new FormulaOneAnalyser(driverConfiguration);
        }

        private static GetawayDriverAnalyser GetawayDriverAnalyser()
        {
            var driverAnalysisCriteria = new DriverAnalysisCriteria()
            {
                AllowedStartTime = new TimeSpan(13, 0, 0),
                AllowedEndTime = new TimeSpan(14, 0, 0),
                AllowedMaxSpeed = 80m,
                RatingForExceedingSpeedLimit = 1.0m,
                IsPenaltyApplicable = true,
                Penalty = 0.5m
            };

            return new GetawayDriverAnalyser(driverAnalysisCriteria);
        }
    }
}
