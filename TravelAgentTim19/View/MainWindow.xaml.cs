using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TravelAgentTim19.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordBox.Password) && PasswordBox.Password.Length > 0)
                TextPassword.Visibility = Visibility.Collapsed;
            else
                TextPassword.Visibility = Visibility.Visible;
        }
        private void signUpPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SignUpPasswordBox.Password) && SignUpPasswordBox.Password.Length > 0)
                TextSignUpPassword.Visibility = Visibility.Collapsed;
            else
                TextSignUpPassword.Visibility = Visibility.Visible;
        }
        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PasswordBox.Focus();
        }
        private void textSignUpPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SignUpPasswordBox.Focus();
        }

        private void SignInFormButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtEmail.Text) && !string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Successfully Signed In");
            }
        }
        
        private void SignUpFormButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SignUpEmailBox.Text) && !string.IsNullOrEmpty(SignUpPasswordBox.Password))
            {
                MessageBox.Show("Successfully Signed Up");
            }
        }

        private void emailBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtEmail.Text) && TxtEmail.Text.Length > 0)
                TextEmail.Visibility = Visibility.Collapsed;
            else
                TextEmail.Visibility = Visibility.Visible;
        }
        
        private void signUpEmailBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SignUpEmailBox.Text) && SignUpEmailBox.Text.Length > 0)
                TextSignUpEmail.Visibility = Visibility.Collapsed;
            else
                TextSignUpEmail.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TxtEmail.Focus();
        }
        private void textSignUpEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SignUpEmailBox.Focus();
        }
        
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            
            SignUpCanvas.Visibility = Visibility.Hidden;
            SignInCanvas.Visibility = Visibility.Visible;
            
            SignUpPanel.Visibility = Visibility.Hidden;
            SignInPanel.Visibility = Visibility.Visible;
            
            SignUpFormPanel.Visibility = Visibility.Visible;
            SignInFormPanel.Visibility = Visibility.Hidden;
            
            SignUpBorder.Background = Brushes.White;
            SignInBorder.Background = new LinearGradientBrush(
                new GradientStopCollection()
                {
                    new GradientStop(Color.FromRgb(58, 169, 173), 0),
                    new GradientStop(Color.FromRgb(58, 173, 161), 1)
                },
                new Point(0, 0),
                new Point(1, 1)
            );
            MyGrid.ColumnDefinitions.Clear(); // Clear existing column definitions

            MyGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.5, GridUnitType.Star) });
            MyGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                
        }
        
        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpCanvas.Visibility = Visibility.Visible;
            SignInCanvas.Visibility = Visibility.Hidden;
            
            SignUpPanel.Visibility = Visibility.Visible;
            SignInPanel.Visibility = Visibility.Hidden;
            
            SignUpFormPanel.Visibility = Visibility.Hidden;
            SignInFormPanel.Visibility = Visibility.Visible;
            
            SignInBorder.Background = Brushes.White;
            SignUpBorder.Background = new LinearGradientBrush(
                new GradientStopCollection()
                {
                    new GradientStop(Color.FromRgb(58, 169, 173), 0),
                    new GradientStop(Color.FromRgb(58, 173, 161), 1)
                },
                new Point(0, 0),
                new Point(1, 1)
            );
            MyGrid.ColumnDefinitions.Clear(); // Clear existing column definitions

            MyGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            MyGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.5, GridUnitType.Star) });
        }

        
    }
}