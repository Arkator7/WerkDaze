using System;
using WerkDaze.Api;
using Xunit;

namespace WerkDaze.UnitTests
{
    public class DateHashTests
    {
        private readonly DateHash datehash;

        public DateHashTests()
        {
            this.datehash = new DateHash();
        }

        [Theory]
        [InlineData(0, 1, 1, 1)]
        [InlineData(1, 1, 1, 2)]
        [InlineData(31, 1, 2, 1)]
        [InlineData(59, 1, 3, 1)]
        [InlineData(365, 2, 1, 1)]
        [InlineData(730, 3, 1, 1)]
        [InlineData(1461, 5, 1, 1)]
        [InlineData(3652, 11, 1, 1)]
        [InlineData(36524, 101, 1, 1)]
        [InlineData(146097, 401, 1, 1)]
        [InlineData(584388, 1601, 1, 1)]
        public void GetDateHash_SampleData_ReturnsCorrectValue(int result, int year, int month, int day)
        {
            Assert.Equal(result, this.datehash.GetDateHash(new DateTime(year, month, day)));
        }

        [Theory]
        [InlineData(639539, 1752, 1, 1)]
        [InlineData(639598, 1752, 2, 29)]
        [InlineData(639599, 1752, 3, 1)]
        [InlineData(639609, 1752, 3, 11)]
        [InlineData(639619, 1752, 3, 21)]
        [InlineData(639629, 1752, 3, 31)]
        [InlineData(639630, 1752, 4, 1)]
        [InlineData(639904, 1752, 12, 31)]
        [InlineData(639905, 1753, 1, 1)]
        public void GetDateHash_SampleData_1752EdgeCase_ReturnsCorrectValue(int result, int year, int month, int day)
        {
            Assert.Equal(result, this.datehash.GetDateHash(new DateTime(year, month, day)));
        }

        [Theory]
        [InlineData(652713, 1788, 1, 26)]
        [InlineData(730485, 2001, 1, 1)]
        [InlineData(731946, 2005, 1, 1)]
        [InlineData(733407, 2009, 1, 1)]
        [InlineData(734868, 2013, 1, 1)]
        public void GetDateHash_SampleData_ModernDates_ReturnsCorrectValue(int result, int year, int month, int day)
        {
            Assert.Equal(result, this.datehash.GetDateHash(new DateTime(year, month, day)));
        }

        [Theory]
        [InlineData(735963, 2016, 1, 1)]
        [InlineData(735994, 2016, 2, 1)]
        [InlineData(736022, 2016, 2, 29)]
        [InlineData(736023, 2016, 3, 1)]
        public void GetDateHash_SampleData_LeapYearTest_ReturnsCorrectValue(int result, int year, int month, int day)
        {
            Assert.Equal(result, this.datehash.GetDateHash(new DateTime(year, month, day)));
        }

        [Theory]
        [InlineData(737059, 2019, 1, 1)]
        [InlineData(737090, 2019, 2, 1)]
        [InlineData(737099, 2019, 2, 10)]
        public void GetDateHash_SampleData_2019Test_ReturnsCorrectValue(int result, int year, int month, int day)
        {
            Assert.Equal(result, this.datehash.GetDateHash(new DateTime(year, month, day)));
        }

        [Theory]
        [InlineData(2019, 2, 10, 737099)]
        [InlineData(2019, 2, 1, 737090)]
        [InlineData(2019, 1, 1, 737059)]
        [InlineData(2001, 1, 1, 730485)]
        [InlineData(1788, 1, 26, 652713)]
        [InlineData(1752, 12, 31, 639904)]
        [InlineData(1752, 4, 1, 639630)]
        [InlineData(1752, 2, 29, 639598)]
        [InlineData(1752, 1, 1, 639539)]
        [InlineData(1601, 1, 1, 584388)]
        [InlineData(2, 1, 1, 365)]
        [InlineData(1, 3, 1, 59)]
        [InlineData(1, 1, 2, 1)]
        [InlineData(1, 1, 1, 0)]
        public void ReverseDayHash_SampleData_ReturnsCorrectValue(int year, int month, int day, int result)
        {
            Assert.Equal(new DateTime(year, month, day), this.datehash.ReverseDayHash(result));
        }

