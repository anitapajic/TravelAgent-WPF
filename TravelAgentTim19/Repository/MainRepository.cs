namespace TravelAgentTim19.Repository;

public class MainRepository
{
    public UserRepository UserRepository { get; set; }

    public MainRepository()
    {
        UserRepository = new UserRepository();
    }

    public void Save()
    {
        UserRepository.Save();
    }
}