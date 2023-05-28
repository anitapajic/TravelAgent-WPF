using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TravelAgentTim19.Model;

namespace TravelAgentTim19.Repository;

public class BookedTripRepository
{
    private List<BookedTrip> bookedTrips;
    
    public BookedTripRepository()
    {
        string json = File.ReadAllText(@"..\..\..\Data\BookedTrips.json");
        List<BookedTrip> _trips = JsonConvert.DeserializeObject<List<BookedTrip>>(json);
        bookedTrips = _trips;
    }
    public List<BookedTrip> GetBookedTrips()
    {
        return this.bookedTrips;
    }

    public void AddBookedTrip(BookedTrip trip)
    {
        this.bookedTrips.Add(trip);
    }
    
    public BookedTrip GetBookedTripById(int id)
    {
        foreach (BookedTrip trip in bookedTrips)
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
        File.WriteAllText(@"..\..\..\Data\BookedTrips.json", 
            JsonConvert.SerializeObject(bookedTrips));
    }
}