using System.Collections.Generic;
using System.Windows;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View;

public partial class UserMainWindow : Window
{
    private MainRepository MainRepository;
    public List<Trip> Trips { get; set; }
    public UserMainWindow(MainRepository mainRepository)
    {
        MainRepository = mainRepository;
        Trips = MainRepository.TripRepository.GetTrips();
        InitializeComponent();
    }

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private void TripItem_Click(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void EditTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}