using System;

namespace WerkDaze.Api
{
    public class BusinessDayCounter
    {
        public static int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            DateTimeOffset dto1 = new DateTimeOffset(firstDate);
            DateTimeOffset dto2 = new DateTimeOffset(secondDate);

            TimeSpan difference = dto2 - dto1;

            return Convert.ToInt32(Math.Round(difference.TotalDays)) - 1;


            //throw new NotImplementedException();
        }

        public static int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            throw new NotImplementedException();
        }
    }
}
