using DealOrNoDeal.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DealOrNoDeal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {
        public static GameObject go;
        private StorageFile file;
        private string savePath;
        private Case userCase = GameLogic.userCase;
        private bool canPlay = false;

        ImageBrush brush1 = new ImageBrush();

        private TextBlock instructions;

        public Game()
        {
            brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/VisualAssets/CasePic.png"));
            this.InitializeComponent();
            if (go != null)
            {
                userCase = go.UserCase;
                GameLogic.cases = go.Cases;
            }
            else
            {
                GameLogic.ProduceCases();
            }
            ButtonCreation();
            ValueDisplayCreation();                       
        }

        private void ButtonCreation()
        {
            int count = 1;
            for (int i = 1; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (i == 4 && (j == 0 || j == 6))
                    {

                    }
                    else
                    {
                        string caseName = $"{count}";
                        Button b = new Button();
                        b.Content = caseName;
                        b.Background = brush1;
                        b.Width = 100;
                        b.Height = 90;
                        b.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetColumn(b, j);
                        Grid.SetRow(b, i);
                        Case c = GameLogic.cases[count - 1];
                        if (c.IsOpened)
                        {
                                b.IsEnabled = false;
                        }
                        gameGrid.Children.Add(b);
                        b.Click += Case_Click;
                        count++;
                    }
                }
            }
        }

        private void ValueDisplayCreation()
        {
            int count2 = 1;
            foreach (double value in GameLogic.Values)
            {
                TextBlock tb = new TextBlock();
                tb.Text = value.ToString();
                tb.HorizontalAlignment = HorizontalAlignment.Stretch;
                tb.VerticalAlignment = VerticalAlignment.Stretch;
                tb.TextAlignment = TextAlignment.Center;
                tb.Foreground = new SolidColorBrush(Colors.White);
                foreach (Case c in GameLogic.cases)
                {
                    if (c.CaseValue.ToString().Equals(value.ToString()) && c.IsOpened)
                    {
                        tb.TextDecorations = Windows.UI.Text.TextDecorations.Strikethrough;
                    }
                }
                if (count2 > 13)
                {
                    RightStackPanel.Children.Add(tb);
                }
                else
                {
                    LeftStackPanel.Children.Add(tb);
                }
                count2++;
            }
        }

        private void Case_Click(object sender, RoutedEventArgs e)
        {
            if (canPlay)
            {
                var b = sender as Button;
                CaseReveal(b);
            }  
        }

        private void CaseReveal(Button b)
        {
            if (b != null)
            {
                int.TryParse((string)b.Content, out int caseNumber);
                double caseValue = GameLogic.cases[caseNumber - 1].CaseValue;
                var i = RightStackPanel.Children.ToList();
                foreach (TextBlock v in i)
                {
                    double.TryParse(v.Text, out double d);
                    if (d == caseValue)
                    {
                        v.TextDecorations = Windows.UI.Text.TextDecorations.Strikethrough;
                        v.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
                var j = LeftStackPanel.Children.ToList();
                foreach (TextBlock v in j)
                {
                    double.TryParse(v.Text, out double d);
                    if (d == caseValue)
                    {
                        v.TextDecorations = Windows.UI.Text.TextDecorations.Strikethrough;
                        v.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
                b.IsEnabled = false;
                GameLogic.cases[caseNumber - 1].IsOpened = true;
                //instructions.Text = "You selected a case.";
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(savePath))
            {
                go = new GameObject();
                go.UserCase = userCase;
                go.Cases = GameLogic.cases;
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.FileTypeChoices.Add("type", new List<string> { ".dond" });
                XmlSerializer ser = new XmlSerializer(typeof(GameObject));
                file = await savePicker.PickSaveFileAsync();
                if (file != null)
                {
                    savePath = file.Path;
                    using (Stream fs = await file.OpenStreamForWriteAsync())
                    {
                        ser.Serialize(fs, go);
                    }
                }
            }
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            InstructionsStackPanel.Children.Remove(StartGameButton);
            instructions = new TextBlock();
            instructions.Margin = new Thickness(0, 300, 0, 0);
            instructions.TextWrapping = TextWrapping.WrapWholeWords;
            instructions.Text = "Please select your intial case. Your intial case will be your case filled with your potential prize money unless you take a deal with the dealer. Select wisely.";
            instructions.HorizontalAlignment = HorizontalAlignment.Center;
            instructions.VerticalAlignment = VerticalAlignment.Bottom;
            instructions.TextAlignment = TextAlignment.Center;
            InstructionsStackPanel.Children.Add(instructions);
            canPlay = true;
            instructions.Foreground = new SolidColorBrush(Colors.White);
            instructions.FontSize = 20;
        }

        private int buttoncount = 0;
        private void button_counter(object sender, EventArgs e)
        {
            //buttoncount++;
        }
    }
}
