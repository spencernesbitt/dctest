using System;
using System.Collections.Generic;

namespace designcrowd.spencer.logic
{
    /// <summary>
    /// Calculates the business days (working days) and weekdays that occur between two given dates.
    /// </summary>
    public class BusinessDayCounter
    {
        /// <summary>
        /// Calculates the number of Weekdays (Mon through Fri) that occur between two dates. 
        /// The calculation excludes the start and end of the date range. 
        /// </summary>
        /// <returns> An integer representing the number of weekdays.</returns>
        /// <param name="firstDate">A DateTime value representing the start of the date range.</param>
        /// <param name="secondDate">A DateTime value representing the end of the date range.</param>
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            if (firstDate == null)
                throw new ArgumentException ("The firstDate is required");
            
            if (secondDate == null)
                throw new ArgumentException ("The secondDate is required");

            if (secondDate <= firstDate)
                return 0;

            DateTime current = firstDate.AddDays(1);
            int weekdays  = 0;
            // convert t0 linq
            while (current < secondDate){
                if (current.DayOfWeek != DayOfWeek.Saturday && current.DayOfWeek != DayOfWeek.Sunday){
                    weekdays ++; 
                }
                current = current.AddDays(1);
            }

            return weekdays;
        }

        /// <summary>
        /// Calculates the number of Business (working) days that occur between two dates. 
        /// The calculation excludes the start and end of the date range. 
        /// </summary>
        /// <returns> An integer representing the number of business days.</returns>
        /// <param name="firstDate">A DateTime value representing the start of the date range.</param>
        /// <param name="secondDate">A DateTime value representing the end of the date range.</param>
        /// <param name="publicHolidays">A IList of DateTime values representing public holidays to exclude form the calculation.</param>
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            if (firstDate == null)
                throw new ArgumentException ("The firstDate is required");
            
            if (secondDate == null)
                throw new ArgumentException ("The secondDate is required");

            if (secondDate <= firstDate)
                return 0;

            DateTime current = firstDate.AddDays(1);
            int businessDays  = 0;
            // convert to linq
            while (current < secondDate){
                if (current.DayOfWeek != DayOfWeek.Saturday && 
                current.DayOfWeek != DayOfWeek.Sunday && (publicHolidays == null || !publicHolidays.Contains(current))
                )
                {
                    businessDays ++; 
                }
                current = current.AddDays(1);
            }

            return businessDays;
        }

        /// <summary>
        /// Calculates the number of Business (working) days that occur between two dates. 
        /// The calculation excludes the start and end of the date range. 
        /// </summary>
        /// <returns> An integer representing the number of business days.</returns>
        /// <param name="firstDate">A DateTime value representing the start of the date range.</param>
        /// <param name="secondDate">A DateTime value representing the end of the date range.</param>
        /// <param name="publicHolidays">A IList of Rule values representing public holidays to exclude form the calculation.</param>
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<IPublicHolidayRule> publicHolidays)
        {
            if (firstDate == null)
                throw new ArgumentException ("The firstDate is required");
            
            if (secondDate == null)
                throw new ArgumentException ("The secondDate is required");

            if (secondDate <= firstDate)
                return 0;
            
            // Get distinct year values between first and second dates as we'll need the public holidays for each of these.
            var yearCount = (secondDate.Year - firstDate.Year) + 1; 
            List<DateTime> holidayDates = null;
            if (publicHolidays != null){
                holidayDates = new List<DateTime>(publicHolidays.Count * yearCount);
            } 
            
            var firstYear = firstDate.Year;
            for (int i = 0; i < yearCount; i++)
            {
               holidayDates.AddRange(publicHolidays.GetHolidayDatesForYear(firstYear + i));
            }
                
            return BusinessDaysBetweenTwoDates(firstDate, secondDate, holidayDates);
        }
    }
}
