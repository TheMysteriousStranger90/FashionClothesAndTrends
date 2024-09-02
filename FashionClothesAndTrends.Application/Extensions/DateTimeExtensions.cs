namespace FashionClothesAndTrends.Application.Extensions;

public static class DateTimeExtensions
{
    public static int CalcuateAge(this DateOnly dob)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var age = today.Year - dob.Year;

        if (dob > today.AddYears(-age)) age--;

        return age;
    }

    public static string DateTimeAgo(this DateTime dateTime)
    {
        var timeSpan = DateTime.UtcNow.Subtract(dateTime);

        return timeSpan switch
        {
            { TotalSeconds: <= 60 } => $"{timeSpan.Seconds} seconds ago",
            { TotalMinutes: <= 60 } => timeSpan.Minutes > 1 ? $"{timeSpan.Minutes} minutes ago" : "about a minute ago",
            { TotalHours: <= 24 } => timeSpan.Hours > 1 ? $"{timeSpan.Hours} hours ago" : "about an hour ago",
            { TotalDays: <= 30 } => timeSpan.Days > 1 ? $"{timeSpan.Days} days ago" : "yesterday",
            { TotalDays: <= 365 } => timeSpan.Days > 30 ? $"{timeSpan.Days / 30} months ago" : "about a month ago",
            _ => timeSpan.Days > 365 ? $"{timeSpan.Days / 365} years ago" : "a year ago"
        };
    }
}