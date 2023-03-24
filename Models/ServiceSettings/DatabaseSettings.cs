namespace MoonlightShadow.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        // Collections
        public string UsersCollectionName { get; set; }
        public string CameraCollectionName { get; set; }
        public string LaptopCollectionName { get; set; }
        public string GameCollectionName { get; set; }
        public string PhoneCollectionName { get; set; }
        public string ContactCollectionName { get; set; }
        public string TransactionCollectionName { get; set; }
        public string TokenCollectionName { get; set; }
        public string Salt { get; set; }
    }

    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

        // Collections
        string UsersCollectionName { get; set; }
        string CameraCollectionName { get; set; }
        string LaptopCollectionName { get; set; }
        string GameCollectionName { get; set; }
        string PhoneCollectionName { get; set; }
        string ContactCollectionName { get; set; }
        string TransactionCollectionName { get; set; }
        string TokenCollectionName { get; set; }
        string Salt { get; set; }
    }
}