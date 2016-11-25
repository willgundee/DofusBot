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

        /// <summary>
        /// Liste qui contient la copie originel de la liste des utilisateurs
        /// </summary>
        public ObservableCollection<Utilisateur> lstBackUp;

        /// <summary>
        /// Liste qui contient les utilisateurs, Elle est liée à la datagrid.
        /// C'est cette liste qui subira les modifications
        /// </summary>
        public ObservableCollection<Utilisateur> lstUtilisateurs;

        /// <summary>
        /// Gestion d'admin,
        /// Contient la liste des utilisateurs dans la bd.
        ///  st
        /// </summary>
        public GestionAdminWindow()
        {
            InitializeComponent();
            // Initialisation des objets requis.
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
                lstBackUp.Add(new Utilisateur(u.nom, u.estAdmin));
            dataGrid.Items.Refresh();
        }
        private void btnSauvegarder_Click(object sender, RoutedEventArgs e)
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
            BackUpComptes();
        }
        public void BackUpComptes()
        {
            lstBackUp.Clear();
            foreach (Utilisateur u in lstUtilisateurs)
            {
                lstBackUp.Add(new Utilisateur(u.nom, u.estAdmin));
            }
        }

        /// <summary>
        ///  Permet de reset la datagrid et de réafficher la lste originel.
        /// </summary>
        public void Reset()
        {
            dataGrid.ItemsSource = null;
            lstUtilisateurs = new ObservableCollection<Utilisateur>();
            foreach (Utilisateur u in lstBackUp)
            {
                lstUtilisateurs.Add(new Utilisateur(u.nom, u.estAdmin));
            }
            dataGrid.ItemsSource = lstUtilisateurs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => Reset()));
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
