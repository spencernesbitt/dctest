using System;
using Xunit;
using designcrowd.spencer.logic;
using System.Collections.Generic;

namespace designcrowd.spencer.tests
{
    public class BusinessDatCounterTests
    {
        [Fact]
        public void WeekdaysBetweenTwoDatesReturns0WhenSecondEqualsFirst()
        {
            var counter = new BusinessDayCounter();
            var firstDate = DateTime.Now;
            var secondDate = firstDate;
            int expected = 0;
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WeekdaysBetweenTwoDatesReturns0WhenSecondBeforeFirst()
        {
            var counter = new BusinessDayCounter();
            var firstDate = DateTime.Now;
            var secondDate = firstDate.AddDays(-1);
            int expected = 0;
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WeekdaysBetweenTwoDatesReturns1ForTestData1()
        {
            var counter = new BusinessDayCounter();
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2013, 10, 9);
            int expected = 1;
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WeekdaysBetweenTwoDatesReturns5ForTestData2()
        {
            var counter = new BusinessDayCounter();
            var firstDate = new DateTime(2013, 10, 5);
            var secondDate = new DateTime(2013, 10, 14);
            int expected = 5;
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WeekdaysBetweenTwoDatesReturns61ForTestData3()
        {
            var counter = new BusinessDayCounter();
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2014, 1, 1);
            int expected = 61;
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WeekdaysBetweenTwoDatesReturns0ForTestData3()
        {
            var counter = new BusinessDayCounter();
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2013, 10, 5);
            int expected = 0;
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDatesReturns0WhenSecondEqualsFirst()
        {
            var counter = new BusinessDayCounter();
            var firstDate = DateTime.Now;
            var secondDate = firstDate;
            int expected = 0;
            var publicHols = new List<DateTime>();
            var actual = counter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHols);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDatesReturns0WhenSecondBeforeFirst()
        {
            var counter = new BusinessDayCounter();
            var firstDate = DateTime.Now;
            var secondDate = firstDate.AddDays(-1);
            int expected = 0;
            var publicHols = new List<DateTime>();
            var actual = counter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHols);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDatesReturns1ForTestData1()
        {
            var counter = new BusinessDayCounter();
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2013, 10, 9);
            var publicHolidays = new List<DateTime>(){
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2014, 1, 1)
            };
            int expected = 1;
            var actual = counter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDatesReturns0ForTestData2()
        {
            var counter = new BusinessDayCounter();
            var firstDate = new DateTime(2013, 12, 24);
            var secondDate = new DateTime(2013, 12, 27);
            var publicHolidays = new List<DateTime>(){
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2014, 1, 1)
            };
            int expected = 0;
            var actual = counter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDatesReturns59ForTestData3()
        {
            var counter = new BusinessDayCounter();
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2014, 1, 1);
            var publicHolidays = new List<DateTime>(){
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2014, 1, 1)
            };
            int expected = 59;
            var actual = counter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDatesReturn2ForRuleSet1()
        {
            var counter = new BusinessDayCounter();
            var firstDate = new DateTime(2019, 6, 6); // Thursday 6th June 2019
            var secondDate = new DateTime(2019, 6, 12); // Tuesday 12th June 2019
            // Add Queen's birthday rule for 10th June 2019
            var publicHolidays = new List<IPublicHolidayRule>() 
            { 
                new DayBasedPublicHolidayRule() 
                {
                    BaseDayOfWeek = DayOfWeek.Monday,
                    WeekOrdinal = WeekInMonthOrdinal.Second,
                    MonthOfYear = 6
                }
            };
            // Between 6th June 2019 and 12th June 2019 exclusive we have 5 days with Sat, Sun and Mon (10th) as non-business days so expect a return value of 2
            int expected = 2;
            var actual = counter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void BusinessDaysBetweenTwoDatesReturn1ForRuleSet2()
        {
            var counter = new BusinessDayCounter();
            var firstDate = new DateTime(2019, 4, 23); // Tuesday 23rd April 2019
            var secondDate = new DateTime(2019, 4, 26); // Friday 26th April 2019
            // Add Anzac day rule for 25th April 2019
            var publicHolidays = new List<IPublicHolidayRule>() 
            { 
                new DateBasedPublicHolidayRule() 
                {
                    DayOfMonth = 25,
                    MonthOfYear = 4
                }
            };
            // Between 23rd April 2019 and 26th April 2019 exclusive we have 2 days with Anzac Day(25th) as a non-business day so expect a return value of 1
            int expected = 1;
            var actual = counter.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);

            Assert.Equal(expected, actual);
        }
    }
}
