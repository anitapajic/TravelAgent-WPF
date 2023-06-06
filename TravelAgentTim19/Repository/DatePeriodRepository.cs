using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TravelAgentTim19.Model;

namespace TravelAgentTim19.Repository;

public class DatePeriodRepository
{
    private List<DatePeriods> datePeriods;
    
    public DatePeriodRepository()
    {
        string json = File.ReadAllText(@"..\..\..\Data\DatePeriods.json");
        List<DatePeriods> _datePeriods = JsonConvert.DeserializeObject<List<DatePeriods>>(json);
        datePeriods = _datePeriods;
    }
    public List<DatePeriods> GetDatePeriods()
    {
        return this.datePeriods;
    }

    public void AddDatePeriod(DatePeriods datePeriod)
    {
        this.datePeriods.Add(datePeriod);
    }
    
    public DatePeriods GetDatePeriodById(int id)
    {
        foreach (DatePeriods datePeriod in datePeriods)
        {
            if (datePeriod.Id.Equals(id))
            {
                return datePeriod;
            }
        }
        return null;
    }
    
    public void Save()
    {
        File.WriteAllText(@"..\..\..\Data\DatePeriods.json", 
            JsonConvert.SerializeObject(datePeriods));
    }
}