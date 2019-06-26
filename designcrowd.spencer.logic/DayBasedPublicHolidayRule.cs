using System;
using System.Collections.Generic;

namespace designcrowd.spencer.logic
{
    public class DayBasedPublicHolidayRule: IPublicHolidayRule
    {
        public DayOfWeek BaseDayOfWeek {get; set;}

        public WeekInMonthOrdinal WeekOrdinal {get; set;}
        public int MonthOfYear {get; set;}

        public DateTime GetHolidayDateForYear(int year)
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
    }
}