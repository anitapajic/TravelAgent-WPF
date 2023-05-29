using System.Windows;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View.Edit;

public partial class EditAccomodationWindow : Window
{
    public Accomodation Accomodation { get; set; }
    public MainRepository MainRepository { get; set; }
    public EditAccomodationWindow(Accomodation accomodation, MainRepository mainRepository)
    {
        Accomodation = accomodation;
        MainRepository = mainRepository;
        InitializeComponent();
    }
}