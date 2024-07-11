namespace IT_Web.Extensions;

public static class JwtConfigExtensions
{
    public static string GetJwtKey(this IConfiguration config)
    {
        return config["Jwt:Key"] ?? string.Empty;
    }
}