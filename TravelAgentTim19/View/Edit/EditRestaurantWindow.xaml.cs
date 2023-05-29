using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View.Edit;

public partial class EditRestaurantWindow : Window
{
    public Restaurant Restaurant { get; set; }
    private MainRepository MainRepository;
    public EditRestaurantWindow(Restaurant restaurant, MainRepository mainRepository)
    {
        MainRepository = mainRepository;
        Restaurant = restaurant;
        InitializeComponent();
    }

    private void EditRestaurantBtn_Clicked(object sender, RoutedEventArgs e)
    {
        InfoGrid.Visibility = Visibility.Hidden;
        EditGrid.Visibility = Visibility.Visible;
    }
    
    private void InfoRestaurantBtn_Clicked(object sender, RoutedEventArgs e)
    {
        InfoGrid.Visibility = Visibility.Visible;
        EditGrid.Visibility = Visibility.Hidden;
        NameBox.Text = Restaurant.Name;
        LocationBox.Text = Restaurant.Location.Address;
        
    }
    
    private void SaveChangesBtn_Clicked(object sender, RoutedEventArgs e)
    {
        string name = NameBox.Text;
        string address = LocationBox.Text;
        ItemCollection Images = ImageList.Items;

        // double rating = RatingSlider.Value;
        double rating = 3;

        // Validate inputs
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || Images == null || Images.Count == 0)
        {
            MessageBox.Show("Molimo Vas popunite sva polja i ubacite bar 1 sliku.");
            return;
        }
        

        MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da izmenite ovaj restoran?", "Potvrda",
            MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            
            Restaurant.Location.Address = address;
            Restaurant.Name = name;
            Restaurant.Rating = rating;
            //dodati slike
            
            MainRepository.RestaurantsRepository.UpdateRestaurant(Restaurant);
            Close();
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
}