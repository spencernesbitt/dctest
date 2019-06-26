using System;
using System.Collections.Generic;

namespace designcrowd.spencer.logic
{
    public class BusinessDayCounter
    {
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

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<IPublicHolidayRule> publicHolidays)
        {
            if (firstDate == null)
                throw new ArgumentException ("The firstDate is required");
            
            if (secondDate == null)
                throw new ArgumentException ("The secondDate is required");

            if (secondDate <= firstDate)
                return 0;

            //To do, check public holidays is not null - should cause a compile issue as the call will be ambiguos

            // Get distinct year values between first and second dates as we'll need the public holidays for each of these.
            var yearCount = (secondDate.Year - firstDate.Year) + 1; 
            List<DateTime> holidayDates = new List<DateTime>(publicHolidays.Count * yearCount);
            var firstYear = firstDate.Year;
            for (int i = 0; i < yearCount; i++)
            {
               holidayDates.AddRange(publicHolidays.GetHolidayDatesForYear(firstYear + i));
            }
                
            return BusinessDaysBetweenTwoDates(firstDate, secondDate, holidayDates);
        }
    }
}
