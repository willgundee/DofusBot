using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Gofus
{

    public partial class pageArene : UserControl
    {
        public BDService bd;
        public ObservableCollection<string> lstScripts;
        public ObservableCollection<string> lstTypeAdver;
        public Dictionary<int, string> lstPerso;
        public ObservableCollection<Entite> lstAdversaire;

        public int idJoueur;


        public pageArene(int id, ObservableCollection<Entite> lstPersonnages)
        {
            InitializeComponent();
            lstScripts = new ObservableCollection<string>();
            lstAdversaire = new ObservableCollection<Entite>();
            lstPerso = new Dictionary<int, string>();
            lstTypeAdver = new ObservableCollection<string>();

            bd = new BDService();
            lstTypeAdver.Add("Personnage");
            lstTypeAdver.Add("Monstre");

            idJoueur = id;
            cboTypeAdversaire.ItemsSource = lstTypeAdver;
            int i = 0;
            foreach (Entite perso in lstPersonnages)
            {
                lstPerso.Add(i, perso.Nom);
                i++;
            }



            cboPersonna.ItemsSource = lstPerso;
            cboPersonna.DisplayMemberPath = "Value";

            cboTypeAdversaire.SelectedIndex = 0;

            cboPersonna.SelectedIndex = 0;


            dataGrid.ItemsSource = lstAdversaire;



        }

        private void cboTypeAdversaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTypeAdversaire.SelectedIndex == 0)
            {
                List<string>[] Result = bd.selection("SELECT * FROM Entites WHERE idJoueur IS NOT NULL AND idJoueur != " + idJoueur.ToString());
                lstAdversaire = new ObservableCollection<Entite>();

                foreach (List<string> enti in Result)
                {
                    lstAdversaire.Add(new Entite(enti));
                }

                dataGrid.ItemsSource = lstAdversaire;
                dataGrid.Items.Refresh();
            }
            else
            {
                List<string>[] Result = bd.selection("SELECT * FROM Entites WHERE idJoueur IS NULL");

                lstAdversaire = new ObservableCollection<Entite>();
                foreach (List<string> enti in Result)
                {
                    lstAdversaire.Add(new Entite(enti));
                }

                dataGrid.ItemsSource = lstAdversaire;
                dataGrid.Items.Refresh();
            }

        }

        private void btnAtt_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex != -1)
            {
               MessageBox.Show("1233");

            }
        }

        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
