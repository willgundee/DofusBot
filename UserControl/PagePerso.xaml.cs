using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour PagePerso.xaml
    /// </summary>
    public partial class PagePerso : UserControl
    {
        public bool validePg;
        private Joueur Player;
        public BDService bd = new BDService();
        public Entite persoActuel;
        public ObservableCollection<Statistique> lstStat = new ObservableCollection<Statistique>();
        public int nbScript = 0;
        public int refresh = 0;
        public DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Création de la page personnage
        /// </summary>
        /// <param name="ent">les entites du joueurs</param>
        /// <param name="Player">un Joueur</param>
        public PagePerso(Entite ent, Joueur Player)
        {// refaire le min/max
            InitializeComponent();
            this.Player = Player;
            //refresh a tous les 5 secondes
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += timer_Tick;
            timer.Start();
            //focntion qui crée la page
            starter(Player, ent);
        }
        /// <summary>
        /// le refresh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            BackgroundWorker bgWorker = new BackgroundWorker() { WorkerReportsProgress = true };
            bgWorker.DoWork += (s, z) =>
            {   //s'il y a au moins un personnage
                List<string> entit = bd.selection("SELECT * FROM Entites WHERE nom='" + persoActuel.Nom + "'")[0];
                if (entit[0] != "rien")
                {
                    persoActuel = new Entite(entit);
                    //on crée la fenêtre avec les informations du joueur et du perso actuel
                    starter(Player, persoActuel);
                }
            };
            bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Fucntion qui construit le coeur de 
        /// </summary>
        /// <param name="player">le Joueur</param>
        /// <param name="ent">les entites</param>
        private void starter(Joueur player, Entite ent)
        {
            persoActuel = ent;
            Dispatcher.Invoke(new Action(() =>
            {//affiche le niveau du personnage
                lblLevelEntite.Content = "Niv. " + ent.Niveau;
                //affiche le nom du joueur
                lblNomJoueur.Content = Player.NomUtilisateur;

            }));
            //si le niveau est inférieur a 200(niveau maximum)
            if (ent.Niveau < 200)
            {//on set la bar d'experience
                Dispatcher.Invoke(new Action(() =>
                {
                    //set le max et le min de la bar d'experience
                    pgbExp.Maximum = Statistique.dictLvl[ent.Niveau + 1];
                    pgbExp.Minimum = Statistique.dictLvl[ent.Niveau];
                    //info dans le tool tip
                    pgbExp.ToolTip = ent.LstStats.First(x => x.Nom == Statistique.element.experience).Valeur.ToString() + " sur " + Statistique.dictLvl[ent.LstStats.First(x => x.Nom == Statistique.element.experience).toLevel() + 1].ToString() + " exp";
                }));
            }
            else
            {  //si le niveau est supérieur a 200(niveau maximum)
                Dispatcher.Invoke(new Action(() =>
                {//set le max et le min de la bar d'experience
                    pgbExp.Maximum = ent.LstStats.First(x => x.Nom == Statistique.element.experience).Valeur;
                    pgbExp.Minimum = ent.LstStats.First(x => x.Nom == Statistique.element.experience).Valeur;
                    //info dans le tool tip
                    pgbExp.ToolTip = "IT'S OVER 9000!!!";
                }));
            }
            Dispatcher.Invoke(new Action(() =>
            {//set la valeur dans la bar d'expérience
                pgbExp.Value = ent.LstStats.First(x => x.Nom == Statistique.element.experience).Valeur;
            }));

            nbScript = Player.LstScripts.Count();
            Dispatcher.Invoke(new Action(() =>
            {
                cbScript.Items.Clear();
                //met tous les scripts dans la combo box de script
                foreach (Script item in player.LstScripts)
                    cbScript.Items.Add(item.Nom);
            }));

            Dispatcher.Invoke(new Action(() =>
            {//affiche le nom de la classe
                lblNomClasse.Content = ent.ClasseEntite.Nom;
                //affiche les capitals libres restants
                lblNbPointsC.Content = ent.CapitalLibre;
            }));
            double Exp;
            //on affiche l'image de la classe
            string SourceImgClasse = "../resources/" + ent.ClasseEntite.Nom;
            Dispatcher.BeginInvoke((Action)delegate
            {
                Imgclasse.Source = new BitmapImage(new Uri(SourceImgClasse + ".png", UriKind.Relative));
            });
            Dispatcher.BeginInvoke((Action)delegate
            {
                BitmapImage path = new BitmapImage(new Uri("../resources/fondEquipement.jpg", UriKind.Relative));

                Imgfond.Source = path;
            });

            //permet d'afficher les équipements équipés sur un personnage
            List<string>[] equipementsPerso = bd.selection("SELECT ee.emplacement,e.noImage FROM Equipementsentites ee INNER JOIN equipements e ON ee.idEquipement = e.idEquipement WHERE  idEntite =(SELECT idEntite FROM Entites WHERE nom='" + ent.Nom + "')");
            Dispatcher.Invoke(new Action(() =>
            {
                if (equipementsPerso[0][0] != "rien")
                    foreach (List<string> item in equipementsPerso)
                    {
                        ImageSource pathImg = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/items/200/" + item[1] + ".png"));
                        switch (item[0])
                        {
                            case "tête":
                                imgChapeauInv.Source = pathImg;
                                break;
                            case "dos":
                                imgCapeInv.Source = pathImg;
                                break;
                            case "arme":
                                imgArmeInv.Source = pathImg;
                                break;
                            case "hanche":
                                imgCeintureInv.Source = pathImg;
                                break;
                            case "ano1":
                                imgAnneau1Inv.Source = pathImg;
                                break;
                            case "ano2":
                                imgAnneau2Inv.Source = pathImg;
                                break;
                            case "pied":
                                imgBotteInv.Source = pathImg;
                                break;
                            case "cou":
                                imgAmuletteInv.Source = pathImg;
                                break;
                        }
                    }
            }));
            Dispatcher.Invoke(new Action(() =>
            {//met le script utilisé par le personnage par défaut
                cbScript.SelectedValue = ent.ScriptEntite.Nom;
            }));
            #region btnStats
            //bouton + pour les statistique doivent être visible seulement s'il y a des points capitals disponible
            Dispatcher.Invoke(new Action(() =>
            {
                if (ent.CapitalLibre > 0)
                {
                    btnAgilite.Visibility = Visibility.Visible;
                    btnChance.Visibility = Visibility.Visible;
                    btnForce.Visibility = Visibility.Visible;
                    btnIntelligence.Visibility = Visibility.Visible;
                    btnSagesse.Visibility = Visibility.Visible;
                    btnVitalite.Visibility = Visibility.Visible;
                }
            }));
            #endregion
            foreach (Statistique st in ent.LstStats)
            {
                if (st.Nom == Statistique.element.experience)
                    Exp = st.Valeur;
            }

            Dispatcher.Invoke(new Action(() =>
            {//crée met les infos dans la grid de statistique de base
                initialiserLstStats(ent.LstStats);
                dgStats.ItemsSource = lstStat;
                //crée met les infos dans la grid de statistique de avancé
                dgDommage.ItemsSource = initialiserLstDMG(ent);
            }));
            refresh++;

        }
        #region grid_listes
        /// <summary>
        /// initialise la list de stats de bases
        /// </summary>
        /// <param name="lstStats">la liste de tous ls statistiques</param>
        private void initialiserLstStats(ObservableCollection<Statistique> lstStats)
        {
            //on ajoute les statistique a la liste dans l'ordre voulue
            lstStat.Clear();
            for (int i = 0; i < 12; i++)
            {
                foreach (Statistique stat in lstStats)
                {
                    switch (i)
                    {
                        case 0:
                            if (stat.Nom == Statistique.element.vie)
                                lstStat.Add(stat);
                            break;
                        case 1:
                            if (stat.Nom == Statistique.element.PA)
                                lstStat.Add(stat);
                            break;
                        case 2:
                            if (stat.Nom == Statistique.element.PM)
                                lstStat.Add(stat);
                            break;
                        case 3:
                            if (stat.Nom == Statistique.element.initiative)
                                lstStat.Add(stat);
                            break;
                        case 4:
                            if (stat.Nom == Statistique.element.invocation)
                                lstStat.Add(stat);
                            break;
                        case 5:
                            if (stat.Nom == Statistique.element.portee)
                                lstStat.Add(stat);
                            break;
                        case 6:
                            if (stat.Nom == Statistique.element.vitalite)
                                lstStat.Add(stat);
                            break;
                        case 7:
                            if (stat.Nom == Statistique.element.sagesse)
                                lstStat.Add(stat);
                            break;
                        case 8:
                            if (stat.Nom == Statistique.element.force)
                                lstStat.Add(stat);
                            break;
                        case 9:
                            if (stat.Nom == Statistique.element.intelligence)
                                lstStat.Add(stat);
                            break;
                        case 10:
                            if (stat.Nom == Statistique.element.chance)
                                lstStat.Add(stat);
                            break;
                        case 11:
                            if (stat.Nom == Statistique.element.agilite)
                                lstStat.Add(stat);
                            break;

                        default:
                            break;
                    }
                }
            }

        }
        /// <summary>
        /// initilalise la liste de stats avancées
        /// </summary>
        /// <param name="perso">Joueur</param>
        /// <returns></returns>
        private List<Statistique> initialiserLstDMG(Entite perso)
        {
            List<Statistique> LstDMG = new List<Statistique>();
            //on ajoute les statistique a la liste dans l'ordre voulue
            for (int i = 0; i < 24; i++)
            {
                foreach (Statistique stat in perso.LstStats)
                {
                    switch (i)
                    {
                        case 0:
                            if (stat.Nom == Statistique.element.DMG_neutre)
                                LstDMG.Add(stat);
                            break;
                        case 1:
                            if (stat.Nom == Statistique.element.DMG_terre)
                                LstDMG.Add(stat);
                            break;
                        case 2:
                            if (stat.Nom == Statistique.element.DMG_feu)
                                LstDMG.Add(stat);
                            break;
                        case 3:
                            if (stat.Nom == Statistique.element.DMG_air)
                                LstDMG.Add(stat);
                            break;
                        case 4:
                            if (stat.Nom == Statistique.element.DMG_eau)
                                LstDMG.Add(stat);
                            break;
                        case 5:
                            if (stat.Nom == Statistique.element.DMG_poussee)
                                LstDMG.Add(stat);
                            break;
                        case 6:
                            if (stat.Nom == Statistique.element.renvoie_DMG)
                                LstDMG.Add(stat);
                            break;
                        case 7:
                            if (stat.Nom == Statistique.element.puissance)
                                LstDMG.Add(stat);
                            break;
                        case 8:
                            if (stat.Nom == Statistique.element.RES_neutre)
                                LstDMG.Add(stat);
                            break;
                        case 9:
                            if (stat.Nom == Statistique.element.RES_terre)
                                LstDMG.Add(stat);
                            break;
                        case 10:
                            if (stat.Nom == Statistique.element.RES_feu)
                                LstDMG.Add(stat);
                            break;
                        case 11:
                            if (stat.Nom == Statistique.element.RES_air)
                                LstDMG.Add(stat);
                            break;
                        case 12:
                            if (stat.Nom == Statistique.element.RES_eau)
                                LstDMG.Add(stat);
                            break;
                        case 13:
                            if (stat.Nom == Statistique.element.RES_poussee)
                                LstDMG.Add(stat);
                            break;
                        case 14:
                            if (stat.Nom == Statistique.element.RES_Pourcent_neutre)
                                LstDMG.Add(stat);
                            break;
                        case 15:
                            if (stat.Nom == Statistique.element.RES_Pourcent_feu)
                                LstDMG.Add(stat);
                            break;
                        case 16:
                            if (stat.Nom == Statistique.element.RES_Pourcent_air)
                                LstDMG.Add(stat);
                            break;
                        case 17:
                            if (stat.Nom == Statistique.element.RES_Pourcent_terre)
                                LstDMG.Add(stat);
                            break;
                        case 18:
                            if (stat.Nom == Statistique.element.RES_Pourcent_eau)
                                LstDMG.Add(stat);
                            break;
                        case 19:
                            if (stat.Nom == Statistique.element.retrait_PA)
                                LstDMG.Add(stat);
                            break;
                        case 20:
                            if (stat.Nom == Statistique.element.retrait_PM)
                                LstDMG.Add(stat);
                            break;
                        case 21:
                            if (stat.Nom == Statistique.element.esquive_PA)
                                LstDMG.Add(stat);
                            break;
                        case 22:
                            if (stat.Nom == Statistique.element.esquive_PM)
                                LstDMG.Add(stat);
                            break;
                        case 23:
                            if (stat.Nom == Statistique.element.soin)
                                LstDMG.Add(stat);
                            break;

                        default:
                            break;
                    }
                }
            }
            return LstDMG;
        }
        #endregion
        /// <summary>
        /// bouton qui ouvre l'inventaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgInv_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                string i = convertPathToNoItem((sender as Image).Source.ToString());
                if (i != "vide")
                {
                    itmCtrlDesc.Items.Clear();
                    itmCtrlDesc.Items.Add(new DescItem(Player.Inventaire.First(x => x.NoImg == i)));
                }
            }
        }
        /// <summary>
        /// drag and drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgInv_Drop(object sender, System.Windows.DragEventArgs e)
        {

            Image cible = (Image)sender;
            ImageItem data = e.Data.GetData("image") as ImageItem;
            Equipement itemDejaEquipe = null;
            Equipement itemVoulantEtreEquiper = Player.Inventaire.First(x => x.NoImg == convertPathToNoItem(data.imgItem.Source.ToString()));
            string emplacement = "";
            if (itemVoulantEtreEquiper.Type == "Anneau")
            {
                borderAno1.BorderBrush = Brushes.Transparent;
                borderAno2.BorderBrush = Brushes.Transparent;
                imgAnneau1Inv.AllowDrop = false;
                imgAnneau2Inv.AllowDrop = false;
            }
            else
            {
                (cible.Parent as Border).BorderBrush = Brushes.Transparent;
                cible.AllowDrop = false;
            }



            if (convertPathToNoItem(cible.Source.ToString()) != "vide")
                itemDejaEquipe = Player.Inventaire.First(x => x.NoImg == convertPathToNoItem(cible.Source.ToString()));

            if (Player.LstEntites.First(x => x.Nom == persoActuel.Nom).peutEquiper(itemVoulantEtreEquiper))
            {
                emplacement = TrouveEmplacement(cible);

                cible.Source = data.imgItem.Source;

                if (itemDejaEquipe != null)
                {
                    Player.LstEntites.First(x => x.Nom == persoActuel.Nom).enleverItem(Player.LstEntites.First(x => x.Nom == persoActuel.Nom).LstEquipements.First(x => x.Nom == itemDejaEquipe.Nom));
                    Player.Inventaire.First(x => x.Nom == itemDejaEquipe.Nom).QuantiteEquipe--;
                    bd.Update("UPDATE equipementsentites SET idEquipement = (SELECT idEquipement FROM Equipements WHERE nom = '" + itemVoulantEtreEquiper.Nom + "') WHERE emplacement='" + emplacement + "' AND idEntite = (SELECT idEntite FROM Entites WHERE nom ='" + persoActuel.Nom + "')");
                    bd.Update("UPDATE JoueursEquipements SET quantiteEquipe= " + Player.Inventaire.First(x => x.Nom == itemDejaEquipe.Nom).QuantiteEquipe.ToString() + " WHERE idJoueur = (SELECT idJoueur FROM Joueurs WHERE nomUtilisateur='" + Player.NomUtilisateur + "') AND idEquipement= (SELECT idEquipement FROM Equipements WHERE nom ='" + itemDejaEquipe.Nom + "')");

                }
                else
                    bd.insertion("INSERT INTO equipementsEntites (idEquipement,idEntite,Emplacement)VALUES((SELECT idEquipement FROM Equipements WHERE nom ='" + itemVoulantEtreEquiper.Nom + "'),(SELECT idEntite FROM Entites WHERE nom ='" + persoActuel.Nom + "'),'" + emplacement + "');COMMIT;");

                Player.LstEntites.First(x => x.Nom == persoActuel.Nom).ajouterEquipement(itemVoulantEtreEquiper);
                Player.Inventaire.First(x => x.Nom == itemVoulantEtreEquiper.Nom).QuantiteEquipe++;
                bd.Update("UPDATE JoueursEquipements SET quantiteEquipe= " + Player.Inventaire.First(x => x.Nom == itemVoulantEtreEquiper.Nom).QuantiteEquipe.ToString() + " WHERE idJoueur = (SELECT idJoueur FROM Joueurs WHERE nomUtilisateur='" + Player.NomUtilisateur + "') AND idEquipement= (SELECT idEquipement FROM Equipements WHERE nom ='" + itemVoulantEtreEquiper.Nom + "')");

            }
            PageInventaire i = (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(PageInventaire)) as PageInventaire);


            if (i._dragdropWindow != null)
            {
                i._dragdropWindow.Close();
                i._dragdropWindow = null;
            }

            i.refreshInv();
            initialiserLstStats(persoActuel.LstStats);
            dgStats.ItemsSource = lstStat;
            dgDommage.ItemsSource = initialiserLstDMG(persoActuel);

        }
        private string convertPathToNoItem(string path)
        {
            return System.IO.Path.GetFileNameWithoutExtension(path.Split('/').Last());
        }

        private void btnStatsPlus_Click(object sender, RoutedEventArgs e)
        {
            //si le nombre de point =0 on quitte 
            if (Convert.ToInt32(lblNbPointsC.Content) < 1)
            {
                return;
            }
            //cache les bouton + si on n'a plus de  point capital
            if (Convert.ToInt32(lblNbPointsC.Content) <= 1)
            {
                btnAgilite.Visibility = Visibility.Hidden;
                btnChance.Visibility = Visibility.Hidden;
                btnForce.Visibility = Visibility.Hidden;
                btnIntelligence.Visibility = Visibility.Hidden;
                btnSagesse.Visibility = Visibility.Hidden;
                btnVitalite.Visibility = Visibility.Hidden;
            }

            string choix = (sender as Button).Name;
            Statistique.element s;
            int changement;

            //si on ajoute des statistiques on augmente la valeur avec son nom
            switch (choix.ToString())
            {
                case "btnVitalite":
                    s = Statistique.element.vitalite;
                    //pour chaque statistique 
                    foreach (Statistique st in persoActuel.LstStats)
                        //si le nom = vie
                        if (st.Nom == Statistique.element.vie)
                        {
                            st.Valeur += 1;
                            //on sélectionne la valeur actuel de la vie
                            string valeurInitial = "SELECT valeur FROM statistiquesEntites WHERE idEntite = (SELECT idEntite FROM Entites WHERE  nom ='" + persoActuel.Nom + "') AND idTypeStatistique=(SELECT idTypeStatistique FROM typesStatistiques WHERE nom ='" + st.Nom + "' ) ";
                            int values = Convert.ToInt32(bd.selection(valeurInitial)[0][0]) + 1;
                            //on met a jour les statistiques avec les nouvelles valeurs
                            bd.Update("UPDATE statistiquesEntites SET valeur = " + values + " WHERE idEntite = (SELECT idEntite FROM Entites WHERE  nom ='" + persoActuel.Nom + "') AND idTypeStatistique=(SELECT idTypeStatistique FROM typesStatistiques WHERE nom ='" + st.Nom + "' ) ");
                            break;
                        }
                    break;
                case "btnSagesse":
                    s = Statistique.element.sagesse;
                    break;
                case "btnForce":
                    s = Statistique.element.force;
                    break;
                case "btnIntelligence":
                    s = Statistique.element.intelligence;
                    break;
                case "btnChance":
                    s = Statistique.element.chance;
                    break;
                case "btnAgilite":
                    s = Statistique.element.agilite;
                    break;
                default:

                    return;
            }

            //on aujment la valeur de la statistique qui a été augmenté
            foreach (Statistique sts in persoActuel.LstStats)
                if (sts.Nom == s)
                {
                    sts.Valeur += 1;
                    string valeurInitial = "SELECT valeur FROM statistiquesEntites WHERE idEntite = (SELECT idEntite FROM Entites WHERE  nom ='" + persoActuel.Nom + "') AND idTypeStatistique=(SELECT idTypeStatistique FROM typesStatistiques WHERE nom ='" + s.ToString() + "' ) ";
                    int values = Convert.ToInt32(bd.selection(valeurInitial)[0][0]) + 1;
                    bd.Update("UPDATE statistiquesEntites SET valeur = " + values + " WHERE idEntite = (SELECT idEntite FROM Entites WHERE  nom ='" + persoActuel.Nom + "') AND idTypeStatistique=(SELECT idTypeStatistique FROM typesStatistiques WHERE nom ='" + s.ToString() + "' ) ");
                    break;
                }
            //on refresh la list de stats
            initialiserLstStats(persoActuel.LstStats);
            //on diminu le nombre de points capitals disponible
            changement = Convert.ToInt32(lblNbPointsC.Content);
            lblNbPointsC.Content = (changement - 1);
            //on change en bd le nombre de points capitals disponible
            bd.Update("UPDATE Entites SET CapitalLibre =" + lblNbPointsC.Content + " WHERE nom ='" + persoActuel.Nom + "' ");
        }

        private void btnInventaire_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PageInventaire inv = null;
            if (Application.Current.Windows.Cast<Window>().FirstOrDefault(x => x.GetType() == typeof(PageInventaire)) == null)
            {
                inv = new PageInventaire(Player);
                inv.Show();
            }

        }

        private void imgInv_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContextMenu cm = FindResource("cmClick") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.DataContext = sender as Image;
            cm.IsOpen = true;
        }


        private void ClickDesequip(object sender, RoutedEventArgs e)
        {
            Image item = ((sender as MenuItem).Parent as ContextMenu).DataContext as Image;
            if (convertPathToNoItem(item.Source.ToString()) != "vide")
            {
                string emplacement = TrouveEmplacement(item);
                Equipement equiper = Player.LstEntites.First(x => x.Nom == persoActuel.Nom).LstEquipements.First(x => x.NoImg == convertPathToNoItem(item.Source.ToString()));
                item.Source = new BitmapImage(new Uri("../resources/vide.png", UriKind.Relative));
                Player.LstEntites.First(x => x.Nom == persoActuel.Nom).enleverItem(equiper);
                Player.Inventaire.First(x => x.Nom == equiper.Nom).QuantiteEquipe--;
                bd.Update("UPDATE JoueursEquipements SET quantiteEquipe= " + Player.Inventaire.First(x => x.Nom == equiper.Nom).QuantiteEquipe.ToString() + " WHERE idJoueur = (SELECT idJoueur FROM Joueurs WHERE nomUtilisateur='" + Player.NomUtilisateur + "') AND idEquipement= (SELECT idEquipement FROM Equipements WHERE nom ='" + equiper.Nom + "');COMMIT;");
                bd.delete("DELETE FROM EquipementsEntites WHERE idEntite = (SELECT idEntite FROM Entites WHERE nom ='" + persoActuel.Nom + "') AND idEquipement= (SELECT idEquipement FROM Equipements WHERE nom ='" + equiper.Nom + "') AND emplacement ='" + emplacement + "'");

                if ((Application.Current.Windows.Cast<Window>().FirstOrDefault(x => x.GetType() == typeof(PageInventaire)) as PageInventaire) != null)
                    (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(PageInventaire)) as PageInventaire).refreshInv();
                initialiserLstStats(persoActuel.LstStats);
                dgStats.ItemsSource = lstStat;
                dgDommage.ItemsSource = initialiserLstDMG(persoActuel);

            }
        }
        /// <summary>
        /// trouve l'emplacement où l'équipement est équipé
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        private string TrouveEmplacement(Image img)
        {
            string emplacement = "";
            switch (img.Name)
            {
                case "imgCapeInv":
                    emplacement = "dos";
                    break;
                case "imgChapeauInv":
                    emplacement = "tête";
                    break;
                case "imgBotteInv":
                    emplacement = "pied";
                    break;
                case "imgCeintureInv":
                    emplacement = "hanche";
                    break;
                case "imgAnneau1Inv":
                    emplacement = "ano1";
                    break;
                case "imgAnneau2Inv":
                    emplacement = "ano2";
                    break;
                case "imgAmuletteInv":
                    emplacement = "cou";
                    break;
                case "imgArmeInv":
                    emplacement = "arme";
                    break;
            }
            return emplacement;
        }
        /// <summary>
        /// vendre des équipements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickVendre(object sender, RoutedEventArgs e)
        {
            Image item = ((sender as MenuItem).Parent as ContextMenu).DataContext as Image;

            Equipement equiper = null;
            if (convertPathToNoItem(item.Source.ToString()) != "vide")
            {
                equiper = Player.LstEntites.First(x => x.Nom == persoActuel.Nom).LstEquipements.First(x => x.NoImg == convertPathToNoItem(item.Source.ToString()));
                float k = equiper.Prix * (float)0.8;
                MessageBoxResult m = System.Windows.MessageBox.Show("Voulez vous vraiment vendre l'objet : " + equiper.Nom + ". Au cout de " + (int)k + " Kamas ?", "Achat", MessageBoxButton.YesNo, MessageBoxImage.Information);// affichage d'un message box te demandant situ veut vendre ceci
                if (m == MessageBoxResult.Yes)
                {
                    equiper = Player.LstEntites.First(x => x.Nom == persoActuel.Nom).LstEquipements.First(x => x.NoImg == convertPathToNoItem(item.Source.ToString()));
                    string emplacement = TrouveEmplacement(item);
                    item.Source = new BitmapImage(new Uri("../resources/vide.png", UriKind.Relative));
                    Player.LstEntites.First(x => x.Nom == persoActuel.Nom).enleverItem(equiper);
                    Player.Inventaire.First(x => x.Nom == equiper.Nom).QuantiteEquipe--;
                    Player.Inventaire.First(x => x.Nom == equiper.Nom).Quantite--;
                    if (Player.Inventaire.First(x => x.Nom == equiper.Nom).Quantite == 0)
                        bd.delete("DELETE FROM JoueursEquipements WHERE idJoueur = (SELECT idJoueur FROM Joueurs WHERE nomUtilisateur='" + Player.NomUtilisateur + "')AND idEquipement= (SELECT idEquipement FROM Equipements WHERE nom ='" + equiper.Nom + "')");
                    else
                        bd.Update("UPDATE JoueursEquipements SET quantiteEquipe= " + Player.Inventaire.First(x => x.Nom == equiper.Nom).QuantiteEquipe.ToString() + " WHERE idJoueur = (SELECT idJoueur FROM Joueurs WHERE nomUtilisateur='" + Player.NomUtilisateur + "') AND idEquipement= (SELECT idEquipement FROM Equipements WHERE nom ='" + equiper.Nom + "');COMMIT;");

                    bd.delete("DELETE FROM EquipementsEntites WHERE idEntite = (SELECT idEntite FROM Entites WHERE nom ='" + persoActuel.Nom + "') AND idEquipement= (SELECT idEquipement FROM Equipements WHERE nom ='" + equiper.Nom + "') AND emplacement ='" + emplacement + "'");
                    Player.Kamas += (int)k;

                    bd.Update("UPDATE  Joueurs SET  argent =  " + Player.Kamas.ToString() + " WHERE  nomUtilisateur  ='" + Player.NomUtilisateur + "';COMMIT;");

                    if ((Application.Current.Windows.Cast<Window>().FirstOrDefault(x => x.GetType() == typeof(PageInventaire)) as PageInventaire) != null)
                    {
                        (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(PageInventaire)) as PageInventaire).refreshInv();
                        (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(PageInventaire)) as PageInventaire).lblArgent.Content = Player.Kamas;
                    }
                        (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(MainWindow)) as MainWindow).lblKamas.Content = Player.Kamas;

                    initialiserLstStats(persoActuel.LstStats);
                    dgStats.ItemsSource = lstStat;
                    dgDommage.ItemsSource = initialiserLstDMG(persoActuel);

                }

            }
        }
        /// <summary>
        /// permet de supprimer un de vos personnages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {//message pour confirmer que vous voulez supprimer ce personnage
            List<string> info = new List<string>();
            MessageBoxResult m = System.Windows.MessageBox.Show("Voulez-vous vraiment supprimer ce personages! ", "Avertisement", MessageBoxButton.YesNo, MessageBoxImage.Information);// affichage d'un message box te demandant situ veut acheter ceci
            //si oui
            if (m == MessageBoxResult.Yes)
            {   //on deséquipe le personnage pour que ses équipements équipés ne soient pas perdu
                List<string>[] idequip = bd.selection("SELECT idEquipement FROM EquipementsEntites ee INNER JOIN Entites e ON ee.idEntite = e.idEntite WHERE e.nom = '" + persoActuel.Nom + "' ");
                //s'il y a quelque chose d'équipé
                if (idequip[0][0] != "rien")
                    for (int i = 0; i < idequip.Count(); i++)
                    {
                        List<string>[] nomE = bd.selection("SELECT nom FROM Equipements WHERE idEquipement =" + idequip[i][0]);
                        info.Add(nomE[0][0]);
                        // on indique a l'utilisateur que ses équipement on été remis dans son inventaire
                        if (i + 1 == idequip.Count())
                            foreach (string item in info)
                            {
                                MessageBox.Show(item + " a été replacé dans l'inventaire");
                            }
                        //on met a jour l'inventaire dans la bd et sur le personnage
                        Player.Inventaire.First(x => x.Nom == nomE[0][0].ToString()).QuantiteEquipe--;
                        bd.Update("UPDATE JoueursEquipements SET quantiteEquipe= " + Player.Inventaire.First(x => x.Nom == nomE[0][0].ToString()).QuantiteEquipe.ToString() + " WHERE idJoueur = (SELECT idJoueur FROM Joueurs WHERE nomUtilisateur='" + Player.NomUtilisateur + "') AND idEquipement= (SELECT idEquipement FROM Equipements WHERE nom ='" + nomE[0][0].ToString() + "');COMMIT;");
                        bd.delete("DELETE FROM EquipementsEntites WHERE idEntite = (SELECT idEntite FROM Entites WHERE nom ='" + persoActuel.Nom + "') AND idEquipement= (SELECT idEquipement FROM Equipements WHERE nom ='" + nomE[0][0].ToString() + "')");

                    }
                //on supprime l'entite de la bd on commence par ses statistiques pui son supprime le personage
                bd.delete("DELETE FROM StatistiquesEntites WHERE idEntite=( SELECT idEntite FROM Entites WHERE nom='" + persoActuel.Nom + "')");
                bd.delete("DELETE FROM Entites WHERE nom='" + persoActuel.Nom + "'");
                //on le supprime de la liste d'entité du joueur
                Player.LstEntites.Remove(Player.LstEntites.First(x => x.Nom == persoActuel.Nom));
                //on enlève l'onglet qui porte le nom du personage.
                MainWindow main = (System.Windows.Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(MainWindow)) as MainWindow);
                main.tCPerso.Items.Remove(main.tCPerso.SelectedItem);
                foreach (Entite perso in Player.LstEntites)
                {
                    TabItem onglet = new TabItem();
                    onglet.Header = perso.Nom;
                    onglet.Content = new PagePerso(perso, Player);
                }
                main.tCPerso.SelectedIndex = 0;
                if (Player.LstEntites.Count == 4)
                {
                    TabItem onglet = new TabItem();
                    onglet.Header = "+";
                    onglet.Content = new pageCpersonage(Player);
                    main.tCPerso.Items.Add(onglet);
                }
                //on refresh l'inventaire pour voir les item qui y sont retournée.
                if (System.Windows.Application.Current.Windows.Cast<Window>().FirstOrDefault(x => x.GetType() == typeof(PageInventaire)) != null)
                    (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(PageInventaire)) as PageInventaire).refreshInv();
            }
            return;
        }
        /// <summary>
        /// resfresh la combo box avec les nouveaux scripts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbScript_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Script item in Player.LstScripts)
            {//on met en bd le script de l'entité utilise.
                if (cbScript.SelectedItem != null)
                    if (item.Nom == cbScript.SelectedItem.ToString())
                        bd.Update("UPDATE Entites SET idScript = (SELECT idScript FROM Scripts WHERE Uuid ='" + item.Uuid + "') WHERE nom ='" + persoActuel.Nom + "'");
            }

        }
    }
}
