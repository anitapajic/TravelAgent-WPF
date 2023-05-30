using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;
using Geometry = System.Windows.Media.Geometry;
using Location = TravelAgentTim19.Model.Location;

namespace TravelAgentTim19.View;

public partial class UserMainWindow : Window
{
    private MainRepository MainRepository { get; set; }
    public List<Trip> Trips { get; set; }
    public List<Location> attractionsLocations { get; set; }
    public UserMainWindow(MainRepository mainRepository)
    {
        MainRepository = mainRepository;
        Trips = MainRepository.TripRepository.GetTrips();
        attractionsLocations = new List<Location>();
        GetAttractionsLocation();
        InitializeComponent();
        
    }

    public void GetAttractionsLocation()
    {
        foreach (Attraction att in MainRepository.AttractionRepository.GetAttractions())
        {
            attractionsLocations.Add(att.Location);
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
        
        foreach (Location l in attractionsLocations)
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

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private void TripItem_Click(object sender, RoutedEventArgs e)
    {
        MapGrid.Visibility = Visibility.Hidden;
        TripsGrid.Visibility = Visibility.Visible;
    }

    private void EditTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void MapItem_Click(object sender, RoutedEventArgs e)
    {
        TripsGrid.Visibility = Visibility.Hidden;
        MapGrid.Visibility = Visibility.Visible;
    }
    

    
}