using AutoMapper;
using Din.Service.Clients.ResponseObjects;
using Din.Service.Dtos;

namespace Din.Service.Mapping.Converters
{
    public class McCalendarConverter : ITypeConverter<McCalendar, CalendarItemDto>
    {
        public CalendarItemDto Convert(McCalendar source, CalendarItemDto destination, ResolutionContext context)
        {
            return new CalendarItemDto
            {
                Title = source.Title,
                Start = source.PhysicalRelease,
                TextColor = "#d0d2d5",
                Color = source.Downloaded? "rgba(0, 215, 124, .5)" : "rgba(180, 50, 50, .5)"
            };
        }
    }
}
