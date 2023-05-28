namespace TravelAgentTim19.Model;

public class Restaurant
{
    public int Id { get; set; }
    public Location Location{ get; set; }
    public string Name{ get; set; }
    public double Rating{ get; set; }

    public Restaurant()
    {
    }

    public Restaurant(int id, Location location, string name, double rating)
    {
        Id = id;
        Location = location;
        Name = name;
        Rating = rating;
    }
    public override string ToString()
    {
        return Id + "\t" + Location + "\t" + Name + "\t" + Rating;
    }
}