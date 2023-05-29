using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TravelAgentTim19.Model;
using TravelAgentTim19.Model.Enum;
﻿using System.Windows;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View.Edit;

public partial class EditAccomodationWindow : Window
{
    public Accomodation Accomodation { get; set; }
    public MainRepository MainRepository { get; set; }
    public EditAccomodationWindow(Accomodation accomodation, MainRepository mainRepository)
    {
        Accomodation = accomodation;
        MainRepository = mainRepository;
        InitializeComponent();
    }
     private void EditAccomodationBtn_Clicked(object sender, RoutedEventArgs e)
    {
        InfoGrid.Visibility = Visibility.Hidden;
        EditGrid.Visibility = Visibility.Visible;
    }
    
    private void InfoAccomodationBtn_Clicked(object sender, RoutedEventArgs e)
    {
        InfoGrid.Visibility = Visibility.Visible;
        EditGrid.Visibility = Visibility.Hidden;
        NameBox.Text = Accomodation.Name;
        LocationBox.Text = Accomodation.Location.Address;

    }
    
    private void SaveAccomodationBtn_Clicked(object sender, RoutedEventArgs e)
    {
        string name = NameBox.Text;
        string address = LocationBox.Text;
        ItemCollection Images = ImageList.Items;

        double rating = slider.Value;
        AccomodationType type = (AccomodationType)accomodationComboBox.SelectedItem;

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
            
            Accomodation.Location.Address = address;
            Accomodation.Name = name;
            Accomodation.Rating = rating;
            Accomodation.AccomodationType = type;
            //dodati slike
            
            MainRepository.AccomodationRepository.UpdateAccomodation(Accomodation);
            InfoAccomodationBtn_Clicked(sender,e);
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