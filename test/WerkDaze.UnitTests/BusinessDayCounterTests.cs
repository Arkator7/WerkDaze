using NSubstitute;
using System;
using WerkDaze.Api;
using WerkDaze.Api.Interface;
using Xunit;

namespace WerkDaze.UnitTests
{
    public class BusinessDayCounterTests
    {
        [Theory]
        [InlineData(1, 2013, 10, 7, 2013, 10, 9)]
        [InlineData(5, 2013, 10, 5, 2013, 10, 14)]
        [InlineData(60, 2013, 10, 7, 2013, 12, 31)]
        [InlineData(61, 2013, 10, 7, 2014, 1, 1)]
        [InlineData(0, 2013, 10, 7, 2013, 10, 5)]
        public void WeekdaysBetweenTwoDates_SampleData_ReturnCorrectValue(
            int result, int firstDateYear, int firstDateMonth, int firstDateDay, int secondDateYear, int secondDateMonth, int secondDateDay)
        {
            var dhMock = Substitute.For<IDateHash>();
            var bdc = new BusinessDayCounter(dhMock);

            Assert.Equal(result, bdc.WeekdaysBetweenTwoDates(
                new DateTime(firstDateYear, firstDateMonth, firstDateDay),
                new DateTime(secondDateYear, secondDateMonth, secondDateDay)
            ));
        }

        [Theory]
        [InlineData(1, 2013, 10, 6, 2013, 10, 8)]
        [InlineData(1, 2013, 10, 7, 2013, 10, 9)]
        [InlineData(1, 2013, 10, 8, 2013, 10, 10)]
        [InlineData(1, 2013, 10, 9, 2013, 10, 11)]
        [InlineData(1, 2013, 10, 10, 2013, 10, 12)]
        [InlineData(0, 2013, 10, 11, 2013, 10, 13)]
        [InlineData(0, 2013, 10, 12, 2013, 10, 14)]
        [InlineData(1, 2013, 10, 13, 2013, 10, 15)]
        [InlineData(0, 2013, 12, 24, 2013, 12, 27)]
        [InlineData(58, 2013, 10, 7, 2013, 12, 31)]
        [InlineData(59, 2013, 10, 7, 2014, 1, 1)]
        [InlineData(1, 2014, 4, 10, 2014, 4, 13)]
        [InlineData(0, 2014, 4, 17, 2014, 4, 22)]
        public void BusinessDaysBetweenTwoDates_SampleData_ReturnCorrectValue(
            int result, int firstDateYear, int firstDateMonth, int firstDateDay, int secondDateYear, int secondDateMonth, int secondDateDay)
        {
            var dhMock = Substitute.For<IDateHash>();
            var bdc = new BusinessDayCounter(dhMock);

            Assert.Equal(result, bdc.BusinessDaysBetweenTwoDates(
                new DateTime(firstDateYear, firstDateMonth, firstDateDay),
                new DateTime(secondDateYear, secondDateMonth, secondDateDay)
            ));
        }
    }
}
