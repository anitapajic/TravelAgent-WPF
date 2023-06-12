using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GMap.NET.MapProviders;
using HelpSistem;
using Microsoft.Win32;
using NodaTime;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View;

public partial class AddNewTripWindow
{
    private MainRepository MainRepository;
    public AddNewTripWindow(MainRepository mainRepository)
    {
        MainRepository = mainRepository;
        InitializeComponent();
        AddData();
        DataContext = this;

    }

    private void AddData()
    {
        foreach (Attraction attraction in MainRepository.AttractionRepository.GetAttractions())
        {
            AllAttractionsListBox.Items.Add(attraction);
        }        
        foreach (Accomodation accomodation in MainRepository.AccomodationRepository.GetAccomodations())
        {
            AllAccommodationsListBox.Items.Add(accomodation);
        }
        foreach (Restaurant restaurant in MainRepository.RestaurantsRepository.GetRestaurants())
        {
            AllRestaurantsListBox.Items.Add(restaurant);
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

        string fileName = Path.GetFileName(filePath);

        string destinationFolderPath = "../../../Images/Trips"; // Destination folder path
        string destinationFilePath = Path.Combine(destinationFolderPath, fileName);

        // Copy the image to the destination folder
        File.Copy(filePath, destinationFilePath, true);

        
        Image image = new Image
        {
            Source = new BitmapImage(new Uri(filePath)),
            Width = 60,
            Height = 60
        };
        ImageList.Items.Clear();
        ImageList.Items.Add(image);

    }
    
    private void ListView_MouseClick(object sender, MouseButtonEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Multiselect = true; // Allow multiple file selection
        openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"; // Filter image files

        if (openFileDialog.ShowDialog() == true)
        {
            AddImage(openFileDialog.FileName);
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

    private void CreateTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        string name = TxtName.Text;
        string description = DescriptionBox.Text;

        double price;
        if (!double.TryParse(TxtPrice.Text, out price))
        {   
            try
            {
                string p = TxtPrice.Text;
                price = Double.Parse(p);
            }
            catch
            {
                price = -1;
                MessageBox.Show("Unesite cenu. Mora biti numericka vrednost.");
                return;
            }


            ItemCollection attractions = ChosenAccommodationsListBox.Items;
            ItemCollection accommodations = ChosenAccommodationsListBox.Items;
            ItemCollection restaurants = ChosenAccommodationsListBox.Items;
            ItemCollection dataPeriods = DateListBox.Items;
            ItemCollection Images = ImageList.Items;

            if (attractions == null || accommodations == null || restaurants == null || attractions.Count == 0 ||
                accommodations.Count == 0 || restaurants.Count == 0 || Images == null || Images.Count == 0)
            {
                MessageBox.Show(
                    "Izaberite atrakcije, smestaje i restorane za ovo putovanje i ubacite bar jednu sliku/");
                return;
            }

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || price < 0)
            {
                MessageBox.Show("Molimo Vas popunite sva polja i ubacite bar jednu sliku.");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da dodate ovao putovanje?",
                "Potvrda",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Trip trip = new Trip();
                Random rand = new Random();
                trip.Id = rand.Next(10000);
                trip.Name = name;
                trip.Price = price;
                trip.Description = description;
                trip.Accomodations = accommodations.OfType<Accomodation>().ToList();
                trip.Attractions = attractions.OfType<Attraction>().ToList();
                trip.Restaurants = restaurants.OfType<Restaurant>().ToList();
                trip.DatePeriods = dataPeriods.OfType<DatePeriods>().ToList();


                Image image = (Image)Images[0]; // Assuming there is only one image in the list
                string imagePath = ((BitmapImage)image.Source).UriSource.AbsolutePath;
                string imageFilename = Path.GetFileName(imagePath);
                trip.ImgPath = "/Images/Trips/" + imageFilename;


                foreach (DatePeriods dp in trip.DatePeriods)
                {
                    MainRepository.DatePeriodRepository.AddDatePeriod(dp);
                }

                MainRepository.TripRepository.AddTrip(trip);
                Close();
            }
        }

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
    
    private void SaveBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Button saveButton = FindName("SaveButton") as Button;
        if (saveButton != null)
        {
            CreateTripBtn_Clicked(saveButton, null);
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

    private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape && WindowState == WindowState.Maximized)
        {
            WindowState = WindowState.Normal;
        }
    }
    private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left && IsMouseOverDraggableComponent(e))
            this.DragMove();
    }

    private bool IsMouseOverDraggableComponent(MouseButtonEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;
        return !(element is TextBox) && (element.Name != "Ximg") && (element.Name != "imgDrop");
    }
    private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        string str = HelpProvider.GetHelpKey(this);
        HelpProvider.ShowHelp(str, this);
    }
}