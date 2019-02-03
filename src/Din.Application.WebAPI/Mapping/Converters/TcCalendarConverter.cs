using AutoMapper;
using Din.Domain.Clients.ResponseObjects;
using Din.Domain.Dtos;

namespace Din.Application.WebAPI.Mapping.Converters
{
    public class TcCalendarConverter : ITypeConverter<TcCalendar, CalendarItemDto>
    {
        public CalendarItemDto Convert(TcCalendar source, CalendarItemDto destination, ResolutionContext context)
        {
            return new CalendarItemDto
            {
                Title = $"{source.Series.Title} \n S{source.SeasonNumber}E{source.EpisodeNumber} : {source.Title}",
                Start = source.AirDate,
                TextColor = "#d0d2d5",
                Color = source.HasFile ? "rgba(0, 215, 124, .5)" : "rgba(180, 50, 50, .5)"
            };
        }
    }
}
