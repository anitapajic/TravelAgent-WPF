using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View;

public partial class AddNewAttractionWindow 
{
    private MainRepository MainRepository;
    private List<Attraction> Attractions { get; set; }
    public AddNewAttractionWindow(MainRepository mainRepository)
    {
        MainRepository = mainRepository;
        Attractions = MainRepository.AttractionRepository.GetAttractions();
        InitializeComponent();
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        Random rand = new Random();
        int id = rand.Next(10000);
         Attraction attraction = new Attraction(id, TxtName.Text, "",new Location(TxtCity.Text, TxtAddress.Text), Convert.ToDouble(TxtPrice.Text), TxtDescription.Text);
         MainRepository.AttractionRepository.AddAttraction(attraction);
        MessageBox.Show("Uspesno si dodao novu atrakciju!");
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

    private void textCity_MouseDown(object sender, MouseButtonEventArgs e)
    {
        TxtCity.Focus();
    }

    private void cityBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtCity.Text) && TxtCity.Text.Length > 0)
            TextCity.Visibility = Visibility.Collapsed;
        else
            TextCity.Visibility = Visibility.Visible;
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

    private void textDesc_MouseDown(object sender, MouseButtonEventArgs e)
    {
        TxtDescription.Focus();
    }

    private void descBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtDescription.Text) && TxtDescription.Text.Length > 0)
            TextDescription.Visibility = Visibility.Collapsed;
        else
            TextDescription.Visibility = Visibility.Visible;
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

                // ImageList.Items.Add(image);
            }
            
        }
    }

    private void SaveBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Button saveButton = FindName("SaveButton") as Button;
        if (saveButton != null)
        {
            SaveButton_OnClick(saveButton, null);
        }
    }
    
    private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
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
        return !(element is TextBox);
    }
}