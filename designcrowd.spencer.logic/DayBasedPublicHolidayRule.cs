using System;

namespace designcrowd.spencer.logic
{
    /// <summary>
    /// Use a DayBasedPublicHolidaRule to represent a public holiday that is based on the nth weekday of a month.
    /// E.G. The Commonwealth holiday for the Queen's bithrday occurs on the second Monday of June each year.   
    /// </summary>
    public class DayBasedPublicHolidayRule: IPublicHolidayRule
    {
        /// <value>Identifies the weekday on which the holiday occurs, e.g. Monday</value>
        public DayOfWeek BaseDayOfWeek {get; set;}

        /// <value>Identifies which occurance of the weekday to use, 
        /// e.g. Second will set the holiday to the second Monday of the month,
        /// assuming the DayOfWeek parameter is set to Monday. </value>
        public WeekInMonthOrdinal WeekOrdinal {get; set;}

        /// <value>Identifies the month of the year in which the holiday occurs, 
        /// e.g. 1 for January, 2 for February and so on.</value>
        public int MonthOfYear {get; set;}

        ///<summary>
        /// Calculates the actual date for a public holiday for a given year.
        ///</summary>
        ///<returns>A DateTime value representing a concrete date for a public holiday.</returns>
        ///<param name="year">An interger representing the year of interest, e.g. 2020</param>
        public DateTime GetHolidayDateForYear(int year)
        {
            try 
            {
                 var testDate = new DateTime(year, MonthOfYear, 1);
                int steps = 0;
                while (steps < (int) WeekOrdinal)
                {
                    if (testDate.DayOfWeek == BaseDayOfWeek)
                        steps ++;
                    testDate = testDate.AddDays(1);    
                }

                return testDate.AddDays(-1);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured calculating the Holiday Date, see inner exception for details. Using a PublicHolidayRuleValidator may help diagnose the root cause.", ex);
            }
           
        }
    }
}