using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HelpSistem;
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
        if (TxtName.Text.Length > 40)
        {
            MessageBox.Show("Naziv je predugačak");
            return;
        }
        if (TxtAddress.Text.Length > 40 || TxtCity.Text.Length > 20)
        {
            MessageBox.Show("Adresa i naziv grada su predugački");
            return;
        }

        // Validate inputs
        if (string.IsNullOrEmpty(TxtName.Text) || string.IsNullOrEmpty(TxtCity.Text) ||
            string.IsNullOrEmpty(TxtAddress.Text) || string.IsNullOrEmpty(TxtPrice.Text) ||
            string.IsNullOrEmpty(TxtDescription.Text))
        {
            MessageBox.Show("Molimo Vas popunite sva polja.");
            return;
        }

        double price;
        if (!double.TryParse(TxtPrice.Text, out price))
        {
            MessageBox.Show("Cena mora biti numerička vrednost.");
            return;
        }

        Random rand = new Random();
        int id = rand.Next(10000);
        Attraction attraction = new Attraction(id, TxtName.Text, "", new Location(TxtCity.Text, TxtAddress.Text),
            price, TxtDescription.Text);
        MainRepository.AttractionRepository.AddAttraction(attraction);
        MessageBox.Show("Uspešno si dodao novu atrakciju!");
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
        TextName.Visibility = Visibility.Collapsed;
        TxtName.Focus();
    }

    private void textCity_MouseDown(object sender, MouseButtonEventArgs e)
    {
        TextCity.Visibility = Visibility.Collapsed;
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
        TextAddress.Visibility = Visibility.Collapsed;
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
        TextPrice.Visibility = Visibility.Collapsed;
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
        TextDescription.Visibility = Visibility.Collapsed;
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
        return !(element is TextBox) && (element.Name != "Ximg");
    }
    private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
     {
         string str = HelpProvider.GetHelpKey(this);
         HelpProvider.ShowHelp(str, this);
    }
}