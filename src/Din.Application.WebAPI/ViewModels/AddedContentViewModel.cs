using System;
using Din.Domain.Models.Entity;

namespace Din.Application.WebAPI.ViewModels
{
    public class AddedContentViewModel
    {
        public Guid Id { get; set; }
        public int SystemId { get; set; }
        public int ForeignId { get; set; }
        public string Title { get; set; }
        public ContentType Type { get; set; }
        public DateTime DateAdded { get; set; }
        public ContentStatus Status { get; set; }
        public int Eta { get; set; }
        public double Percentage { get; set; }
    }
}
