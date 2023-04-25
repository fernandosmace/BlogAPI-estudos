namespace Blog;


public static class Configuration
{
    //TOKEN - JWT
    public static string JwtKey = "FU4Uja8HbDJp67qk8DSNNMLfjM8SqEGpOe4pHCNOaQ35HBqft8";
    public static string ApiKeyName = "api_key";
    public static string ApiKey = "curso_api_md8@E!F14Va75fmaj1%¨GAFA51a62AH-X=Z";
    public static SmtpConfiguration Smtp = new();

    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}