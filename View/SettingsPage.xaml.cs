using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using NFL_Quiz.Model;
using NFL_Quiz.ViewModel;

namespace NFL_Quiz
{
    public partial class SettingsPage : Page
    {


        public SettingsPage()
        {
            InitializeComponent();
            this.DataContext = new SettingsPageViewModel();
        }
    }
}