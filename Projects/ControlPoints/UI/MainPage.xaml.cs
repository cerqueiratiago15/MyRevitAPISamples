using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ControlPoints
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public ViewModel Model { get; set; }
        public MainPage(ViewModel model)
        {
            InitializeComponent();
            this.Model = model;
            this.DataContext = this.Model;

        }

        private void lbMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            this.Model.SelectedRemove = new System.Collections.ObjectModel.ObservableCollection<ViewModel.MyParameters>(lbMain.SelectedItems.OfType<ViewModel.MyParameters>().ToList());

        }
    }
}
