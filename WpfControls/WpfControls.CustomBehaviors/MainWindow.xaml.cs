using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfControls.CustomBehaviors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            dg.ItemsSource = new ObservableCollection<TestData>
            {
                new TestData { Column1Value = 45, Column2Value = 75 },
                new TestData { Column1Value = 55, Column2Value = 30 },
                new TestData { Column1Value = 150, Column2Value = 110 },
                new TestData { Column1Value = 10, Column2Value = 200 },
                new TestData { Column1Value = 200, Column2Value = 50 }
            };


         
        }
    }


    public class TestData
    {
        public double Column1Value { get; set; }
        public double Column2Value { get; set; }
    }
}
