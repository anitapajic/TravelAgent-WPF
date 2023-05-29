using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;



namespace TravelAgentTim19.View;

public partial class AddNewRestaurantWindow : Window
{
    private MainRepository MainRepository;

    public AddNewRestaurantWindow(MainRepository mainRepository)
    {
        MainRepository = mainRepository;
        InitializeComponent();
        DataContext = this;
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

    private void CreateRestaurantBtn_Clicked(object sender, RoutedEventArgs e)
    {
        string name = NameBox.Text;
        Location location = new Location();
        location.Address = LocationBox.Text;
        ItemCollection Images = ImageList.Items;

        // double rating = RatingSlider.Value;
        double rating = slider.Value;

        // Validate inputs
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(location.Address) || Images == null || Images.Count == 0)
        {
            MessageBox.Show("Please ensure all fields are filled and at least one image is added.");
            return;
        }

        Restaurant restaurant = new Restaurant();
        restaurant.Location = location;
        restaurant.Name = name;
        restaurant.Rating = rating;
        //dodati slike

        MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da dodate ovaj restoran?", "Potvrda",
            MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            MainRepository.RestaurantsRepository.AddRestaurant(restaurant);
            Close();
        }
    }
}

