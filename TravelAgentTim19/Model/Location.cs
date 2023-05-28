namespace TravelAgentTim19.Model;

public class Location
{
    public string City { get; set; }
    public string Address{ get; set; }
    public double Latitude{ get; set; }
    public double Longitude{ get; set; }

    public Location(string city, string address, double latitude, double longitude)
    {
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    public Location()
    {
    }
    public override string ToString()
    {
        return City + "\t" + Address + "\t" + Latitude + "\t" + Longitude;
    }
}