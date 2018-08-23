using DealOrNoDeal.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DealOrNoDeal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Random rand = new Random();
            if (rand.Next(1, 100) < 1)
            {
                Logo.Source = new BitmapImage(new Uri("ms-appx:///Assets/VisualAssets/dond_easterLogo.png"));
            }
        }



        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Game));
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Options));
        }
        private async void Open_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            openPicker.FileTypeFilter.Add(".dond");

            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                using (Stream st = await file.OpenStreamForReadAsync())
                {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
                    Game.go = Serializer.Deserialize<GameObject>(st);
=======
=======
>>>>>>> 81d4c829d24364be77a7cd1916983c05b12de9fa
=======
>>>>>>> 81d4c829d24364be77a7cd1916983c05b12de9fa
=======
>>>>>>> 81d4c829d24364be77a7cd1916983c05b12de9fa
                    GameObject go = new GameObject();
                    go = Serializer.Deserialize<GameObject>(st);
                    //string test = Serializer.Deserialize<string>(st);
                    //Game.go.CurrentCases = GO.CurrentCases;
                    //Game.go.AllCases = GO.AllCases;
                    //Game.go.UserCase = GO.UserCase;
                    this.Frame.Navigate(typeof(Game));
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> 81d4c829d24364be77a7cd1916983c05b12de9fa
=======
>>>>>>> 81d4c829d24364be77a7cd1916983c05b12de9fa
=======
>>>>>>> 81d4c829d24364be77a7cd1916983c05b12de9fa
=======
>>>>>>> 81d4c829d24364be77a7cd1916983c05b12de9fa
                }
                this.Frame.Navigate(typeof(Game));
            }
            else
            {

            }
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD

=======
>>>>>>> 81d4c829d24364be77a7cd1916983c05b12de9fa
=======
>>>>>>> 81d4c829d24364be77a7cd1916983c05b12de9fa
=======
>>>>>>> 81d4c829d24364be77a7cd1916983c05b12de9fa
=======
>>>>>>> 81d4c829d24364be77a7cd1916983c05b12de9fa
        }
    }
}
