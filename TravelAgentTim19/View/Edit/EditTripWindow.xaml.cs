using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using HelpSistem;
using Microsoft.Win32;
using NodaTime;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;
using Location = TravelAgentTim19.Model.Location;

namespace TravelAgentTim19.View.Edit;

public partial class EditTripWindow 
{
    public Trip Trip { get; set; }
    private Trip editTrip { get; set; }
    
    private MainRepository MainRepository;
    private List<Location> AttractionsLocations { get; set; }
    public EditTripWindow(Trip trip, MainRepository mainRepository)
    {
        Trip = trip;
        AttractionsLocations = new List<Location>();
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

    private void ClearData()
    {
        AllAttractionsListBox.Items.Clear();
        ChosenAttractionsListBox.Items.Clear();
        AllAccommodationsListBox.Items.Clear();
        ChosenAccommodationsListBox.Items.Clear();
        AllRestaurantsListBox.Items.Clear();
        ChosenRestaurantsListBox.Items.Clear();
        DateListBox.Items.Clear();
    }

    private void AddData()
    {
        foreach (Attraction attraction in MainRepository.AttractionRepository.GetAttractions())
        {
            AllAttractionsListBox.Items.Add(attraction);
        }
        foreach (Attraction attraction in Trip.Attractions)
        {
            AllAttractionsListBox.Items.Remove(attraction);
            ChosenAttractionsListBox.Items.Add(attraction);
        }
        
        foreach (Accomodation accomodation in MainRepository.AccomodationRepository.GetAccomodations())
        {
            AllAccommodationsListBox.Items.Add(accomodation);
        }
        foreach (Accomodation accomodation in Trip.Accomodations)
        {
            AllAccommodationsListBox.Items.Remove(accomodation);
            ChosenAccommodationsListBox.Items.Add(accomodation);
        }
        
        foreach (Restaurant restaurant in MainRepository.RestaurantsRepository.GetRestaurants())
        {
            AllRestaurantsListBox.Items.Add(restaurant);
        }
        foreach (Restaurant restaurant in Trip.Restaurants)
        {
            AllRestaurantsListBox.Items.Remove(restaurant);
            ChosenRestaurantsListBox.Items.Add(restaurant);
        }

        foreach (DatePeriods period in Trip.DatePeriods)
        {
            DateListBox.Items.Add(period);
        }


    }
    
    private void Border_DragEnter(object sender, DragEventArgs e)
    {
        e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        e.Handled = true;
    }

    private void Border_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (IsImageFile(file))
                {
                    AddImage(file);
                }
            }
        }
    }

    private bool IsImageFile(string filePath)
    {
        string extension = Path.GetExtension(filePath);

        if (extension != null)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            return imageExtensions.Contains(extension.ToLower());
        }

        return false;
    }

    private void AddImage(string filePath)
    {
        Image image = new Image
        {
            Source = new BitmapImage(new Uri(filePath)),
            Width = 60,
            Height = 60
        };
        ImageList.Items.Add(image);
    }
    private void ListView_MouseClick(object sender, MouseButtonEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Multiselect = true; // Allow multiple file selection
        openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"; // Filter image files

        if (openFileDialog.ShowDialog() == true)
        {

            foreach (string filename in openFileDialog.FileNames)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(filename);
                bitmapImage.EndInit();

                Image image = new Image();
                image.Source = bitmapImage;
                image.Width = 50;
                image.MaxHeight = 50;

                ImageList.Items.Add(image);
            }
            
        }
    }
    
    
    private object draggedItem;
    private ListBox sourceListBox;

    private void SelectedItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        ListBox listBox = sender as ListBox;
        sourceListBox = listBox;
        draggedItem = listBox.SelectedItem;
    }

    private void MoveItem_PreviewMouseMove(object sender, MouseEventArgs e)
    {
        ListBox listBox = sender as ListBox;
        if (e.LeftButton == MouseButtonState.Pressed && draggedItem != null)
        {
            DragDrop.DoDragDrop(listBox, draggedItem, DragDropEffects.Move);
        }
    }

    private void DragAttraction_PreviewDragEnter(object sender, DragEventArgs e)
    {
        if (!e.Data.GetDataPresent(typeof(Attraction)))
        {
            e.Effects = DragDropEffects.None;
        }
    }

    private void DragOverAttraction_PreviewDragOver(object sender, DragEventArgs e)
    {
        e.Effects = !e.Data.GetDataPresent(typeof(Attraction)) ? DragDropEffects.None : DragDropEffects.Move;
        e.Handled = true;
    }

    private void DropAttraction_PreviewDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(Attraction))) 
        {
            Attraction draggedItem = e.Data.GetData(typeof(Attraction)) as Attraction; 
            ListBox listBox = sender as ListBox;
            listBox.Items.Add(draggedItem);
           
            
            if (sourceListBox.Items.Contains(this.draggedItem))
            {
                sourceListBox.Items.Remove(draggedItem);
            }  
            
            e.Handled = true;
        }
    }

    
    private void DragAccomodation_PreviewDragEnter(object sender, DragEventArgs e)
    {
        if (!e.Data.GetDataPresent(typeof(Accomodation)))
        {
            e.Effects = DragDropEffects.None;
        }
    }

    private void DragOverAccomodation_PreviewDragOver(object sender, DragEventArgs e)
    {
        e.Effects = !e.Data.GetDataPresent(typeof(Accomodation)) ? DragDropEffects.None : DragDropEffects.Move;
        e.Handled = true;
    }

    private void DropAccomodation_PreviewDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(Accomodation))) 
        {
            Accomodation draggedItem = e.Data.GetData(typeof(Accomodation)) as Accomodation; 
            ListBox listBox = sender as ListBox;
            listBox.Items.Add(draggedItem);
           
            if (sourceListBox != null)
            {
                sourceListBox.Items.Remove(draggedItem);
            }  
            
            e.Handled = true;
        }
    }
    
    private void DragRestaurant_PreviewDragEnter(object sender, DragEventArgs e)
    {
        if (!e.Data.GetDataPresent(typeof(Restaurant)))
        {
            e.Effects = DragDropEffects.None;
        }
    }

    private void DragOverRestaurant_PreviewDragOver(object sender, DragEventArgs e)
    {
        e.Effects = !e.Data.GetDataPresent(typeof(Restaurant)) ? DragDropEffects.None : DragDropEffects.Move;
        e.Handled = true;
    }

    private void DropRestaurant_PreviewDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(Restaurant))) 
        {
            Restaurant draggedItem = e.Data.GetData(typeof(Restaurant)) as Restaurant; 
            ListBox listBox = sender as ListBox;
            listBox.Items.Add(draggedItem);
           
            if (sourceListBox != null)
            {
                sourceListBox.Items.Remove(draggedItem);
            }  
            
            e.Handled = true;
        }
    }
    
    
    private void AddDateRange_Click(object sender, RoutedEventArgs e)
    {
        // Assuming your DatePicker's have names startDatePicker and endDatePicker
        DateTime startDate = StartDatePicker.SelectedDate ?? DateTime.Now;
        DateTime endDate = EndDatePicker.SelectedDate ?? DateTime.Now;

        DatePeriods newPeriod = new DatePeriods();
        Random rand = new Random();
        newPeriod.Id = rand.Next(10000);
        newPeriod.StartDate = LocalDate.FromDateTime(startDate);
        newPeriod.EndDate = LocalDate.FromDateTime(endDate);
        DateListBox.Items.Add(newPeriod);
    }

    private void DeleteDateRange_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var dateRangeToDelete = (DatePeriods)button.DataContext;
        DateListBox.Items.Remove(dateRangeToDelete);
    }

    private void EditTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        ClearData();
        AddData();
        TxtName.Text = Trip.Name;
        DescriptionBox.Text = Trip.Description;
        TxtPrice.Text = Trip.Price.ToString();
        
        InfoGrid.Visibility = Visibility.Hidden;
        EditGrid.Visibility = Visibility.Visible;
    }
    
    private void InfoTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        InfoGrid.Visibility = Visibility.Visible;
        EditGrid.Visibility = Visibility.Hidden;
    }
    private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtName.Text) && TxtName.Text.Length > 0)
            TextName.Visibility = Visibility.Collapsed;
        else
            TextName.Visibility = Visibility.Visible;
    }
        

    private void textName_MouseDown(object sender, MouseButtonEventArgs e)
    {
        TxtName.Focus();
    }

   
    private void textDesc_MouseDown(object sender, MouseButtonEventArgs e)
    {
        TextDes.Focus();
    }

    private void descBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(DescriptionBox.Text) && DescriptionBox.Text.Length > 0)
            DescriptionBox.Visibility = Visibility.Collapsed;
        else
            DescriptionBox.Visibility = Visibility.Visible;
    }
    private void textPrice_MouseDown(object sender, MouseButtonEventArgs e)
    {
        TxtPrice.Focus();
    }

    private void priceBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtPrice.Text) && TxtPrice.Text.Length > 0)
            TextPrice.Visibility = Visibility.Collapsed;
        else
            TextPrice.Visibility = Visibility.Visible;
    }
    
    private void SaveTripBtn_Clicked(object sender, RoutedEventArgs e)
    {

        string name = TxtName.Text;
        string description = DescriptionBox.Text;
        string p = TxtPrice.Text;
        if (!double.TryParse(p, out double price))
        {
            MessageBox.Show("Unesite cenu. Mora biti numericka vrednost.");
            return;
        }
        Trip.Price = price;
        
        ItemCollection attractions = ChosenAttractionsListBox.Items;
        ItemCollection accommodations = ChosenAccommodationsListBox.Items;
        ItemCollection restaurants = ChosenRestaurantsListBox.Items;
        ItemCollection dataPeriods = DateListBox.Items;
        ItemCollection Images = ImageList.Items;

        if (attractions == null || accommodations == null || restaurants == null || attractions.Count == 0 || accommodations.Count == 0 || restaurants.Count == 0 || Images == null || Images.Count == 0)
        {
            MessageBox.Show("Izaberite atrakcije, smestaje i restorane za ovo putovanje i ubacite bar jednu sliku/");
            return;
        }
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
        {
            MessageBox.Show("Molimo Vas popunite sva polja i ubacite bar jednu sliku.");
            return;
        }

        MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da dodate ovo putovanje?", "Potvrda",
            MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            Trip.Name = name;
            Trip.Description = description;
            Trip.Accomodations = accommodations.OfType<Accomodation>().ToList();
            Trip.Attractions = attractions.OfType<Attraction>().ToList();
            Trip.Restaurants = restaurants.OfType<Restaurant>().ToList();
            Trip.DatePeriods = dataPeriods.OfType<DatePeriods>().ToList();

            //dodati slike

            foreach (DatePeriods dp in Trip.DatePeriods)
            {
                MainRepository.DatePeriodRepository.AddDatePeriod(dp);
            }
            MainRepository.TripRepository.UpdateTrip(Trip);

            TripNameTextBlock.Text = Trip.Name;
            DescriptionTextBlock.Text = Trip.Description;
            PriceTextBlock.Text = Trip.Price.ToString();
            tripAttractionItems.ItemsSource = Trip.Attractions;
            tripAccomodationsItems.ItemsSource = Trip.Accomodations;
            tripRestaurantsItems.ItemsSource = Trip.Restaurants;
            tripPeriodsItems.ItemsSource = Trip.DatePeriods;

            InfoTripBtn_Clicked(sender, e);
        }

    }
    
    private void SaveBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Button saveButton = FindName("EditButton") as Button;
        if (saveButton != null)
        {
            SaveTripBtn_Clicked(saveButton, null);
        }
    }
    private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Close(); 
    }
    private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        string helpKey;
        if (InfoGrid.Visibility == Visibility.Visible)
        {
            helpKey = "infoTrip";
        }
        else if (EditGrid.Visibility == Visibility.Visible)
        {
            helpKey = "editTrip";
        }
        else
        {
            helpKey = "index"; // default key
        }

        HelpProvider.ShowHelp(helpKey, this);
    }
}