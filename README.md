This document illustrates the project implementation approach.

During the project implementation, I have used OOPS (Inheritance, Relationships, Static classes), SOLID principles(Dependency inversion, Single responsibility, Open-close Principle). 

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

- Overrided AnalyseValidPeriods as formula one analyser have different implementation than base.

### Task 1.3 Gateway analyser implementation
Overrided AnalyseValidPeriods as Gateway driver analyser have different implementation than base.
	
### Task 1.4 PENALISE FAULTY RECORDING
- Added below properties to DriverAnalysisCriteria
	1. IsPenaltyApplicable - flag holds if penalty is applicable 
	2. Penalty - Penalty Applicable
	
- Modified ComputeHistoryAnalysis method of BaseDriverAnalysisClass to check & apply penalty if applicable.


	
