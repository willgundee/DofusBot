﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
        public List<String> lstNomPerso;
        public ObservableCollection<Partie> lstpartie;
        public pageArchive(int id)
        {
            InitializeComponent();
            idJoueur = id;
            GenererChamps();
            lstpartie = new ObservableCollection<Partie>();
            dgHistorique.ItemsSource = lstpartie;
            cboTypePartie.Items.Add("Les partie de tout le monde"); cboTypePartie.Items.Add("Mes Parties");
            btnVisionner.IsEnabled = false;
        }
        /// <summary>
        /// Constructeur de la page archive pour visiteur
        /// </summary>
        public pageArchive()
        {
            InitializeComponent();
            GenererChamps();
            lstpartie = new ObservableCollection<Partie>();
            //on rend visible des éléments qui son essentiel pour un non joueur 
            //quitter la page
            btnQuitter.Visibility = Visibility.Visible;
            //se créer un compte
            btnCreer.Visibility = Visibility.Visible;
            //on cache les élément relatif au joueurs
            btn_Refresh.Visibility = Visibility.Hidden;
            lblTitre.Visibility = Visibility.Hidden;
            cboTypePartie.Visibility = Visibility.Hidden;
            //on lie la liste des parties recentes avec la gird
            dgHistorique.ItemsSource = lstpartie;
            loadParties("all");
            //le bouton visionner est bloqué tent que l'utilisateur n'as pas sélectionné une partie a visionner.
            btnVisionner.IsEnabled = false;
            //on refresh la liste
            dgHistorique.Items.Refresh();
        }

        public int idJoueur { get; set; }
        public void GenererChamps()
        {
            DataGridTextColumn textColumn = new DataGridTextColumn();
            textColumn.Header = "Attaquant";
            textColumn.Binding = new Binding("Attaquant");
            dgHistorique.Columns.Add(textColumn);
            textColumn = new DataGridTextColumn();
            textColumn.Header = "Defendant";
            textColumn.Binding = new Binding("Defendant");
            dgHistorique.Columns.Add(textColumn);
            textColumn = new DataGridTextColumn();

            textColumn.Header = "Date";
            textColumn.Binding = new Binding("trueDate");
            textColumn.Binding.StringFormat = "yyyy/MM/dd";
            dgHistorique.Columns.Add(textColumn);

            textColumn = new DataGridTextColumn();

            textColumn.Header = "Gagnant";
            textColumn.Binding = new Binding("Gagnant");
            dgHistorique.Columns.Add(textColumn);

            textColumn = new DataGridTextColumn();
        }
        public void RefreshListe()
        {
            if (cboTypePartie.SelectedIndex == 0)
            {
                loadParties("all");
            }
            else
            {
                loadParties("joueur");
            }
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshListe();
        }

        private void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            CreationUser creation = new CreationUser();
            creation.Show();
            System.Windows.Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(PageVisionneuse)).Close();
        }

        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            Authentification A = new Authentification();
            A.Show();
            System.Windows.Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(PageVisionneuse)).Close();
        }

        private void btnVisionner_Click(object sender, RoutedEventArgs e)
        {
            Partie PartieChoisi = dgHistorique.SelectedValue as Partie;

            StringBuilder Requete = new StringBuilder();
            Requete.Append("SELECT infoEntites FROM Parties WHERE idPartie = ");
            Requete.Append(PartieChoisi.IdPartie);
            string strJson = bd.selection(Requete.ToString())[0][0];
            List<List<Entite>> infoJson = JsonConvert.DeserializeObject<List<List<Entite>>>(strJson);
            GofusSharp.Combat combat = new GofusSharp.Combat(infoJson[0], infoJson[1], PartieChoisi.seed, PartieChoisi.IdPartie, false);
        }

        private void cboTypePartie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshListe();
        }

        private void dgHistorique_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => btnVisionner.IsEnabled = (dgHistorique.SelectedIndex != -1) ? true : false));
        }

        private void loadParties(string type)
        {
            List<Entite> lstAtt = new List<Entite>();
            List<Entite> lstDef = new List<Entite>();
            lstpartie.Clear();
            StringBuilder Joueur = new StringBuilder();
            Joueur.Append("Select p.idPartie,temps,seed,infoEntites,attaquantAGagne From Parties p INNER JOIN PartiesJoueurs j ON j.idPartie = p.idPartie WHERE j.idJoueur = ");
            Joueur.Append(idJoueur.ToString());
            Joueur.Append(" order by temps desc ");
            string selectid = (type == "joueur") ? Joueur.ToString() : "Select idPartie,temps,seed,infoEntites,attaquantAGagne From Parties order by temps desc LIMIT 100";
            BackgroundWorker Refresh = new BackgroundWorker() { WorkerReportsProgress = true };
            Refresh.DoWork += (s, e) =>
            {
                List<string>[] lstPartieBd = bd.selection(selectid);
                if (lstPartieBd[0][0] != "rien")
                {
                    foreach (List<string> p in lstPartieBd)
                    {
                        if (p[3] != "infoEntites")
                        {
                            List<List<Entite>> infoJson = JsonConvert.DeserializeObject<List<List<Entite>>>(p[3]);
                            lstAtt = infoJson[0];
                            lstDef = infoJson[1];
                            if (type == "all")
                            {
                                Dispatcher.Invoke(new Action(() =>
                                {
                                    lstpartie.Add(new Partie(lstAtt[0].Nom, lstDef[0].Nom, p[1], int.Parse(p[2]), ((p[4] == "1" || p[4] == "True") ? lstAtt[0].Nom : ((p[4] == "False" || p[4] == "0") ? lstDef[0].Nom : "Match Nul")), Convert.ToInt32(p[0])));
                                }));
                            }
                            else
                            {
                                Dispatcher.Invoke(new Action(() =>
                                {
                                    lstpartie.Add(new Partie(lstAtt[0].Nom, lstDef[0].Nom, p[1], int.Parse(p[2]), ((p[4] == "1" || p[4] == "True") ? lstAtt[0].Nom : ((p[4] == "False" || p[4] == "0") ? lstDef[0].Nom : "Match Nul")), Convert.ToInt32(p[0])));
                                }));
                            }
                        }
                    }
                }
            };

            Refresh.RunWorkerCompleted += (s, e) =>
            {
                dgHistorique.Items.Refresh();
            };

            Refresh.RunWorkerAsync();
        }
    }
}