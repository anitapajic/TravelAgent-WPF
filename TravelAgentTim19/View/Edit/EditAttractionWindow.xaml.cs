using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using Microsoft.Win32;
using TravelAgentTim19.Model;
using TravelAgentTim19.Repository;

namespace TravelAgentTim19.View.Edit;

public partial class EditAttractionWindow 
{
    public Attraction Attraction { get; set; }
    private MainRepository MainRepository { get; set; }
    
    public EditAttractionWindow(Attraction attraction, MainRepository mainRepository)
    {
        Attraction = attraction;
        MainRepository = mainRepository;
        InitializeComponent();
    }
    private void MapControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            gmap.Zoom = (gmap.Zoom < gmap.MaxZoom) ? gmap.Zoom + 1 : gmap.MaxZoom;
        }
    }
    private void Image_MouseUp(object sender, MouseButtonEventArgs e)
    {
        Close();
    }
    private void map_load(object sender, RoutedEventArgs e)
    {
        gmap.Bearing = 0;
        gmap.CanDragMap = true;
        gmap.DragButton = MouseButton.Left;
        gmap.MaxZoom = 18;
        gmap.MinZoom = 2;
        gmap.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;
    
        gmap.ShowTileGridLines = false;
        gmap.Zoom = 10;
        gmap.ShowCenter = false;
    
        gmap.MapProvider = GMapProviders.GoogleMap;
        GMaps.Instance.Mode = AccessMode.ServerOnly;
        gmap.Position = new PointLatLng(Attraction.Location.Latitude, Attraction.Location.Longitude);
    
        GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
        GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;
        
        GMapMarker marker = new GMapMarker(new PointLatLng(Attraction.Location.Latitude, Attraction.Location.Longitude));
        BitmapImage bi = new BitmapImage();
        bi.BeginInit();
        bi.UriSource = new Uri("pack://application:,,,/Images/redPin.png");
        bi.EndInit();
        Image pinImage = new Image();
        pinImage.Source = bi;
        pinImage.Width = 50; // Adjust as needed
        pinImage.Height = 50; // Adjust as needed
        pinImage.ToolTip = Attraction.Name;
    
        ToolTipService.SetShowDuration(pinImage, Int32.MaxValue);
        ToolTipService.SetInitialShowDelay(pinImage, 0);
        marker.Shape = pinImage;
        gmap.Markers.Add(marker);
        
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
            nameTextBlock.Text = Attraction.Name;
            addressTextBlock.Text = Attraction.Location.Address;
            priceTextBlock.Text = Attraction.Price.ToString();
            descTextBlock.Text = Attraction.Description;
            
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

            foreach (string filename in openFileDialog.FileNames)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(filename);
                bitmapImage.EndInit();

                Image image = new Image();
                image.Source = bitmapImage;
                image.Width = 50;
                image.MaxHeight = 50;

                ImageList.Items.Add(image);
            }
            
        }
    }
    
    private void SaveBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        Button saveButton = FindName("EditButton") as Button;
        if (saveButton != null)
        {
            SaveChangesBtn_Clicked(saveButton, null);
        }
    }
}