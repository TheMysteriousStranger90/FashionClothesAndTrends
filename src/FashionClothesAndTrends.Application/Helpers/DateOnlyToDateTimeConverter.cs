using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FashionClothesAndTrends.Application.Helpers;

public class DateOnlyToDateTimeConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyToDateTimeConverter() : base(
        dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
        dateTime => DateOnly.FromDateTime(dateTime))
    {
    }
}