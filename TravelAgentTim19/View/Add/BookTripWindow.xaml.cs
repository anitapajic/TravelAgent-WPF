using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TravelAgentTim19.Model;
using TravelAgentTim19.Model.Enum;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View;

public partial class BookTripWindow 
{
    public Trip Trip { get; set; }
    private BookedTrip BookedTrip { get; set; }
    private User LoggedUser { get; set; }
    
    
    private MainRepository MainRepository;
    
    public BookTripWindow(User user,Trip trip, MainRepository mainRepository)
    {
        Trip = trip;
        BookedTrip = new BookedTrip();
        LoggedUser = user;
        MainRepository = mainRepository;
        InitializeComponent();
        DataContext = this;

    }
    
    
    private void EditTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        
        InfoGrid.Visibility = Visibility.Hidden;
        EditGrid.Visibility = Visibility.Visible;
    }
    
    private void InfoTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
        InfoGrid.Visibility = Visibility.Visible;
        EditGrid.Visibility = Visibility.Hidden;
    }

    
    private void BookTripBtn_Clicked(object sender, RoutedEventArgs e)
    {
     
 
        Accomodation accommodation = (Accomodation)AccomodationComboBox.SelectedItem;
        DatePeriods datePeriods = (DatePeriods)DateComboBox.SelectedItem;


        if (accommodation == null || datePeriods == null)
        {
            MessageBox.Show("Izaberite smestaj i datum za ovo putovanje");
            return;
        }

        int days = datePeriods.EndDate.Minus(datePeriods.StartDate).Days;
        double totalPrice = Trip.Price + accommodation.Price*days;
        
        MessageBoxResult result = MessageBox.Show("Ukupna cena putovanja je: " + totalPrice + "din.\n Da li ste sigurni da zelite da rezervisete ovao putovanje?", "Potvrda",
            MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            
            Random random = new Random();
            BookedTrip.Id = random.Next();
            BookedTrip.TripId = Trip.Id;
            BookedTrip.TripName = Trip.Name;
            BookedTrip.Status = BookedTripStatus.Reserved;
            
            BookedTrip.Price = totalPrice;
            BookedTrip.DatePeriod = datePeriods;
            BookedTrip.Accomodation = accommodation;
            BookedTrip.ChoosenAttractions = Trip.Attractions;
            BookedTrip.User = LoggedUser;
            
            MainRepository.BookedTripRepository.AddBookedTrip(BookedTrip);
            
            InfoTripBtn_Clicked(sender, e);
        }
    }
    
    private void SaveBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Button saveButton = FindName("SaveBookButton") as Button;
        if (saveButton != null)
        {
            BookTripBtn_Clicked(saveButton, null);
        }
    }
    
    private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Close(); 
    }
    
}