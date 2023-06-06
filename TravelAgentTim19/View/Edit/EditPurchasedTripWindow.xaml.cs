using System.Windows;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View.Edit;

public partial class EditPurchasedTripWindow : Window
{
    public BookedTrip BookedTrip;
    public MainRepository MainRepository;
    public EditPurchasedTripWindow(BookedTrip bookedTrip, MainRepository mainRepository)
    {
        BookedTrip = bookedTrip;
        MainRepository = mainRepository;
        InitializeComponent();
    }
}