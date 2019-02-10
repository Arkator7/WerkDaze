using System;
using WerkDaze.Api;
using Xunit;

namespace WerkDaze.UnitTests
{
    public class BusinessDayCounterTests
    {
        [Fact]
        public void WeekdaysBetweenTwoDates_SampleData_ReturnCorrectValue()
        {
            Assert.Equal(1, BusinessDayCounter.WeekdaysBetweenTwoDates(new DateTime(2013, 10, 7), new DateTime(2013, 10, 9)));
            Assert.Equal(5, BusinessDayCounter.WeekdaysBetweenTwoDates(new DateTime(2013, 10, 5), new DateTime(2013, 10, 14)));
            Assert.Equal(61, BusinessDayCounter.WeekdaysBetweenTwoDates(new DateTime(2013, 10, 7), new DateTime(2014, 1, 1)));
            Assert.Equal(0, BusinessDayCounter.WeekdaysBetweenTwoDates(new DateTime(2013, 10, 7), new DateTime(2013, 10, 5)));
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_SampleData_ReturnCorrectValue()
        {
            Assert.Equal(1, BusinessDayCounter.BusinessDaysBetweenTwoDates(new DateTime(2013, 10, 7), new DateTime(2013, 10, 9)));
            Assert.Equal(0, BusinessDayCounter.BusinessDaysBetweenTwoDates(new DateTime(2013, 12, 24), new DateTime(2013, 12, 27)));
            Assert.Equal(59, BusinessDayCounter.BusinessDaysBetweenTwoDates(new DateTime(2013, 10, 7), new DateTime(2014, 1, 1)));
        }

        [Fact]
        public void GetHashDay_SampleData_ReturnsCorrectValue()
        {
            Assert.Equal(0, BusinessDayCounter.GetDayHash(new DateTime(1, 1, 1)));
            Assert.Equal(1, BusinessDayCounter.GetDayHash(new DateTime(1, 1, 2)));
            Assert.Equal(31, BusinessDayCounter.GetDayHash(new DateTime(1, 2, 1)));
            Assert.Equal(59, BusinessDayCounter.GetDayHash(new DateTime(1, 3, 1)));
            Assert.Equal(365, BusinessDayCounter.GetDayHash(new DateTime(2, 1, 1)));
            Assert.Equal(730, BusinessDayCounter.GetDayHash(new DateTime(3, 1, 1)));
            Assert.Equal(1461, BusinessDayCounter.GetDayHash(new DateTime(5, 1, 1)));
            Assert.Equal(3652, BusinessDayCounter.GetDayHash(new DateTime(11, 1, 1)));
            Assert.Equal(36524, BusinessDayCounter.GetDayHash(new DateTime(101, 1, 1)));
            Assert.Equal(146097, BusinessDayCounter.GetDayHash(new DateTime(401, 1, 1)));
            Assert.Equal(584388, BusinessDayCounter.GetDayHash(new DateTime(1601, 1, 1)));
        }

        [Fact]
        public void GetHashDay_SampleData_1752EdgeCase_ReturnsCorrectValue()
        {
            Assert.Equal(639539, BusinessDayCounter.GetDayHash(new DateTime(1752, 1, 1)));
            Assert.Equal(639598, BusinessDayCounter.GetDayHash(new DateTime(1752, 2, 29)));
            Assert.Equal(639599, BusinessDayCounter.GetDayHash(new DateTime(1752, 3, 1)));
            Assert.Equal(639609, BusinessDayCounter.GetDayHash(new DateTime(1752, 3, 11)));
            Assert.Equal(639619, BusinessDayCounter.GetDayHash(new DateTime(1752, 3, 21)));
            Assert.Equal(639629, BusinessDayCounter.GetDayHash(new DateTime(1752, 3, 31)));
            Assert.Equal(639630, BusinessDayCounter.GetDayHash(new DateTime(1752, 4, 1)));
            Assert.Equal(639904, BusinessDayCounter.GetDayHash(new DateTime(1752, 12, 31)));
            Assert.Equal(639905, BusinessDayCounter.GetDayHash(new DateTime(1753, 1, 1)));
        }

        [Fact]
        public void GetHashDay_SampleData_ModernDates_ReturnsCorrectValue()
        {
            Assert.Equal(652713, BusinessDayCounter.GetDayHash(new DateTime(1788, 1, 26)));

            Assert.Equal(730485, BusinessDayCounter.GetDayHash(new DateTime(2001, 1, 1)));
            Assert.Equal(731946, BusinessDayCounter.GetDayHash(new DateTime(2005, 1, 1)));
            Assert.Equal(733407, BusinessDayCounter.GetDayHash(new DateTime(2009, 1, 1)));
            Assert.Equal(734868, BusinessDayCounter.GetDayHash(new DateTime(2013, 1, 1)));
        }

        [Fact]
        public void GetHashDay_SampleData_LeapYearTest_ReturnsCorrectValue()
        {
            Assert.Equal(735963, BusinessDayCounter.GetDayHash(new DateTime(2016, 1, 1)));
            Assert.Equal(735994, BusinessDayCounter.GetDayHash(new DateTime(2016, 2, 1)));
            Assert.Equal(736022, BusinessDayCounter.GetDayHash(new DateTime(2016, 2, 29)));
            Assert.Equal(736023, BusinessDayCounter.GetDayHash(new DateTime(2016, 3, 1)));
        }

        [Fact]
        public void GetHashDay_SampleData_2019Test_ReturnsCorrectValue()
        {
            Assert.Equal(737059, BusinessDayCounter.GetDayHash(new DateTime(2019, 1, 1)));
            Assert.Equal(737090, BusinessDayCounter.GetDayHash(new DateTime(2019, 2, 1)));
            Assert.Equal(737099, BusinessDayCounter.GetDayHash(new DateTime(2019, 2, 10)));
        }

        [Fact]
        public void ReverseDayHash_SampleData_ReturnsCorrectValue()
        {
            Assert.Equal(new DateTime(2019, 2, 10), BusinessDayCounter.ReverseDayHash(737099));
            Assert.Equal(new DateTime(2019, 2, 1), BusinessDayCounter.ReverseDayHash(737090));
            Assert.Equal(new DateTime(2019, 1, 1), BusinessDayCounter.ReverseDayHash(737059));
        }

        [Fact]
        public void GetDay_SampleData_ReturnsCorrectValue()
        {
            Assert.Equal(0, BusinessDayCounter.GetDay(new DateTime(1, 1, 1)));
            Assert.Equal(1, BusinessDayCounter.GetDay(new DateTime(1, 1, 2)));
            Assert.Equal(3, BusinessDayCounter.GetDay(new DateTime(1, 2, 1)));
            Assert.Equal(3, BusinessDayCounter.GetDay(new DateTime(1, 3, 1)));
            Assert.Equal(1, BusinessDayCounter.GetDay(new DateTime(2, 1, 1)));
            Assert.Equal(2, BusinessDayCounter.GetDay(new DateTime(3, 1, 1)));
            Assert.Equal(5, BusinessDayCounter.GetDay(new DateTime(5, 1, 1)));
            Assert.Equal(5, BusinessDayCounter.GetDay(new DateTime(11, 1, 1)));
            Assert.Equal(5, BusinessDayCounter.GetDay(new DateTime(101, 1, 1)));
            Assert.Equal(0, BusinessDayCounter.GetDay(new DateTime(401, 1, 1)));
            Assert.Equal(0, BusinessDayCounter.GetDay(new DateTime(1601, 1, 1))); // Monday
        }

        [Fact]
        public void GetDay_SampleData_1752EdgeCase_ReturnsCorrectValue()
        {
            Assert.Equal(5, BusinessDayCounter.GetDay(new DateTime(1752, 1, 1)));
            Assert.Equal(1, BusinessDayCounter.GetDay(new DateTime(1752, 2, 1)));
            Assert.Equal(2, BusinessDayCounter.GetDay(new DateTime(1752, 3, 1)));
            Assert.Equal(5, BusinessDayCounter.GetDay(new DateTime(1752, 4, 1)));
            Assert.Equal(0, BusinessDayCounter.GetDay(new DateTime(1752, 5, 1)));
            Assert.Equal(3, BusinessDayCounter.GetDay(new DateTime(1752, 6, 1)));
            Assert.Equal(5, BusinessDayCounter.GetDay(new DateTime(1752, 7, 1)));
            Assert.Equal(1, BusinessDayCounter.GetDay(new DateTime(1752, 8, 1)));

            // Why Our Calendars Skipped 11 Days in 1752
            //http://mentalfloss.com/article/51370/why-our-calendars-skipped-11-days-1752
            // Old 0 = Friday
            // New 0 = Monday
            Assert.Equal(4, BusinessDayCounter.GetDay(new DateTime(1752, 9, 1))); // Tuesday
            Assert.Equal(5, BusinessDayCounter.GetDay(new DateTime(1752, 9, 2))); // Wednesday
            Assert.Equal(3, BusinessDayCounter.GetDay(new DateTime(1752, 9, 14))); // Thursday

            Assert.Equal(6, BusinessDayCounter.GetDay(new DateTime(1752, 10, 1)));
            Assert.Equal(2, BusinessDayCounter.GetDay(new DateTime(1752, 11, 1)));
            Assert.Equal(4, BusinessDayCounter.GetDay(new DateTime(1752, 12, 1)));
            Assert.Equal(6, BusinessDayCounter.GetDay(new DateTime(1752, 12, 31)));
            Assert.Equal(0, BusinessDayCounter.GetDay(new DateTime(1753, 1, 1)));
        }

        [Fact]
        public void GetDay_SampleData_ModernDates_ReturnsCorrectValue()
        {
            Assert.Equal(5, BusinessDayCounter.GetDay(new DateTime(1788, 1, 26))); // Saturday
            Assert.Equal(3, BusinessDayCounter.GetDay(new DateTime(1801, 1, 1))); // Thursday
            Assert.Equal(1, BusinessDayCounter.GetDay(new DateTime(1901, 1, 1))); // Tuesday
            Assert.Equal(0, BusinessDayCounter.GetDay(new DateTime(2001, 1, 1))); // Monday
            Assert.Equal(5, BusinessDayCounter.GetDay(new DateTime(2005, 1, 1)));
            Assert.Equal(3, BusinessDayCounter.GetDay(new DateTime(2009, 1, 1)));
            Assert.Equal(1, BusinessDayCounter.GetDay(new DateTime(2013, 1, 1)));
            Assert.Equal(6, BusinessDayCounter.GetDay(new DateTime(2017, 1, 1)));
            Assert.Equal(1, BusinessDayCounter.GetDay(new DateTime(2017, 10, 10))); // Tuesday
        }

        [Fact]
        public void GetDay_SampleData_2019Test_ReturnsCorrectValue()
        {
            Assert.Equal(1, BusinessDayCounter.GetDay(new DateTime(2019, 1, 1)));
            Assert.Equal(4, BusinessDayCounter.GetDay(new DateTime(2019, 2, 1)));
            Assert.Equal(6, BusinessDayCounter.GetDay(new DateTime(2019, 2, 10))); // Sunday
        }
    }
}
