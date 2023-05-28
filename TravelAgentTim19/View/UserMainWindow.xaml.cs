using System.Windows;

namespace TravelAgentTim19.View;

public partial class UserMainWindow : Window
{
    public UserMainWindow()
    {
        InitializeComponent();
    }

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }
}