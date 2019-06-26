using System;

namespace designcrowd.spencer.logic
{
    /// <summary>
    /// Use a DateBasedPublicHolidaRule to represent a public holiday that is associated with a fixed date.
    /// E.G. Western Culture New Years Day which occurs on 1 January each year. If this happens to fall on a weekend, 
    /// the date for the associated public holiday can be pushed to the following Monday by setting the 
    /// PushWeekendValueToMonday property to True.  
    /// </summary>
    public class DateBasedPublicHolidayRule: IPublicHolidayRule
    {
        ///<value>If true, then dates that fall on a Saturday or Sunday will be pushed to the following Monday.</value>
        public bool PushWeekendValueToMonday {get; set;}
        ///<value>The 'default' day for the holiday, e.g. 1 for New Years Day. </Value>
        public int DayOfMonth {get; set;}
        ///<value>The 'default' month for the holiday, e.g. 12 for Christmas Day. </Value>
        public int MonthOfYear{get; set;}

        ///<summary>
        /// Calculates the actual date for a public holiday for a given year.
        ///</summary>
        ///<returns>A DateTime value representing a concrete date for a public holiday.</returns>
        ///<param name="year">An interger representing the year of interest, e.g. 2020</param>

        public DateTime GetHolidayDateForYear(int year)
        {
            try
            {
                var holidayDate = new DateTime(year, MonthOfYear, DayOfMonth);
                if (PushWeekendValueToMonday && (holidayDate.DayOfWeek == DayOfWeek.Saturday || holidayDate.DayOfWeek == DayOfWeek.Sunday))
                {
                    if (holidayDate.DayOfWeek == DayOfWeek.Saturday)
                        holidayDate = holidayDate.AddDays(2);
                    else 
                        holidayDate = holidayDate.AddDays(1);
                }
                return holidayDate;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured calculating the Holiday Date, see inner exception for details. Using a PublicHolidayRuleValidator may help diagnose the root cause.", ex);
            }
        }
    }
}