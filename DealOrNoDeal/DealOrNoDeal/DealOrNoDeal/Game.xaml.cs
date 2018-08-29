using DealOrNoDeal.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
        private int remainingTurnIteration = 6;
        private int buttonCount = 0;
        public static double offer;
        private int openedCases;

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
                buttonCount = go.TurnCycle;
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
                        b.Tapped += Button_Tapped;

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
                        tb.Foreground = new SolidColorBrush(Colors.Black);
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
                    if (d == caseValue && buttonCount > 0)
                    {
                        v.TextDecorations = Windows.UI.Text.TextDecorations.Strikethrough;
                        v.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
                var j = LeftStackPanel.Children.ToList();
                foreach (TextBlock v in j)
                {
                    double.TryParse(v.Text, out double d);
                    if (d == caseValue && buttonCount > 0)
                    {
                        v.TextDecorations = Windows.UI.Text.TextDecorations.Strikethrough;
                        v.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
                b.IsEnabled = false;
                GameLogic.cases[caseNumber - 1].IsOpened = true;
                GameContinue(caseNumber, b);
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(savePath))
            {
                go = new GameObject();
                go.UserCase = userCase;
                go.Cases = GameLogic.cases;
                go.TurnCycle = buttonCount;
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

        private async void GameContinue(int caseNumber, Button b)
        {
            var inst = instructions;
            b.IsEnabled = false;
            GameLogic.cases[caseNumber - 1].IsOpened = true;
            userCase = GameLogic.cases[caseNumber - 1];

            inst.Text = $"Choose {remainingTurnIteration} more cases.";

            if (buttonCount == 6)
            {
                canPlay = false;
                await CallTheBanker();
                remainingTurnIteration = 5;
                inst.Text = $"Let's continue! Choose {remainingTurnIteration} more cases.";
                canPlay = true;
            }
            if (buttonCount == 11)
            {
                canPlay = false;
                await CallTheBanker();
                remainingTurnIteration = 4;
                inst.Text = $"Let's continue! Choose {remainingTurnIteration} more cases.";
                canPlay = true;
            }
            if (buttonCount == 15)
            {
                canPlay = false;
                await CallTheBanker();
                remainingTurnIteration = 3;
                inst.Text = $"Let's continue! Choose {remainingTurnIteration} more cases.";
                canPlay = true;
            }
            if (buttonCount == 18)
            {
                canPlay = false;
                await CallTheBanker();
                remainingTurnIteration = 2;
                inst.Text = $"Let's continue! Choose {remainingTurnIteration} more cases.";
                canPlay = true;
            }
            if (buttonCount == 20)
            {
                canPlay = false;
                await CallTheBanker();
                remainingTurnIteration = 1;
                inst.Text = $"Let's continue! Choose {remainingTurnIteration} more cases.";
                canPlay = true;
            }
            if (buttonCount >= 21)
            {
                int count = 0;
                foreach(Case c in GameLogic.cases)
                {
                    if(c.IsOpened == false)
                    {
                        count++;
                    }
                }
                canPlay = false;
                if(count == 1)
                {
                    await CallTheBanker();
                }
                remainingTurnIteration = 1;
                inst.Text = $"Let's continue! Choose {remainingTurnIteration} more cases.";
                canPlay = true;
            }
            remainingTurnIteration--;
            if (buttonCount >= 24)
            {
                inst.Text = "Game over!";
                GameCannotContinue(caseNumber);
            }
        }

        private void GameCannotContinue(int caseNumber)
        {
            foreach (Case c in GameLogic.cases)
            {
                if (c.IsOpened == true)
                {
                    openedCases++;
                }
            }
            if (openedCases >= 26)
            {
                offer = userCase.CaseValue;
                this.Frame.Navigate(typeof(EndGameScreen));
            }
        }

        private async Task CallTheBanker()
        {
            await dealerPop.ShowAsync();
            if (remainingTurnIteration > 1)
            {
                remainingTurnIteration--;
            }
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            buttonCount += 1;
        }

        private void DealerPop_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Frame.Navigate(typeof(EndGameScreen));
        }

        private void DealerPop_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //nothing to be done, the popup is dismissed and the game continues
        }

        private void DealerPop_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            offer = GameLogic.BankerOffer();
            offer = Math.Round(offer, 2);
            dealerPop.Content = $"The Banker has offered you ${offer} for your case";
        }
    }
}