        [Theory]
        [InlineData(1, 1, 1, 2)]
        [InlineData(3, 1, 2, 1)]
        [InlineData(3, 1, 3, 1)]
        [InlineData(1, 2, 1, 1)]
        [InlineData(2, 3, 1, 1)]
        [InlineData(5, 5, 1, 1)]
        [InlineData(5, 11, 1, 1)]
        [InlineData(5, 101, 1, 1)]
        [InlineData(0, 401, 1, 1)]
        [InlineData(0, 1601, 1, 1)] // Friday
        public void GetDay_SampleData_ReturnsCorrectValue(int result, int year, int month, int day)
        {
            Assert.Equal(result, this.datehash.GetDay(new DateTime(year, month, day)));
        }

        [Theory]
        [InlineData(5, 1752, 1, 1)]
        [InlineData(1, 1752, 2, 1)]
        [InlineData(2, 1752, 3, 1)]
        [InlineData(5, 1752, 4, 1)]
        [InlineData(0, 1752, 5, 1)]
        [InlineData(3, 1752, 6, 1)]
        [InlineData(5, 1752, 7, 1)]
        [InlineData(1, 1752, 8, 1)]
        // Why Our Calendars Skipped 11 Days in 1752
        // Old 0 = Friday
        // New 0 = Monday
        //http://mentalfloss.com/article/51370/why-our-calendars-skipped-11-days-1752
        [InlineData(4, 1752, 9, 1)]
        [InlineData(5, 1752, 9, 2)]
        [InlineData(3, 1752, 9, 14)]
        [InlineData(6, 1752, 10, 1)]
        [InlineData(2, 1752, 11, 1)]
        [InlineData(4, 1752, 12, 1)]
        [InlineData(6, 1752, 12, 31)]
        [InlineData(0, 1753, 1, 1)]
        public void GetDay_SampleData_1752EdgeCase_ReturnsCorrectValue(int result, int year, int month, int day)
        {
            Assert.Equal(result, this.datehash.GetDay(new DateTime(year, month, day)));
        }

        [Theory]
        [InlineData(5, 1788, 1, 26)] // Saturday
        [InlineData(3, 1801, 1, 1)] // Thursday
        [InlineData(1, 1901, 1, 1)] // Tuesday
        [InlineData(0, 2001, 1, 1)] // Monday
        [InlineData(5, 2005, 1, 1)]
        [InlineData(3, 2009, 1, 1)]
        [InlineData(1, 2013, 1, 1)]
        [InlineData(6, 2017, 1, 1)]
        [InlineData(1, 2017, 10, 10)] // Tuesday
        public void GetDay_SampleData_ModernDates_ReturnsCorrectValue(int result, int year, int month, int day)
        {
            Assert.Equal(result, this.datehash.GetDay(new DateTime(year, month, day)));
        }

        [Theory]
        [InlineData(1, 2019, 1, 1)]
        [InlineData(4, 2019, 2, 1)]
        [InlineData(6, 2019, 2, 10)]
        public void GetDay_SampleData_2019Test_ReturnsCorrectValue(int result, int year, int month, int day)
        {
            Assert.Equal(result, this.datehash.GetDay(new DateTime(year, month, day)));
        }

        [Theory]
        [InlineData(true, 3, 1, 1)]
        [InlineData(false, 5, 1, 1)]
        [InlineData(false, 1752, 1, 1)]
        [InlineData(false, 1601, 1, 1)] // Friday
        [InlineData(true, 1601, 1, 2)]
        [InlineData(true, 1601, 1, 3)]
        [InlineData(true, 1752, 8, 1)] // 1,2
        [InlineData(true, 2005, 1, 1)] // 5,6
        [InlineData(false, 2009, 1, 1)]
        [InlineData(false, 2013, 1, 1)]
        [InlineData(true, 2017, 1, 1)]
        [InlineData(true, 2019, 2, 10)]
        public void IsWeekend_SampleData_ReturnCorrectValue(bool result, int year, int month, int day)
        {
            Assert.Equal(result, this.datehash.IsWeekend(new DateTime(year, month, day)));
        }
    }
}
