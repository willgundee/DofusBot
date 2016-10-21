using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Logique d'interaction pour PagePerso.xaml
    /// </summary>
    public partial class PagePerso : UserControl
    {

        private Joueur Player;
      
        public PagePerso(Entite ent, Joueur Player  )
        {
            InitializeComponent();
            this.Player = Player;

            int nbScript = Player.LstScripts.Count;
            for (int i = 0; i < nbScript; i++)
            {
                cbScript.Items.Add(Player.LstScripts[i].Nom);

            }
            foreach (Entite perso in Player.LstEntites)
            {
                //todo création de plusieurs onglets personnage
                lblNom.Content = perso.Nom;
                lblNomClasse.Content = perso.ClasseEntite.Nom;
                lblNbPointsC.Content = perso.CapitalLibre;
                string SourceImgClasse = "resources/" + perso.ClasseEntite.Nom;
                double Exp;
                BitmapImage path = new BitmapImage();
                path.BeginInit();
                path.UriSource = new Uri(SourceImgClasse + ".png", UriKind.Relative);
                path.EndInit();
                Imgclasse.Source = path;

                cbScript.SelectedItem = perso.ScriptEntite.Nom;

                foreach (Statistique st in perso.LstStats)
                {
                    if (st.Nom == Statistique.element.experience)
                        Exp = st.Valeur;
                }

                dgStats.ItemsSource = initialiserLstStats(perso);
                dgDommage.ItemsSource = initialiserLstDMG(perso);
                dgResistance.ItemsSource = initialiserLstRES(perso);

            }

        }


        private List<Statistique> initialiserLstStats(Entite perso)
        {
            List<Statistique> lstStat = new List<Statistique>();

            for (int i = 0; i < 11; i++)
            {
                foreach (Statistique stat in perso.LstStats)
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
                            if (stat.Nom == Statistique.element.portee)
                                lstStat.Add(stat);
                            break;
                        case 5:
                            if (stat.Nom == Statistique.element.vitalite)
                                lstStat.Add(stat);
                            break;
                        case 6:
                            if (stat.Nom == Statistique.element.sagesse)
                                lstStat.Add(stat);
                            break;
                        case 7:
                            if (stat.Nom == Statistique.element.force)
                                lstStat.Add(stat);
                            break;
                        case 8:
                            if (stat.Nom == Statistique.element.intelligence)
                                lstStat.Add(stat);
                            break;
                        case 9:
                            if (stat.Nom == Statistique.element.chance)
                                lstStat.Add(stat);
                            break;
                        case 10:
                            if (stat.Nom == Statistique.element.agilite)
                                lstStat.Add(stat);
                            break;

                        default:
                            break;
                    }
                }
            }
            return lstStat;
        }


        private List<Statistique> initialiserLstDMG(Entite perso)
        {
            List<Statistique> LstDMG = new List<Statistique>();

            for (int i = 0; i < 5; i++)
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
                        default:
                            break;
                    }

                }

            }
            return LstDMG;
        }
        private List<Statistique> initialiserLstRES(Entite perso)
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
        }


        private void imageCasque_MouseDown(object sender, MouseButtonEventArgs e)
        {
            String TypeEquipement = "Chapeau";

            PageEquipement Equip = new PageEquipement(TypeEquipement, Player.NomUtilisateur);
            Equip.ShowDialog();

        }

        private void imageCape_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            String TypeEquipement = "Cape";

            PageEquipement Equip = new PageEquipement(TypeEquipement, Player.NomUtilisateur);
            Equip.ShowDialog();
        }

        private void imageArme_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            String TypeEquipement = "Arme";

            PageEquipement Equip = new PageEquipement(TypeEquipement, Player.NomUtilisateur);
            Equip.ShowDialog();
        }

        private void imageAnneau1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            String TypeEquipement = "Chapeau";

            PageEquipement Equip = new PageEquipement(TypeEquipement, Player.NomUtilisateur);
            Equip.ShowDialog();
        }

        private void imageBotte_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            String TypeEquipement = "botte";

            PageEquipement Equip = new PageEquipement(TypeEquipement, Player.NomUtilisateur);
            Equip.ShowDialog();
        }

        private void imageCeinture_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            String TypeEquipement = "Ceinture";

            PageEquipement Equip = new PageEquipement(TypeEquipement, Player.NomUtilisateur);
            Equip.ShowDialog();
        }
    }
}