namespace TravelAgentTim19.Model;

public class Restaurant
{
    public int Id { get; set; }
    public string ImgPath{ get; set; }
    public Location Location{ get; set; }
    public string Name{ get; set; }
    public double Rating{ get; set; }

    public Restaurant()
    {
    }

    public Restaurant(int id,string imgPath, Location location, string name, double rating)
    {
        Id = id;
        ImgPath = imgPath;
        Location = location;
        Name = name;
        Rating = rating;
    }
    public override string ToString()
    {
        return Id + "\t" + Location + "\t" + Name + "\t" + Rating;
    }
}