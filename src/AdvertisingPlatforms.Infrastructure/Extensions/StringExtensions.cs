namespace AdvertisingPlatforms.Infrastructure.Extensions;

public static class StringExtensions
{
    public static string[] GetSegments(this string location)
    {
        if (location == "/")
            throw new Exception("Локация не может быть корневой");
        return location.TrimStart('/').Split('/');
    }
}
