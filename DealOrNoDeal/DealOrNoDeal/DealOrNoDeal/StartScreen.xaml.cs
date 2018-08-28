using DealOrNoDeal.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
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
            Play("ms-appx:///Assets/AudioAssets/Theme Song.mp3");
        }

        private void Play(string fileName)
        {
            var mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"{fileName}", UriKind.RelativeOrAbsolute));
            mediaPlayer.Play();
            mediaPlayer.IsLoopingEnabled = true;
        }


        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Game), null, new DrillInNavigationTransitionInfo());
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
            XmlSerializer ser = new XmlSerializer(typeof(GameObject));
            if (file != null)
            {
                using (Stream st = await file.OpenStreamForReadAsync())
                {
                    GameObject go = (GameObject)ser.Deserialize(st);
                    Game.go = new GameObject();
                    Game.go = go;
                }
                this.Frame.Navigate(typeof(Game), null, new DrillInNavigationTransitionInfo());


            }

        }
    }
}
