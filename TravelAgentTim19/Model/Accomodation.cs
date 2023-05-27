using System.Collections.Generic;
using TravelAgentTim19.Model.Enum;

namespace TravelAgentTim19.Model;

public class Accomodation
{
    private int Id { get; set; }
    private double Price{ get; set; }
    private Location Location{ get; set; }
    private string Name{ get; set; }
    private double Rating{ get; set; }
    private AccomodationType AccomodationType { get; set; }
}