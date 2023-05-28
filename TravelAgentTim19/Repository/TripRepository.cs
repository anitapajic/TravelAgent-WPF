using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TravelAgentTim19.Model;

namespace TravelAgentTim19.Repository;

public class TripRepository
{
    private List<Trip> trips;
    
    public TripRepository()
    {
        string json = File.ReadAllText(@"..\..\..\Data\Trips.json");
        List<Trip> _trips = JsonConvert.DeserializeObject<List<Trip>>(json);
        trips = _trips;
    }
    public List<Trip> GetTrips()
    {
        return this.trips;
    }

    public void AddTrip(Trip trip)
    {
        this.trips.Add(trip);
    }
    
    public Trip GetTripById(int id)
    {
        foreach (Trip trip in trips)
        {
            if (trip.Id.Equals(id))
            {
                return trip;
            }
        }
        return null;
    }
    
    public void Save()
    {
        File.WriteAllText(@"..\..\..\Data\Trips.json", 
            JsonConvert.SerializeObject(trips));
    }
}