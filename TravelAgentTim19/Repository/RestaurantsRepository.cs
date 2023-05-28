using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TravelAgentTim19.Model;

namespace TravelAgentTim19.Repository;

public class RestaurantsRepository
{
    private List<Restaurant> restaurants;
    
    public RestaurantsRepository()
    {
        string json = File.ReadAllText(@"..\..\..\Data\Restaurants.json");
        List<Restaurant> _restaurants = JsonConvert.DeserializeObject<List<Restaurant>>(json);
        restaurants = _restaurants;
    }
    public List<Restaurant> GetRestaurants()
    {
        return this.restaurants;
    }

    public void AddRestaurant(Restaurant restaurant)
    {
        this.restaurants.Add(restaurant);
    }
    
    public Restaurant GetRestaurantByid(int id)
    {
        foreach (Restaurant restaurant in restaurants)
        {
            if (restaurant.Id.Equals(id))
            {
                return restaurant;
            }
        }
        return null;
    }
    
    public void Save()
    {
        File.WriteAllText(@"..\..\..\Data\Restaurants.json", 
            JsonConvert.SerializeObject(restaurants));
    }
}