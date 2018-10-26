using System;
using System.Collections.Generic;

namespace Din.Service.DTO.Content
{
    public class CalendarDto
    {
        public Tuple<DateTime, DateTime> DateRange { get; set; }
        public IEnumerable<CalendarItemDto> Items { get; set; }
    }


    public class CalendarItemDto
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public string TextColor { get; set; }
        public string Color { get; set; }
    }
}
