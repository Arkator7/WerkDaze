using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WerkDaze.UnitTests")]
namespace WerkDaze.Api
{
    public class BusinessDayCounter
    {
        public const int WEEK = 7;
        public const int WORK_WEEK = 5;

        public const string FILE = "./NSWHoliday.json"; 

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
            return WeekdaysBetweenTwoDates(firstDate, secondDate) - HolidaysBetweenDates(firstDate, secondDate);
        }

        internal static int HolidaysBetweenDates(DateTime firstDate, DateTime secondDate)
        {
            var holidays = LoadData<HolidayResponse>(FILE);
            var holidaysInRange = 0;

            int year = firstDate.Year;

            List<int> holidayHash = new List<int>();

            while(year <= secondDate.Year)
            {
                foreach(var holiday in holidays.Holiday)
                {
                    if (holiday.Day.HasValue)
                    {
                        DateTime date = new DateTime(year, holiday.Month, holiday.Day.Value);
                        int dateHash = DateHash.GetDateHash(date);

                        holidayHash.Add(dateHash);
                    } else if (holiday.Iteration.HasValue && holiday.DayOfWeek.HasValue)
                    {
                        holidayHash.Add(FindHolidayIteration(holiday, year));
                    } else
                    {
                        throw new ArgumentException("Incorrect Format");
                    }
                }

                int startDateHash = DateHash.GetDateHash(firstDate);
                int endDateHash = DateHash.GetDateHash(secondDate);

                foreach (var hash in holidayHash)
                {
                    if (hash > startDateHash && hash < endDateHash)
                    {
                        holidaysInRange++;
                    }
                }

                //Discard at end of year cycle
                holidayHash = new List<int>();
                year++;
            }

            return holidaysInRange;
        }

        // Old (< 2/9/1752)  0 = Friday (5)
        // New (> 14/9/1752) 0 = Monday (1)
        // holiday.DayOfWeek 0 = Sunday (0)
        private static int FindHolidayIteration(Holiday holiday, int year)
        {
            // Adjust dayoftheweek hash
            int targetDayOfTheWeek = 0;

            if (year <= 1751)
            {
                targetDayOfTheWeek = (holiday.DayOfWeek.Value + 5) % 7;
            } else if (year >= 1753)
            {
                targetDayOfTheWeek = (holiday.DayOfWeek.Value + 1) % 7;
            } else
            {
                // TODO: Fix 1752, where old system is until Sept 2, and new system is after Sept 14
                targetDayOfTheWeek = (holiday.DayOfWeek.Value + 3) % 7;
            }

            // Get first day of the month
            int day = 1;
            int dayOfTheWeek = DateHash.GetDay(new DateTime(year, holiday.Month, day));

            // Get first XDay of the month *e.g. Monday
            while (dayOfTheWeek != targetDayOfTheWeek)
            {
                day += 1;
                dayOfTheWeek = DateHash.GetDay(new DateTime(year, holiday.Month, day));
            }

            return DateHash.GetDay(
                new DateTime(year, holiday.Month, day + (WEEK * (holiday.Iteration.Value - 1)))
            );
        }

        private static T LoadData<T>(string path)
        {
            T response;

            using (StreamReader sr = File.OpenText(path))
            {
                var temp = sr.ReadToEnd();
                response = JsonConvert.DeserializeObject<T>(temp);
            }

            return response;
        }

        // https://codereview.stackexchange.com/questions/193847/find-easter-on-any-given-year
        private static DateTime Easter(int year)
        {
            int a = year % 19;
            int b = year / 100;
            int c = (b - (b / 4) - ((8 * b + 13) / 25) + (19 * a) + 15) % 30;
            int d = c - (c / 28) * (1 - (c / 28) * (29 / (c + 1)) * ((21 - a) / 11));
            int e = d - ((year + (year / 4) + d + 2 - b + (b / 4)) % 7);
            int month = 3 + ((e + 40) / 44);
            int day = e + 28 - (31 * (month / 4));
            return new DateTime(year, month, day);
        }
    }
}
