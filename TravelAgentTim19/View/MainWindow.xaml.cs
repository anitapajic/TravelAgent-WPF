﻿using System;
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
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainRepository MainRepository;
        public MainWindow()
        {
            MainRepository = new MainRepository();
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
        
        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PasswordBox.Focus();
        }


        private void SignInFormButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtEmail.Text) && !string.IsNullOrEmpty(PasswordBox.Password))
            {
                bool found = false;
                foreach (User user in MainRepository.UserRepository.GetUsers())
                {
                    if (txtEmail.Text.Equals(user.Email) && passwordBox.Password.Equals(user.Password))
                    {
                        found = true;
                        MessageBox.Show("Successfully Signed In");
                    }
                }
                if (found == false)
                {
                    MessageBox.Show("User does not exist!");
                }
            }
            else
            {
                MessageBox.Show("Invalid data!");
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
        

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TxtEmail.Focus();
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




        private void textSignUpFName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SignUpFNameBox.Focus();
        }

        private void signUpFNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SignUpFNameBox.Text) && SignUpFNameBox.Text.Length > 0)
                TextSignUpFName.Visibility = Visibility.Collapsed;
            else
                TextSignUpFName.Visibility = Visibility.Visible;         }
        
        private void textSignUpLName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SignUpLNameBox.Focus();
        }

        private void signUpLNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SignUpLNameBox.Text) && SignUpLNameBox.Text.Length > 0)
                TextSignUpLName.Visibility = Visibility.Collapsed;
            else
                TextSignUpLName.Visibility = Visibility.Visible;        
        }
        private void textSignUpEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SignUpEmailBox.Focus();
        }
        private void signUpEmailBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SignUpEmailBox.Text) && SignUpEmailBox.Text.Length > 0)
                TextSignUpEmail.Visibility = Visibility.Collapsed;
            else
                TextSignUpEmail.Visibility = Visibility.Visible;
        }
 
        private void textSignUpPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SignUpPasswordBox.Focus();
        }
        private void signUpPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SignUpPasswordBox.Password) && SignUpPasswordBox.Password.Length > 0)
                TextSignUpPassword.Visibility = Visibility.Collapsed;
            else
                TextSignUpPassword.Visibility = Visibility.Visible;
        }
        
        private void textSignUpPassword2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SignUpPassword2Box.Focus();
        }
        private void signUpPassword2Box_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SignUpPassword2Box.Password) && SignUpPassword2Box.Password.Length > 0)
                TextSignUpPassword2.Visibility = Visibility.Collapsed;
            else
                TextSignUpPassword2.Visibility = Visibility.Visible;
        }
    }
}