using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour pageArchive.xaml
    /// </summary>
    /// 


    public partial class pageArchive : UserControl
    {

        public BDService bd = new BDService();
        public List<Partie> lstpartie;
        public List<String> lstNomPerso;

        public int idJoueur { get; set; }
        public pageArchive(int id)
        {
            InitializeComponent();
            idJoueur = id;

            lstpartie = new List<Partie>();
            //lstNomPerso = new List<string>();

            dgHistorique.ItemsSource = lstpartie;

            cboTypePartie.Items.Add("Mes Parties");
            cboTypePartie.Items.Add("Les partie de tout le monde");
        }

        public pageArchive()
        {
            InitializeComponent();
            lstpartie = new List<Partie>();
            btnQuitter.Visibility = Visibility.Visible;
            lblQuitter.Visibility = Visibility.Visible;
            lblCreer.Visibility = Visibility.Visible;
            btnCreer.Visibility = Visibility.Visible;
            dgHistorique.ItemsSource = lstpartie;
            cboTypePartie.Visibility = Visibility.Hidden;
            loadParties("all");
            dgHistorique.Items.Refresh();
        }

        private void loadParties(string type)
        {

                string selectid = "Select  idPartie,temps,seed From Parties";
                List<string>[] lstPartieBd = bd.selection(selectid);



               
                    lstpartie.Clear();
                    foreach (List<string> p in lstPartieBd)
                    {
                        string selectPartici = "SELECT estAttaquant,idEntite FROM PartiesEntites WHERE idPartie = " + p[0];
                        List<string>[] result = bd.selection(selectPartici);
                        if (result[0][0] != "rien")
                        {
                            int seed = Int32.Parse(p[2]);
                            string att = "";
                            string def = "";
                            string jrAtt = "";
                            string jrDef = "";
                            int idatt = -1;
                            int iddef = -1;

                            foreach (List<string> particip in result)
                            {

                                if (particip[0] == "False")
                                {
                                    string selectN = "SELECT nom,idJoueur FROM Entites WHERE idEntite=" + particip[1].ToString();
                                    List<string>[] selectNom = bd.selection(selectN);
                                    def = selectNom[0][0];
                                    jrDef = selectNom[0][1];
                                    iddef = Int32.Parse(particip[1]);
                                }
                                else
                                {
                                    string selectN = "SELECT nom,idJoueur FROM Entites  WHERE idEntite=" + particip[1].ToString();
                                    List<string>[] selectNom = bd.selection(selectN);
                                    att = selectNom[0][0];
                                    jrAtt = selectNom[0][1];
                                    idatt = Int32.Parse(particip[1]);
                                }

                            }


                            if (type == "all")
                            {
                                lstpartie.Add(new Partie(att, def, p[1], seed));
                            }
                            else
                            {
                                if (jrDef == idJoueur.ToString() || jrAtt == idJoueur.ToString())
                                {
                                    lstpartie.Add(new Partie(att, def, p[1], seed));
                                }
                            }

                        }

                    }
              
    



        }


        private void cboTypePartie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTypePartie.SelectedIndex == 0)
            {

                loadParties("joueur");
                dgHistorique.Items.Refresh();

            }
            else
            {
                loadParties("all");
                dgHistorique.Items.Refresh();
            }
        }

        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            Authentification A = new Authentification();
            A.Show();
            System.Windows.Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(PageVisionneuse)).Close();
        }

        private void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            CreationCompteWindow creation = new CreationCompteWindow();
            creation.Show();
            System.Windows.Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(PageVisionneuse)).Close();
        }
    }
}
