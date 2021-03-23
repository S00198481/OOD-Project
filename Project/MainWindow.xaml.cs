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
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

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
        static bool loadingModdedCar = false;
        Model1Container db = new Model1Container();

        public MainWindow()
        {
            InitializeComponent();

            loaded = true;

            imgDetailsPageImage.Source = new BitmapImage(new Uri("", UriKind.Relative));
        }

        public void LoadCars()
        {
            //here we declare our car objects
            //in future, these should be loaded from a CSV file
            Coupe MazdaMx5 = new Coupe("Mazda MX-5", 191, 9.7, 108, 134, 7200, 35, "/images/mx5.jpg");
            CarList.Add(MazdaMx5);
            Hatchback CivicTR = new Hatchback("Honda Civic Type-R", 270, 5.7, 306, 400, 8000, 39, "/images/civictr.jpg");
            CarList.Add(CivicTR);
            Saloon Impreza = new Saloon("Subaru Impreza WRX", 230, 5.9, 277, 320, 6500, 27, "/images/impreza.jpg");
            CarList.Add(Impreza);
            Estate RS6 = new Estate("Audi RS6 Avant", 250, 3.6, 592, 800, 6000, 24, "/images/rs6.jpg");
            CarList.Add(RS6);

            var query = from c in db.CarTBLs
                        where c.Id == 1
                        select c;

            var car = query.ToList();

            ReloadCars();
        }

        private void ReloadCars()
        {
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
                case "Modded":
                    foreach (Car car in CarList)
                    {
                        if (car is Modded)
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
            
        }

        private void lbx_Cars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //first we set the relevant text blocks back to null
            tblk_CarStats.Text = "Performance Stats : ";
            tblk_SelectedCar.Text = "";
            tblk_Mods.Text = "Modifications : ";

            //we get the selected car
            Car SelectedCar = lbx_Cars.SelectedItem as Car;

            if(SelectedCar == null)
            {
                return;
            }

            if(SelectedCar.Mods != null)
            {
                FillModFields(SelectedCar);
            }

            //set car image and name in other tab
             tblkDetailsPageName.Text = "";
             tblkDetailsPageName.Text = " " + SelectedCar.Name;
             imgDetailsPageImage.Source = new BitmapImage(new Uri(SelectedCar.ImageUrl, UriKind.Relative));


            //we set the car's performance number into the CarStats text block
            tblk_CarStats.Text += String.Format("\n\nTop Speed : {0}KM/H\n\n0-100KM/H Time : {1}s\n\nHorsepower : {2}bhp\n\nTorque : {3}Nm\n\nMax RPM : {4}\n\nMPG : {5}Mpg", 
                SelectedCar.TopSpeed, SelectedCar.ZeroTo100, SelectedCar.Horsepower, SelectedCar.Torque, SelectedCar.MaxRpm, SelectedCar.FuelMpg);

            //we set the name title block to the car's name
            tblk_SelectedCar.Text = SelectedCar.Name;
        }

        private void FillModFields(Car SelectedCar)
        {
            List<Modification> ModsCopy = new List<Modification>();

            ModsCopy = SelectedCar.Mods;

            loadingModdedCar = true;

            foreach (Modification modification in ModsCopy)
            {
                switch (modification.Name)
                {
                    case "Engine":
                        cbx_Engine.SelectedIndex = modification.Index;
                        break;
                    case "Exhaust":
                        cbx_Exhaust.SelectedIndex = modification.Index;
                        break;
                    case "Supercharger":
                        cbx_Super.SelectedIndex = modification.Index;
                        break;
                    case "Turbo":
                        cbx_Turbo.SelectedIndex = modification.Index;
                        break;
                    case "Brakes":
                        cbx_Brakes.SelectedIndex = modification.Index;
                        break;
                    case "Tires":
                        cbx_Tires.SelectedIndex = modification.Index;
                        break;
                    case "Suspension":
                        cbx_Suspension.SelectedIndex = modification.Index;
                        break;
                    case "Name":
                        tbx_Name.Text = modification.SetupName;
                        break;
                    default:
                        break;
                }
            }

            loadingModdedCar = false;
        }

        private void UpdateMods()
        {
            Car SelectedCar = lbx_Cars.SelectedItem as Car;

            tblk_Mods.Text = "Modifications : ";
            tblk_CarStats.Text = "Performance Stats : ";

            int totalHp = 0;
            foreach (Modification mod in SelectedCar.Mods)
            {
                totalHp += mod.HorsepowerMod;
            }
            int totalSpeed = 0;
            foreach (Modification mod in SelectedCar.Mods)
            {
                totalSpeed += mod.TopSpeedMod;
            }
            double acceleration = 0;
            foreach (Modification mod in SelectedCar.Mods)
            {
                acceleration += mod.ZeroTo100Mod;
            }

            tblk_Mods.Text += String.Format("\n\nTop Speed : {0}KM/H\n0-100KM/H Time : {1}s\nHorsepower : {2}bhp",
               totalSpeed, acceleration, totalHp);

            tblk_CarStats.Text += String.Format("\n\nTop Speed : {0}KM/H\n\n0-100KM/H Time : {1}s\n\nHorsepower : {2}bhp\n\nTorque : {3}Nm\n\nMax RPM : {4}\n\nMPG : {5}Mpg",
                SelectedCar.TopSpeed+totalSpeed, SelectedCar.ZeroTo100+acceleration, SelectedCar.Horsepower+totalHp, SelectedCar.Torque, SelectedCar.MaxRpm, SelectedCar.FuelMpg);
        }

        private void cbx_Engine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaded == false)
                return;

            Car SelectedCar = lbx_Cars.SelectedItem as Car;

            if (SelectedCar == null)
                return;

            //first we get the value of the engine chosen
            ComboBoxItem item = (ComboBoxItem)cbx_Engine.SelectedItem;
            int index = cbx_Engine.Items.IndexOf(item);

            if (loadingModdedCar == false)
            {
                if (SelectedCar.Mods != null)
                {
                    RemoveMod("Engine", SelectedCar);
                }

                switch (index)
                {
                    case 0:
                        break;
                    case 1:
                        SelectedCar.Mods.Add(new Modification("Engine", 0, 10, -.4, 1));
                        break;
                    case 2:
                        SelectedCar.Mods.Add(new Modification("Engine", 10, 25, 0, 2));
                        break;
                    case 3:
                        SelectedCar.Mods.Add(new Modification("Engine", 10, 35, -.5, 3));
                        break;
                    case 4:
                        SelectedCar.Mods.Add(new Modification("Engine", 20, 50, .6, 4));
                        break;
                    case 5:
                        SelectedCar.Mods.Add(new Modification("Engine", -30, 20, -.75, 5));
                        break;
                }
            }
            UpdateMods();
        }

        private void cbx_Exhaust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaded == false)
                return;

            //first we get the value of the engine chosen
            ComboBoxItem item = (ComboBoxItem)cbx_Exhaust.SelectedItem;
            int index = cbx_Exhaust.Items.IndexOf(item);

            Car SelectedCar = lbx_Cars.SelectedItem as Car;

            if (SelectedCar == null)
                return;

            if (loadingModdedCar == false)
            {
                if (SelectedCar.Mods != null)
                {
                    RemoveMod("Exhaust", SelectedCar);
                }

                switch (index)
                {
                    case 0:
                        break;
                    case 1:
                        SelectedCar.Mods.Add(new Modification("Exhaust", 0, 5, 0, 1));
                        break;
                    case 2:
                        SelectedCar.Mods.Add(new Modification("Exhaust", 0, 10, 0, 2));
                        break;
                    case 3:
                        SelectedCar.Mods.Add(new Modification("Exhaust", 0, 5, 0, 3));
                        break;
                }
            }
            UpdateMods();
        }

        private void cbx_Turbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaded == false)
                return;

            //first we get the value of the engine chosen
            ComboBoxItem item = (ComboBoxItem)cbx_Turbo.SelectedItem;
            int index = cbx_Turbo.Items.IndexOf(item);

            Car SelectedCar = lbx_Cars.SelectedItem as Car;

            if (SelectedCar == null)
                return;

            if (loadingModdedCar == false)
            {
                if (SelectedCar.Mods != null)
                {
                    RemoveMod("Turbo", SelectedCar);
                }

                switch (index)
                {
                    case 0:
                        break;
                    case 1:
                        SelectedCar.Mods.Add(new Modification("Turbo", 30, 100, 2, 1));
                        break;
                    case 2:
                        SelectedCar.Mods.Add(new Modification("Turbo", 30, 150, 1, 2));
                        break;
                }
            }
            UpdateMods();
        }

        private void cbx_Super_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaded == false)
                return;

            //first we get the value of the engine chosen
            ComboBoxItem item = (ComboBoxItem)cbx_Super.SelectedItem;
            int index = cbx_Super.Items.IndexOf(item);

            Car SelectedCar = lbx_Cars.SelectedItem as Car;

            if (SelectedCar == null)
                return;

            if (loadingModdedCar == false)
            {
                if (SelectedCar.Mods != null)
                {
                    RemoveMod("Supercharger", SelectedCar);
                }

                switch (index)
                {
                    case 0:
                        break;
                    case 1:
                        SelectedCar.Mods.Add(new Modification("Supercharger", 0, 50, -.7, 1));
                        break;
                    case 2:
                        SelectedCar.Mods.Add(new Modification("Supercharger", 0, 75, -1, 2));
                        break;
                }
            }
            UpdateMods();
        }

        private void cbx_Brakes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaded == false)
                return;

            //first we get the value of the engine chosen
            ComboBoxItem item = (ComboBoxItem)cbx_Brakes.SelectedItem;
            int index = cbx_Brakes.Items.IndexOf(item);

            Car SelectedCar = lbx_Cars.SelectedItem as Car;

            if (SelectedCar == null)
                return;

            if (loadingModdedCar == false)
            {
                if (SelectedCar.Mods != null)
                {
                    RemoveMod("Brakes", SelectedCar);
                }

                switch (index)
                {
                    case 0:
                        break;
                    case 1:
                        SelectedCar.Mods.Add(new Modification("Brakes", 0, 0, 0, 1));
                        break;
                    case 2:
                        SelectedCar.Mods.Add(new Modification("Brakes", 0, 0, 0, 2));
                        break;
                }
            }
            UpdateMods();
        }

        private void cbx_Suspension_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaded == false)
                return;

            //first we get the value of the engine chosen
            ComboBoxItem item = (ComboBoxItem)cbx_Suspension.SelectedItem;
            int index = cbx_Suspension.Items.IndexOf(item);

            Car SelectedCar = lbx_Cars.SelectedItem as Car;

            if (SelectedCar == null)
                return;

            if (loadingModdedCar == false)
            {
                if (SelectedCar.Mods != null)
                {
                    RemoveMod("Suspension", SelectedCar);
                }

                switch (index)
                {
                    case 0:
                        break;
                    case 1:
                        SelectedCar.Mods.Add(new Modification("Suspension", 0, 0, 0, 1));
                        break;
                    case 2:
                        SelectedCar.Mods.Add(new Modification("Suspension", 0, 0, 0, 2));
                        break;
                    case 3:
                        SelectedCar.Mods.Add(new Modification("Suspension", 0, 0, 0, 3));
                        break;
                }
            }
            UpdateMods();
        }

        private void cbx_Tires_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaded == false)
                return;

            //first we get the value of the engine chosen
            ComboBoxItem item = (ComboBoxItem)cbx_Tires.SelectedItem;
            int index = cbx_Tires.Items.IndexOf(item);

            Car SelectedCar = lbx_Cars.SelectedItem as Car;

            if (SelectedCar == null)
                return;

            if (loadingModdedCar == false)
            {
                if (SelectedCar.Mods != null)
                {
                    RemoveMod("Tires", SelectedCar);
                }

                switch (index)
                {
                    case 0:
                        break;
                    case 1:
                        SelectedCar.Mods.Add(new Modification("Tires", 5, 0, 0, 1));
                        break;
                    case 2:
                        SelectedCar.Mods.Add(new Modification("Tires", 10, 0, 1, 2));
                        break;
                    case 3:
                        SelectedCar.Mods.Add(new Modification("Tires", -10, 0, -.5, 2));
                        break;
                }
            }
            UpdateMods();
        }

        private void RemoveMod(string chosenModification, Car SelectedCar)
        {
            bool modToRemove = false;
            Modification RemoveMod = null;
            foreach (Modification mod in SelectedCar.Mods)
            {
                if (mod.Name == chosenModification)
                {
                    modToRemove = true;
                    RemoveMod = mod;
                }
            }
            if (modToRemove)
                SelectedCar.Mods.Remove(RemoveMod);
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            Car SelectedCar = lbx_Cars.SelectedItem as Car;
            string filename, filepath;

            if (tbx_Name != null)
            {
                SelectedCar.Mods.Add(new Modification("Name", tbx_Name.Text));
                filename = tbx_Name.Text;

            }
            else
            {
                SelectedCar.Mods.Add(new Modification("Name", SelectedCar.Name));
                filename = SelectedCar.Name;
            }

            string json = JsonConvert.SerializeObject(SelectedCar, Formatting.Indented);
            filepath = string.Format(@"C:\temp\{0}", filename);


            using (StreamWriter sw = new StreamWriter(filepath))
            {
                sw.Write(json);
            }
        }

        private void btn_Load_Click(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\temp";

                if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            CarList.Add(JsonConvert.DeserializeObject<Modded>(fileContent));

            Car NewCar = CarList[CarList.Count - 1];
            foreach (Modification modification in NewCar.Mods)
            {
                if(modification.SetupName != null)
                {
                    NewCar.Name = modification.SetupName;
                }
            }

        }

        private void btn_Reload_Click(object sender, RoutedEventArgs e)
        {
            ReloadCars();
        }
    }
}
