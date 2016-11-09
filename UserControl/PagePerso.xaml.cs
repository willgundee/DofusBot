using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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


        public PagePerso(Entite ent, Joueur Player)
        {// refaire le min/max
            InitializeComponent();
            this.Player = Player;
            persoActuel = ent;
            lblLevelEntite.Content = "Niv. " + ent.Niveau;

            #region .
            /* pgbExp.Foreground = new SolidColorBrush(Colors.CornflowerBlue);
             pgbExp.Background = new SolidColorBrush(Colors.Chartreuse);*/
            #endregion
            pgbExp.Maximum = ent.LstStats.First(x => x.Nom == Statistique.element.experience).dictLvl[ent.Niveau + 1];
            pgbExp.Minimum = ent.LstStats.First(x => x.Nom == Statistique.element.experience).dictLvl[ent.Niveau];
            pgbExp.Value = ent.LstStats.First(x => x.Nom == Statistique.element.experience).Valeur;

            //lblPourcentExp.Content = Math.Round(pgbExp.Value / (pgbExp.Maximum-pgbExp.Minimum)) + " %";

            pgbExp.ToolTip = ent.LstStats.First(x => x.Nom == Statistique.element.experience).Valeur.ToString() + " sur " + ent.LstStats.First(x => x.Nom == Statistique.element.experience).dictLvl[ent.LstStats.First(x => x.Nom == Statistique.element.experience).toLevel() + 1].ToString() + " exp";
            int nbScript = Player.LstScripts.Count;
            for (int i = 0; i < nbScript; i++)
                cbScript.Items.Add(Player.LstScripts[i].Nom);

            //todo création de plusieurs onglets personnage
            lblNomClasse.Content = ent.ClasseEntite.Nom;
            lblNbPointsC.Content = ent.CapitalLibre;
            double Exp;
            string SourceImgClasse = "../resources/" + ent.ClasseEntite.Nom;
            BitmapImage path = new BitmapImage();
            path.BeginInit();
            path.UriSource = new Uri(SourceImgClasse + ".png", UriKind.Relative);
            path.EndInit();
            Imgclasse.Source = path;
            //itmCtrlDesc.ItemsSource = LstDesc;
            foreach (Equipement item in ent.LstEquipements)
            {
                List<string> emplacement = bd.selection("SELECT emplacement FROM Equipementsentites WHERE idEquipement = (SELECT idEquipement FROM Equipements WHERE nom='" + item.Nom + "' )AND idEntite =(SELECT idEntite FROM Entites WHERE nom='" + ent.Nom + "')")[0];

                if (emplacement != null)
                    AfficherElementEquipe(item, emplacement[0].ToString());
            }
            cbScript.SelectedValue = ent.ScriptEntite.Nom;

            if (ent.CapitalLibre > 0)
            {
                btnAgilite.Visibility = Visibility.Visible;
                btnChance.Visibility = Visibility.Visible;
                btnForce.Visibility = Visibility.Visible;
                btnIntelligence.Visibility = Visibility.Visible;
                btnSagesse.Visibility = Visibility.Visible;
                btnVitalite.Visibility = Visibility.Visible;
            }

            foreach (Statistique st in ent.LstStats)
            {
                if (st.Nom == Statistique.element.experience)
                    Exp = st.Valeur;
            }
            initialiserLstStats(ent.LstStats);
            dgStats.ItemsSource = lstStat;
            dgDommage.ItemsSource = initialiserLstDMG(ent);



        }

        #region grid_listes
        private void initialiserLstStats(ObservableCollection<Statistique> lstStats)
        {
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
        private List<Statistique> initialiserLstDMG(Entite perso)
        {
            List<Statistique> LstDMG = new List<Statistique>();

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
        /*      private List<Statistique> initialiserLstRES(Entite perso)
              {
                  List<Statistique> LstRES = new List<Statistique>();

                  for (int i = 0; i < 5; i++)
                  {
                      foreach (Statistique stat in perso.LstStats)
                      {
                          switch (i)
                          {
                              case 0:
                                  if (stat.Nom == Statistique.element.RES_neutre)
                                      LstRES.Add(stat);
                                  break;
                              case 1:
                                  if (stat.Nom == Statistique.element.RES_terre)
                                      LstRES.Add(stat);
                                  break;
                              case 2:
                                  if (stat.Nom == Statistique.element.RES_feu)
                                      LstRES.Add(stat);
                                  break;
                              case 3:
                                  if (stat.Nom == Statistique.element.RES_air)
                                      LstRES.Add(stat);
                                  break;
                              case 4:
                                  if (stat.Nom == Statistique.element.RES_eau)
                                      LstRES.Add(stat);
                                  break;
                              default:
                                  break;
                          }

                      }

                  }
                  return LstRES;
              }*/
        #endregion

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
        private void AfficherElementEquipe(Equipement eq, string emp)
        {
            ImageSource path = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/items/200/" + eq.NoImg + ".png"));
            switch (emp)
            {
                case "tête":
                    imgChapeauInv.Source = path;
                    break;
                case "dos":
                    imgCapeInv.Source = path;
                    break;
                case "arme":
                    imgArmeInv.Source = path;
                    break;
                case "hanche":
                    imgCeintureInv.Source = path;
                    break;
                case "ano1":
                    imgAnneau1Inv.Source = path;
                    break;
                case "ano2":
                    imgAnneau2Inv.Source = path;
                    break;
                case "pied":
                    imgBotteInv.Source = path;
                    break;
                case "cou":
                    imgAmuletteInv.Source = path;
                    break;
            }
        }

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

            //TODO: l'add dans la list d'equipement du perso quand tu la drop dedans et l'enlever l'inverse
            //TODO: bouger l'image au lieu de rien
            //TODO: le modif dans bd

            if (Player.LstEntites.First(x => x.Nom == persoActuel.Nom).peutEquiper(itemVoulantEtreEquiper))
            {
                switch (cible.Name)
                { //emplacement possible : tete, cou, pied, ano1, ano2, arme, hanche, dos.

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
            Inventaire i = (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Inventaire)) as Inventaire);


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

            if (Convert.ToInt32(lblNbPointsC.Content) < 1)
            {
                btnAgilite.Visibility = Visibility.Hidden;
                btnChance.Visibility = Visibility.Hidden;
                btnForce.Visibility = Visibility.Hidden;
                btnIntelligence.Visibility = Visibility.Hidden;
                btnSagesse.Visibility = Visibility.Hidden;
                btnVitalite.Visibility = Visibility.Hidden;
                return;
            }

            string choix = (sender as Button).Name;
            Statistique.element s;
            int changement;


            switch (choix.ToString())
            {
                case "btnVitalite":
                    s = Statistique.element.vitalite;
                    // modif = Statistique.element.vie;                          
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


            foreach (Statistique sts in persoActuel.LstStats)
                if (sts.Nom == s)
                {
                    sts.Valeur += 1;
                    string valeurInitial = "SELECT valeur FROM statistiquesEntites WHERE idEntite = (SELECT idEntite FROM Entites WHERE  nom ='" + persoActuel.Nom + "') AND idTypeStatistique=(SELECT idTypeStatistique FROM typesStatistiques WHERE nom ='" + s.ToString() + "' ) ";
                    int values = Convert.ToInt32(bd.selection(valeurInitial)[0][0]) + 1;
                    bd.Update("UPDATE statistiquesEntites SET valeur = " + values + " WHERE idEntite = (SELECT idEntite FROM Entites WHERE  nom ='" + persoActuel.Nom + "') AND idTypeStatistique=(SELECT idTypeStatistique FROM typesStatistiques WHERE nom ='" + s.ToString() + "' ) ");
                    break;
                }

            initialiserLstStats(persoActuel.LstStats);

            changement = Convert.ToInt32(lblNbPointsC.Content);
            lblNbPointsC.Content = (changement - 1);
            bd.Update("UPDATE Entites SET CapitalLibre =" + lblNbPointsC.Content + " WHERE nom ='" + persoActuel.Nom + "' ");
        }

        private void btnInventaire_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Inventaire inv = null;
            if (Application.Current.Windows.Cast<Window>().FirstOrDefault(x => x.GetType() == typeof(Inventaire)) == null)
            {
                inv = new Inventaire(Player);
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

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClickDesequip(object sender, RoutedEventArgs e)
        {
            Image item = ((sender as MenuItem).Parent as ContextMenu).DataContext as Image;

            Equipement equiper = Player.LstEntites.First(x => x.Nom == persoActuel.Nom).LstEquipements.First(x => x.NoImg == convertPathToNoItem(item.Source.ToString()));
            item.Source = new BitmapImage(new Uri("../ resources / vide.png", UriKind.Relative));
            Player.LstEntites.First(x => x.Nom == persoActuel.Nom).enleverItem(equiper);
        }


        private void ClickVendre(object sender, RoutedEventArgs e)
        {

        }
    }
}
