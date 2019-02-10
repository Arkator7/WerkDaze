using System;
using WerkDaze.Api;
using Xunit;

namespace WerkDaze.UnitTests
{
    public class DateHashTests
    {
        [Fact]
        public void GetHashDay_SampleData_ReturnsCorrectValue()
        {
            Assert.Equal(0, DateHash.GetDateHash(new DateTime(1, 1, 1)));
            Assert.Equal(1, DateHash.GetDateHash(new DateTime(1, 1, 2)));
            Assert.Equal(31, DateHash.GetDateHash(new DateTime(1, 2, 1)));
            Assert.Equal(59, DateHash.GetDateHash(new DateTime(1, 3, 1)));
            Assert.Equal(365, DateHash.GetDateHash(new DateTime(2, 1, 1)));
            Assert.Equal(730, DateHash.GetDateHash(new DateTime(3, 1, 1)));
            Assert.Equal(1461, DateHash.GetDateHash(new DateTime(5, 1, 1)));
            Assert.Equal(3652, DateHash.GetDateHash(new DateTime(11, 1, 1)));
            Assert.Equal(36524, DateHash.GetDateHash(new DateTime(101, 1, 1)));
            Assert.Equal(146097, DateHash.GetDateHash(new DateTime(401, 1, 1)));
            Assert.Equal(584388, DateHash.GetDateHash(new DateTime(1601, 1, 1)));
        }

        [Fact]
        public void GetHashDay_SampleData_1752EdgeCase_ReturnsCorrectValue()
        {
            Assert.Equal(639539, DateHash.GetDateHash(new DateTime(1752, 1, 1)));
            Assert.Equal(639598, DateHash.GetDateHash(new DateTime(1752, 2, 29)));
            Assert.Equal(639599, DateHash.GetDateHash(new DateTime(1752, 3, 1)));
            Assert.Equal(639609, DateHash.GetDateHash(new DateTime(1752, 3, 11)));
            Assert.Equal(639619, DateHash.GetDateHash(new DateTime(1752, 3, 21)));
            Assert.Equal(639629, DateHash.GetDateHash(new DateTime(1752, 3, 31)));
            Assert.Equal(639630, DateHash.GetDateHash(new DateTime(1752, 4, 1)));
            Assert.Equal(639904, DateHash.GetDateHash(new DateTime(1752, 12, 31)));
            Assert.Equal(639905, DateHash.GetDateHash(new DateTime(1753, 1, 1)));
        }

        [Fact]
        public void GetHashDay_SampleData_ModernDates_ReturnsCorrectValue()
        {
            Assert.Equal(652713, DateHash.GetDateHash(new DateTime(1788, 1, 26)));

            Assert.Equal(730485, DateHash.GetDateHash(new DateTime(2001, 1, 1)));
            Assert.Equal(731946, DateHash.GetDateHash(new DateTime(2005, 1, 1)));
            Assert.Equal(733407, DateHash.GetDateHash(new DateTime(2009, 1, 1)));
            Assert.Equal(734868, DateHash.GetDateHash(new DateTime(2013, 1, 1)));
        }

        [Fact]
        public void GetHashDay_SampleData_LeapYearTest_ReturnsCorrectValue()
        {
            Assert.Equal(735963, DateHash.GetDateHash(new DateTime(2016, 1, 1)));
            Assert.Equal(735994, DateHash.GetDateHash(new DateTime(2016, 2, 1)));
            Assert.Equal(736022, DateHash.GetDateHash(new DateTime(2016, 2, 29)));
            Assert.Equal(736023, DateHash.GetDateHash(new DateTime(2016, 3, 1)));
        }

        [Fact]
        public void GetHashDay_SampleData_2019Test_ReturnsCorrectValue()
        {
            Assert.Equal(737059, DateHash.GetDateHash(new DateTime(2019, 1, 1)));
            Assert.Equal(737090, DateHash.GetDateHash(new DateTime(2019, 2, 1)));
            Assert.Equal(737099, DateHash.GetDateHash(new DateTime(2019, 2, 10)));
        }

        [Fact]
        public void ReverseDayHash_SampleData_ReturnsCorrectValue()
        {
            Assert.Equal(new DateTime(2019, 2, 10), DateHash.ReverseDayHash(737099));
            Assert.Equal(new DateTime(2019, 2, 1), DateHash.ReverseDayHash(737090));
            Assert.Equal(new DateTime(2019, 1, 1), DateHash.ReverseDayHash(737059));
        }

