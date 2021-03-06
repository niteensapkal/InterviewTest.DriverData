This document illustrates the project implementation approach.

During the project implementation, I have used OOPS (Inheritance, Relationships, Static classes), SOLID principles(Dependency inversion, Single responsibility, Open-close Principle). 

## Task 1 Analysers

### Task 1.1 Delivery driver analyser implementation

- Added below entity classes
	1. DriverAnalysisCriteria - Hold analysis criteria for the driver. Contains AllowedStartTime, AllowedEndTime, AllowedMaxSpeed.
	2. PeriodAnalysis - Holds analysed priod data. Contains Start, End , AverageSpeed, IsUnDcoumentedPeriod flag and Rating.
- Added BaseDriverAnalyser
	1. BaseDriverAnalyser - Parent class to DeliveryDriverAnalyser. Contains  required methods for analysed duration and driver rating calculations.
	2. PeriodAnalysis - Holds analysed period data. Contains Start, End , AverageSpeed, IsUnDcoumentedPeriod flag and Rating.
- Added DriverAnalysisCriteria entity as a readonly parameter to BaseDeliveryDriverAnalyser
- BaseDriverAnalyser contains below methods
	1. PrepareAnalysisData - prepares data for analysis based on history data. Initializes fields for futher calcuations.
	2. AnalyseValidPeriods - Analyses valid periods. Calculates  rating and duration.
	3. AnalyseUndocumentedPeriodList - Analyses undocumented periods list  and adds it to analysed data list.
	4. ComputeHistoryAnalysis - Calculates drier rating and analysed duration
- Added below Helper classes
	1. MathFunctions static class - Contains maths related functions
- MathFunctions contains below methods
	1. CalculateWeightedAverage - Calculate weighted average for analysed period list.
	
### Task 1.2 Formula one analyser implementation

- Overrided AnalyseValidPeriods as formula one analyser have different implementation than parent class.

### Task 1.3 Gateway analyser implementation
Overrided AnalyseValidPeriods as Gateway driver analyser have different implementation than parent class.
	
### Task 1.4 Penalise faulty recording
- Added below properties to DriverAnalysisCriteria
	1. IsPenaltyApplicable - flag holds if penalty is applicable 
	2. Penalty - Penalty Applicable
	
- Modified ComputeHistoryAnalysis method of BaseDriverAnalysisClass to check & apply penalty if applicable.


## Task 2 : Better Analyser Lookup
- Used IReadOnlyDictionary to get respective analyser based on given input type.
- Added AnalyserLookupTests - Tests for Analyser Lookup.


## Task 3 : Canned Data Schmanned Data
- Added Json file format for history data. 
- Added below interfaces
 	1. IDataFileReader - To Reade file from given source
	2. IDataParser - To parse file into specified format
- Added below classes
	1. FileDataReader - Implementation for IDataFileReader. Reads file from given source location into string.
	2. JsonDataParser - Implementation for IDataParser. Expect string format and parses data to json format.
	
- Added FileDataReaderTests for FileDataReader Tests
- Added JsonDataParserTests for JsonParser Tests	


## Task 4 : Improve the tests
- Added test cases for DeliveryDriverAnalyser
- Added test cases for FormulaOneAnalyser
- Added test cases for GatewayAnalyser
- Added test cases for FileDataReader
- Added test cases for JsonDataParser

