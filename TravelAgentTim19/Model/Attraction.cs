namespace TravelAgentTim19.Model;

public class Attraction
{
    public int Id { get; set; }
    public string Name{ get; set; }
    public Location Location{ get; set; }
    public double Price{ get; set; }
    public string Description{ get; set; }

    public Attraction(int id, string name, Location location, double price, string description)
    {
        Id = id;
        Name = name;
        Location = location;
        Price = price;
        Description = description;
    }
    public Attraction(){}
    public override string ToString()
    {
        return Id + "\t" + Name + "\t" + Location + "\t" + Price + "\t" + Description;
    }
}