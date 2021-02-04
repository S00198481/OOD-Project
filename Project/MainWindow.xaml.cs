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

namespace Project
{
    /// LARGE ERROR PRESENT
    /// LOGIC ERROR - LBX_NULL ON LOAD
    /// MUST AVOID PROGRAM FROM JUMPING INTO SELECTION BOX CHANGED ON LOAD
    /// 
    public partial class MainWindow : Window
    {
        static List<Car> CarList = new List<Car>();
        static List<Car> DisplayList = new List<Car>();

        public MainWindow()
        {
            

            InitializeComponent();
        }

        public void LoadCars()
        {
            Coupe MazdaMx5 = new Coupe("Mazda MX-5", 191, 9.7, 108, 134, 7200, 35);
            CarList.Add(MazdaMx5);
            Hatchback CivicTR = new Hatchback("Honda Civic Type-R", 270, 5.7, 306, 400, 8000, 39);
            CarList.Add(CivicTR);

            if (lbx_Cars.ItemsSource != null)
                lbx_Cars.ItemsSource = null;

            ComboBoxItem item = (ComboBoxItem)cbx_CarClass.SelectedItem;
            string selection = item.Content.ToString();

            switch (selection)
            {
                case "All":
                    foreach (Car car in CarList)
                    {
                        DisplayList.Add(car);
                    }
                    break;
                case "Coupe":
                    foreach (Coupe car in CarList)
                    {
                        DisplayList.Add(car);
                    }
                    break;
                case "Hatchback":
                    foreach (Hatchback car in CarList)
                    {
                        DisplayList.Add(car);
                    }
                    break;
                case "Saloon":
                    foreach (Saloon car in CarList)
                    {
                        DisplayList.Add(car);
                    }
                    break;
                case "Estate":
                    foreach (Estate car in CarList)
                    {
                        DisplayList.Add(car);
                    }
                    break;
                default:
                    break;
            }

            lbx_Cars.ItemsSource = DisplayList; 
        }

        private void cbx_CarClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         

            ComboBoxItem item = (ComboBoxItem)cbx_CarClass.SelectedItem;
            string selection = item.Content.ToString();

            switch(selection)
            {
                case "All":
                    foreach (Car car in CarList)
                    {
                        DisplayList.Add(car);
                    }
                    break;
                case "Coupe":
                    foreach (Coupe car in CarList)
                    {
                        DisplayList.Add(car);
                    }
                    break;
                case "Hatchback":
                    foreach (Hatchback car in CarList)
                    {
                        DisplayList.Add(car);
                    }
                    break;
                case "Saloon":
                    foreach (Saloon car in CarList)
                    {
                        DisplayList.Add(car);
                    }
                    break;
                case "Estate":
                    foreach (Estate car in CarList)
                    {
                        DisplayList.Add(car);
                    }
                    break;
                default:
                    break;
            }

            lbx_Cars.ItemsSource = DisplayList;
        }

        private void lbx_Cars_Initialized(object sender, EventArgs e)
        {
            LoadCars();
        }
    }
}
