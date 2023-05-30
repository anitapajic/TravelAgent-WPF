using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using NodaTime;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View.Edit;

public partial class EditTripWindow : Window
{
    public Trip Trip { get; set; }
    public Trip editTrip { get; set; }
    
    private MainRepository MainRepository;
    public EditTripWindow(Trip trip, MainRepository mainRepository)
    {
        Trip = trip;
        // editTrip = new Trip(trip);
        
        MainRepository = mainRepository;
        InitializeComponent();
        DataContext = this;

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
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effects = DragDropEffects.Copy;
        }
        else
        {
            e.Effects = DragDropEffects.None;
        }
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
        if (!e.Data.GetDataPresent(typeof(Attraction))) 
        {
            e.Effects = DragDropEffects.None;
        }
        else
        {
            e.Effects = DragDropEffects.Move;
        }
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
        if (!e.Data.GetDataPresent(typeof(Accomodation))) 
        {
            e.Effects = DragDropEffects.None;
        }
        else
        {
            e.Effects = DragDropEffects.Move;
        }
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
        if (!e.Data.GetDataPresent(typeof(Restaurant))) 
        {
            e.Effects = DragDropEffects.None;
        }
        else
        {
            e.Effects = DragDropEffects.Move;
        }
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
        NameBox.Text = Trip.Name;
        DescriptionBox.Text = Trip.Description;
        PriceBox.Text = Trip.Price.ToString();
        
        InfoGrid.Visibility = Visibility.Hidden;
        EditGrid.Visibility = Visibility.Visible;
    }
    
    private void InfoTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        editTrip = Trip;
        InfoGrid.Visibility = Visibility.Visible;
        EditGrid.Visibility = Visibility.Hidden;
    }

    
    private void SaveTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
     
        string name = NameBox.Text;
        string description = DescriptionBox.Text;

      
        try
        {
            string p = PriceBox.Text;
            Trip.Price = Double.Parse(p);
        }
        catch
        {
            MessageBox.Show("Unesite cenu. Mora biti numericka vrednost.");
            return;
        }


        ItemCollection attractions = ChosenAttractionsListBox.Items;
        ItemCollection accommodations = ChosenAccommodationsListBox.Items;
        ItemCollection restaurants = ChosenRestaurantsListBox.Items;
        ItemCollection dataPeriods = DateListBox.Items;
        ItemCollection Images = ImageList.Items;

        if (attractions == null || accommodations == null || restaurants == null || attractions.Count == 0 || accommodations.Count == 0 || restaurants.Count == 0 ||Images == null || Images.Count == 0)
        {
            MessageBox.Show("Izaberite atrakcije, smestaje i restorane za ovo putovanje i ubacite bar jednu sliku/");
            return;
        }
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
        {
            MessageBox.Show("Molimo Vas popunite sva polja i ubacite bar jednu sliku.");
            return;
        }

        MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da dodate ovao putovanje?", "Potvrda",
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
            InfoTripBtn_Clicked(sender, e);
        }
    }
    
}