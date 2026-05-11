using PrjAndmebaas;
using PrjHindamine;
using PrjRiistvara;
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
using System.Windows.Shapes;

namespace PrjVabavaraValijaAken
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class VabavaraValija : Window
    {
        private int _kategooriaId;
        private readonly ILoeAndmed _andmed;
        private readonly IRiistvara _riistvara;
        private readonly IHindamine _hindaja;
        public VabavaraValija()
        {
            InitializeComponent();
            _andmed = new LoeAndmed();
            _riistvara = new Riistvara();
            _hindaja = new Hindamine();
        }

        private void LaeKriteeriumid()
        {
            stkKriteeriumid.Children.Clear();

            List<Kriteeriumid> kriteerium = _andmed.LoeKriteeriumidKategooriaJargi(_kategooriaId);

            foreach (Kriteeriumid criteria in kriteerium)
            {
                CheckBox cb = new CheckBox();
                cb.Content = criteria.Nimi;
                cb.Tag = criteria;

                cb.Click += Kriteerium_Click;

                stkKriteeriumid.Children.Add(cb);
            }
        }

        private List<Kriteeriumid> LoeValitudKriteeriumid()
        {
            List<Kriteeriumid> valitudKriteeriumid = new List<Kriteeriumid>();

            foreach (var checkbox in stkKriteeriumid.Children)
            {
                if (checkbox is CheckBox cb && cb.IsChecked == true && cb.Tag is Kriteeriumid criteria)
                {
                    valitudKriteeriumid.Add(criteria);
                }
            }

            return valitudKriteeriumid;
        }

        private void PeidaVaated()
        {
            grdMainMenu.Visibility = Visibility.Collapsed;
            grdKategooriad.Visibility = Visibility.Collapsed;
            grdKriteeriumid.Visibility = Visibility.Collapsed;
            grdSoovitus.Visibility = Visibility.Collapsed;
            grdSoovitusTäpne.Visibility = Visibility.Collapsed;
            grdLitsentsEe.Visibility = Visibility.Collapsed;
            grdLitsentsEng.Visibility = Visibility.Collapsed;
        }

        private void AvaPeamenüü()
        {
            PeidaVaated();
            grdMainMenu.Visibility = Visibility.Visible;
        }

        private void AvaKategooriad()
        {
            PeidaVaated();
            grdKategooriad.Visibility = Visibility.Visible;
        }

        private void AvaKriteeriumid()
        {
            PeidaVaated();
            grdKriteeriumid.Visibility = Visibility.Visible;

            LaeKriteeriumid();
        }

        private void AvaSoovitused(List<Skoorid> tulemused)
        {
            lstTulemused.Items.Clear();

            foreach (Skoorid tulemus in tulemused)
            {
                lstTulemused.Items.Add(tulemus);
            }

            PeidaVaated();
            grdSoovitus.Visibility = Visibility.Visible;
        }

        private void AvaTäpneSoovitus(Skoorid tulemus)
        {
            PeidaVaated();

            txtTarkvaraNimi.Text = tulemus.Nimi;
            txtSkoor.Text = $"Sobivus: {tulemus.Skoor}/{tulemus.MaxSkoor}";

            lstSobivadKriteeriumid.ItemsSource = tulemus.SobivadKriteeriumid;
            lstPuuduvadKriteeriumid.ItemsSource = tulemus.PuuduvadKriteeriumid;

            grdSoovitusTäpne.Visibility = Visibility.Visible;
        }

        private void PuhastaKriteeriumiVaade()
        {
            stkKriteeriumid.Children.Clear();
            txtKriteeriumiKirjeldus.Text = "Vali funktsionaalsus, et näha selle kirjeldust";
        }

        private void btnAlusta_Click(object sender, RoutedEventArgs e)
        {
            AvaKategooriad();
        }

        private void BtnValju_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnLitsentsEng_Click(object sender, RoutedEventArgs e)
        {
            PeidaVaated();
            grdLitsentsEng.Visibility = Visibility.Visible;
        }

        private void BtnLitsentsEe_Click(object sender, RoutedEventArgs e)
        {
            PeidaVaated();
            grdLitsentsEe.Visibility = Visibility.Visible;
        }

        private void BtnLeiaTarkvara_Click(object sender, RoutedEventArgs e)
        {

            List<Kriteeriumid> valitudKriteeriumid = LoeValitudKriteeriumid();

            if (valitudKriteeriumid.Count == 0)
            {
                MessageBox.Show("Palun vali vähemalt üks funktsionaalsus!");
                return;
            }

            List<Tarkvaranõuded> sobivadTarkvarad = _andmed.LeiaSobivadTarkvarad(_riistvara.OSVersioon, _riistvara.RAM, _riistvara.VabaKettamaht, _kategooriaId);

            if (sobivadTarkvarad.Count == 0)
            {
                MessageBox.Show("Sinu arvutile sobivat tarkvara ei leitud!");
                return;
            }

            List<Skoorid> tulemused = _hindaja.HindaTarkvarad(sobivadTarkvarad, valitudKriteeriumid, _andmed);

            AvaSoovitused(tulemused);
        }

        private void LstTulemused_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(lstTulemused.SelectedItem is Skoorid tulemus)
            {
                AvaTäpneSoovitus(tulemus);
            }
        }

        private void BtnTagasiMain_Click(object sender, RoutedEventArgs e)
        {
            AvaPeamenüü();
        }
        private void BtnTagasiKategooriad_Click(object sender, RoutedEventArgs e)
        {
            PuhastaKriteeriumiVaade();
            AvaKategooriad();
        }

        private void BtnTagasiKriteeriumid_Click(object sender, RoutedEventArgs e)
        {
            AvaKriteeriumid();
        }

        private void BtnTagasiSoovitused_Click(object sender, RoutedEventArgs e)
        {
            PeidaVaated();
            grdSoovitus.Visibility = Visibility.Visible;
        }

        private void BtnBrauser_Click(object sender, RoutedEventArgs e)
        {
            _kategooriaId = 1;
            AvaKriteeriumid();
        }

        private void BtnKontor_Click(object sender, RoutedEventArgs e)
        {
            _kategooriaId = 2;
            AvaKriteeriumid();
        }

        private void BtnEmail_Click(object sender, RoutedEventArgs e)
        {
            _kategooriaId = 3;
            AvaKriteeriumid();
        }

        private void BtnKommunikatsioon_Click(object sender, RoutedEventArgs e)
        {
            _kategooriaId = 4;
            AvaKriteeriumid();
        }

        private void Kriteerium_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox cb && cb.Tag is Kriteeriumid criteria)
            {
                txtKriteeriumiKirjeldus.Text = criteria.Kirjeldus;
            }
        }
    }
}
