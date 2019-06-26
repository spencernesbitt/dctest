using System;

namespace designcrowd.spencer.logic
{
    public interface IPublicHolidayRule
    {
        DateTime GetHolidayDateForYear(int year);
    }
}