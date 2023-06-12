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
using Location = TravelAgentTim19.Model.Location;

namespace TravelAgentTim19.View;

public partial class BookTripWindow 
{
    public Trip Trip { get; set; }
    private BookedTrip BookedTrip { get; set; }
    private User LoggedUser { get; set; }
    private List<Location> AttractionsLocations { get; set; }
    
    
    private MainRepository MainRepository;
    
    public BookTripWindow(User user,Trip trip, MainRepository mainRepository)
    {
        Trip = trip;
        BookedTrip = new BookedTrip();
        AttractionsLocations = new List<Location>();
        LoggedUser = user;
        MainRepository = mainRepository;
        GetAttractionsLocation();
        InitializeComponent();
        DataContext = this;

    }
    public void GetAttractionsLocation()
    {
        foreach (Trip trip in MainRepository.TripRepository.GetTrips())
        {
            if (Trip.Id.Equals(trip.Id))
            {
                foreach (Attraction att in trip.Attractions)
                {
                    AttractionsLocations.Add(att.Location);
                }
            }
        }
        
    }
    
    private void EditTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        
        InfoGrid.Visibility = Visibility.Hidden;
        EditGrid.Visibility = Visibility.Visible;
    }
    
    private void InfoTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        InfoGrid.Visibility = Visibility.Visible;
        EditGrid.Visibility = Visibility.Hidden;
    }

    
    private void BookTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        Accomodation accommodation = (Accomodation)AccomodationComboBox.SelectedItem;
        DatePeriods datePeriods = (DatePeriods)DateComboBox.SelectedItem;

        if (accommodation == null || datePeriods == null)
        {
            MessageBox.Show("Izaberite smestaj i datum za ovo putovanje.");
            return;
        }

        NodaTime.Period period = datePeriods.EndDate - datePeriods.StartDate;

        int days = period.Days;
        if (days <= 0)
        {
            MessageBox.Show("Krajnji datum mora biti posle početnog datuma.");
            return;
        }

        double totalPrice = Trip.Price + (accommodation.Price * days);

        MessageBoxResult result = MessageBox.Show("Ukupna cena putovanja je: " + totalPrice + " din.\nDa li ste sigurni da želite da rezervišete ovo putovanje?", "Potvrda", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            Random random = new Random();
            BookedTrip.Id = random.Next();
            BookedTrip.TripId = Trip.Id;
            BookedTrip.TripName = Trip.Name;
            BookedTrip.Status = BookedTripStatus.Reserved;
            BookedTrip.Price = totalPrice;
            BookedTrip.DatePeriod = datePeriods;
            BookedTrip.Accomodation = accommodation;
            BookedTrip.ChoosenAttractions = Trip.Attractions;
            BookedTrip.User = LoggedUser;

            MainRepository.BookedTripRepository.AddBookedTrip(BookedTrip);

            InfoTripBtn_Clicked(sender, e);
        }
    }

    
    private void SaveBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Button saveButton = FindName("SaveEditButton") as Button;
        if (saveButton != null)
        {
            BookTripBtn_Clicked(saveButton, null);
        }
    }
    
    private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Close(); 
    }
    private void Image_MouseUp(object sender, MouseButtonEventArgs e)
    {
        Close();
    }
    private void MapControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            gmap.Zoom = (gmap.Zoom < gmap.MaxZoom) ? gmap.Zoom + 1 : gmap.MaxZoom;
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
        gmap.Position = new PointLatLng(Trip.Attractions[0].Location.Latitude, Trip.Attractions[0].Location.Longitude);
    
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
    
    private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        string helpKey;
        if (InfoGrid.Visibility == Visibility.Visible)
        {
            helpKey = "infoTripUser";
        }
        else if (EditGrid.Visibility == Visibility.Visible)
        {
            helpKey = "bookTripUser";
        }
        else
        {
            helpKey = "indexUser"; // default key
        }

        HelpProvider.ShowHelp(helpKey, this);
    }
}