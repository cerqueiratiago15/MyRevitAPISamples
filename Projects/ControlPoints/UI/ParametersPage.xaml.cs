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
    /// Interaction logic for ParametersPage.xaml
    /// </summary>
    public partial class ParametersPage : Page
    {
        public ViewModel Model { get; set; }
        public ParametersPage(ViewModel model)
        {
            InitializeComponent();
            this.Model = model;
            this.DataContext = this.Model;
        }

        private void parametersBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Model.SelectedParameters = new System.Collections.ObjectModel.ObservableCollection<ViewModel.MyParameters>(lboxAvailablep.SelectedItems.OfType<ViewModel.MyParameters>().ToList());
        }
    }
}
