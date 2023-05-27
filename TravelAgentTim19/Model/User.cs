using TravelAgentTim19.Model.Enum;

namespace TravelAgentTim19.Model;

public class User
{
    public int Id { get; set; }
    public string Email{ get; set; }
    public string Password{ get; set; }
    public string FirstName{ get; set; }
    public string LastName{ get; set; }
    public Role Role{ get; set; }
    
    
}