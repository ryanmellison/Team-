﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DealOrNoDeal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EndGameScreen : Page
    {
        public EndGameScreen()
        {
            this.InitializeComponent();
            EndText();
        }

        private void EndText()
        {
            double moneyWon = Game.offer;
            TextBlock EndGameText = new TextBlock();
            EndGameText.HorizontalAlignment = HorizontalAlignment.Center;
            EndGameText.VerticalAlignment = VerticalAlignment.Center;
            EndGameText.FontSize = 50;
            EndGameText.Text = $"You're going home with: ${moneyWon} in cash!";
            EndGameText.Foreground = new SolidColorBrush(Colors.White);
            EndGameStackPanel.Children.Add(EndGameText);
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            Game.go = null;
            this.Frame.Navigate(typeof(MainPage));
        }

    }
}
