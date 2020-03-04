using Microsoft.Win32;
using PathInJson.WPF.Helpers;
using PathInJson.WPF.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PathInJson.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }
    }
}
