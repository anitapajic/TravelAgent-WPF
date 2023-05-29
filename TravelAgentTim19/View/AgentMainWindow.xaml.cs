using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using TravelAgentTim19.Model;
using TravelAgentTim19.Model.Enum;
using TravelAgentTim19.Repository;
using TravelAgentTim19.View.Edit;

namespace TravelAgentTim19.View;

public partial class AgentMainWindow : Window
{
    private MainRepository MainRepository;
    public List<Trip> Trips { get; set; }
    public List<Attraction> Attractions { get; set; }
    public List<Accomodation> Accomodations { get; set; }
    public List<Restaurant> Restaurants { get; set; }
    public List<BookedTrip> BookedTrips { get; set; }
    public List<BookedTrip> PurchasedTrips { get; set; }

    public AgentMainWindow(MainRepository mainRepository)
    {
        MainRepository = mainRepository;
        Trips = MainRepository.TripRepository.GetTrips();
        Attractions = MainRepository.AttractionRepository.GetAttractions();
        Accomodations = MainRepository.AccomodationRepository.GetAccomodations();
        Restaurants = MainRepository.RestaurantsRepository.GetRestaurants();
        BookedTrips = new List<BookedTrip>();
        PurchasedTrips = new List<BookedTrip>();
        GetBookedTrips();
        InitializeComponent();
        DataContext = this; 
    }

    public void GetBookedTrips()
    {
        foreach (BookedTrip booked in MainRepository.BookedTripRepository.GetBookedTrips())
        {
            if (booked.Status.Equals(BookedTripStatus.Reserved))
            {
                BookedTrips.Add(booked);
            }
            else
            {
                PurchasedTrips.Add(booked);
            }
        }
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
        PurchasedTripGrid.Visibility = Visibility.Hidden;
        BookedTripGrid.Visibility = Visibility.Hidden;
        AccomodationGrid.Visibility = Visibility.Hidden;
        RestaurantsGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Hidden;
        TripsGrid.Visibility = Visibility.Visible;
    }
    private void AttractionItem_Click(object sender, RoutedEventArgs e)
    {
        PurchasedTripGrid.Visibility = Visibility.Hidden;
        BookedTripGrid.Visibility = Visibility.Hidden;
        TripsGrid.Visibility = Visibility.Hidden;
        AccomodationGrid.Visibility = Visibility.Hidden;
        RestaurantsGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Visible;
    }
    private void AccomodationItem_Click(object sender, RoutedEventArgs e)
    {
        PurchasedTripGrid.Visibility = Visibility.Hidden;
        BookedTripGrid.Visibility = Visibility.Hidden;
        TripsGrid.Visibility = Visibility.Hidden;
        RestaurantsGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Hidden;
        AccomodationGrid.Visibility = Visibility.Visible;
    }
    private void RestaurantItem_Click(object sender, RoutedEventArgs e)
    {
        PurchasedTripGrid.Visibility = Visibility.Hidden;
        BookedTripGrid.Visibility = Visibility.Hidden;
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
        AddNewRestaurantWindow addNewRestaurantWindow = new AddNewRestaurantWindow(MainRepository);
        addNewRestaurantWindow.Show();
        addNewRestaurantWindow.Closed += NewRestaurantWindow_Closed;
        
    }
    private void NewRestaurantWindow_Closed(object sender, EventArgs e)
    {
        restaurantItemsControl.Items.Refresh();
    }
    private void ToggleButtonAccomodation_Click(object sender, RoutedEventArgs e)
    {
        AddNewAccomodationWindow addNewAccomodationWindow = new AddNewAccomodationWindow();
        addNewAccomodationWindow.Show();
    }
    private void ToggleButtonAttraction_Click(object sender, RoutedEventArgs e)
    {
        AddNewAttractionWindow addNewAttractionWindow = new AddNewAttractionWindow(MainRepository);
        addNewAttractionWindow.Show();
        addNewAttractionWindow.Closed += NewAttractionWindow_Closed;
        
    }
    private void NewAttractionWindow_Closed(object sender, EventArgs e)
    {
        attractionItemsControl.Items.Refresh();
    }
    private void ToggleButtonTrip_Click(object sender, RoutedEventArgs e)
    {
        AddNewTripWindow addNewTripWindow = new AddNewTripWindow();
        addNewTripWindow.Show();
    }

