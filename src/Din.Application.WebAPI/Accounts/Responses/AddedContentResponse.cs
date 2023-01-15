using System;
using Din.Domain.Models.Entities;

namespace Din.Application.WebAPI.Accounts.Responses
{
    public record struct AddedContentResponse
    {
        public Guid Id { get; init; }
        public int SystemId { get; init; }
        public int ForeignId { get; init; }
        public string Title { get; init; }
        public ContentType Type { get; set; }
        public DateTime DateAdded { get; set; }
        public ContentStatus Status { get; set; }

        public static implicit operator AddedContentResponse(AddedContent addedContent) => new()
        {
            Id = addedContent.Id,
            SystemId = addedContent.SystemId,
            ForeignId = addedContent.ForeignId,
            Title = addedContent.Title,
            Type = addedContent.Type,
            DateAdded = addedContent.DateAdded,
            Status = addedContent.Status
        };
    }
}
