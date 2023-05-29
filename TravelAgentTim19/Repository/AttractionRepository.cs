using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TravelAgentTim19.Model;

namespace TravelAgentTim19.Repository;

public class AttractionRepository
{
    private List<Attraction> attractions;
    
    public AttractionRepository()
    {
        string json = File.ReadAllText(@"..\..\..\Data\Attractions.json");
        List<Attraction> _attractions = JsonConvert.DeserializeObject<List<Attraction>>(json);
        attractions = _attractions;
    }
    public List<Attraction> GetAttractions()
    {
        return this.attractions;
    }

    public void AddAttraction(Attraction attraction)
    {
        this.attractions.Add(attraction);
    }
    
    public Attraction GetAttractionById(int id)
    {
        foreach (Attraction attraction in attractions)
        {
            if (attraction.Id.Equals(id))
            {
                return attraction;
            }
        }
        return null;
    }

    public void DeleteAttraction(Attraction attraction)
    {
        this.attractions.Remove(attraction);
    }

    public void UpdateAttraction(Attraction attraction)
    {
        Attraction toBeDeleted = GetAttractionById(attraction.Id);
        DeleteAttraction(toBeDeleted);
        AddAttraction(attraction);
    }
    
    public void Save()
    {
        File.WriteAllText(@"..\..\..\Data\Attractions.json", 
            JsonConvert.SerializeObject(attractions));
    }
}