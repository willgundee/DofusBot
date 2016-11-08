using System.Windows;
using System.Windows.Controls;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour pAdminMessages.xaml
    /// </summary>
    public partial class pAdminMessages : UserControl
    {
        BDService bd;

        public pAdminMessages()
        {
            InitializeComponent();
            bd = new BDService();
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            string delete = "DELETE";
        }
    }
}
