using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Threading;
using System;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Windows.Data;
using System.Text;

namespace Gofus
{

    /// <summary>
    /// User Conrol pour la page Arène
    /// Affiche un grille qui contient les adversaires. 
    /// Il y a deux type d'adversaires, Monstre et Personnages
    /// </summary>
    public partial class pageArene : UserControl
    {
        /// <summary>
        /// La connexion BD
        /// </summary>
        public BDService bd;

        /// <summary>
        /// La liste qui contient les adversaires
        /// </summary>
        public ObservableCollection<Adversaire> lstAdversaires;

        /// <summary>
        /// La liste des personnages du joueur
        /// </summary>
        public Dictionary<int, string> lstPerso;


        /// <summary>
        /// La liste des scripts du joueur
        /// </summary>
        public ObservableCollection<string> lstScripts;
        /// <summary>
        /// La liste des type d'adversaires
        /// </summary>
        public ObservableCollection<string> lstTypeAdver;


        /// <summary>
        /// Le nom du Joueur
        /// </summary>
        public string nomUtilisateur { get; set; }


        /// <summary>
        /// Constructeur de la page arène qui est utiliser lorsque le joueur est connecté
        /// </summary>
        /// <param name="id">id du Joueur en question</param>
        /// <param name="lstPersonnages"></param>
        public pageArene(string nomJoueur, ObservableCollection<Entite> lstPersonnages)
        {
            InitializeComponent();
            lstScripts = new ObservableCollection<string>();
            lstPerso = new Dictionary<int, string>();
            lstTypeAdver = new ObservableCollection<string>();
            bd = new BDService();

            // Remplissage de la combobox pour le type d'adversaires.
            lstTypeAdver.Add("Personnage");
            lstTypeAdver.Add("Monstre");
            cboTypeAdversaire.ItemsSource = lstTypeAdver;
            cboTypeAdversaire.SelectedIndex = 0;


            //Remplissage de la liste des personnages du joueur
            foreach (Entite perso in lstPersonnages)
            {
                lstPerso.Add(perso.IdEntite, perso.Nom);
            }
            cboPerso.ItemsSource = lstPerso;
            cboPerso.DisplayMemberPath = "Value";
            cboPerso.SelectedIndex = 0;



            lstAdversaires = new ObservableCollection<Adversaire>();
            dataGrid.ItemsSource = lstAdversaires;

            nomUtilisateur = nomJoueur;
        }

        /// <summary>
        /// Fonction qui permet d'attaquer l'adversaire sélectionner dans la dataGrid.
        /// </summary>
        public void Attaquer()
        {
            // Il faut au minimum deux entités sélectionnées.
            if (dataGrid.SelectedIndex != -1 || cboPerso.SelectedIndex != -1)
            {
                // Construction de la String du select.
                StringBuilder select = new StringBuilder();
                select.Append("SELECT * FROM Entites WHERE nom = '");
                select.Append(((Adversaire)dataGrid.SelectedItem).nom);
                select.Append("'");

                // SELECT DES défendants.
                List<string>[] defen = bd.selection(select.ToString());
                Entite def = new Entite(defen[0]);


                List<Entite> lstAtt = new List<Entite>();
                List<Entite> lstDef = new List<Entite>();

                Dispatcher.Invoke(new Action(() => lstAtt.Add((Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(MainWindow)) as MainWindow).Player.LstEntites.First(x => x.IdEntite == ((KeyValuePair<int, string>)cboPerso.SelectedItem).Key))));
                lstDef.Add(def);

                List<List<Entite>> jsonObj = new List<List<Entite>> { lstAtt, lstDef };
                string strJson = JsonConvert.SerializeObject(jsonObj);
                lstAtt.Sum(x => x.idProprietaire);
                int seed = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + lstAtt.Sum(x => x.idProprietaire) + lstDef.Sum(x => x.idProprietaire);
                StringBuilder insertIdPartie = new StringBuilder();
                insertIdPartie.Append("INSERT INTO Parties (seed, temps, infoEntites) VALUE(");
                insertIdPartie.Append(seed); insertIdPartie.Append(", NOW(), '");
                insertIdPartie.Append(MySqlHelper.EscapeString(strJson));
                insertIdPartie.Append("');");
                long idPartie = bd.insertion(insertIdPartie.ToString());
                foreach (int idPropUnique in lstAtt.Select(x => x.idProprietaire).Distinct())
                {
                    StringBuilder insertInfoJoueurs = new StringBuilder();
                    insertInfoJoueurs.Append("INSERT INTO PartiesJoueurs (idPartie, idJoueur, estAttaquant) VALUE(");
                    insertInfoJoueurs.Append(idPartie);
                    insertInfoJoueurs.Append(", ");
                    insertInfoJoueurs.Append((idPropUnique == 0 ? 103 : idPropUnique));
                    insertInfoJoueurs.Append(", true);");
                    bd.insertion(insertInfoJoueurs.ToString());
                }
                foreach (int idPropUnique in lstDef.Select(x => x.idProprietaire).Distinct())
                {
                    StringBuilder insertInfoJoueurs = new StringBuilder();
                    insertInfoJoueurs.Append("INSERT INTO PartiesJoueurs (idPartie, idJoueur, estAttaquant) VALUE(");
                    insertInfoJoueurs.Append(idPartie);
                    insertInfoJoueurs.Append(", ");
                    insertInfoJoueurs.Append((idPropUnique == 0 ? 103 : idPropUnique));
                    insertInfoJoueurs.Append(", false);");
                    bd.insertion(insertInfoJoueurs.ToString());
                }
                GofusSharp.Combat combat = new GofusSharp.Combat(lstAtt, lstDef, seed, idPartie);
            }
        }


