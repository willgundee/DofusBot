﻿using System;
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

        public List<string> lstUpdate;

        public ObservableCollection<Utilisateur> lstUtilisateurs;


        public GestionAdminWindow()
        {
            InitializeComponent();


            dataGrid.AutoGenerateColumns = false;
            lstUpdate = new List<string>();
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



            dataGrid.Items.Refresh();

        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Test");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(((Utilisateur)dataGrid.SelectedItem).estAdmin.ToString());
        }
    }
}
