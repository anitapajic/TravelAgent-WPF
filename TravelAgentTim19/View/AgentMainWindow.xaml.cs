using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View;

public partial class AgentMainWindow : Window
{
    private MainRepository MainRepository;
    public List<Trip> Trips { get; set; }
    public List<Attraction> Attractions { get; set; }
    public List<Accomodation> Accomodations { get; set; }
    public List<Restaurant> Restaurants { get; set; }

    public AgentMainWindow(MainRepository mainRepository)
    {
        MainRepository = mainRepository;
        Trips = mainRepository.TripRepository.GetTrips();
        Attractions = mainRepository.AttractionRepository.GetAttractions();
        Accomodations = mainRepository.AccomodationRepository.GetAccomodations();
        Restaurants = mainRepository.RestaurantsRepository.GetRestaurants();
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
    private void TripItem_Click(object sender, RoutedEventArgs e)
    {
        AccomodationGrid.Visibility = Visibility.Hidden;
        RestaurantsGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Hidden;
        TripsGrid.Visibility = Visibility.Visible;
    }
    private void AttractionItem_Click(object sender, RoutedEventArgs e)
    {
        TripsGrid.Visibility = Visibility.Hidden;
        AccomodationGrid.Visibility = Visibility.Hidden;
        RestaurantsGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Visible;
    }
    private void AccomodationItem_Click(object sender, RoutedEventArgs e)
    {
        TripsGrid.Visibility = Visibility.Hidden;
        RestaurantsGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Hidden;
        AccomodationGrid.Visibility = Visibility.Visible;
    }
    private void RestaurantItem_Click(object sender, RoutedEventArgs e)
    {
        TripsGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Hidden;
        AccomodationGrid.Visibility = Visibility.Hidden;
        RestaurantsGrid.Visibility = Visibility.Visible;
    }

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private void ToggleButtonRestaurant_Click(object sender, RoutedEventArgs e)
    {
        AddNewRestaurantWindow addNewRestaurantWindow = new AddNewRestaurantWindow();
        addNewRestaurantWindow.Show();
    }
    private void ToggleButtonAccomodation_Click(object sender, RoutedEventArgs e)
    {
        AddNewAccomodationWindow addNewAccomodationWindow = new AddNewAccomodationWindow();
        addNewAccomodationWindow.Show();
    }
    private void ToggleButtonAttraction_Click(object sender, RoutedEventArgs e)
    {
        AddNewAttractionWindow addNewAttractionWindow = new AddNewAttractionWindow();
        addNewAttractionWindow.Show();
    }
    private void ToggleButtonTrip_Click(object sender, RoutedEventArgs e)
    {
        AddNewTripWindow addNewTripWindow = new AddNewTripWindow();
        addNewTripWindow.Show();
    }
}