    private void EditTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int tripId = (int)editButton.Tag;
        Trip trip = MainRepository.TripRepository.GetTripById(tripId);

        EditTripWindow editTripWindow = new EditTripWindow(trip);
        editTripWindow.Show();
    }
    
    private void DeleteTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int tripId = (int)editButton.Tag;
        Trip trip = MainRepository.TripRepository.GetTripById(tripId);
        MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da obrisete ovo putovanje?", "Potvrda", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            MainRepository.TripRepository.Delete(trip);
            tripItemsControl.Items.Refresh();
        }

    }
    private void EditRestaurantBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int restaurantId = (int)editButton.Tag;
        Restaurant restaurant = MainRepository.RestaurantsRepository.GetRestaurantByid(restaurantId);

        EditRestaurantWindow editRestaurantWindow = new EditRestaurantWindow(restaurant, MainRepository);
        editRestaurantWindow.Show();
    }
    
    private void DeleteRestaurantBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int restaurantId = (int)editButton.Tag;
        Restaurant restaurant = MainRepository.RestaurantsRepository.GetRestaurantByid(restaurantId);
        //Potvrdi da li zelis da obrises
        MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da obrisete ovaj restoran?", "Potvrda", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            MainRepository.RestaurantsRepository.Delete(restaurant);
            restaurantItemsControl.Items.Refresh();
        }
        
    }
    
    private void DeleteAttractionBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int attId = (int)editButton.Tag;
        Attraction att = MainRepository.AttractionRepository.GetAttractionById(attId);
        MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da obrisete ovu atrakciju?", "Potvrda", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            MainRepository.AttractionRepository.DeleteAttraction(att);
            attractionItemsControl.Items.Refresh();
        }

    }


    private void BookedTripItem_Click(object sender, RoutedEventArgs e)
    {
        PurchasedTripGrid.Visibility = Visibility.Hidden;
        TripsGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Hidden;
        AccomodationGrid.Visibility = Visibility.Hidden;
        RestaurantsGrid.Visibility = Visibility.Hidden;
        BookedTripGrid.Visibility = Visibility.Visible;
    }

    private void PurchasedTripItem_Click(object sender, RoutedEventArgs e)
    {
        BookedTripGrid.Visibility = Visibility.Hidden;
        TripsGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Hidden;
        AccomodationGrid.Visibility = Visibility.Hidden;
        RestaurantsGrid.Visibility = Visibility.Hidden;
        PurchasedTripGrid.Visibility = Visibility.Visible;
    }

    private void EditAttractionBtn_Click(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int attId = (int)editButton.Tag;
        Attraction attraction = MainRepository.AttractionRepository.GetAttractionById(attId);

        EditAttractionWindow editAttractionWindow = new EditAttractionWindow(attraction, MainRepository);
        editAttractionWindow.Show();
    }

    private void EditAccomodationBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int attId = (int)editButton.Tag;
        Accomodation accomodation = MainRepository.AccomodationRepository.GetAccomodationById(attId);

        EditAccomodationWindow editAccomodationWindow = new EditAccomodationWindow(accomodation, MainRepository);
        editAccomodationWindow.Show();
    }

    private void DeleteAccomodationBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int attId = (int)editButton.Tag;
        Accomodation att = MainRepository.AccomodationRepository.GetAccomodationById(attId);
        MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da obrisete ovaj smestaj?", "Potvrda", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            MainRepository.AccomodationRepository.DeleteAccomodation(att);
            attractionItemsControl.Items.Refresh();
        }
    }

    private void EditBookedTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int attId = (int)editButton.Tag;
        BookedTrip bookedTrip = MainRepository.BookedTripRepository.GetBookedTripById(attId);

        EditBookedTripWindow editBookedTripWindow = new EditBookedTripWindow(bookedTrip, MainRepository);
        editBookedTripWindow.Show();
    }

    private void EditPurchasedTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int attId = (int)editButton.Tag;
        BookedTrip bookedTrip = MainRepository.BookedTripRepository.GetBookedTripById(attId);

        EditPurchasedTripWindow editPurchasedTripWindow = new EditPurchasedTripWindow(bookedTrip, MainRepository);
        editPurchasedTripWindow.Show();
    }
}