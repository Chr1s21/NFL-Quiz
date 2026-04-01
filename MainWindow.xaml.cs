using NFL_Quiz.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace NFL_Quiz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel vm = new MainWindowViewModel();
            DataContext = vm;


           // GamePage gamePage = new GamePage();
           // MainFrame.Content = gamePage;

        }

        /*private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            GamePage gamePage = new GamePage();
            MainFrame.Content = gamePage;
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsPage settingsPage = new SettingsPage();
            MainFrame.Content = settingsPage;
        }
        */
    }

}
