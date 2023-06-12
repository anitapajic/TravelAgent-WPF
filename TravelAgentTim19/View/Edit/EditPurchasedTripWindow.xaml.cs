using System.Windows;
using System.Windows.Input;
using HelpSistem;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View.Edit;

public partial class EditPurchasedTripWindow 
{
    public BookedTrip BookedTrip { get; set; }
    private MainRepository MainRepository;
    public EditPurchasedTripWindow(BookedTrip bookedTrip, MainRepository mainRepository)
    {
        BookedTrip = bookedTrip;
        MainRepository = mainRepository;
        InitializeComponent();
    }
    private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Close(); 
    }
    private void Image_MouseUp(object sender, MouseButtonEventArgs e)
    {
        MainRepository.Save();
        Close(); 
    }
    private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        string str = HelpProvider.GetHelpKey(this);
        HelpProvider.ShowHelp(str, this);
    }
}