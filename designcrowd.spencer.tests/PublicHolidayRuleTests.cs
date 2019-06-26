using System;
using Xunit;
using designcrowd.spencer.logic;
using System.Collections.Generic;

namespace designcrowd.spencer.tests
{
    
    public class PublicHolidayRuleValidatorTest{
        [Fact]
        public void ValidateReturnsTrueForValidRule()
        {   
            var rule = new DateBasedPublicHolidayRule(){
                PushWeekendValueToMonday = false,
                DayOfMonth = 26,
                MonthOfYear = 3
            };

            var expected = true;
            var actual = new DateBasedPublicHolidayRuleValidator().Validate(rule);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateReturnsFalseForInvalidRule()
        {   
            var rule = new DateBasedPublicHolidayRule(){
                PushWeekendValueToMonday = false,
                DayOfMonth = 34,
                MonthOfYear = 13
            };

            var expected = false;
            var actual = new DateBasedPublicHolidayRuleValidator().Validate(rule);
            Assert.Equal(expected, actual);
        }
    }
    
    
    public class DateBasedPublicHolidayRuleTests
    {
        [Fact]
        public void GetHolidayDateForYearReturnsApril25ForAnzacDay()
        {
            var anzacDayRule = new DateBasedPublicHolidayRule(){
                PushWeekendValueToMonday = false,
                DayOfMonth = 25,
                MonthOfYear = 4
            };
            
            var expected = new DateTime(2019, 4, 25);
            var actual = anzacDayRule.GetHolidayDateForYear(2019);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetHolidayDateForYearReturnsJanuary2For1Jan2017()
        {
            var newYearsDayRule = new DateBasedPublicHolidayRule(){
                PushWeekendValueToMonday = true,
                DayOfMonth = 1,
                MonthOfYear = 1
            };
            
            var expected = new DateTime(2017, 1, 2);
            var actual = newYearsDayRule.GetHolidayDateForYear(2017);
            Assert.Equal(expected, actual);
        }
    }

    public class DayBasedPublicHolidayRuleTest
    {
        [Fact]
        public void GetHolidayDateForYearReturnsJune10ForQueensBirthday(){
            var queenRule = new DayBasedPublicHolidayRule()
            {
                BaseDayOfWeek = DayOfWeek.Monday,
                WeekOrdinal = WeekInMonthOrdinal.Second,
                MonthOfYear = 6
            };

            var expected = new DateTime (2019, 6, 10);
            var actual = queenRule.GetHolidayDateForYear(2019);
            Assert.Equal(expected, actual);
        }
    }

}
