using System.Windows;
using TravelAgentTim19.Model;

namespace TravelAgentTim19.View.Edit;

public partial class EditTripWindow : Window
{
    private Trip Trip;
    public EditTripWindow(Trip trip)
    {
        Trip = trip;
        InitializeComponent();
    }
}