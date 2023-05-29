using System.Windows;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View.Edit;

public partial class EditBookedTripWindow : Window
{
    public BookedTrip BookedTrip;
    public MainRepository MainRepository;
    public EditBookedTripWindow(BookedTrip bookedTrip, MainRepository mainRepository)
    {
        BookedTrip = bookedTrip;
        MainRepository = mainRepository;
        InitializeComponent();
    }
}