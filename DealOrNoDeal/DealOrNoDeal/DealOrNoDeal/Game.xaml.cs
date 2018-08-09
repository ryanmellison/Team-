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
    public sealed partial class Game : Page
    {
        public Game()
        {
            int count = 1;
            this.InitializeComponent();
            for(int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string caseName = $"Case {count}";
                    Button b = new Button();
                    b.Content = caseName;
                    b.HorizontalAlignment = HorizontalAlignment.Stretch;
                    b.VerticalAlignment = VerticalAlignment.Stretch;
                    b.Margin = new Thickness(1);
                    b.Background = new SolidColorBrush(Colors.Beige);
                    if (j == 4 && i <=0 && i != 6)
                    {
                        Grid.SetColumn(b, i);
                        Grid.SetRow(b, j+1);
                    }
                    else
                    {
                        Grid.SetColumn(b, i);
                        Grid.SetRow(b, j);
                    }
                    gameGrid.Children.Add(b);
                    count++;
                }
            }
        }
    }
}
