using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using TravelAgentTim19.Model;
using TravelAgentTim19.Model.Enum;
using TravelAgentTim19.Repository;
using TravelAgentTim19.View.Edit;
using Location = TravelAgentTim19.Model.Location;

namespace TravelAgentTim19.View;

public partial class UserMainWindow : Window
{
    private MainRepository MainRepository;
    public List<Trip> Trips { get; set; }
    public List<Attraction> Attractions { get; set; }
    public List<Accomodation> Accomodations { get; set; }
    public List<Restaurant> Restaurants { get; set; }
    public List<BookedTrip> BookedTrips { get; set; }
    public List<BookedTrip> PurchasedTrips { get; set; }
    public List<Trip> SoldTrips { get; set; }
    public List<BookedTrip> SoldBookedTrips { get; set; }
    public List<string> TripsNameList { get; set; }
    public List<Location> AttractionsLocations { get; set; }
    public User User { get; set; }
    
    public UserMainWindow(MainRepository mainRepository, User user)
    {
        User = user;
        MainRepository = mainRepository;
        User = user;
        Trips = MainRepository.TripRepository.GetTrips();
        Attractions = MainRepository.AttractionRepository.GetAttractions();
        Accomodations = MainRepository.AccomodationRepository.GetAccomodations();
        Restaurants = MainRepository.RestaurantsRepository.GetRestaurants();
        BookedTrips = new List<BookedTrip>();
        PurchasedTrips = new List<BookedTrip>(); 
        SoldTrips = new List<Trip>();
        SoldBookedTrips = new List<BookedTrip>();
        TripsNameList = new List<string>();
        AttractionsLocations = new List<Location>();
        GetAttractionsLocation();
        
         GetBookedTrips();
         InitializeComponent();

        DataContext = this; 
        
    }

