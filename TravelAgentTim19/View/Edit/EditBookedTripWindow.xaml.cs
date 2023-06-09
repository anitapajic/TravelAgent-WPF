﻿using System.Windows;
using System.Windows.Input;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View.Edit;

public partial class EditBookedTripWindow
{
    private BookedTrip BookedTrip;
    private MainRepository MainRepository;
    public EditBookedTripWindow(BookedTrip bookedTrip, MainRepository mainRepository)
    {
        BookedTrip = bookedTrip;
        MainRepository = mainRepository;
        InitializeComponent();
    }
    private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Close(); 
    }
}