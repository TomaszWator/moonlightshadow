namespace MoonlightShadow.Models
{
    public class Address
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        public string GetGeneralAddress()
        {
            return Country + ", " + State;
        }

        public string GetAccurateAddress()
        {
            return ZipCode + " " + Town + ", " + Street;
        }

        public override string ToString()
        {
            return Country + " " + State + " " + Town + " " + Street + " " + ZipCode;
        }
    }
}