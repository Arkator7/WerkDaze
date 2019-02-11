using System;

namespace WerkDaze.Api.Interface
{
    public interface IDateHash
    {
        int GetDateHash(DateTime date);

        DateTime ReverseDayHash(int hash);

        int GetDay(DateTime date);

        bool IsWeekend(DateTime date);
    }
}
