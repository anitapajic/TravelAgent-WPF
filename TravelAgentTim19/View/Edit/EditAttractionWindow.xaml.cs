using System.Windows;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View.Edit;

public partial class EditAttractionWindow : Window
{
    public Attraction Attraction { get; set; }
    public MainRepository MainRepository { get; set; }
    
    public EditAttractionWindow(Attraction attraction, MainRepository mainRepository)
    {
        Attraction = attraction;
        MainRepository = mainRepository;
        InitializeComponent();
    }
}