using System.Collections.Generic;
using TravelAgentTim19.Model.Enum;

namespace TravelAgentTim19.Model;

public class Accomodation
{
    public int Id { get; set; }
    public double Price{ get; set; }
    public string ImgPath{ get; set; }
    public Location Location{ get; set; }
    public string Name{ get; set; }
    public double Rating{ get; set; }
    public AccomodationType AccomodationType { get; set; }

    public Accomodation()
    {
    }

    public Accomodation(int id, double price,string imgPath, Location location, string name, double rating, AccomodationType accomodationType)
    {
        Id = id;
        Price = price;
        ImgPath = imgPath;
        Location = location;
        Name = name;
        Rating = rating;
        AccomodationType = accomodationType;
    }
    
    public override string ToString()
    {
        return Id + "\t" + Price + "\t" + ImgPath+ "\t" + Location + "\t" + Name + "\t" + Rating + "\t" + AccomodationType;
    }
}