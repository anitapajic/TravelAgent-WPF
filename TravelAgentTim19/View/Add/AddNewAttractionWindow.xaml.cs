using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        string destinationFolderPath = "../../../Images/Attractions"; // Destination folder path
        string destinationFilePath = Path.Combine(destinationFolderPath, fileName);
    
        // Copy the image to the destination folder
        File.Copy(filePath, destinationFilePath, true);
        
        
        Image image = new Image
        {
            Source = new BitmapImage(new Uri(filePath)),
            Width = 60,
            Height = 60
        };
        // ImageList.Items.Clear();
        // ImageList.Items.Add(image);
    }
    
    private void ListView_MouseClick(object sender, MouseButtonEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Multiselect = true; // Allow multiple file selection
        openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"; // Filter image files

        if (openFileDialog.ShowDialog() == true)
        {
            // AddImage(openFileDialog.FileName);

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
}