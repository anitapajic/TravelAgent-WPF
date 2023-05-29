using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View.Edit;

public partial class EditAttractionWindow : Window
{
    public Attraction Attraction { get; set; }
    public MainRepository MainRepository { get; set; }
    
    public EditAttractionWindow(Attraction attraction, MainRepository mainRepository)
    {
        Attraction = attraction;
        MainRepository = mainRepository;
        InitializeComponent();
    }

    private void EditAttractionBtn_Clicked(object sender, RoutedEventArgs e)
    {
        InfoGrid.Visibility = Visibility.Hidden;
        EditGrid.Visibility = Visibility.Visible;
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
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    if (IsImageFile(file))
                    {
                        AddImage(file);
                    }
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

    private void InfoAttractionBtn_Clicked(object sender, RoutedEventArgs e)
    {
        InfoGrid.Visibility = Visibility.Visible;
        EditGrid.Visibility = Visibility.Hidden;
        NameBox.Text = Attraction.Name;
        LocationBox.Text = Attraction.Location.Address;
        PriceBox.Text = Attraction.Price.ToString();
        DescBox.Text = Attraction.Description;
    }

    private void SaveChangesBtn_Clicked(object sender, RoutedEventArgs e)
    {
        string name = NameBox.Text;
        string address = LocationBox.Text;
        double price = Convert.ToDouble(PriceBox.Text);
        string desc = DescBox.Text;
        ItemCollection Images = ImageList.Items;
        
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || Images == null || Images.Count == 0)
        {
            MessageBox.Show("Molimo Vas popunite sva polja i ubacite bar 1 sliku.");
            return;
        }
        MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da izmenite ovu atrakciju?", "Potvrda",
            MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {

            Attraction.Name = name;
            Attraction.Description = desc;
            Attraction.Location.Address = address;
            Attraction.Price = price;
            //dodati slike
            
            MainRepository.AttractionRepository.UpdateAttraction(Attraction);
            Close();
        }
        
    }
}