using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TravelAgentTim19.Model;
using TravelAgentTim19.Model.Enum;

namespace TravelAgentTim19.Repository;

public class BookedTripRepository
{
    private List<BookedTrip> bookedTrips;
    public List<BookedTrip> purchasedTrips;
    
    public BookedTripRepository()
    {
        purchasedTrips = new List<BookedTrip>();
        string json = File.ReadAllText(@"..\..\..\Data\BookedTrips.json");
        List<BookedTrip> _trips = JsonConvert.DeserializeObject<List<BookedTrip>>(json);
        bookedTrips = _trips;
        purchasedTrips = GetPurchasedTrips();
    }
    public List<BookedTrip> GetBookedTrips()
    {
        return this.bookedTrips;
    }

    public List<BookedTrip> GetPurchasedTrips()
    {
        foreach (BookedTrip bookedTrip in bookedTrips)
        {
            if (bookedTrip.Status == BookedTripStatus.Purchased)
            {
                purchasedTrips.Add(bookedTrip);
            }
        }

        return purchasedTrips;
    }
    

    public void AddBookedTrip(BookedTrip trip)
    {
        this.bookedTrips.Add(trip);
    }
    public void UpdateBookedTrip(BookedTrip bookedTrip)
    {
        BookedTrip toBeDeleted = GetBookedTripById(bookedTrip.Id);
        DeleteBookedTrip(toBeDeleted);
        AddBookedTrip(bookedTrip);
    }

    public void DeleteBookedTrip(BookedTrip bookedTrip)
    {
        bookedTrips.Remove(bookedTrip);
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