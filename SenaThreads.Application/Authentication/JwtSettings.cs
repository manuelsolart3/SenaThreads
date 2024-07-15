namespace SenaThreads.Application.Authentication;
public class JwtSettings
{
    public string key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public double ExpiryInDays { get; set; }
}
