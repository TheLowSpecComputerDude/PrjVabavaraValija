using PrjAndmebaas;
using PrjRiistvara;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrjVabavaraValijaAken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRiistvara _riistvara;

        public MainWindow()
        {
            InitializeComponent();
            _riistvara = new Riistvara();
            
            lblOSName.Content = _riistvara.OSVersion.ToString();
            lblRAMAmount.Content = _riistvara.RAM.ToString("F3");
            lblDriveSize.Content = _riistvara.FreeSpace.ToString("F2");
            lblDriveName.Content = _riistvara.Drive.ToString();

            ILoeAndmed tarkvarad = new LoeAndmed();
            //List<Tarkvaranõuded> sobivad = tarkvarad.LeiaSobivadTarkvarad(riistvara);

           // foreach (Tarkvaranõuded sobivtarkvara in sobivad)
            //{
               // lbxSobivadTarkvarad.Items.Add(sobivtarkvara.Nimi);
           // }


        }

        private void btnKontoritarkvara_Click(object sender, RoutedEventArgs e)
        {
            Window1 aken = new Window1(1);
            aken.ShowDialog();
        }

        private void btnVeebibrauser_Click(object sender, RoutedEventArgs e)
        {
            Window1 aken = new Window1(2);
            aken.ShowDialog();
        }

        private void btnKommunikatsioon_Click(object sender, RoutedEventArgs e)
        {
            Window1 aken = new Window1(3);
            aken.ShowDialog();
        }

        private void btnFailihaldus_Click(object sender, RoutedEventArgs e)
        {
            Window1 aken = new Window1(4);
            aken.ShowDialog();
        }
    }
}