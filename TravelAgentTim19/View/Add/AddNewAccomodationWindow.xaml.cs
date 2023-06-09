using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using TravelAgentTim19.Model;
using TravelAgentTim19.Model.Enum;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View;

public partial class AddNewAccomodationWindow 
{
    private MainRepository MainRepository;
    public AddNewAccomodationWindow(MainRepository mainRepository)
    {
        MainRepository = mainRepository;

        InitializeComponent();
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
    
    private void CreateAccomodationBtn_Clicked(object sender, RoutedEventArgs e)
    {
        string name = TxtName.Text;
        Location location = new Location();
        location.Address = TxtAddress.Text;
        ItemCollection Images = ImageList.Items;
        AccomodationType type = (AccomodationType)accomodationComboBox.SelectedItem;
        // double rating = RatingSlider.Value;
        double rating = slider.Value;

        // Validate inputs
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(location.Address) || Images == null || Images.Count == 0)
        {
            MessageBox.Show("Molimo Vas popunite sva polja i ubacite bar jednu sliku.");
            return;
        }
        

        MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da dodate ovaj smestaj?", "Potvrda",
            MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            Accomodation accomodation = new Accomodation();
            Random rand = new Random();
            accomodation.Id = rand.Next(10000);
            accomodation.Location = location;
            accomodation.Name = name;
            accomodation.Rating = rating;
            accomodation.AccomodationType = type;
            //dodati slike
            MainRepository.AccomodationRepository.AddAccomodation(accomodation);
            Close();
        }
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

   
    private void textAddress_MouseDown(object sender, MouseButtonEventArgs e)
    {
        TxtAddress.Focus();
    }

    private void addressBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtAddress.Text) && TxtAddress.Text.Length > 0)
            TextAddress.Visibility = Visibility.Collapsed;
        else
            TextAddress.Visibility = Visibility.Visible;
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
            CreateAccomodationBtn_Clicked(saveButton, null);
        }
    }
    
    private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Close();
    }
}