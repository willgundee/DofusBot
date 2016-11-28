using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour GestionAdminWindow.xaml
    /// </summary>
    public partial class GestionAdminWindow : Window
    {
        private BDService bdAdmin;

        /// <summary>
        /// Liste qui contient la copie originel de la liste des utilisateurs
        /// </summary>
        public ObservableCollection<Utilisateur> lstBackUp;

        public ObservableCollection<Utilisateur> lstUndo;



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
            lstUtilisateurs.CollectionChanged += ContentCollectionChanged;
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

     

        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            btnSauvegarder.IsEnabled = true;
            btnAnnuler.IsEnabled = true;
        }


        private void btnSauvegarder_Click(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("Souhaitez-vous enregistrer les modifications? Vous ne pourrez plus utiliser la fonction annuler.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
 
                for (int i = 0; i < lstUtilisateurs.Count; i++)
                {
                    if (lstUtilisateurs[i].estAdmin != lstBackUp[i].estAdmin)
                    {
                        string upt = "UPDATE Joueurs set estAdmin = " + ((lstUtilisateurs[i].estAdmin == true) ? "true" : "false") + " WHERE nomUtilisateur = '" + lstUtilisateurs[i].nom + "';";
                        bool test = bdAdmin.Update(upt);
                    }
                }
                BackUpComptes();
                this.Close();
            }
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
            Dispatcher.Invoke(new Action(() =>
            {
                Reset();
            }));

        }


        //http://stackoverflow.com/questions/3426765/single-click-edit-in-wpf-datagrid
        private void DataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            // Lookup for the source to be DataGridCell
            if (e.OriginalSource.GetType() == typeof(DataGridCell))
            {
                // Starts the Edit on the row;
                DataGrid grd = (DataGrid)sender;
                grd.BeginEdit(e);
            }
        }

    }
}