﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WerkDaze.Api.Interface
{
    public interface IDateHash
    {
        int GetDateHash(DateTime date)
        DateTime ReverseDayHash(int hash)
        bool IsLeapYear(DateTime date)
        int GetDay(DateTime date)
        bool IsWeekend(DateTime date)

    }
}
