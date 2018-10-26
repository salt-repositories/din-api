using System;
using Din.Data.Entities;

namespace Din.Service.Dto.Context
{
    public class AddedContentDto
    {
        public string Title { get; set; }
        public ContentType Type { get; set; }
        public DateTime DateAdded { get; set; }
        public ContentStatus Status { get; set; }
        public int Eta { get; set; }
        public double Percentage { get; set; }
    }
}
