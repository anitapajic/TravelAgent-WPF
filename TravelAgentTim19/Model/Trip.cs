using System;
using System.Collections.Generic;

namespace TravelAgentTim19.Model;

public class  Trip
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price{ get; set; }
    public List<Accomodation> Accomodations{ get; set; }
    public List<Attraction> Attractions{ get; set; }
    public List<Restaurant> Restaurants{ get; set; }
    public List<DatePeriods> DatePeriods{ get; set; }
    public string Description{ get; set; }

    public Trip()
    {
    }

    // public Trip(Trip trip)
    // {
    //    new Trip(trip.Id, trip.Name, trip.Price, trip.Accomodations, trip.Attractions, trip.Restaurants, trip.DatePeriods,
    //         trip.Description);
    // }

    public Trip(int id, string name, double price, List<Accomodation> accomodations, List<Attraction> attractions, List<Restaurant> restaurants, List<DatePeriods> datePeriods, string description)
    {
        Id = id;
        Name = name;
        Price = price;
        Accomodations = accomodations;
        Attractions = attractions;
        Restaurants = restaurants;
        DatePeriods = datePeriods;
        Description = description;
    }
    public override string ToString()
    {
        return Id + "\t" + Name + "\t" + Price + "\t" + Accomodations + "\t" + Attractions + "\t" + Restaurants + "\t" + DatePeriods + "\t" + Description;
    }
}