        /// <summary>
        /// Refresh la liste des personnages
        /// </summary>
        /// <param name="lstPersonnages">néo liste de personnage.</param>
        public void RefreshPersos(ObservableCollection<Entite> lstPersonnages)
        {
            cboPerso.ItemsSource = null;
            lstPerso = new Dictionary<int, string>();
            foreach (Entite perso in lstPersonnages)
            {
                lstPerso.Add(perso.IdEntite, perso.Nom);
            }
            cboPerso.ItemsSource = lstPerso;
            cboPerso.DisplayMemberPath = "Value";
            cboPerso.SelectedIndex = 0;
        }


        /// <summary>
        /// Fonction executé lors d'un clique du boutton attaque.
        /// </summary>
        private void btnAtt_Click(object sender, RoutedEventArgs e)
        {
            Attaquer();
        }

        /// <summary>
        /// Bouton de rafraichissement de la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnAtt.IsEnabled = false;
            lstAdversaires = new ObservableCollection<Adversaire>();
            int index = cboTypeAdversaire.SelectedIndex;
            dataGrid.Items.Refresh();
            dataGrid.Columns.Clear();
            dataGrid.ItemsSource = lstAdversaires;
            Thread trdRefresh = new Thread(() =>
            {
                RefreshAdversaires(index);
            });
            trdRefresh.Start();
            Thread.Yield();
        }

        private void cboPerso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Vérification ->  1 Adversaire sélectionné et un personnage sélectionné.
            btnAtt.IsEnabled = (dataGrid.SelectedIndex == -1 || cboPerso.SelectedIndex == -1) ? false : true;
        }

        private void cboTypeAdversaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAtt.IsEnabled = false;
            lstAdversaires = new ObservableCollection<Adversaire>();
            int index = cboTypeAdversaire.SelectedIndex;
            dataGrid.Items.Refresh();
            dataGrid.Columns.Clear();
            dataGrid.ItemsSource = lstAdversaires;
            Thread trdRefresh = new Thread(() =>
                {
                    RefreshAdversaires(index);
                });
            trdRefresh.Start();
            Thread.Yield();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAtt.IsEnabled = (dataGrid.SelectedIndex == -1 || cboPerso.SelectedIndex == -1) ? false : true;
        }

        private void RefreshAdversaires(int index)
        {
            List<string>[] Result = bd.selection((index == 0) ? "SELECT nom,valeur,nomUtilisateur FROM Entites e INNER JOIN Joueurs j ON e.idJoueur = j.idJoueur INNER JOIN statistiquesentites s ON e.idEntite = s.idEntite WHERE idTypeStatistique = 13 AND j.nomUtilisateur != 'Monstre' AND e.idJoueur != (SELECT idJoueur FROM Joueurs WHERE nomUtilisateur = '" + nomUtilisateur + "')" : "SELECT nom,valeurMin,valeurMax FROM Entites e INNER JOIN Joueurs j ON e.idJoueur = j.idJoueur INNER JOIN statistiquesentites s ON e.idEntite = s.idEntite WHERE s.idTypeStatistique = 13 AND j.nomUtilisateur ='Monstre';");
            Dispatcher.Invoke(new Action(() =>
             {
                 foreach (List<string> enti in Result)
                 {
                     if (index != 0)
                     {
                         int min = Statistique.toLevel((double.Parse(enti[1])));
                         int max = Statistique.toLevel((double.Parse(enti[2])));
                         lstAdversaires.Add(new Adversaire(enti[0], min, max));
                     }
                     else
                     {
                         int niveau = Statistique.toLevel((double.Parse(enti[1])));
                         lstAdversaires.Add(new AdversaireHumain(enti[0], niveau, enti[2]));
                     }
                 }
                 dataGrid.ItemsSource = lstAdversaires;
                 if (index == 0)
                 {
                     DataGridTextColumn textColumn = new DataGridTextColumn();
                     textColumn.Header = "Propriétaire";
                     textColumn.Binding = new Binding("proprietaire");
                     dataGrid.Columns.Add(textColumn);
                     textColumn = new DataGridTextColumn();
                     textColumn.Header = "Niveau";
                     textColumn.Binding = new Binding("trueLevel");
                     dataGrid.Columns.Add(textColumn);
                     textColumn = new DataGridTextColumn();
                     textColumn.Header = "Nom";
                     textColumn.Binding = new Binding("nom");
                     dataGrid.Columns.Add(textColumn);
                     dataGrid.FrozenColumnCount = 3;
                 }
                 else
                 {
                     DataGridTextColumn textColumn = new DataGridTextColumn();
                     textColumn.Header = "Nom";
                     textColumn.Binding = new Binding("nom");
                     dataGrid.Columns.Add(textColumn);
                     textColumn = new DataGridTextColumn();
                     textColumn.Header = "Niveau minimal";
                     textColumn.Binding = new Binding("levelMin");
                     dataGrid.Columns.Add(textColumn);
                     textColumn = new DataGridTextColumn();
                     textColumn.Header = "Niveau maximal";
                     textColumn.Binding = new Binding("levelMax");
                     dataGrid.Columns.Add(textColumn);
                     textColumn = new DataGridTextColumn();
                     dataGrid.FrozenColumnCount = 3;
                 }
                 dataGrid.Items.Refresh();
             }));
        }
    }
}
