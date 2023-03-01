namespace MoonlightShadow.Models
{
    public class MailSettings : IMailSettings
    {
        public string Host_Address { get; set; }
        public int Host_Port { get; set; }
        public string Host_UserName { get; set; }
        public string Host_Password { get; set; }
    }

    public interface IMailSettings
    {
        string Host_Address { get; set; }
        int Host_Port { get; set; }
        string Host_UserName { get; set; }
        string Host_Password { get; set; }
    }
}