namespace Din.Domain.Extensions;

public static class IntExtensions
{
    public static bool MoreOrLessThen(this int number, int other, int amount) =>
        number - amount < other && number + amount > other;
}