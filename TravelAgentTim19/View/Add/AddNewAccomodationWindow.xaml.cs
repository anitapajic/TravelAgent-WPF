using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FontAwesome.WPF;
using HelpSistem;
using Microsoft.Win32;
using TravelAgentTim19.Model;
using TravelAgentTim19.Model.Enum;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View;

public partial class AddNewAccomodationWindow 
{
    private MainRepository MainRepository;
    public AddNewAccomodationWindow(MainRepository mainRepository)
    {
        MainRepository = mainRepository;

        InitializeComponent();
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
private bool isRatingLocked = false;

private void Star_MouseEnter(object sender, MouseEventArgs e)
{
    isRatingLocked = false;
    if (!isRatingLocked)
    {
        ImageAwesome star = sender as ImageAwesome;
        star.Foreground = Brushes.Yellow; // Change the color to yellow or any other color you prefer
        int value = int.Parse(star.Name.Replace("star", ""));
        for (int i = 1; i <= 5; i++)
        {
            ImageAwesome filledStar = FindName("star" + i) as ImageAwesome;
            if (i <= value)
            {
                filledStar.Foreground = Brushes.Yellow; // Change the color to yellow or any other color you prefer
                filledStar.Icon = FontAwesomeIcon.Star;
            }
            else
            {
                filledStar.Foreground = Brushes.Yellow ; // Change the color to black or any other color you prefer
                filledStar.Icon = FontAwesomeIcon.StarOutline;
            }
        }
    }
}

private void Star_MouseLeave(object sender, MouseEventArgs e)
{
    if (!isRatingLocked)
    {
        for (int i = 1; i <= 5; i++)
        {
            ImageAwesome star = FindName("star" + i) as ImageAwesome;
            if (star.Tag == null)
            {
                star.Foreground = Brushes.Yellow; // Change the color to black or any other color you prefer
                star.Icon = FontAwesomeIcon.StarOutline;
            }
            else
            {
                star.Foreground = Brushes.Yellow; // Change the color to yellow or any other color you prefer
                star.Icon = FontAwesomeIcon.Star;
            }
        }
    }
}

private int rstar;
private void Star_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
{
    ImageAwesome star = sender as ImageAwesome;
    int value = int.Parse(star.Name.Replace("star", ""));
    rstar = value;
    if (!isRatingLocked)
    {
        
        for (int i = 1; i <= 5; i++)
        {
            ImageAwesome filledStar = FindName("star" + i) as ImageAwesome;
            if (i <= value)
            {
                filledStar.Foreground = Brushes.Yellow; // Change the color to yellow or any other color you prefer
                filledStar.Icon = FontAwesomeIcon.Star;
            }
            else
            {
                filledStar.Foreground = Brushes.Yellow; // Change the color to black or any other color you prefer
                filledStar.Icon = FontAwesomeIcon.StarOutline;
            }
        }
        isRatingLocked = true; // Lock the rating
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
        string destinationFolderPath = "../../../Images/Accomodations"; // Destination folder path
        string destinationFilePath = Path.Combine(destinationFolderPath, fileName);

        // Copy the image to the destination folder
        File.Copy(filePath, destinationFilePath, true);
        
        
        Image image = new Image
        {
            Source = new BitmapImage(new Uri(filePath)),
            Width = 60,
            Height = 60
        };
        ImageList.Items.Clear();
        ImageList.Items.Add(image);
    }
    private void CreateAccomodationBtn_Clicked(object sender, RoutedEventArgs e)
    {
        string name = TxtName.Text;
        Location location = new Location();
        location.Address = TxtAddress.Text;
        ItemCollection Images = ImageList.Items;
        AccomodationType type = (AccomodationType)accomodationComboBox.SelectedItem;
        // double rating = RatingSlider.Value;

        // Validate inputs
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(location.Address) || Images == null || Images.Count == 0)
        {
            MessageBox.Show("Molimo Vas popunite sva polja i ubacite bar jednu sliku.");
            return;
        }

        // Additional validations
        if (rstar < 0 || rstar > 5)
        {
            MessageBox.Show("Ocena mora biti između 0 i 5.");
            return;
        }

        if (type == null)
        {
            MessageBox.Show("Molimo Vas odaberite tip smještaja.");
            return;
        }
        

        MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da dodate ovaj smeštaj?", "Potvrda", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            Accomodation accomodation = new Accomodation();
            Random rand = new Random();
            accomodation.Id = rand.Next(10000);
            accomodation.Location = location;
            accomodation.Name = name;
            accomodation.Rating = rstar;
            accomodation.AccomodationType = type;
            Image image = (Image)Images[0]; // Assuming there is only one image in the list
            string imagePath = ((BitmapImage)image.Source).UriSource.AbsolutePath;
            string imageFilename = Path.GetFileName(imagePath);
            accomodation.ImgPath = "/Images/Accomodations/" + imageFilename;

            MainRepository.AccomodationRepository.AddAccomodation(accomodation);
            Close();
        }
    }
    private void ListView_MouseClick(object sender, MouseButtonEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Multiselect = true; // Allow multiple file selection
        openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"; // Filter image files

        if (openFileDialog.ShowDialog() == true)
        {
            AddImage(openFileDialog.FileName);
            
        }
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
        TxtName.Focus();
    }

   
    private void textAddress_MouseDown(object sender, MouseButtonEventArgs e)
    {
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
        TxtPrice.Focus();
    }

    private void priceBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtPrice.Text) && TxtPrice.Text.Length > 0)
            TextPrice.Visibility = Visibility.Collapsed;
        else
            TextPrice.Visibility = Visibility.Visible;
    }
    
    private void SaveBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Button saveButton = FindName("SaveButton") as Button;
        if (saveButton != null)
        {
            CreateAccomodationBtn_Clicked(saveButton, null);
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
    }private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        string str = HelpProvider.GetHelpKey(this);
        HelpProvider.ShowHelp(str, this);
    }
}