using System;
using System.Collections.Generic;

namespace TravelAgentTim19.Model;

public class Trip
{
    private int Id { get; set; }
    private string Name { get; set; }
    private double Price{ get; set; }
    private List<Accomodation> Accomodations{ get; set; }
    private List<Attraction> Attractions{ get; set; }
    private List<Restaurant> Restaurants{ get; set; }
    private List<DatePeriods> DatePeriods{ get; set; }
    private string Description{ get; set; }


}