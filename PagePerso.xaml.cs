using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using System.IO;

namespace test
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
        {

            InitializeComponent();
            this.Player = Player;
            persoActuel = ent;

            int nbScript = Player.LstScripts.Count;
            for (int i = 0; i < nbScript; i++)
                cbScript.Items.Add(Player.LstScripts[i].Nom);

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

                if (perso.CapitalLibre > 0)
                {
                    btnAgilite.Visibility = Visibility.Visible;
                    btnChance.Visibility = Visibility.Visible;
                    btnForce.Visibility = Visibility.Visible;
                    btnIntelligence.Visibility = Visibility.Visible;
                    btnSagesse.Visibility = Visibility.Visible;
                    btnVitalite.Visibility = Visibility.Visible;
                }

                foreach (Statistique st in perso.LstStats)
                {
                    if (st.Nom == Statistique.element.experience)
                        Exp = st.Valeur;
                }
                foreach (Equipement eq in perso.LstEquipements)
                {
                    List<string> emplacement = bd.selection("SELECT emplacement FROM Equipementsentites WHERE idEquipement = (SELECT idEquipement FROM Equipements WHERE nom='" + eq.Nom + "' )AND idEntite =(SELECT idEntite FROM Entites WHERE nom='" + perso.Nom + "')")[0];

                    if (emplacement != null)
                        AfficherElementEquipe(eq, emplacement[0].ToString());
                }
                calculervalues(Player);
                initialiserLstStats(perso.LstStats);
                dgStats.ItemsSource = lstStat;
                dgDommage.ItemsSource = initialiserLstDMG(perso);
                dgResistance.ItemsSource = initialiserLstRES(perso);
            }
        }

        #region grid_listes
        private void initialiserLstStats(ObservableCollection<Statistique> lstStats)
        {
            lstStat.Clear();
            for (int i = 0; i < 11; i++)
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
        #endregion

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image source = (sender as Image);
            string choix = (sender as Image).Name;
            string TypeEquipement = null;
            string emp = null;

            switch (choix.ToString())
            {
                case "imageCasque":
                    TypeEquipement = "Chapeau";
                    emp = "tête";
                    break;
                case "imageCape":
                    TypeEquipement = "Cape";
                    emp = "dos";
                    break;
                case "imageArme":
                    TypeEquipement = "Arme";
                    emp = "arme";
                    break;
                case "imageCeinture":
                    TypeEquipement = "Ceinture";
                    emp = "hanche";
                    break;
                case "imageAnneau1":
                    TypeEquipement = "Anneau";
                    emp = "ano1";
                    break;
                case "imageAnneau2":
                    TypeEquipement = "Anneau";
                    emp = "ano2";
                    break;
                case "imageBotte":
                    TypeEquipement = "Botte";
                    emp = "pied";
                    break;
                case "imageAmulette":
                    TypeEquipement = "Amulette";
                    emp = "cou";
                    break;
            }

            PageEquipement Equip = new PageEquipement(TypeEquipement, emp, source, Player);
            if (validePg != false)
                Equip.ShowDialog();

        }

        private void AfficherElementEquipe(Equipement eq, string emp)
        {
            ImageSource path = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/items/200/" + eq.NoImg + ".png"));
            switch (emp)
            {
                case "tête":
                    imageCasque.Source = path;
                    break;
                case "dos":
                    imageCape.Source = path;
                    break;
                case "arme":
                    imageArme.Source = path;
                    break;
                case "hanche":
                    imageCeinture.Source = path;
                    break;
                case "ano1":
                    imageAnneau1.Source = path;
                    break;
                case "ano2":
                    imageAnneau2.Source = path;
                    break;
                case "pied":
                    imageBotte.Source = path;
                    break;
                case "cou":
                    imageAmulette.Source = path;
                    break;
            }
        }

        private void btnStatsPlus_Click(object sender, RoutedEventArgs e)
        {

            if (Convert.ToInt32(lblNbPointsC.Content) == 1)
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

                    break;
                }


            initialiserLstStats(persoActuel.LstStats);

            changement = Convert.ToInt32(lblNbPointsC.Content);
            lblNbPointsC.Content = (changement - 1);
        }

        public void calculervalues(Joueur player)
        {
            foreach (Image img in grdEquip.Children)
            {
                string imag = Path.GetFileNameWithoutExtension(img.Source.ToString().Split('/').Last()).ToString();
                if (imag != "vide")
                {
                    foreach (List<string> item in bd.selection("SELECT idTypeStatistique , valeur FROM StatistiquesEquipements WHERE idEquipement=(SELECT idEquipement From Equipements WHERE noImage=" + imag + ")"))
                    {
                        foreach (List<string> e in bd.selection("SELECT * FROM TypesStatistiques WHERE idTypeStatistique =" + item[0]))
                        {
                         
                            foreach (Statistique sts in persoActuel.LstStats)
                                if (sts.Nom.ToString() == e[1])
                                {
                                    sts.Valeur = Convert.ToInt32(e[0]);

                                    break;
                                }

                        }


                    }
                }
              
            }

        }
    }
}