    public void GetBookedTrips()
    {
        foreach (BookedTrip booked in MainRepository.BookedTripRepository.GetBookedTrips())
        {
            if (User.Email.Equals(booked.User.Email))
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
    }

    public void GetAttractionsLocation()
    {
        foreach (Attraction att in MainRepository.AttractionRepository.GetAttractions())
        {
            AttractionsLocations.Add(att.Location);
        }
    }
    private void map_load(object sender, RoutedEventArgs e)
    {
        gmap.Bearing = 0;
        gmap.CanDragMap = true;
        gmap.DragButton = MouseButton.Left;
        gmap.MaxZoom = 18;
        gmap.MinZoom = 2;
        gmap.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;

        gmap.ShowTileGridLines = false;
        gmap.Zoom = 10;
        gmap.ShowCenter = false;
        
        gmap.MapProvider = GMapProviders.GoogleMap;
        GMaps.Instance.Mode = AccessMode.ServerOnly;
        gmap.Position = new PointLatLng(45.2671, 19.8335);

        GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
        GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;
        
        foreach (Location l in AttractionsLocations)
        {
            GMapMarker marker = new GMapMarker(new PointLatLng(l.Latitude, l.Longitude));
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("pack://application:,,,/Images/redPin.png");
            bi.EndInit();
            Image pinImage = new Image();
            pinImage.Source = bi;
            pinImage.Width = 50;  // Adjust as needed
            pinImage.Height = 50;  // Adjust as needed
            pinImage.ToolTip = l.Address + " " + l.City;
            
            ToolTipService.SetShowDuration(pinImage, Int32.MaxValue); 
            ToolTipService.SetInitialShowDelay(pinImage, 0);
            marker.Shape = pinImage;
            gmap.Markers.Add(marker);
        }
        
        
    }
    private void MapControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            gmap.Zoom = (gmap.Zoom < gmap.MaxZoom) ? gmap.Zoom + 1 : gmap.MaxZoom;
        }
    }

    
    private void TripItem_Click(object sender, RoutedEventArgs e)
    {
        MapGrid.Visibility = Visibility.Hidden;
        PurchasedTripGrid.Visibility = Visibility.Hidden;
        BookedTripGrid.Visibility = Visibility.Hidden;
        AccomodationGrid.Visibility = Visibility.Hidden;
        RestaurantsGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Hidden;
        MapGrid.Visibility = Visibility.Hidden;
        TripsGrid.Visibility = Visibility.Visible;
        TripsGridTopManu.Visibility = Visibility.Visible;
        TripsGridTitle.Visibility = Visibility.Visible;
        RestourantGridTitle.Visibility = Visibility.Hidden;
        AttractionGridTitle.Visibility = Visibility.Hidden;
        AccomodationGridTitle.Visibility = Visibility.Hidden;

    }
    
    private void AttractionItem_Click(object sender, RoutedEventArgs e)
    {

        PurchasedTripGrid.Visibility = Visibility.Hidden;
        BookedTripGrid.Visibility = Visibility.Hidden;
        TripsGrid.Visibility = Visibility.Hidden;
        AccomodationGrid.Visibility = Visibility.Hidden;
        RestaurantsGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Visible;
        MapGrid.Visibility = Visibility.Visible;
        TripsGrid.Visibility = Visibility.Hidden;
        TripsGridTopManu.Visibility = Visibility.Hidden;
        TripsGridTitle.Visibility = Visibility.Hidden;
        RestourantGridTitle.Visibility = Visibility.Hidden;
        AccomodationGridTitle.Visibility = Visibility.Hidden;
        AttractionGridTitle.Visibility = Visibility.Visible;
    }
    
    private void AccomodationItem_Click(object sender, RoutedEventArgs e)
    {
        MapGrid.Visibility = Visibility.Hidden;
        PurchasedTripGrid.Visibility = Visibility.Hidden;
        BookedTripGrid.Visibility = Visibility.Hidden;
        RestaurantsGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Hidden;
        AccomodationGrid.Visibility = Visibility.Visible;
        TripsGrid.Visibility = Visibility.Hidden;
        TripsGridTopManu.Visibility = Visibility.Hidden;
        TripsGridTitle.Visibility = Visibility.Hidden;
        TripsGridTitle.Visibility = Visibility.Hidden;
        RestourantGridTitle.Visibility = Visibility.Hidden;
        AttractionGridTitle.Visibility = Visibility.Hidden;
        AccomodationGridTitle.Visibility = Visibility.Visible;
    }
    private void RestaurantItem_Click(object sender, RoutedEventArgs e)
    {
        MapGrid.Visibility = Visibility.Hidden;
        PurchasedTripGrid.Visibility = Visibility.Hidden;
        BookedTripGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Hidden;
        AccomodationGrid.Visibility = Visibility.Hidden;
        RestaurantsGrid.Visibility = Visibility.Visible;
        TripsGrid.Visibility = Visibility.Hidden;
        TripsGridTopManu.Visibility = Visibility.Hidden;
        TripsGridTitle.Visibility = Visibility.Hidden;
        AttractionGridTitle.Visibility = Visibility.Hidden;
        AccomodationGridTitle.Visibility = Visibility.Hidden;
        RestourantGridTitle.Visibility = Visibility.Visible;
    }
    
    private void BookedTripItem_Click(object sender, RoutedEventArgs e)
    {
        MapGrid.Visibility = Visibility.Hidden;
        PurchasedTripGrid.Visibility = Visibility.Hidden;
        TripsGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Hidden;
        AccomodationGrid.Visibility = Visibility.Hidden;
        RestaurantsGrid.Visibility = Visibility.Hidden;
        BookedTripGrid.Visibility = Visibility.Visible;
    }

    private void PurchasedTripItem_Click(object sender, RoutedEventArgs e)
    {
        MapGrid.Visibility = Visibility.Hidden;
        BookedTripGrid.Visibility = Visibility.Hidden;
        TripsGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Hidden;
        AccomodationGrid.Visibility = Visibility.Hidden;
        RestaurantsGrid.Visibility = Visibility.Hidden;
        PurchasedTripGrid.Visibility = Visibility.Visible;
    }
    
    private void BookTripWindow_Closed(object sender, EventArgs e)
    {
        BookedTrips.Clear();
        PurchasedTrips.Clear();
        GetBookedTrips();
        bookedTripItemsControl.Items.Refresh();
        purchasedTripItemsControl.Items.Refresh();
    }

    private void EditRestaurantBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int restaurantId = (int)editButton.Tag;
        Restaurant restaurant = MainRepository.RestaurantsRepository.GetRestaurantByid(restaurantId);

        EditRestaurantWindow editRestaurantWindow = new EditRestaurantWindow(restaurant, MainRepository);
        editRestaurantWindow.Show();
    }
    private void EditAccomodationBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int accomodationId = (int)editButton.Tag;
        Accomodation accomodation = MainRepository.AccomodationRepository.GetAccomodationById(accomodationId);

        EditAccomodationWindow editAccomodationWindow= new EditAccomodationWindow(accomodation, MainRepository);
        editAccomodationWindow.Show();
    }
    private void EditAttractionBtn_Click(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int attId = (int)editButton.Tag;
        Attraction attraction = MainRepository.AttractionRepository.GetAttractionById(attId);

        EditAttractionWindow editAttractionWindow = new EditAttractionWindow(attraction, MainRepository);
        editAttractionWindow.Show();
    }
    
    private void EditBookedTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int attId = (int)editButton.Tag;
        BookedTrip bookedTrip = MainRepository.BookedTripRepository.GetBookedTripById(attId);

        EditBookedTripWindow editBookedTripWindow = new EditBookedTripWindow(bookedTrip, MainRepository);
        editBookedTripWindow.Show();
    }
    
    private void PurchaseTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Da li zelite da platite ovo putovanje?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            Button button = (Button)sender;
            int id = (int)button.Tag;
            BookedTrip bookedTrip = MainRepository.BookedTripRepository.GetBookedTripById(id);
            MainRepository.BookedTripRepository.DeleteBookedTrip(bookedTrip);
            bookedTrip.Status = BookedTripStatus.Purchased;
            MainRepository.BookedTripRepository.AddBookedTrip(bookedTrip);
            BookedTrips.Clear();
            PurchasedTrips.Clear();
            GetBookedTrips();
            bookedTripItemsControl.Items.Refresh();
        }
    }

    private void EditPurchasedTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int attId = (int)editButton.Tag;
        BookedTrip bookedTrip = MainRepository.BookedTripRepository.GetBookedTripById(attId);

        EditPurchasedTripWindow editPurchasedTripWindow = new EditPurchasedTripWindow(bookedTrip, MainRepository);
        editPurchasedTripWindow.Show();
    }
    
    private void EditTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int tripId = (int)editButton.Tag;
        Trip trip = MainRepository.TripRepository.GetTripById(tripId);

        BookTripWindow bookTripWindow = new BookTripWindow(User, trip, MainRepository);
        bookTripWindow.Show();
        bookTripWindow.Closed += BookTripWindow_Closed;

    }

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        MainRepository.Save();
        Close();
    }
    
    private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape && WindowState == WindowState.Maximized)
        {
            WindowState = WindowState.Normal;
        }
    }

    private void Image_MouseUp(object sender, MouseButtonEventArgs e)
    {
        MainRepository.Save();
        Application.Current.Shutdown();
    }
    private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
            this.DragMove();
    }
}