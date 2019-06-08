using FluentValidation;

namespace Din.Domain.Commands.Movies
{
    public class AddMovieCommandValidator : AbstractValidator<AddMovieCommand>
    {
        public AddMovieCommandValidator()
        {
            RuleFor(cmd => cmd.Movie.TmdbId)
                .NotEmpty().WithMessage("Movie TMDB id cannot be empty");

            RuleFor(cmd => cmd.Movie.Title)
                .NotEmpty().WithMessage("Movie title cannot be empty");

            RuleFor(cmd => cmd.Movie.TitleSlug)
                .NotEmpty().WithMessage("Movie title slug cannot be empty");

            RuleFor(cmd => cmd.Movie.Year)
                .NotEmpty().WithMessage("Movie year cannot be empty");

            RuleFor(cmd => cmd.Movie.Downloaded)
                .Must(d => d.Equals(false)).WithMessage("Movie downloaded cannot be true");

            RuleFor(cmd => cmd.Movie.Monitored)
                .Must(m => m.Equals(true)).WithMessage("Movie monitored cannot be false");

            RuleFor(cmd => cmd.Movie.Images.Count)
                .GreaterThanOrEqualTo(1).WithMessage("Movie must have at least one image");

            RuleFor(cmd => cmd.Movie.MovieOptions)
                .NotEmpty().WithMessage("Movie options cannot be empty");
        }
    }
}
