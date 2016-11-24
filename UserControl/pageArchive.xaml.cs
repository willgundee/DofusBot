using Newtonsoft.Json;
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
    /// Affichage dans une datagrid des parties.
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
            dgHistorique.ItemsSource = lstpartie;
            cboTypePartie.Items.Add("Les partie de tout le monde");
            cboTypePartie.Items.Add("Mes Parties");
            btnVisionner.IsEnabled = false;
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
            btnVisionner.IsEnabled = false;
            dgHistorique.Items.Refresh();
        }

        private void loadParties(string type)
        {
            string selectid = "Select idPartie,temps,seed,infoEntites,attaquantAGagne From Parties";
            List<string>[] lstPartieBd = bd.selection(selectid);

            lstpartie.Clear();
            if (lstPartieBd[0][0] != "rien")
            {
                foreach (List<string> p in lstPartieBd)
                {

                    List<Entite> lstAtt = new List<Entite>();
                    List<Entite> lstDef = new List<Entite>();
                    if (p[3] != "infoEntites")
                    {
                        List<List<Entite>> infoJson = JsonConvert.DeserializeObject<List<List<Entite>>>(p[3]);
                        lstAtt = infoJson[0];
                        lstDef = infoJson[1];

                        if (type == "all")
                        {
                            lstpartie.Add(new Partie(lstAtt[0].Nom, lstDef[0].Nom, p[1].Substring(0, 10), Int32.Parse(p[2]), (p[4] == "True") ? lstAtt[0].Nom: lstDef[0].Nom));
                        }
                        else
                        {
                            if (lstAtt[0].idProprietaire == idJoueur || lstDef[0].idProprietaire == idJoueur)
                            {
                                lstpartie.Add(new Partie(lstAtt[0].Nom, lstDef[0].Nom, p[1].Substring(0, 10), Int32.Parse(p[2]), (p[4] == "True") ? lstAtt[0].Nom : lstDef[0].Nom));
                            }
                        }

                    }





                    //List<List<Entite>> infoJson = JsonConvert.DeserializeObject<List<List<Entite>>>(strJson);
                    //lstAtt = infoJson[0];
                    //lstDef = infoJson[1];


                    /*  string selectPartici = "SELECT estAttaquant,nomEntite,idJoueur FROM PartiesJoueurs WHERE idPartie = " + p[0];
                      List<string>[] result = bd.selection(selectPartici);
                      if (result[0][0] != "rien")
                      {
                          int seed = Int32.Parse(p[2]);
                          string att = "";
                          string def = "";
                          string jrAtt = "";
                          string jrDef = "";
                          foreach (List<string> particip in result)
                          {
                              if (particip[0] == "False")
                              {
                                  def = particip[1];
                                  jrDef = particip[2];
                              }
                              else
                              {

                                  att = particip[1];
                                  jrAtt = particip[2];
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
                      }*/
                }
            }


        }

        private void cboTypePartie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshListe();
        }

        public void RefreshListe()
        {
            if (cboTypePartie.SelectedIndex == 0)
            {
                loadParties("all");
                dgHistorique.Items.Refresh();


            }
            else
            {
                loadParties("joueur");
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

        private void btnVisionner_Click(object sender, RoutedEventArgs e)
        {
            ///TODO : Regarder une partie.
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshListe();
        }

        private void dgHistorique_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnVisionner.IsEnabled = (dgHistorique.SelectedIndex != -1) ? true : false;
        }
    }
}
