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
using HelpSistem;
using TravelAgentTim19.Model;
using TravelAgentTim19.Model.Enum;
using TravelAgentTim19.Repository;
using TravelAgentTim19.View.Edit;
using Location = TravelAgentTim19.Model.Location;

namespace TravelAgentTim19.View;

public partial class AgentMainWindow 
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
    private List<string> TripsNameList { get; set; }
    private List<Location> AttractionsLocations { get; set; }

    public AgentMainWindow(MainRepository mainRepository)
    {
     
        MainRepository = mainRepository;
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
        GetTripNameList();
        InitializeComponent();
        ToggleButtonAttraction.Visibility = Visibility.Hidden;
        ToggleButtonTrip.Visibility = Visibility.Visible;
        ToggleButtonAccomodation.Visibility = Visibility.Hidden;
        ToggleButtonRestourant.Visibility = Visibility.Hidden;
        tripComboBox.ItemsSource = TripsNameList;
        DataContext = this; 
    }

    private void GetBookedTrips()
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
    private void Image_MouseUp(object sender, MouseButtonEventArgs e)
    {
        MainRepository.Save();
        Application.Current.Shutdown();
    }
    
    private List<Trip> GetNumOfMonthlySoldTrips(int month)
    {
        List<BookedTrip> bookedTripsDate = new List<BookedTrip>();
        HashSet<int> uniqueTripIds = new HashSet<int>();
        
        foreach (BookedTrip bookedTrip in PurchasedTrips)
        {
            if (bookedTrip.DatePeriod.StartDate.Month == month)
            {
                bookedTripsDate.Add(bookedTrip);
            }
        }

        foreach (BookedTrip bt in bookedTripsDate)
        {
            int tripId = bt.TripId;
            // Check if the trip ID has already been added
            if (!uniqueTripIds.Contains(tripId))
            {
                uniqueTripIds.Add(tripId);
                SoldTrips.Add(MainRepository.TripRepository.GetTripById(tripId));
            }
        }

        return SoldTrips;
    }

    private void GetTripNameList()
    {
        foreach (Trip trip in MainRepository.TripRepository.GetTrips())
        {
            TripsNameList.Add(trip.Name);
        }

        
    }

    private List<BookedTrip> GetSoldBookedTrips(string tripName)
    {
        foreach (BookedTrip bookedTrip in PurchasedTrips)
        {
            if (bookedTrip.TripName.Equals(tripName))
            {
                SoldBookedTrips.Add(bookedTrip);
            }
        }

        return SoldBookedTrips;
    }
    
    
    private void TripItem_Click(object sender, RoutedEventArgs e)
    {
        MapGrid.Visibility = Visibility.Hidden;
        BookedTripGridForm.Visibility = Visibility.Hidden;
        SoldTripGridForm.Visibility = Visibility.Hidden;
        SoldTripGrid.Visibility = Visibility.Hidden;
        BookedTripsGrid.Visibility = Visibility.Hidden;
        ToggleButtonAttraction.Visibility = Visibility.Hidden;
        ToggleButtonTrip.Visibility = Visibility.Visible;
        ToggleButtonAccomodation.Visibility = Visibility.Hidden;
        ToggleButtonRestourant.Visibility = Visibility.Hidden;
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
        BookedTripGridForm.Visibility = Visibility.Hidden;
        SoldTripGridForm.Visibility = Visibility.Hidden;
        SoldTripGrid.Visibility = Visibility.Hidden;
        BookedTripsGrid.Visibility = Visibility.Hidden;
        ToggleButtonAttraction.Visibility = Visibility.Visible;
        ToggleButtonTrip.Visibility = Visibility.Hidden;
        ToggleButtonAccomodation.Visibility = Visibility.Hidden;
        ToggleButtonRestourant.Visibility = Visibility.Hidden;
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
        BookedTripGridForm.Visibility = Visibility.Hidden;
        SoldTripGridForm.Visibility = Visibility.Hidden;
        SoldTripGrid.Visibility = Visibility.Hidden;
        BookedTripsGrid.Visibility = Visibility.Hidden;
        ToggleButtonAttraction.Visibility = Visibility.Hidden;
        ToggleButtonTrip.Visibility = Visibility.Hidden;
        ToggleButtonAccomodation.Visibility = Visibility.Visible;
        ToggleButtonRestourant.Visibility = Visibility.Hidden;
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
         BookedTripGridForm.Visibility = Visibility.Hidden;
         SoldTripGrid.Visibility = Visibility.Hidden;
         BookedTripsGrid.Visibility = Visibility.Hidden;
        SoldTripGridForm.Visibility = Visibility.Hidden;
        ToggleButtonAttraction.Visibility = Visibility.Hidden;
        ToggleButtonTrip.Visibility = Visibility.Hidden;
        ToggleButtonAccomodation.Visibility = Visibility.Hidden;
        ToggleButtonRestourant.Visibility = Visibility.Visible;
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

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        MainRepository.Save();

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
        AddNewAccomodationWindow addNewAccomodationWindow = new AddNewAccomodationWindow(MainRepository);
        addNewAccomodationWindow.Show();
        addNewAccomodationWindow.Closed += NewAccomodationWindow_Closed;

    }
    private void NewAccomodationWindow_Closed(object sender, EventArgs e)
    {
        AccomodationItemsControl.Items.Refresh();
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
        AddNewTripWindow addNewTripWindow = new AddNewTripWindow(MainRepository);
        addNewTripWindow.Show();
        addNewTripWindow.Closed += NewTripWindow_Closed;
    }
    
    private void NewTripWindow_Closed(object sender, EventArgs e)
    {
        tripItemsControl.Items.Refresh();
    }

    private void EditTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int tripId = (int)editButton.Tag;
        Trip trip = MainRepository.TripRepository.GetTripById(tripId);

        EditTripWindow editTripWindow = new EditTripWindow(trip, MainRepository);
        editTripWindow.Show();
        editTripWindow.Closed += EditTripWindow_Closed;
    }

    private void EditTripWindow_Closed(object sender, EventArgs e)
    {
        tripItemsControl.Items.Refresh();
    }
    private void EditAccommodationpWindow_Closed(object sender, EventArgs e)
    {
        AccomodationItemsControl.Items.Refresh();
    }
    private void EditAttractionWindow_Closed(object sender, EventArgs e)
    {
        attractionItemsControl.Items.Refresh();
    }
    private void EditRestaurantWindow_Closed(object sender, EventArgs e)
    {
        restaurantItemsControl.Items.Refresh();
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

        EditRestaurantWindow editRestaurantWindow = new EditRestaurantWindow(restaurant, MainRepository, true);
        editRestaurantWindow.Show();
        editRestaurantWindow.Closed += EditRestaurantWindow_Closed;
    }
    
    private void DeleteRestaurantBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int restaurantId = (int)editButton.Tag;
        Restaurant restaurant = MainRepository.RestaurantsRepository.GetRestaurantByid(restaurantId);
        MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da obrisete ovaj restoran?", "Potvrda", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            MainRepository.RestaurantsRepository.Delete(restaurant);
            restaurantItemsControl.Items.Refresh();
        }
        
    }
    
    private void EditAccomodationBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int accomodationId = (int)editButton.Tag;
        Accomodation accomodation = MainRepository.AccomodationRepository.GetAccomodationById(accomodationId);

        EditAccomodationWindow editAccomodationWindow= new EditAccomodationWindow(accomodation, MainRepository, true);
        editAccomodationWindow.Show();
        editAccomodationWindow.Closed += EditAccommodationpWindow_Closed;
    }
    
    private void DeleteAccomodationBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int accomodationId = (int)editButton.Tag;
        Accomodation accomodation = MainRepository.AccomodationRepository.GetAccomodationById(accomodationId);
        MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da obrisete ovaj smestaj?", "Potvrda", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            MainRepository.AccomodationRepository.Delete(accomodation);
            AccomodationItemsControl.Items.Refresh();
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
        ToggleButtonTrip.Visibility = Visibility.Hidden;
        MapGrid.Visibility = Visibility.Hidden;
        BookedTripGridForm.Visibility = Visibility.Hidden;
        SoldTripGridForm.Visibility = Visibility.Hidden;
        SoldTripGrid.Visibility = Visibility.Hidden;
        BookedTripsGrid.Visibility = Visibility.Hidden;
        PurchasedTripGrid.Visibility = Visibility.Hidden;
        TripsGrid.Visibility = Visibility.Hidden;
        BookedTripGrid.Visibility = Visibility.Visible;
    }

    private void PurchasedTripItem_Click(object sender, RoutedEventArgs e)
    {        
        ToggleButtonTrip.Visibility = Visibility.Hidden;
        MapGrid.Visibility = Visibility.Hidden; 
        BookedTripGridForm.Visibility = Visibility.Hidden;
        SoldTripGridForm.Visibility = Visibility.Hidden;
        SoldTripGrid.Visibility = Visibility.Hidden;
        BookedTripsGrid.Visibility = Visibility.Hidden;
        BookedTripGrid.Visibility = Visibility.Hidden;
        TripsGrid.Visibility = Visibility.Hidden;
        PurchasedTripGrid.Visibility = Visibility.Visible;
    }
    
    private void EditAttractionBtn_Click(object sender, RoutedEventArgs e)
    {
        Button editButton = (Button)sender;
        int attId = (int)editButton.Tag;
        Attraction attraction = MainRepository.AttractionRepository.GetAttractionById(attId);

        EditAttractionWindow editAttractionWindow = new EditAttractionWindow(attraction, MainRepository, true);
        editAttractionWindow.Show();
        editAttractionWindow.Closed += EditAttractionWindow_Closed;
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

    private void SoldTrips_Click(object sender, RoutedEventArgs e)
    {        
        ToggleButtonTrip.Visibility = Visibility.Hidden;
        MapGrid.Visibility = Visibility.Hidden;
        BookedTripGridForm.Visibility = Visibility.Hidden;
        BookedTripGrid.Visibility = Visibility.Hidden;
        TripsGrid.Visibility = Visibility.Hidden;
        PurchasedTripGrid.Visibility = Visibility.Hidden;
        SoldTripGrid.Visibility = Visibility.Hidden;
        SoldTripGridForm.Visibility = Visibility.Visible;
    }

    private void FindTripsReport1_Clicked(object sender, RoutedEventArgs e)
    {
         int selectedIndex = monthComboBox.SelectedIndex;
         int month = selectedIndex + 1;
         SoldTrips.Clear();
         SoldTrips = GetNumOfMonthlySoldTrips(month);
        if (SoldTrips.Count > 0)
        {
            soldTripItemsControl.Items.Refresh();
            BookedTripGridForm.Visibility = Visibility.Hidden;
            TripsGrid.Visibility = Visibility.Hidden;
            PurchasedTripGrid.Visibility = Visibility.Hidden;
            BookedTripsGrid.Visibility = Visibility.Hidden;
            SoldTripGridForm.Visibility = Visibility.Visible;
        }
        else
        {
            SoldTrips.Clear();
            soldTripItemsControl.Items.Refresh();
            MessageBox.Show("Ne postoji nijedno prodato putovanje u ovom mesecu.");
            
        }

        SoldTripGrid.Visibility = Visibility.Visible;
        SoldTrips.Clear();
    }

    private void FindTripsReport2_Clicked(object sender, RoutedEventArgs e)
    {
        string selectedTrip = tripComboBox.SelectedItem.ToString();
        SoldBookedTrips.Clear();
        SoldBookedTrips = GetSoldBookedTrips(selectedTrip);
        if (SoldBookedTrips.Count > 0)
        {
            soldBookedTripItemsControl.Items.Refresh();
            SoldTripGridForm.Visibility = Visibility.Hidden;
            SoldTripGrid.Visibility = Visibility.Hidden;
            TripsGrid.Visibility = Visibility.Hidden;
            PurchasedTripGrid.Visibility = Visibility.Hidden;
            BookedTripGridForm.Visibility = Visibility.Visible;
        }
        else
        {
            SoldBookedTrips.Clear();
            soldBookedTripItemsControl.Items.Refresh();
            MessageBox.Show("Ne postoji nijedan prodati aranzman za ovo putovanje.");
            
        }

        BookedTripsGrid.Visibility = Visibility.Visible;
        SoldBookedTrips.Clear();
    }

    private void SoldBookedTrips_Click(object sender, RoutedEventArgs e)
    {        
        ToggleButtonTrip.Visibility = Visibility.Hidden;
        MapGrid.Visibility = Visibility.Hidden;
        BookedTripGrid.Visibility = Visibility.Hidden;
        TripsGrid.Visibility = Visibility.Hidden;
        AttractionGrid.Visibility = Visibility.Hidden;
        AccomodationGrid.Visibility = Visibility.Hidden;
        RestaurantsGrid.Visibility = Visibility.Hidden;
        PurchasedTripGrid.Visibility = Visibility.Hidden;
        SoldTripGridForm.Visibility = Visibility.Hidden;
        SoldTripGrid.Visibility = Visibility.Hidden;
        BookedTripGridForm.Visibility = Visibility.Visible;
        BookedTripsGrid.Visibility = Visibility.Hidden;
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
            pinImage.Width = 50; // Adjust as needed
            pinImage.Height = 50; // Adjust as needed
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
    private void GetAttractionsLocation()
    {
        foreach (Attraction att in MainRepository.AttractionRepository.GetAttractions())
        {
            AttractionsLocations.Add(att.Location);
        }
    }
    
    private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        string helpKey;
        if (SoldTripGridForm.Visibility == Visibility.Visible || SoldTripGrid.Visibility==Visibility.Visible)
        {
            helpKey = "report1";
        }
        else if (BookedTripGridForm.Visibility == Visibility.Visible || BookedTripsGrid.Visibility==Visibility.Visible)
        {
            helpKey = "report2";
        }
        else
        {
            helpKey = "index"; // default key
        }

        HelpProvider.ShowHelp(helpKey, this);
    }
    private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape && WindowState == WindowState.Maximized)
        {
            WindowState = WindowState.Normal;
        }
    }
    private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left && !IsMouseOverDraggableComponent(e))
            this.DragMove();
    }

    private bool IsMouseOverDraggableComponent(MouseButtonEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;
        return element != null && (element.Name == "Ximg" || element.Name == "gmap");
    }


    private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        if (TripsGrid.Visibility == Visibility.Visible)
        {
            AddNewTripWindow newTripWindow = new AddNewTripWindow(MainRepository);
            newTripWindow.Show();
            newTripWindow.Closed += NewTripWindow_Closed;
        }
        else if (AttractionGrid.Visibility == Visibility.Visible)
        {
            AddNewAttractionWindow newAttractionWindow = new AddNewAttractionWindow(MainRepository);
            newAttractionWindow.Show();
            newAttractionWindow.Closed += NewAttractionWindow_Closed;
        }
        else if (AccomodationGrid.Visibility == Visibility.Visible)
        {
            AddNewAccomodationWindow newAccomodationWindow = new AddNewAccomodationWindow(MainRepository);
            newAccomodationWindow.Show();
            newAccomodationWindow.Closed += NewAccomodationWindow_Closed;
        }
        else if (RestaurantsGrid.Visibility == Visibility.Visible)
        {
            AddNewRestaurantWindow newRestaurantWindow = new AddNewRestaurantWindow(MainRepository);
            newRestaurantWindow.Show();
            newRestaurantWindow.Closed += NewRestaurantWindow_Closed;
        }
    }
}