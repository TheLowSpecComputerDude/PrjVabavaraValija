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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly int _kategooriaId;
        private readonly ILoeAndmed _andmed;
        private readonly IRiistvara _riistvara;
        private readonly IHindamine _hindaja;

        public Window1(int kategooriaId)
        {
            InitializeComponent();

            _kategooriaId = kategooriaId;
            _andmed = new LoeAndmed();
            _riistvara = new Riistvara();
            _hindaja = new Hindamine();

            LaeKriteeriumid();
        }

        private void LaeKriteeriumid()
        {
            List<Kriteeriumid> kriteerium = _andmed.LoeKriteeriumidKategooriaJargi(_kategooriaId);

            foreach (Kriteeriumid criteria in kriteerium)
            {
                CheckBox cb = new CheckBox();
                cb.Content = criteria.Nimi;
                cb.Tag = criteria.Id;

                stkKriteeriumid.Children.Add(cb);
            }
        }

        private List<int> LoeValitudKriteeriumid()
        {
            List<int> valitudKriteeriumid = new List<int>();

            foreach (var checkbox in stkKriteeriumid.Children)
            {
                if(checkbox is CheckBox cb && cb.IsChecked == true)
                {
                    valitudKriteeriumid.Add((int)cb.Tag);
                }
            }

            return valitudKriteeriumid;
        }

        private void btnLeiaTarkvara_Click(object sender, RoutedEventArgs e)
        {
            List<int> valitudKriteeriumid = LoeValitudKriteeriumid();

            List<Tarkvaranõuded> sobivadTarkvarad = _andmed.LeiaSobivadTarkvarad(_riistvara, _kategooriaId);

            if(sobivadTarkvarad.Count == 0)
            {
                MessageBox.Show("Sinu arvutile sobivat tarkvara ei leitud!");
                return;
            }

            List<Skoorid> tulemused = _hindaja.HindaTarkvarad(sobivadTarkvarad, valitudKriteeriumid, _andmed);

            Window2 aken = new Window2(tulemused);
            aken.ShowDialog();
        }
    }

    
}
