namespace TravelAgentTim19.Repository;

public class MainRepository
{
    public UserRepository UserRepository { get; set; }
    public AccomodationRepository AccomodationRepository { get; set; }
    public AttractionRepository AttractionRepository { get; set; }
    public DatePeriodRepository DatePeriodRepository { get; set; }
    public RestaurantsRepository RestaurantsRepository { get; set; }
    public TripRepository TripRepository { get; set; }
    public BookedTripRepository BookedTripRepository { get; set; }

    public MainRepository()
    {
        UserRepository = new UserRepository();
        AccomodationRepository = new AccomodationRepository();
        AttractionRepository = new AttractionRepository();
        DatePeriodRepository = new DatePeriodRepository();
        RestaurantsRepository = new RestaurantsRepository();
        TripRepository = new TripRepository();
        BookedTripRepository = new BookedTripRepository();
    }

    public void Save()
    {
        UserRepository.Save();
        AccomodationRepository.Save();
        AttractionRepository.Save();
        DatePeriodRepository.Save();
        RestaurantsRepository.Save();
        TripRepository.Save();
        BookedTripRepository.Save();
    }
}