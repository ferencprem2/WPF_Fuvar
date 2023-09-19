using System;
using System.Collections.Generic;
using System.IO;
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

namespace WPF_Fuvar
{
    public partial class MainWindow : Window
    {
        List<Fuvar> rideInfos = new();
        public MainWindow()
        {
            InitializeComponent();
            LoadData();

            foreach (var item in rideInfos)
            {
                taxiIDComboBox.Items.Add(item.TaxiId);
            }


        }
        private void LoadData()
        {
            rideInfos.Clear();
            StreamReader sr = new("fuvar.csv");
            sr.ReadLine();
            while (!sr.EndOfStream) 
            {
                string[] lines = sr.ReadLine().Split(";");
                Fuvar newRideInfos = new Fuvar(int.Parse(lines[0]), DateTime.Parse(lines[1]), int.Parse(lines[2]), double.Parse(lines[3]), double.Parse(lines[4]), double.Parse(lines[5]), lines[6]);
                rideInfos.Add(newRideInfos);
            }
            sr.Close();
        }
        private void thirdQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            thirdQuestionLabel.Content = $"3. feladat: {rideInfos.Count()} fuvar";
        }

        private void fourthQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (taxiIDComboBox.SelectedIndex != -1)
            {
                fourthQuestionLabel.Content = $"4. feladat: {rideInfos.Where(x => x.TaxiId == (int)taxiIDComboBox.SelectedItem).Count()} fuvar alatt: {Math.Round(rideInfos.Sum(x => x.Price + x.Tip), 2)}$";
            }
            else
            {
                MessageBox.Show("Kérem válasszon ki létező id-t");
            }
        }

        private void fifthQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            fifthQuestionLabel.Content = "5. feldat:";
            var payments = rideInfos.GroupBy(x => x.PaymentType).Select(y => new { Payment = y.Key, Count = y.Count() });
            foreach (var item in payments)
            {
                fifthQuestionListBox.Items.Add($"{item.Payment}: {item.Count} fuvar");
            }
        }

        private void sixthQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            sixthQuestionLabel.Content = $"6. feladat: {Math.Round(rideInfos.Sum(x => x.FullTripDistance) * 1.6, 2)} km";
        }

        private void seventhQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            seventhQuestionLabel.Content = "7. feladat: Leghosszabb fuvar: ";
            var longestTrip = rideInfos.Select(x => x).Max(y => y.FullTimeOfTheRiseInSec);
            foreach (var item in rideInfos.Where(x => x.FullTimeOfTheRiseInSec == longestTrip))
            {
                seventhQuestionListBox.Items.Add($"Fuvar hossza: {item.FullTimeOfTheRiseInSec} másodperc \n Taxi azonosító: {item.TaxiId} \n Megtett távolság: {Math.Round(item.FullTripDistance * 1.6, 2)}km \n Viteldík: {item.Price + item.Tip}$");
            }
        }

        private void eightQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            eightQuestionLabel.Content = "A fájl elkészült, neve: hibak.txt";

            StreamWriter sw = new("hibak.txt");
            sw.WriteLine("taxi_id;indulas;idotartam;tavolsag;viteldij;borravalo;fizetes_modja");
            var falseValue = rideInfos.Select(x => x).Where(x => x.FullTimeOfTheRiseInSec > 0 && x.Price > 0 && x.FullTripDistance == 0).OrderBy(x => x.StartingTime);
            foreach (var item in falseValue)
            {
                sw.WriteLine($"{item.TaxiId}; {item.StartingTime}; {item.FullTimeOfTheRiseInSec}; {item.FullTripDistance}; {item.Price}; {item.Tip}; {item.PaymentType}");
            }
        }
    }
}
