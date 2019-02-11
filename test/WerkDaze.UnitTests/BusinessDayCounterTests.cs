using NSubstitute;
using System;
using WerkDaze.Api;
using WerkDaze.Api.Interface;
using Xunit;

namespace WerkDaze.UnitTests
{
    public class BusinessDayCounterTests
    {
        [Fact]
        public void WeekdaysBetweenTwoDates_SampleData_ReturnCorrectValue()
        {
            var dhMock = Substitute.For<IDateHash>();
            var bdc = new BusinessDayCounter(dhMock);

            Assert.Equal(1, bdc.WeekdaysBetweenTwoDates(new DateTime(2013, 10, 7), new DateTime(2013, 10, 9)));
            Assert.Equal(5, bdc.WeekdaysBetweenTwoDates(new DateTime(2013, 10, 5), new DateTime(2013, 10, 14)));
            Assert.Equal(61, bdc.WeekdaysBetweenTwoDates(new DateTime(2013, 10, 7), new DateTime(2014, 1, 1)));
            Assert.Equal(0, bdc.WeekdaysBetweenTwoDates(new DateTime(2013, 10, 7), new DateTime(2013, 10, 5)));
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_SampleData_ReturnCorrectValue()
        {
            var dhMock = Substitute.For<IDateHash>();
            var bdc = new BusinessDayCounter(dhMock);

            Assert.Equal(1, bdc.BusinessDaysBetweenTwoDates(new DateTime(2013, 10, 7), new DateTime(2013, 10, 9)));
            Assert.Equal(0, bdc.BusinessDaysBetweenTwoDates(new DateTime(2013, 12, 24), new DateTime(2013, 12, 27)));
            Assert.Equal(59, bdc.BusinessDaysBetweenTwoDates(new DateTime(2013, 10, 7), new DateTime(2014, 1, 1)));
        }
    }
}
