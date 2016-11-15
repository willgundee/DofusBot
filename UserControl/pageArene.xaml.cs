using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Threading;
using System;

namespace Gofus
{

    public partial class pageArene : UserControl
    {
        public BDService bd;
        public ObservableCollection<string> lstScripts;
        public ObservableCollection<string> lstTypeAdver;
        public Dictionary<int, string> lstPerso;


        public Dictionary<string, string> adversaires;

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


            dataGrid.ItemsSource = adversaires;



        }

        private void cboTypeAdversaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            adversaires = new Dictionary<string, string>();
            int index = cboTypeAdversaire.SelectedIndex;
            dataGrid.Items.Refresh();
            dataGrid.ItemsSource = adversaires;

            Thread trdRefresh = new Thread(() =>
                {

                    List<string>[] Result = bd.selection((index == 0) ? "SELECT nom,valeur FROM Entites INNER JOIN statistiquesentites ON Entites.idEntite = statistiquesentites.idEntite WHERE idTypeStatistique = 13 AND idJoueur IS NOT NULL AND idJoueur != " + idJoueur.ToString() : "SELECT nom,valeurMin,valeurMax FROM Entites INNER JOIN statistiquesentites ON Entites.idEntite = statistiquesentites.idEntite WHERE idTypeStatistique = 13 AND idJoueur IS NULL");
                    System.Windows.Application.Current.Dispatcher.Invoke(new System.Action(() =>
                    {
                        foreach (List<string> enti in Result)
                        {

                            if (index != 0)
                            {
                                Statistique stat = new Statistique();

                                int min = stat.toLevel((double.Parse(enti[1])));
                                int max = stat.toLevel((double.Parse(enti[2])));
                                string lvl = "Entre " + min + " et " + max;
                                adversaires.Add(enti[0], lvl);
                            }
                            else
                            {
                                Statistique stat = new Statistique();
                                int niveau = stat.toLevel((double.Parse(enti[1])));
                                adversaires.Add(enti[0], niveau.ToString());
                            }


                        }

                        dataGrid.ItemsSource = adversaires;
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
                
                    string sele = "SELECT * FROM Entites WHERE nom = '" + ((KeyValuePair<string,string>)dataGrid.SelectedItem).Key + "'";
                List<string>[] defen = bd.selection(sele);
                    Entite def = new Entite(defen[0]);
               
                


                List<Entite> lstAtt = new List<Entite>();
                List<Entite> lstDef = new List<Entite>();
                lstAtt.Add((Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(MainWindow)) as MainWindow).Player.LstEntites.First(x => x.IdEntite == ((KeyValuePair<int, string>)cboPerso.SelectedItem).Key));
                lstDef.Add(def);
                GofusSharp.Combat combat = new GofusSharp.Combat(lstAtt, lstDef);
            }
        }
    }
}
