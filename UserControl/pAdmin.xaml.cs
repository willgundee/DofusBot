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
    public partial class pAdmin : UserControl
    {
        BDService bd;

        private ObservableCollection<Rapport> lstRapport;

        public pAdmin()
        {
            InitializeComponent();
            bd = new BDService();
            lstRapport = new ObservableCollection<Rapport>();
            SelectRaports();

            dataGrid.ItemsSource = lstRapport;
            datePick.SelectedDate = DateTime.Now;
            datePick.IsEnabled = true;
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (datePick.SelectedDate != null)
            {
                var result = System.Windows.MessageBox.Show("Souhaitez-vous supprimer les messages qui ont étés envoyés avant le " + datePick.SelectedDate.ToString(), "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    string delete = "DELETE FROM Messages WHERE temps < '" + datePick.SelectedDate.ToString() + "'";

                    bool test = bd.delete(delete);
                }

            }



        }


        private void supprimerRap(Rapport r)
        {

            lstRapport.Remove(r);

        }

        private void SelectRaports()
        {
            string sel = "SELECT contenu,temps,titre,nom,Rapports.idRapport FROM Rapports INNER JOIN  typerapport ON Rapports.idTypeRapport =  typerapport.idTypeRapport ORDER by temps";



            List<string>[] result = bd.selection(sel);
            if (result[0][0] != "rien")
                foreach (List<string> l in result)
                {
                    lstRapport.Add(new Rapport(l[0], l[1], l[2], l[3],l[4]));
                }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            tblContenu.Text = (dataGrid.SelectedItem != null) ? ((Rapport)dataGrid.SelectedItem).msg : "";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            tblContenu.Text = "";
            Rapport del = (Rapport)dataGrid.SelectedItem;
            bool test = bd.delete("DELETE FROM Rapports WHERE idRapport = " + del.id);
            lstRapport.Remove(del);
            

        }
    }
}
