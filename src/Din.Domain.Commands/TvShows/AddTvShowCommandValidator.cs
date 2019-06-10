using FluentValidation;

namespace Din.Domain.Commands.TvShows
{
    public class AddTvShowCommandValidator : AbstractValidator<AddTvShowCommand>
    {
        public AddTvShowCommandValidator()
        {
            RuleFor(cmd => cmd.TvShow.TvdbId)
                .NotEmpty().WithMessage("TvShow TVDB id cannot be empty");

            RuleFor(cmd => cmd.TvShow.Title)
                .NotEmpty().WithMessage("TvShow title cannot be empty");

            RuleFor(cmd => cmd.TvShow.TitleSlug)
                .NotEmpty().WithMessage("TvShow title slug cannot be empty");

            RuleFor(cmd => cmd.TvShow.Seasons.Count)
                .GreaterThan(1).WithMessage("TvShow must have at least 1 season");

            RuleFor(cmd => cmd.TvShow.Monitored)
                .Must(m => m.Equals(true)).WithMessage("TvShow monitored cannot be false");

            RuleFor(cmd => cmd.TvShow.Images.Count)
                .GreaterThanOrEqualTo(1).WithMessage("TvShow must have at least one image");
        }
    }
}
