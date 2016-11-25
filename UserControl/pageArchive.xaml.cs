﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        public ObservableCollection<Partie> lstpartie;
        public List<String> lstNomPerso;

        public int idJoueur { get; set; }
        public pageArchive(int id)
        {
            InitializeComponent();
            idJoueur = id;
            GenererChamps();
            lstpartie = new ObservableCollection<Partie>();
            dgHistorique.ItemsSource = lstpartie;
            cboTypePartie.Items.Add("Les partie de tout le monde");
            cboTypePartie.Items.Add("Mes Parties");
            btnVisionner.IsEnabled = false;
        }

        public pageArchive()
        {
            InitializeComponent();
            GenererChamps();
            lstpartie = new ObservableCollection<Partie>();
            btnQuitter.Visibility = Visibility.Visible;
            btnCreer.Visibility = Visibility.Visible;
            btn_Refresh.Visibility = Visibility.Hidden;
            dgHistorique.ItemsSource = lstpartie;
            cboTypePartie.Visibility = Visibility.Hidden;
            loadParties("all");
            btnVisionner.IsEnabled = false;
            dgHistorique.Items.Refresh();

        }

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

        }

        private void loadParties(string type)
        {
            List<Entite> lstAtt = new List<Entite>();
            List<Entite> lstDef = new List<Entite>();
            lstpartie.Clear();
            string selectid = "Select idPartie,temps,seed,infoEntites,attaquantAGagne From Parties order by temps desc";
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
                                    lstpartie.Add(new Partie(lstAtt[0].Nom, lstDef[0].Nom, p[1], int.Parse(p[2]), (p[4] == "True" ? lstAtt[0].Nom : lstDef[0].Nom), Convert.ToInt32(p[0])));
                                }));

                            }
                            else
                            {
                                if (lstAtt[0].idProprietaire == idJoueur || lstDef[0].idProprietaire == idJoueur)
                                {
                                    Dispatcher.Invoke(new Action(() =>
                                    {
                                        lstpartie.Add(new Partie(lstAtt[0].Nom, lstDef[0].Nom, p[1], int.Parse(p[2]), (p[4] == "True" ? lstAtt[0].Nom : lstDef[0].Nom), Convert.ToInt32(p[0])));
                                    }));
                                }
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
        private void cboTypePartie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshListe();
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


        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            Authentification A = new Authentification();
            A.Show();
            System.Windows.Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(PageVisionneuse)).Close();
        }

        private void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            CreationUser creation = new CreationUser();
            creation.Show();
            System.Windows.Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(PageVisionneuse)).Close();
        }

        private void btnVisionner_Click(object sender, RoutedEventArgs e)
        {
            Partie PartieChoisi = dgHistorique.SelectedValue as Partie;
            string strJson = bd.selection("SELECT infoEntites FROM Parties WHERE idPartie = " + PartieChoisi.IdPartie)[0][0];
            List<List<Entite>> infoJson = JsonConvert.DeserializeObject<List<List<Entite>>>(strJson);
            GofusSharp.Combat combat = new GofusSharp.Combat(infoJson[0], infoJson[1], PartieChoisi.seed, PartieChoisi.IdPartie, false);
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshListe();
        }

        private void dgHistorique_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => btnVisionner.IsEnabled = (dgHistorique.SelectedIndex != -1) ? true : false));
        }
    }
}
