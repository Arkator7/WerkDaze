using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using WerkDaze.Api.Interface;

namespace WerkDaze.Api
{
    public class BusinessDayCounter : IBusinessDayCounter
    {
        private const int WEEK = 7;
        private const int WORK_WEEK = 5;

        private const int INT_INIT = 0;

        private const string FILE = "./NSWHoliday.json";

        /// <summary>
        /// Calculates number of weekdays between two dates
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="secondDate"></param>
        /// <returns>Number of weekdays between two dates</returns>
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            var dh = new DateHash();

            //Exclude firstDate
            int dto1 = dh.GetDateHash(firstDate) + 1;
            int dto2 = dh.GetDateHash(secondDate);

            int workDays = INT_INIT;

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
                workDays += weeks * WORK_WEEK;
            }

            // Loop Exclude secondDate
            while (dto1 < dto2)
            {
                DateTime d1 = dh.ReverseDayHash(dto1);

                if (!dh.IsWeekend(d1))
                {
                    workDays++;
                }

                dto1++;
            }

            return workDays;
        }

        /// <summary>
        /// Calculates number of business days between two dates
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="secondDate"></param>
        /// <returns>Number of business days between two dates</returns>
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            return WeekdaysBetweenTwoDates(firstDate, secondDate) - HolidaysBetweenDates(firstDate, secondDate);
        }

        /// <summary>
        /// Get the number of holidays between two dates
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="secondDate"></param>
        /// <returns>Get the number of holidays between two dates</returns>
        private int HolidaysBetweenDates(DateTime firstDate, DateTime secondDate)
        {
            var dh = new DateHash();

            var holidays = LoadData<HolidayResponse>(FILE);
            var holidaysInRange = INT_INIT;

            int year = firstDate.Year;

            List<int> holidayHash = new List<int>();

            while (year <= secondDate.Year)
            {
                holidayHash = GetHolidaysForTheYear(holidays, year);

                int startDateHash = dh.GetDateHash(firstDate);
                int endDateHash = dh.GetDateHash(secondDate);

                foreach (var hash in holidayHash)
                {
                    if (hash > startDateHash && hash < endDateHash)
                    {
                        holidaysInRange++;
                    }
                }

                year++;
            }

            return holidaysInRange;
        }

        /// <summary>
        /// Get a list of date hashes for the holidays for a given year
        /// </summary>
        /// <returns></returns>
        private List<int> GetHolidaysForTheYear(HolidayResponse holidays, int year)
        {
            var dh = new DateHash();

            List<int> holidayHash = new List<int>();

            foreach (var holiday in holidays.Holiday)
            {
                if (holiday.Day.HasValue)
                {
                    DateTime date = new DateTime(year, holiday.Month, holiday.Day.Value);
                    int dateHash = dh.GetDateHash(date);

                    holidayHash.Add(dateHash);
                }
                else if (holiday.Iteration.HasValue && holiday.DayOfWeek.HasValue)
                {
                    holidayHash.Add(FindHolidayIteration(holiday, year));
                }
                else
                {
                    throw new ArgumentException("Incorrect Format");
                }
            }

            if (holidays.HasEaster)
            {
                //TODO: Need to move this back to .json file
                holidayHash.Add(dh.GetDateHash(Easter(year)) - 2); // Good Friday
                holidayHash.Add(dh.GetDateHash(Easter(year)) + 1); // Easter Monday
            }

            return holidayHash;
        }

        /// <summary>
        /// Find the date of a holiday that uses the iteration format
        /// e.g. the second monday of June
        /// </summary>
        /// <param name="holiday"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private int FindHolidayIteration(Holiday holiday, int year)
        {
            var dh = new DateHash();

            // Adjust dayoftheweek hash
            int targetDayOfTheWeek = INT_INIT;

            // Old (< 2/9/1752)  0 = Friday (5)
            // New (> 14/9/1752) 0 = Monday (1)
            // holiday.DayOfWeek 0 = Sunday (0)
            if (year < 1752 || (year == 1752 && holiday.Month < 9))
            {
                targetDayOfTheWeek = (holiday.DayOfWeek.Value + 5) % 7;
            } else
            {
                targetDayOfTheWeek = (holiday.DayOfWeek.Value + 1) % 7;
            }

            // Get first day of the month
            int day = 1;
            int dayOfTheWeek = dh.GetDay(new DateTime(year, holiday.Month, day));

            // Get first XDay of the month *e.g. Monday
            while (dayOfTheWeek != targetDayOfTheWeek)
            {
                day++;
                dayOfTheWeek = dh.GetDay(new DateTime(year, holiday.Month, day));
            }

            return dh.GetDateHash(
                new DateTime(year, holiday.Month, day + (WEEK * (holiday.Iteration.Value - 1)))
            );
        }

        private T LoadData<T>(string path)
        {
            T response;

            using (StreamReader sr = File.OpenText(path))
            {
                var temp = sr.ReadToEnd();
                response = JsonConvert.DeserializeObject<T>(temp);
            }

            return response;
        }

        /// <summary>
        /// Determine when Easter Sunday is in a given year
        /// https://codereview.stackexchange.com/questions/193847/find-easter-on-any-given-year
        /// </summary>
        /// <param name="year"></param>
        /// <returns>Determine when Easter Sunday is in a given year</returns>
        private DateTime Easter(int year)
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
