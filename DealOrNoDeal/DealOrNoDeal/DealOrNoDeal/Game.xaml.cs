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

        ImageBrush brush1 = new ImageBrush();

        private TextBlock instructions;

        public Game()
        {
            brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/VisualAssets/CasePic.png"));
            this.InitializeComponent();
            if (go != null)
            {
                userCase = go.UserCase;
            }
            GameLogic.ProduceCases();
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
                        //b.VerticalAlignment = VerticalAlignment.Stretch;
                        //b.Margin = new Thickness(5);
                        //b.Background = new SolidColorBrush(Colors.Beige);
                        Grid.SetColumn(b, j);
                        Grid.SetRow(b, i);
                        b.IsEnabled = false;
                        gameGrid.Children.Add(b);
                        //button.DataContext = cell;
                        //BoolToBrushConverter con = new BoolToBrushConverter();
                        //Binding b = new Binding();
                        //b.Path = new PropertyPath("IsAlive");
                        //b.Mode = BindingMode.TwoWay;
                        //b.Converter = con;
                        //button.SetBinding(Button.BackgroundProperty, b);
                        //button.Click += cell.Toggle;
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

                //if (c.IsOpened)
                //{
                //    tb.TextDecorations = Windows.UI.Text.TextDecorations.Strikethrough;
                //}

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
            foreach(Case c in GameLogic.cases)
            {
                if (c.IsOpened)
                {
                    var i = RightStackPanel.Children.ToList();
                    foreach (TextBlock v in i)
                    {
                        double.TryParse(v.Text, out double d);
                        if (d == c.CaseValue)
                        {
                            v.TextDecorations = Windows.UI.Text.TextDecorations.Strikethrough;
                        }
                    }
                    var j = LeftStackPanel.Children.ToList();
                    foreach (TextBlock v in j)
                    {
                        double.TryParse(v.Text, out double d);
                        if (d == c.CaseValue)
                        {
                            v.TextDecorations = Windows.UI.Text.TextDecorations.Strikethrough;
                        }
                    }
                }
            }
        }

        private void Case_Click(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            CaseReveal(b);
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
                    }
                }
                var j = LeftStackPanel.Children.ToList();
                foreach (TextBlock v in j)
                {
                    double.TryParse(v.Text, out double d);
                    if (d == caseValue)
                    {
                        v.TextDecorations = Windows.UI.Text.TextDecorations.Strikethrough;
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
                go.UserCase = new Case() { CaseNumber = GameLogic.userCase.CaseNumber, CaseValue = GameLogic.userCase.CaseValue, IsOpened = GameLogic.userCase.IsOpened };
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
            instructions.FontSize = 20;
            instructions.Foreground = new SolidColorBrush(Colors.White);
            var list = gameGrid.Children.ToList();
            for(int i = 4; i < 30; i++)
            {
                Button b = (Button)list[i];
                b.IsEnabled = true;
                //b.Click += button_counter;
            }
        }

        private int buttoncount = 0;
        private void button_counter(object sender, EventArgs e)
        {
            //buttoncount++;
        }
    }
}
