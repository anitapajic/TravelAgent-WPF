using System.Collections.Generic;
using TravelAgentTim19.Model.Enum;

namespace TravelAgentTim19.Model;

public class BookedTrip
{
    public int Id { get; set; }
    public User User{ get; set; }
    public int TripId{ get; set; }
    public string TripName { get; set; }
    public Accomodation Accomodation{ get; set; }
    public List<Attraction> ChoosenAttractions{ get; set; }
    public DatePeriods DatePeriod{ get; set; }
    public double Price{ get; set; }
    public BookedTripStatus Status{ get; set; }

    public BookedTrip()
    {
    }

    public BookedTrip(int id, User user, int tripId, string tripName, Accomodation accomodation, List<Attraction> choosenAttractions, DatePeriods datePeriods, double price, BookedTripStatus status)
    {
        Id = id;
        User = user;
        TripId = tripId;
        TripName = tripName;
        Accomodation = accomodation;
        ChoosenAttractions = choosenAttractions;
        DatePeriod = datePeriods;
        Price = price;
        Status = status;
    }
    public override string ToString()
    {
        return Id + "\t" + User + "\t" + TripId + "\t" + TripName + "\t" + Accomodation + "\t" + ChoosenAttractions + "\t" + DatePeriod + "\t" + Price + "\t" + Status;
    }
}