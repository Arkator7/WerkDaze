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
            var dh = new DateHash();

            Assert.Equal(0, dh.GetDateHash(new DateTime(1, 1, 1)));
            Assert.Equal(1, dh.GetDateHash(new DateTime(1, 1, 2)));
            Assert.Equal(31, dh.GetDateHash(new DateTime(1, 2, 1)));
            Assert.Equal(59, dh.GetDateHash(new DateTime(1, 3, 1)));
            Assert.Equal(365, dh.GetDateHash(new DateTime(2, 1, 1)));
            Assert.Equal(730, dh.GetDateHash(new DateTime(3, 1, 1)));
            Assert.Equal(1461, dh.GetDateHash(new DateTime(5, 1, 1)));
            Assert.Equal(3652, dh.GetDateHash(new DateTime(11, 1, 1)));
            Assert.Equal(36524, dh.GetDateHash(new DateTime(101, 1, 1)));
            Assert.Equal(146097, dh.GetDateHash(new DateTime(401, 1, 1)));
            Assert.Equal(584388, dh.GetDateHash(new DateTime(1601, 1, 1)));
        }

        [Fact]
        public void GetHashDay_SampleData_1752EdgeCase_ReturnsCorrectValue()
        {
            var dh = new DateHash();

            Assert.Equal(639539, dh.GetDateHash(new DateTime(1752, 1, 1)));
            Assert.Equal(639598, dh.GetDateHash(new DateTime(1752, 2, 29)));
            Assert.Equal(639599, dh.GetDateHash(new DateTime(1752, 3, 1)));
            Assert.Equal(639609, dh.GetDateHash(new DateTime(1752, 3, 11)));
            Assert.Equal(639619, dh.GetDateHash(new DateTime(1752, 3, 21)));
            Assert.Equal(639629, dh.GetDateHash(new DateTime(1752, 3, 31)));
            Assert.Equal(639630, dh.GetDateHash(new DateTime(1752, 4, 1)));
            Assert.Equal(639904, dh.GetDateHash(new DateTime(1752, 12, 31)));
            Assert.Equal(639905, dh.GetDateHash(new DateTime(1753, 1, 1)));
        }

        [Fact]
        public void GetHashDay_SampleData_ModernDates_ReturnsCorrectValue()
        {
            var dh = new DateHash();

            Assert.Equal(652713, dh.GetDateHash(new DateTime(1788, 1, 26)));

            Assert.Equal(730485, dh.GetDateHash(new DateTime(2001, 1, 1)));
            Assert.Equal(731946, dh.GetDateHash(new DateTime(2005, 1, 1)));
            Assert.Equal(733407, dh.GetDateHash(new DateTime(2009, 1, 1)));
            Assert.Equal(734868, dh.GetDateHash(new DateTime(2013, 1, 1)));
        }

        [Fact]
        public void GetHashDay_SampleData_LeapYearTest_ReturnsCorrectValue()
        {
            var dh = new DateHash();

            Assert.Equal(735963, dh.GetDateHash(new DateTime(2016, 1, 1)));
            Assert.Equal(735994, dh.GetDateHash(new DateTime(2016, 2, 1)));
            Assert.Equal(736022, dh.GetDateHash(new DateTime(2016, 2, 29)));
            Assert.Equal(736023, dh.GetDateHash(new DateTime(2016, 3, 1)));
        }

        [Fact]
        public void GetHashDay_SampleData_2019Test_ReturnsCorrectValue()
        {
            var dh = new DateHash();

            Assert.Equal(737059, dh.GetDateHash(new DateTime(2019, 1, 1)));
            Assert.Equal(737090, dh.GetDateHash(new DateTime(2019, 2, 1)));
            Assert.Equal(737099, dh.GetDateHash(new DateTime(2019, 2, 10)));
        }

        [Fact]
        public void ReverseDayHash_SampleData_ReturnsCorrectValue()
        {
            var dh = new DateHash();

            Assert.Equal(new DateTime(2019, 2, 10), dh.ReverseDayHash(737099));
            Assert.Equal(new DateTime(2019, 2, 1), dh.ReverseDayHash(737090));
            Assert.Equal(new DateTime(2019, 1, 1), dh.ReverseDayHash(737059));
        }

        [Fact]
        public void GetDay_SampleData_ReturnsCorrectValue()
        {
            var dh = new DateHash();

            Assert.Equal(0, dh.GetDay(new DateTime(1, 1, 1)));
            Assert.Equal(1, dh.GetDay(new DateTime(1, 1, 2)));
            Assert.Equal(3, dh.GetDay(new DateTime(1, 2, 1)));
            Assert.Equal(3, dh.GetDay(new DateTime(1, 3, 1)));
            Assert.Equal(1, dh.GetDay(new DateTime(2, 1, 1)));
            Assert.Equal(2, dh.GetDay(new DateTime(3, 1, 1)));
            Assert.Equal(5, dh.GetDay(new DateTime(5, 1, 1)));
            Assert.Equal(5, dh.GetDay(new DateTime(11, 1, 1)));
            Assert.Equal(5, dh.GetDay(new DateTime(101, 1, 1)));
            Assert.Equal(0, dh.GetDay(new DateTime(401, 1, 1)));
            Assert.Equal(0, dh.GetDay(new DateTime(1601, 1, 1))); // Monday
        }

        [Fact]
        public void GetDay_SampleData_1752EdgeCase_ReturnsCorrectValue()
        {
            var dh = new DateHash();

            Assert.Equal(5, dh.GetDay(new DateTime(1752, 1, 1)));
            Assert.Equal(1, dh.GetDay(new DateTime(1752, 2, 1)));
            Assert.Equal(2, dh.GetDay(new DateTime(1752, 3, 1)));
            Assert.Equal(5, dh.GetDay(new DateTime(1752, 4, 1)));
            Assert.Equal(0, dh.GetDay(new DateTime(1752, 5, 1)));
            Assert.Equal(3, dh.GetDay(new DateTime(1752, 6, 1)));
            Assert.Equal(5, dh.GetDay(new DateTime(1752, 7, 1)));
            Assert.Equal(1, dh.GetDay(new DateTime(1752, 8, 1)));

            // Why Our Calendars Skipped 11 Days in 1752
            //http://mentalfloss.com/article/51370/why-our-calendars-skipped-11-days-1752
            // Old 0 = Friday
            // New 0 = Monday
            Assert.Equal(4, dh.GetDay(new DateTime(1752, 9, 1))); // Tuesday
            Assert.Equal(5, dh.GetDay(new DateTime(1752, 9, 2))); // Wednesday
            Assert.Equal(3, dh.GetDay(new DateTime(1752, 9, 14))); // Thursday

            Assert.Equal(6, dh.GetDay(new DateTime(1752, 10, 1)));
            Assert.Equal(2, dh.GetDay(new DateTime(1752, 11, 1)));
            Assert.Equal(4, dh.GetDay(new DateTime(1752, 12, 1)));
            Assert.Equal(6, dh.GetDay(new DateTime(1752, 12, 31)));
            Assert.Equal(0, dh.GetDay(new DateTime(1753, 1, 1)));
        }

        [Fact]
        public void GetDay_SampleData_ModernDates_ReturnsCorrectValue()
        {
            var dh = new DateHash();

            Assert.Equal(5, dh.GetDay(new DateTime(1788, 1, 26))); // Saturday
            Assert.Equal(3, dh.GetDay(new DateTime(1801, 1, 1))); // Thursday
            Assert.Equal(1, dh.GetDay(new DateTime(1901, 1, 1))); // Tuesday
            Assert.Equal(0, dh.GetDay(new DateTime(2001, 1, 1))); // Monday
            Assert.Equal(5, dh.GetDay(new DateTime(2005, 1, 1)));
            Assert.Equal(3, dh.GetDay(new DateTime(2009, 1, 1)));
            Assert.Equal(1, dh.GetDay(new DateTime(2013, 1, 1)));
            Assert.Equal(6, dh.GetDay(new DateTime(2017, 1, 1)));
            Assert.Equal(1, dh.GetDay(new DateTime(2017, 10, 10))); // Tuesday
        }

        [Fact]
        public void GetDay_SampleData_2019Test_ReturnsCorrectValue()
        {
            var dh = new DateHash();

            Assert.Equal(1, dh.GetDay(new DateTime(2019, 1, 1)));
            Assert.Equal(4, dh.GetDay(new DateTime(2019, 2, 1)));
            Assert.Equal(6, dh.GetDay(new DateTime(2019, 2, 10))); // Sunday
        }

    }
}