        [Fact]
        public void GetDay_SampleData_ReturnsCorrectValue()
        {
            Assert.Equal(0, DateHash.GetDay(new DateTime(1, 1, 1)));
            Assert.Equal(1, DateHash.GetDay(new DateTime(1, 1, 2)));
            Assert.Equal(3, DateHash.GetDay(new DateTime(1, 2, 1)));
            Assert.Equal(3, DateHash.GetDay(new DateTime(1, 3, 1)));
            Assert.Equal(1, DateHash.GetDay(new DateTime(2, 1, 1)));
            Assert.Equal(2, DateHash.GetDay(new DateTime(3, 1, 1)));
            Assert.Equal(5, DateHash.GetDay(new DateTime(5, 1, 1)));
            Assert.Equal(5, DateHash.GetDay(new DateTime(11, 1, 1)));
            Assert.Equal(5, DateHash.GetDay(new DateTime(101, 1, 1)));
            Assert.Equal(0, DateHash.GetDay(new DateTime(401, 1, 1)));
            Assert.Equal(0, DateHash.GetDay(new DateTime(1601, 1, 1))); // Monday
        }

        [Fact]
        public void GetDay_SampleData_1752EdgeCase_ReturnsCorrectValue()
        {
            Assert.Equal(5, DateHash.GetDay(new DateTime(1752, 1, 1)));
            Assert.Equal(1, DateHash.GetDay(new DateTime(1752, 2, 1)));
            Assert.Equal(2, DateHash.GetDay(new DateTime(1752, 3, 1)));
            Assert.Equal(5, DateHash.GetDay(new DateTime(1752, 4, 1)));
            Assert.Equal(0, DateHash.GetDay(new DateTime(1752, 5, 1)));
            Assert.Equal(3, DateHash.GetDay(new DateTime(1752, 6, 1)));
            Assert.Equal(5, DateHash.GetDay(new DateTime(1752, 7, 1)));
            Assert.Equal(1, DateHash.GetDay(new DateTime(1752, 8, 1)));

            // Why Our Calendars Skipped 11 Days in 1752
            //http://mentalfloss.com/article/51370/why-our-calendars-skipped-11-days-1752
            // Old 0 = Friday
            // New 0 = Monday
            Assert.Equal(4, DateHash.GetDay(new DateTime(1752, 9, 1))); // Tuesday
            Assert.Equal(5, DateHash.GetDay(new DateTime(1752, 9, 2))); // Wednesday
            Assert.Equal(3, DateHash.GetDay(new DateTime(1752, 9, 14))); // Thursday

            Assert.Equal(6, DateHash.GetDay(new DateTime(1752, 10, 1)));
            Assert.Equal(2, DateHash.GetDay(new DateTime(1752, 11, 1)));
            Assert.Equal(4, DateHash.GetDay(new DateTime(1752, 12, 1)));
            Assert.Equal(6, DateHash.GetDay(new DateTime(1752, 12, 31)));
            Assert.Equal(0, DateHash.GetDay(new DateTime(1753, 1, 1)));
        }

        [Fact]
        public void GetDay_SampleData_ModernDates_ReturnsCorrectValue()
        {
            Assert.Equal(5, DateHash.GetDay(new DateTime(1788, 1, 26))); // Saturday
            Assert.Equal(3, DateHash.GetDay(new DateTime(1801, 1, 1))); // Thursday
            Assert.Equal(1, DateHash.GetDay(new DateTime(1901, 1, 1))); // Tuesday
            Assert.Equal(0, DateHash.GetDay(new DateTime(2001, 1, 1))); // Monday
            Assert.Equal(5, DateHash.GetDay(new DateTime(2005, 1, 1)));
            Assert.Equal(3, DateHash.GetDay(new DateTime(2009, 1, 1)));
            Assert.Equal(1, DateHash.GetDay(new DateTime(2013, 1, 1)));
            Assert.Equal(6, DateHash.GetDay(new DateTime(2017, 1, 1)));
            Assert.Equal(1, DateHash.GetDay(new DateTime(2017, 10, 10))); // Tuesday
        }

        [Fact]
        public void GetDay_SampleData_2019Test_ReturnsCorrectValue()
        {
            Assert.Equal(1, DateHash.GetDay(new DateTime(2019, 1, 1)));
            Assert.Equal(4, DateHash.GetDay(new DateTime(2019, 2, 1)));
            Assert.Equal(6, DateHash.GetDay(new DateTime(2019, 2, 10))); // Sunday
        }

    }
}
