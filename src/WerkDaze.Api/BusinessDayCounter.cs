using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WerkDaze.UnitTests")]
namespace WerkDaze.Api
{
    public class BusinessDayCounter
    {
        public static int STD_DAY_IN_YEAR = 365;
        public static int[] ACCUM_DAYS_FOR_MTHS = new int[] { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };
        public static int LEAP = 4;
        public static int CENTURY = 100;

        public static int MONTHS_IN_YEAR = 12;

        public static int WEEK = 7;
        public static int WORK_WEEK = 5;

        public static int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            int dto1 = GetDayHash(firstDate);
            int dto2 = GetDayHash(secondDate);

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

                return (weeks * WORK_WEEK) + WeekdaysBetweenTwoDates(ReverseDayHash(dto1), secondDate);
            }

            int workDays = 0;

            // Loop Exclusive to end date
            while (dto1 < dto2 - 1)
            {
                DateTime d1 = ReverseDayHash(dto1);

                if (!IsWeekend(d1))
                {
                    workDays += 1;
                }

                dto1 += 1;
            }

            return workDays;
        }

        /// <summary>
        /// Assign a value to each date, starting with 1/1/0001 (being 0) to 31/12/9999. 
        /// As Day of the Week is consistent (7 days a week), you can simply use modulus
        /// to determine day.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        internal static int GetDayHash(DateTime date)
        {
            int year = date.Year - 1;
            int month = date.Month - 1;
            int day = date.Day - 1;

            int leapDays = (year / LEAP) - (year / CENTURY) + (year / (LEAP * CENTURY));

            //If month is beyond Feb and is a leap year
            if (month > 1 && IsLeapYear(date))
            {
                leapDays += 1;
            }

            return (year * STD_DAY_IN_YEAR) + leapDays + ACCUM_DAYS_FOR_MTHS[month] + day;
        }

        internal static int ReverseDayHash_FindYear(int hash)
        {
            int year = (hash / STD_DAY_IN_YEAR) - 1;
            int approx_hash = GetDayHash(new DateTime(year, 1, 1));
            int difference = hash - approx_hash;

            // Self-correction
            while (difference > STD_DAY_IN_YEAR)
            {
                year += 1;
                approx_hash = GetDayHash(new DateTime(year, 1, 1));
                difference = hash - approx_hash;
            }

            return year;
        }

        internal static int ReverseDayHash_FindMonth(int year, int hash)
        {
            int month_index = MONTHS_IN_YEAR - 1;
            int approx_hash = GetDayHash(new DateTime(year, 1, 1));
            int difference = hash - approx_hash;

            while ((difference - ACCUM_DAYS_FOR_MTHS[month_index]) < 0)
            {
                month_index -= 1;
            }

            return month_index + 1;
        }

        internal static int ReverseDayHash_FindDay(int year, int month, int hash)
        {
            int approx_hash = GetDayHash(new DateTime(year, month, 1));
            int difference = hash - approx_hash;

            return difference + 1;
        }

        internal static DateTime ReverseDayHash(int hash)
        {
            int year = ReverseDayHash_FindYear(hash);
            int month = ReverseDayHash_FindMonth(year, hash);
            int day = ReverseDayHash_FindDay(year, month, hash);

            return new DateTime(year, month, day);
        }

        private static bool IsLeapYear(DateTime date)
        {
            if (date.Year % 4 != 0)
            {
                return false;
            } else if (date.Year % 100 != 0)
            {
                return true;
            } else if (date.Year % 400 != 0)
            {
                return false;
            } else
            {
                return true;
            }
        }

        /// <summary>
        /// Returns modulus of GetHashDay()
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        internal static int GetDay(DateTime date)
        {
            return GetDayHash(date) % 7;
        }

        /// <summary>
        /// Determine if day is weekend
        /// 
        /// Why Our Calendars Skipped 11 Days in 1752
        /// http://mentalfloss.com/article/51370/why-our-calendars-skipped-11-days-1752
        /// Old 0 = Friday
        /// New 0 = Monday
        /// 
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        internal static bool IsWeekend(DateTime date)
        {
            return ((date < new DateTime(1752, 9, 2) && (GetDay(date) == 1 || GetDay(date) == 2)) ||
                (date > new DateTime(1752, 9, 14) && (GetDay(date) == 5 || GetDay(date) == 6)));
        }

        public static int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {



            throw new NotImplementedException();
        }


    }
}
