using TravelAgentTim19.Model.Enum;

namespace TravelAgentTim19.Model;

public class User
{
    private string Email{ get; set; }
    private string Password{ get; set; }
    private string FirstName{ get; set; }
    private string LastName{ get; set; }
    private Role Role{ get; set; }
}