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
            var result = System.Windows.MessageBox.Show("Souhaitez-vous supprimer les messages qui ont étés envoyés avant le " + datePick.SelectedDate.ToString(), "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string delete = "DELETE FROM Messages WHERE temps < '" + datePick.SelectedDate.ToString() + "'";

                bool test = bd.delete(delete);
            }



        }
    }
}
