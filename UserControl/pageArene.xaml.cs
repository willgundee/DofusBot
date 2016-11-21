using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Threading;
using System;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace Gofus
{

    public partial class pageArene : UserControl
    {
        public BDService bd;
        public ObservableCollection<string> lstScripts;
        public ObservableCollection<string> lstTypeAdver;
        public Dictionary<int, string> lstPerso;


        public ObservableCollection<Adversaire> lstAdversaires;

        public int idJoueur;


        public pageArene(int id, ObservableCollection<Entite> lstPersonnages)
        {
            InitializeComponent();
            lstScripts = new ObservableCollection<string>();
            lstPerso = new Dictionary<int, string>();
            lstTypeAdver = new ObservableCollection<string>();
            bd = new BDService();
            lstTypeAdver.Add("Personnage");
            lstTypeAdver.Add("Monstre");
            idJoueur = id;
            cboTypeAdversaire.ItemsSource = lstTypeAdver;
            foreach (Entite perso in lstPersonnages)
            {
                lstPerso.Add(perso.IdEntite, perso.Nom);
            }
            cboPerso.ItemsSource = lstPerso;
            cboPerso.DisplayMemberPath = "Value";
            cboTypeAdversaire.SelectedIndex = 0;
            cboPerso.SelectedIndex = 0;
            lstAdversaires = new ObservableCollection<Adversaire>();
            dataGrid.ItemsSource = lstAdversaires;
        }

        private void cboTypeAdversaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstAdversaires = new ObservableCollection<Adversaire>();
            int index = cboTypeAdversaire.SelectedIndex;
            dataGrid.Items.Refresh();
            dataGrid.ItemsSource = lstAdversaires;
            Thread trdRefresh = new Thread(() =>
                {
                    List<string>[] Result = bd.selection((index == 0) ? "SELECT nom,valeur,nomUtilisateur FROM Entites INNER JOIN Joueurs ON Entites.idJoueur = Joueurs.idJoueur INNER JOIN statistiquesentites ON Entites.idEntite = statistiquesentites.idEntite WHERE idTypeStatistique = 13 AND idJoueur IS NOT NULL AND idJoueur != " + idJoueur.ToString() : "SELECT nom,valeurMin,valeurMax FROM Entites INNER JOIN statistiquesentites ON Entites.idEntite = statistiquesentites.idEntite WHERE idTypeStatistique = 13 AND idJoueur IS NULL");
                    System.Windows.Application.Current.Dispatcher.Invoke(new System.Action(() =>
                    {
                        foreach (List<string> enti in Result)
                        {
                            if (index != 0)
                            {
                                int min = Statistique.toLevel((double.Parse(enti[1])));
                                int max = Statistique.toLevel((double.Parse(enti[2])));
                                string lvl = "Entre " + min + " et " + max;
                                lstAdversaires.Add( new Adversaire(enti[0], lvl));
                            }
                            else
                            {
                                int niveau = Statistique.toLevel((double.Parse(enti[1])));
                                lstAdversaires.Add(new AdversaireHumain (enti[0], niveau.ToString(),enti[2]));
                            }
                        }
                        dataGrid.ItemsSource = lstAdversaires;
                        dataGrid.Items.Refresh();
                    }));
                });
            trdRefresh.Start();
            Thread.Yield();
        }

        private void btnAtt_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex != -1)
            {
                string sele = "SELECT * FROM Entites WHERE nom = '" + ((KeyValuePair<string, string>)dataGrid.SelectedItem).Key + "'";
                List<string>[] defen = bd.selection(sele);
                Entite def = new Entite(defen[0]);
                
                List<Entite> lstAtt = new List<Entite>();
                List<Entite> lstDef = new List<Entite>();
                lstAtt.Add((Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(MainWindow)) as MainWindow).Player.LstEntites.First(x => x.IdEntite == ((KeyValuePair<int, string>)cboPerso.SelectedItem).Key));
                lstDef.Add(def);
                int seed = 65555;
                List<List<Entite>> jsonObj = new List<List<Entite>> { lstAtt, lstDef};
                string strJson = JsonConvert.SerializeObject(jsonObj);
                bd.insertion("INSERT INTO Parties (seed, temps, infoEntites) VALUE(" + seed + ", NOW(), '" + MySqlHelper.EscapeString(strJson) + "');");
                
                //bd.insertion("INSERT INTO PartiesJoueurs (idPartie, idJoueur, estAttaquant) VALUE();");
                //List<List<Entite>> infoJson = JsonConvert.DeserializeObject<List<List<Entite>>>(strJson);
                //lstAtt = infoJson[0];
                //lstDef = infoJson[1];
                GofusSharp.Combat combat = new GofusSharp.Combat(lstAtt, lstDef, seed);
            }
        }
    }
}
