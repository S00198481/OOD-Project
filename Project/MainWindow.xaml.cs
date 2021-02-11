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
    /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        Author - Cian Tivnan S00198481
        Date Started - 04/02/2020
        Last Edited - 04/02/2020
        Github Repo - https://github.com/CianTivnan/OOD-Project
        
        Desc : This WPF program allows a user to select and customise a car
        configuration. They can save this configuration and save it and load
        it later. Performance stats for the car are updated as modifications
        are applied.
    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
    public partial class MainWindow : Window
    {
        //here we declare our static variables
        //lists of car objects and a bool for when the page elements have finished loading
        static List<Car> CarList = new List<Car>();
        static List<Car> DisplayList = new List<Car>();
        static bool loaded = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoadCars()
        {
            //here we declare our car objects
            //in future, these should be loaded from a CSV file
            Coupe MazdaMx5 = new Coupe("Mazda MX-5", 191, 9.7, 108, 134, 7200, 35);
            CarList.Add(MazdaMx5);
            Hatchback CivicTR = new Hatchback("Honda Civic Type-R", 270, 5.7, 306, 400, 8000, 39);
            CarList.Add(CivicTR);
            Saloon Impreza = new Saloon("Subaru Impreza WRX", 230, 5.9, 277, 320, 6500, 27);
            CarList.Add(Impreza);
            Estate RS6 = new Estate("Audi RS6 Avant", 250, 3.6, 592, 800, 6000, 24);
            CarList.Add(RS6);

            //set listbox to null
            if (lbx_Cars.ItemsSource != null)
                lbx_Cars.ItemsSource = null;

            //add each car to display list
            foreach (Car car in CarList)
            {
                DisplayList.Add(car);
            }

            //we set our listbox sort back to the display list
            lbx_Cars.ItemsSource = DisplayList; 
        }

        private void cbx_CarClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //here we check if the page has loaded. this method is always called as soon as the combo box loads
            //we do not want this, so when this method gets called before the listbox is ready we return
            //this bool will be set to true once the listbox is initialised
            if(loaded == false)
            {
                return;
            }

            //set lbx source to null
            lbx_Cars.ItemsSource = null;

            //clear display list
            DisplayList.Clear();

            //get string value of selected item
            ComboBoxItem item = (ComboBoxItem)cbx_CarClass.SelectedItem;
            string selection = item.Content.ToString();
    
            //switch statement to load selected car class into display list
            switch(selection)
            {
                case "All":
                    foreach (Car car in CarList)
                    {
                        DisplayList.Add(car);
                    }
                    break;
                case "Coupe":
                    foreach (Car car in CarList)
                    {
                        if(car is Coupe)
                            DisplayList.Add(car);
                    }
                    break;
                case "Hatchback":
                    foreach (Car car in CarList)
                    {
                        if(car is Hatchback)
                            DisplayList.Add(car);
                    }
                    break;
                case "Saloon":
                    foreach (Car car in CarList)
                    {
                        if(car is Saloon)
                            DisplayList.Add(car);
                    }
                    break;
                case "Estate":
                    foreach (Car car in CarList)
                    {
                        if(car is Estate)
                            DisplayList.Add(car);
                    }
                    break;
                default:
                    break;
            }

            //we set our list box back to the display list
            lbx_Cars.ItemsSource = DisplayList;
        }

        private void lbx_Cars_Initialized(object sender, EventArgs e)
        {
            //we call the loadcars method
            LoadCars();
            //and set the loaded method to true
            loaded = true;
        }

        private void lbx_Cars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //first we set the relevant text blocks back to null
            tblk_CarStats.Text = "Performance Stats : ";
            tblk_SelectedCar.Text = "";
            tblk_Mods.Text = "Modification Stats : ";

            //we get the selected car
            Car SelectedCar = lbx_Cars.SelectedItem as Car;

            //we set the car's performance number into the CarStats text block
            tblk_CarStats.Text += String.Format("\n\nTop Speed : {0}KM/H\n\n0-100KM/H Time : {1}s\n\nHorsepower : {2}bhp\n\nTorque : {3}Nm\n\nMax RPM : {4}\n\nMPG : {5}Mpg", 
                SelectedCar.TopSpeed, SelectedCar.ZeroTo100, SelectedCar.Horsepower, SelectedCar.Torque, SelectedCar.MaxRpm, SelectedCar.FuelMpg);

            //we set the name title block to the car's name
            tblk_SelectedCar.Text = SelectedCar.Name;
        }
    }
}
