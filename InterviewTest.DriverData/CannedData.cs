using InterviewTest.DriverData.DataFileReaders;
using InterviewTest.DriverData.Parsers;
using System;
using System.Collections.Generic;

namespace InterviewTest.DriverData
{
	public static class CannedDrivingData
	{
		private static readonly DateTimeOffset _day = new DateTimeOffset(2016, 10, 13, 0, 0, 0, 0, TimeSpan.Zero);

        


        // BONUS: What's so great about IReadOnlyCollections?  
        /* IReadOnlyCollections colection stores read only data. The collection itself cannot be modified.
           i.e. you cannot add, remove, or replace any item in the collection. So, it keeps the collection itself intact.
           We use IReadOnlyCollections when we want to pass the collection for processting but it should not get
           modified by the process/application
        */
        public static readonly IReadOnlyCollection<Period> History = new[]
		{
			new Period
			{
				Start = _day + new TimeSpan(0, 0, 0),
				End = _day + new TimeSpan(8, 54, 0),
				AverageSpeed = 0m
			},
			new Period
			{
				Start = _day + new TimeSpan(8, 54, 0),
				End = _day + new TimeSpan(9, 28, 0),
				AverageSpeed = 28m
			},
			new Period
			{
				Start = _day + new TimeSpan(9, 28, 0),
				End = _day + new TimeSpan(9, 35, 0),
				AverageSpeed = 33m
			},
			new Period
			{
				Start = _day + new TimeSpan(9, 50, 0),
				End = _day + new TimeSpan(12, 35, 0),
				AverageSpeed = 25m
			},
			new Period
			{
				Start = _day + new TimeSpan(12, 35, 0),
				End = _day + new TimeSpan(13, 30, 0),
				AverageSpeed = 0m
			},
			new Period
			{
				Start = _day + new TimeSpan(13, 30, 0),
				End = _day + new TimeSpan(19, 12, 0),
				AverageSpeed = 29m
			},
			new Period
			{
				Start = _day + new TimeSpan(19, 12, 0),
				End = _day + new TimeSpan(24, 0, 0),
				AverageSpeed = 0m
			}
		};

        public static readonly IReadOnlyCollection<Period> NoHistoryRecords = new Period[] { };

        public static readonly IReadOnlyCollection<Period> NotAllowedTimeHistory = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(0, 0, 0),
                End = _day + new TimeSpan(8, 54, 0),
                AverageSpeed = 0m
            },
            new Period
            {
                Start = _day + new TimeSpan(18, 0, 0),
                End = _day + new TimeSpan(19, 54, 0),
                AverageSpeed = 0m
            },
        };

        public static readonly IReadOnlyCollection<Period> SingleRecordValidHistoryWithValidTime = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(9, 0, 0),
                End = _day + new TimeSpan(17, 0, 0),
                AverageSpeed = 30m
            }
        };

        public static IReadOnlyCollection<Period> GetHistoryFromFile()
        {
            string filePath = @"D:\\InterviewTest\InterviewTest.DriverData\DataFiles\HistoryData.json";
            IDataFileReader fileDataReader = new FileDataReader();
            string historyData = fileDataReader.ReadFileData(filePath);
            IDataParser jsondataParser = new JsonDataParser();
            return jsondataParser.ParseData<IReadOnlyCollection<Period>>(historyData);
        }

        public static readonly IReadOnlyCollection<Period> SameStartAndEndTimeHistory = new[]
        {
            new Period
            {
                Start = _day + new TimeSpan(14, 0, 0),
                End = _day + new TimeSpan(14, 0, 0),
                AverageSpeed = 30m
            }
        };

        public static readonly IReadOnlyCollection<Period> FormulaOneHistoryBeyondAllowedMaxSpeed = new[]
        {
            new Period
            {
               Start = _day + new TimeSpan(11, 0, 0),
                End = _day + new TimeSpan(12, 0, 0),
                AverageSpeed = 500m
            }
        };

        public static readonly IReadOnlyCollection<Period> GatewayDriverHistoryBeyondAllowedMaxSpeed = new[]
       {
            new Period
            {
               Start = _day + new TimeSpan(11, 0, 0),
                End = _day + new TimeSpan(12, 0, 0),
                AverageSpeed = 500m
            }
        };
    }
}
