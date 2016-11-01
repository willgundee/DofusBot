using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace test
{

    public partial class pageArene : UserControl
    {
        public BDService bd;
        public ObservableCollection<string> lstScripts;
        public ObservableCollection<string> lstTypeAdver;
        public ObservableCollection<string> lstPerso;
        public ObservableCollection<Entite> lstAdversaire;

        public int idJoueur;

        public pageArene(int id)
        {
            InitializeComponent();
            lstScripts = new ObservableCollection<string>();
            lstAdversaire = new ObservableCollection<Entite>();
            lstPerso = new ObservableCollection<string>();
            lstTypeAdver = new ObservableCollection<string>();

            bd = new BDService();
            lstTypeAdver.Add("Personnage");
            lstTypeAdver.Add("Monstre");

            idJoueur = id;
            cboTypeAdversaire.ItemsSource = lstTypeAdver;
            cboTypeAdversaire.SelectedIndex = 0;
            /*  foreach (Entite perso in ((MainWindow)Application.Current.MainWindow).Player.LstEntites)
              {
                  lstPerso.Add(perso.Nom);
              }*/

            cboPersonna.ItemsSource = lstPerso;

            cboPersonna.SelectedIndex = 0;

            List<string>[] Result = bd.selection("SELECT * FROM Entites WHERE idJoueur IS NOT NULL AND idJoueur != " + id.ToString());

            int i = 0;
            foreach (List<string> enti in Result)
            {

                lstAdversaire.Add(new Entite(enti));
                i++;
            }


            dataGrid.ItemsSource = lstAdversaire;



        }

        private void cboTypeAdversaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTypeAdversaire.SelectedIndex == 0)
            {
                List<string>[] Result = bd.selection("SELECT * FROM Entites WHERE idJoueur IS NOT NULL AND idJoueur != " + idJoueur.ToString());

                
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

               
                foreach (List<string> enti in Result)
                {

                    lstAdversaire.Add(new Entite(enti));
                  
                }


                dataGrid.ItemsSource = lstAdversaire;
                dataGrid.Items.Refresh();
            }

        }
    }
}
