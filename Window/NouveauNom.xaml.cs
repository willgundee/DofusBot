using System.Linq;
using System.Windows;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour NouveauNom.xaml
    /// </summary>
    public partial class NouveauNom : Window
    {
        private string UUID { get; set; }
        private BDService bd = new BDService();
        public NouveauNom(string UUID)
        {
            InitializeComponent();
            this.UUID = UUID;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mW = (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(MainWindow)) as MainWindow);
            if (bd.Update("UPDATE Scripts SET nom =  '" + tb_NewName.Text + "' WHERE uuid  ='" + UUID + "';COMMIT;"))
            {
                mW.Player.LstScripts.First(x => x.Uuid == UUID).Nom = tb_NewName.Text;
            }
            Close();
        }

        private void tb_NewName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Button_Click(null, null);
            }
        }
    }
}
