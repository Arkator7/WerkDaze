using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WerkDaze.UnitTests")]
namespace WerkDaze.Api
{
    public class BusinessDayCounter
    {
        public static int WEEK = 7;
        public static int WORK_WEEK = 5;

        public static int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            int dto1 = DateHash.GetDateHash(firstDate);
            int dto2 = DateHash.GetDateHash(secondDate);

            int difference = dto2 - dto1;

            // Quick Fail
            if (difference <= 0)
            {
                return 0;
            }

            // Shave off weeks
            if (difference >= WEEK)
            {
                int weeks = difference / WEEK;

                dto1 += weeks * WEEK;

                return (weeks * WORK_WEEK) + 
                    WeekdaysBetweenTwoDates(DateHash.ReverseDayHash(dto1), secondDate);
            }

            int workDays = 0;

            // Loop Exclusive to end date
            while (dto1 < dto2 - 1)
            {
                DateTime d1 = DateHash.ReverseDayHash(dto1);

                if (!DateHash.IsWeekend(d1))
                {
                    workDays += 1;
                }

                dto1 += 1;
            }

            return workDays;
        }

        public static int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            throw new NotImplementedException();
        }
    }
}
