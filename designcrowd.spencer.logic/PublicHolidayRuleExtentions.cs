using System;
using System.Collections.Generic;
using System.Linq;

namespace designcrowd.spencer.logic
{
    public static class Extensions
    {
        public static List<DateTime> GetHolidayDatesForYear(this IList<IPublicHolidayRule> rules, int year)
        {            
            return rules.Select (rule => rule.GetHolidayDateForYear(year)).ToList();
        }
    }
}