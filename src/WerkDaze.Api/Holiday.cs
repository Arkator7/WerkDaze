using System.Collections.Generic;

namespace WerkDaze.Api
{
    public class Holiday
    {
        public int Month { get; set; }
        public int? Day { get; set; }
        public int? Iteration { get; set; }
        public int? DayOfWeek { get; set; }
    }

    public class HolidayResponse
    {
        public HolidayResponse()
        {
            Holiday = new List<Holiday>();
        }

        public List<Holiday> Holiday { get; set; }

        public bool HasEaster { get; set; }
    }
}
