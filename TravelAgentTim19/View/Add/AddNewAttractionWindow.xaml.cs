using System;
using System.Collections.Generic;
using System.Windows;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View;

public partial class AddNewAttractionWindow : Window
{
    public MainRepository MainRepository;
    public List<Attraction> Attractions { get; set; }
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
        Attraction attraction = new Attraction(id, NameTextBox.Text, new Location(CityTextBox.Text, AddressTextBox.Text), Convert.ToDouble(PriceTextBox.Text), DescriptionTextBox.Text);
        MainRepository.AttractionRepository.AddAttraction(attraction);
        MessageBox.Show("Uspesno si dodao novu atrakciju!");
        Close();
    }
}