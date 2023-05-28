using System;
using NodaTime;

namespace TravelAgentTim19.Model;

public class DatePeriods
{
    public int Id { get; set; }
    public LocalDate StartDate{ get; set; }
    public LocalDate EndDate{ get; set; }

    public DatePeriods()
    {
    }

    public DatePeriods(int id, LocalDate startDate, LocalDate endDate)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
    }
    public override string ToString()
    {
        return Id + "\t" + StartDate + "\t" + EndDate;
    }
}

