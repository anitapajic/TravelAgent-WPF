using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        return !(element.Name == "Ximg");
    }
}