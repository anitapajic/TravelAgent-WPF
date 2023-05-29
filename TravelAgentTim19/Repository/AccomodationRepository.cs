using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TravelAgentTim19.Model;

namespace TravelAgentTim19.Repository;

public class AccomodationRepository
{
    private List<Accomodation> accomodations;
    
    public AccomodationRepository()
    {
        string json = File.ReadAllText(@"..\..\..\Data\Accomodations.json");
        List<Accomodation> _accomodations = JsonConvert.DeserializeObject<List<Accomodation>>(json);
        accomodations = _accomodations;
    }
    public List<Accomodation> GetAccomodations()
    {
        return this.accomodations;
    }

    public void AddAccomodation(Accomodation accomodation)
    {
        this.accomodations.Add(accomodation);
    }
    
    public Accomodation GetAccomodationById(int id)
    {
        foreach (Accomodation accomodation in accomodations)
        {
            if (accomodation.Id.Equals(id))
            {
                return accomodation;
            }
        }
        return null;
    }
    
    public void UpdateAccomodation(Accomodation accomodation)
    {
        Accomodation toBeDeleted = GetAccomodationById(accomodation.Id);
        Delete(toBeDeleted);
        AddAccomodation(accomodation);
    }

    public bool Delete(Accomodation accomodation)
    {
        return accomodations.Remove(accomodation);
    }
    public void Save()
    {
        File.WriteAllText(@"..\..\..\Data\Accomodations.json", 
            JsonConvert.SerializeObject(accomodations));
    }
}