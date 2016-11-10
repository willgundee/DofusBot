using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Configuration;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour pAdminMessages.xaml
    /// </summary>
    public partial class pAdminMessages : UserControl
    {
        BDService bd;

        private ObservableCollection<Rapport> lstRapport;

        public pAdminMessages()
        {
            InitializeComponent();
            bd = new BDService();
            lstRapport = new ObservableCollection<Rapport>();
            SelectRaports();


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


        private void supprimerRap(Rapport r)
        {
        
            lstRapport.Remove(r);
              
        }

        private void SelectRaports()
        {
            string sel = "SELECT contenu,temps,titre, FROM Rapport ORDER BY temps DESC";

            

            List<string>[] result = bd.selection(sel);

            foreach(List<string> l  in result)
            {
                lstRapport.Add(new Rapport(l[0], l[1], l[2]));
            }
        }

    }
}
