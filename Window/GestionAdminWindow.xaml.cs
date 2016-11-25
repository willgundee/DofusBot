using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour GestionAdminWindow.xaml
    /// </summary>
    public partial class GestionAdminWindow : Window
    {

        BDService bdAdmin;

        public ObservableCollection<Utilisateur> lstBackUp;

        public ObservableCollection<Utilisateur> lstUtilisateurs;





        public GestionAdminWindow()
        {
            InitializeComponent();


            dataGrid.AutoGenerateColumns = false;
            lstBackUp = new ObservableCollection<Utilisateur>();
            lstUtilisateurs = new ObservableCollection<Utilisateur>();
            bdAdmin = new BDService();

            List<string>[] result = bdAdmin.selection("SELECT nomUtilisateur,estAdmin FROM Joueurs");
            foreach (List<string> item in result)
            {
                lstUtilisateurs.Add(new Utilisateur(item[0], ((item[1] == "True") ? true : false)));
            }

            dataGrid.ItemsSource = lstUtilisateurs;
            DataGridTextColumn textColumn = new DataGridTextColumn();
            textColumn.Header = "Nom Utilisateur";
            textColumn.Binding = new System.Windows.Data.Binding("nom");
            textColumn.IsReadOnly = true;
            dataGrid.Columns.Add(textColumn);


            DataGridCheckBoxColumn boolColumn = new DataGridCheckBoxColumn();
            boolColumn.Header = "Administrateur";
            boolColumn.Binding = new System.Windows.Data.Binding("estAdmin");
            dataGrid.Columns.Add(boolColumn);
            dataGrid.FrozenColumnCount = 2;

            foreach (Utilisateur u in lstUtilisateurs)
                lstBackUp.Add(new Utilisateur(u.nom,u.estAdmin));
            dataGrid.Items.Refresh();

        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (Utilisateur u in lstUtilisateurs)
            {
                if (u.estAdmin != lstBackUp[i].estAdmin)
                {
                    string upt = "UPDATE Joueurs set estAdmin = " + ((u.estAdmin == true) ? "true" : "false") + " WHERE nomUtilisateur = '" + u.nom + "';";
                    bool test = bdAdmin.Update(upt);
                }
                i++;
            }
        }

        public void BackUpComptes()
        {
            lstBackUp.Clear();
            foreach (Utilisateur u in lstUtilisateurs)
            {
                lstBackUp.Add(new Utilisateur (u.nom,u.estAdmin));
            }
        }

        public void Reset()
        {
            lstUtilisateurs.Clear();
            foreach (Utilisateur u in lstBackUp)
            {
                lstUtilisateurs.Add(new Utilisateur (u.nom,u.estAdmin));
            }
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => Reset()));
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
