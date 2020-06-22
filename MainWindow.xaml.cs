using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PT_lab4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private List<Car> myCars;
        private MyCarList<Car> myCarsBindingList;

        private int lastFoundElement = -1;

        public MainWindow()
        {
            HelperClass.runAll();

            InitializeComponent();

            InitProperties();
            InitComboBox();

            myCarsBindingList = new MyCarList<Car>(this.myCars);
            MainDataGrid.ItemsSource = myCarsBindingList;

        }

        private void InitProperties()
        {
            this.myCars = new List<Car>(HelperClass.myCars);
            this.myCarsBindingList = new MyCarList<Car>(myCars);
        }

        private void ClearRowsGridStyle(int index = -1)
        {
            if (index == -1)
            {
                for (int i = 0; i < this.myCarsBindingList.Count; i++)
                {
                    var row = (DataGridRow)MainDataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                    row.Background = Brushes.White;
                }
            }
            else
            {
                var row = (DataGridRow)MainDataGrid.ItemContainerGenerator.ContainerFromIndex(index);
                row.Background = Brushes.White;
            }
        }

        private void InitComboBox()
        {
            BindingList<string> values = new BindingList<string>()
            {
                "Model",
                "Year",
                "Motor.Model",
                "Motor.Displacement",
                "Motor.HorsePower"
            };
            SearchComboBox.ItemsSource = values;
            SearchComboBox.SelectedIndex = 0;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            ClearRowsGridStyle(lastFoundElement);

            String searchValue = SearchTextBox.Text;
            String searchInColumn = (string)SearchComboBox.SelectedItem;

            int foundElement = -1;
            
            if (searchValue != "")
            {
                foundElement = this.myCarsBindingList.Find(searchInColumn, searchValue);

                if (foundElement != -1)
                {
                    var row = (DataGridRow)MainDataGrid.ItemContainerGenerator.ContainerFromIndex(foundElement);
                    row.Background = Brushes.Gray;

                    lastFoundElement = foundElement;
                }
            }

        }
    }
        
}
