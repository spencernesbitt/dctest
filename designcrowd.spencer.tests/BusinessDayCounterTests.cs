using designcrowd.spencer.logic;
using System;
using System.Collections.Generic;
using Xunit;

namespace designcrowd.spencer.tests
{
    public static class TestDataSource
    {

        public static IEnumerable<object[]> GetWeekdaysTestData()
        {
            yield return new object[] { new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), 1};
            yield return new object[] { new DateTime(2013, 10, 5), new DateTime(2013, 10, 14), 5};
            yield return new object[] { new DateTime(2013, 10, 7), new DateTime(2014, 1, 1), 61};
            yield return new object[] { new DateTime(2013, 10, 7), new DateTime(2013, 10, 5), 0};
        }

        private static List<DateTime> publicHolidays = new List<DateTime>(){new DateTime(2013, 12, 25), new DateTime(2013, 12, 26), new DateTime(2014, 1, 1)};
       
        public static IEnumerable<object[]> getBusinessDaysDateTimeTestData()
        {
            yield return new object[] { new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), publicHolidays, 1};
            yield return new object[] { new DateTime(2013, 12, 24), new DateTime(2013, 12, 27), publicHolidays, 0};
            yield return new object[] { new DateTime(2013, 10, 7), new DateTime(2014, 1, 1), publicHolidays, 59};
        } 
        public static IEnumerable<object[]> getBusinessDaysRulesTestData()
        {
            yield return new object[] 
            { 
                // Between 6th June 2019 and 12th June 2019 exclusive we have 5 days with Sat, Sun and Mon (10th) as non-business days so expect a return value of 2
                new DateTime(2019, 6, 6), 
                new  DateTime(2019, 6, 12), 
                    new List<IPublicHolidayRule>()
                    {
                        // Add Queen's birthday rule for 10th June 2019
                        new DayBasedPublicHolidayRule() 
                        { 
                            BaseDayOfWeek = DayOfWeek.Monday,
                            WeekOrdinal = WeekInMonthOrdinal.Second,
                            MonthOfYear = 6
                        }
                    }, 
                2
            };

            yield return new object[] 
            { 
                // Between 23rd April 2019 and 26th April 2019 exclusive we have 2 days with Anzac Day(25th) as a non-business day so expect a return value of 1
                new DateTime(2019, 4, 23), // Tuesday 23rd April 2019 
                new DateTime(2019, 4, 26), // Friday 26th April 2019
                new List<IPublicHolidayRule>() 
                {   
                    // Add Anzac day rule for 25th April 2019 
                    new DateBasedPublicHolidayRule() 
                    {
                        DayOfMonth = 25,
                        MonthOfYear = 4
                    }
                }, 
                1
            };            
        }        
    }

    //Add Test case for monday push
   
    public class BusinessDaysCounterTests
    {
        private BusinessDayCounter _counter;

        public BusinessDaysCounterTests()
        {
            _counter = new BusinessDayCounter();
        }

        [Fact(DisplayName = "Weekdays count returns 0 when the first and the second date are the same.")]
        public void WeekdaysBetweenTwoDatesReturns0WhenSecondEqualsFirst()
        {            
            var firstDate = DateTime.Now;
            var secondDate = firstDate;
           
            int expectedResult = 0;
            var actualResult = _counter.WeekdaysBetweenTwoDates(firstDate, secondDate);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact(DisplayName = "Weekdays count returns 0 when first date is after the second date.")]
        public void WeekdaysBetweenTwoDatesReturns0WhenFirstAfterSecond()
        {
            var counter = new BusinessDayCounter();
            var firstDate = DateTime.Now;
            var secondDate = firstDate.AddDays(-1);
            int expected = 0;
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);
            Assert.Equal(expected, actual);
        }

        [Theory(DisplayName = "Weekdays count, data driven tests.")]
        [MemberData(nameof(TestDataSource.GetWeekdaysTestData), MemberType = typeof(TestDataSource))]
        public void WeekdaysBetweenTwoDatesDataDrivenTest(DateTime firstDate, DateTime secondDate, int expectedResult)
        {            
            var actualResult = _counter.WeekdaysBetweenTwoDates(firstDate, secondDate);
            Assert.Equal(expectedResult, actualResult);
        }     

        [Fact(DisplayName = "Business day count returns 0 when the first and the second date are the same.")]
        public void BusinessDaysBetweenTwoDatesReturns0WhenSecondEqualsFirst()
        {           
            var firstDate = DateTime.Now;
            var secondDate = firstDate;
            var publicHols = new List<DateTime>();
            int expectedResult = 0;            
            var actualResult = _counter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHols);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact(DisplayName = "Business day count returns 0 when first date is after the second date.")]
        public void BusinessDaysBetweenTwoDatesReturns0WhenFirstAfterSecond()
        {           
            var firstDate = DateTime.Now;
            var secondDate = firstDate.AddDays(-1);
            var publicHols = new List<DateTime>();
            int expectedResult = 0;            
            var actualResult = _counter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHols);
            Assert.Equal(expectedResult, actualResult);
        }
        
        [Theory(DisplayName = "Business days count with DateTime list, data driven tests.")]
        [MemberData(nameof(TestDataSource.getBusinessDaysDateTimeTestData), MemberType = typeof(TestDataSource))]
        public void BusinessDaysBetweenTwoDatesDataDrivenTestWithDateTime(DateTime firstDate, DateTime secondDate, List<DateTime> publicHolidays, int expectedResult)
        {           
            var actualResult = _counter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);
            Assert.Equal(expectedResult, actualResult);
        }

      
        [Theory(DisplayName = "Business days count with PublicHolidayRules list, data driven tests.")]
        [MemberData(nameof(TestDataSource.getBusinessDaysRulesTestData), MemberType = typeof(TestDataSource))]
        public void BusinessDaysBetweenTwoDatesDataDrivenTestWithRules(DateTime firstDate, DateTime secondDate, List<IPublicHolidayRule> publicHolidays, int expectedResult)
        {           
            var actualResult = _counter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);
            Assert.Equal(expectedResult, actualResult);
        }        
    }
}
