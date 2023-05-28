using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View;

public partial class AgentMainWindow : Window
{
    private MainRepository MainRepository;
    public List<Trip> Trips { get; set; }

    public AgentMainWindow(MainRepository mainRepository)
    {
        MainRepository = mainRepository;
        Trips = mainRepository.TripRepository.GetTrips();
        InitializeComponent();
        DataContext = this; 
    }

    private void PutovanjaCRUD_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }

    private void PutovanjaCRUD_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        //TODO: prikazi sva putovanja
    }

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }
}