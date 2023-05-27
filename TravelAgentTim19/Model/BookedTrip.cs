using System.Collections.Generic;
using TravelAgentTim19.Model.Enum;

namespace TravelAgentTim19.Model;

public class BookedTrip
{
    private int Id { get; set; }
    private User User{ get; set; }
    private Trip Trip{ get; set; }
    private Accomodation Accomodation{ get; set; }
    private List<Attraction> ChoosenAttractions{ get; set; }
    private DatePeriods DatePeriods{ get; set; }
    private double Price{ get; set; }
    private BookedTripStatus Status{ get; set; }
}