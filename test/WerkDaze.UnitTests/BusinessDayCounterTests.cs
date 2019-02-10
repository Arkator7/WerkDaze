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
    }
}
