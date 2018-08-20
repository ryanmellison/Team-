﻿using DealOrNoDeal.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private Case userCase;
        public Game()
        {
            int count = 1;
            this.InitializeComponent();
            if(go != null)
            {
                GameLogic.AllCases = go.AllCases;
                GameLogic.CurrentCases = go.CurrentCases;
                userCase = go.UserCase;
            }
            GameLogic.ProduceCases();

            

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
                        b.HorizontalAlignment = HorizontalAlignment.Stretch;
                        b.VerticalAlignment = VerticalAlignment.Stretch;
                        b.Margin = new Thickness(1);
                        b.Background = new SolidColorBrush(Colors.Beige);
                        Grid.SetColumn(b, j);
                        Grid.SetRow(b, i);
                        
                        if (!GameLogic.CurrentCases.TryGetValue(count, out double numCase))
                        {
                            b.IsEnabled = false;
                        }
                        gameGrid.Children.Add(b);
                        b.Click += Case_Click;
                        count++;
                    }
                }
            }
            int count2 = 1;
            foreach(double value in GameLogic.Values)
            {
                TextBlock tb = new TextBlock();
                tb.Text = value.ToString();
                tb.HorizontalAlignment = HorizontalAlignment.Stretch;
                tb.VerticalAlignment = VerticalAlignment.Stretch;
                tb.TextAlignment = TextAlignment.Center;
                //tb.TextDecorations = Windows.UI.Text.TextDecorations.Strikethrough;
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
            var b = sender as Button;
            CaseReveal(b);
        }

        private void CaseReveal(Button b)
        {
            if (b != null)
            {
                double.TryParse((string)b.Content, out double caseNumber);
                GameLogic.CurrentCases.TryGetValue(caseNumber, out double caseValue);
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
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(savePath))
            {
                go = new GameObject();
                go.AllCases = GameLogic.AllCases;
                go.CurrentCases = GameLogic.CurrentCases;
                go.UserCase = userCase;
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.FileTypeChoices.Add("type", new List<string> { ".dond" });
                file = await savePicker.PickSaveFileAsync();
                if (file != null)
                {
                    savePath = file.Path;
                    using (Stream fs = await file.OpenStreamForWriteAsync())
                    {
                        Serializer.Serialize(fs, go);
                    }
                }
            }
            else
            {
                using (Stream fs = await file.OpenStreamForWriteAsync())
                {
                    //Serializer.Serialize(fs, contacts);
                }
            }

        }

       
        
    }
}
