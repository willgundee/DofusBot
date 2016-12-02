using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using GofusSharp;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Gofus
{
    public enum ScrollBarType : uint
    {
        SbHorz = 0,
        SbVert = 1,
        SbCtl = 2,
        SbBoth = 3
    }
    public enum Message : uint
    {
        WM_VSCROLL = 0x0115
    }
    public enum ScrollBarCommands : uint
    {
        SB_THUMBPOSITION = 4
    }

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public BDService bd = new BDService();

        #region Variable De Binding Lou

        ObservableCollection<ImageItem> LstImgItems;
        ObservableCollection<string> LstStats;
        ObservableCollection<string> LstConds;
        ObservableCollection<string> LstCaras;
        #endregion

        public Joueur Player { get; set; }

        public int idJoueur { get; set; }

        private pageClavardage pgchat;
        private PageDocumentation pgDoc;
        private pAdmin pgAdmin;
        private Timer refreshConnection = new Timer();

        public MainWindow(int id)
        {
            refreshConnection.Interval = 8000;
            refreshConnection.Tick += RefreshConnection_Tick;
            refreshConnection.Start();
            //CombatTest combat = new CombatTest();
            InitializeComponent();
            Player = new Joueur(bd.selection("SELECT * FROM Joueurs WHERE idJoueur = " + id)[0]);
            idJoueur = id;
            pgchat = new pageClavardage(Player.NomUtilisateur, false, id.ToString());
            contentClavardage.Content = pgchat;
            string n = " -" + Player.NomUtilisateur;
            pgDoc = new PageDocumentation();

            Title += n;
            if (Player.estAdmin)
            {
                pgAdmin = new pAdmin();
                PaneauAdmin.Visibility = Visibility.Visible;
                controlAdmin.Content = pgAdmin;
            }
            else
            {
                PaneauAdmin.Visibility = Visibility.Hidden;
            }
            #region Lou
            LstImgItems = new ObservableCollection<ImageItem>();
            LstStats = new ObservableCollection<string>();
            LstConds = new ObservableCollection<string>();
            LstCaras = new ObservableCollection<string>();

            itmCtrlEquip.ItemsSource = LstImgItems;
            lbxStats.ItemsSource = LstStats;
            lbxCond.ItemsSource = LstConds;
            lbxCara.ItemsSource = LstCaras;

            fillSortCbo();
            #endregion
        }

        private void RefreshConnection_Tick(object sender, EventArgs e)
        {
            BackgroundWorker bgWorker = new BackgroundWorker() { WorkerReportsProgress = true };
            bgWorker.DoWork += (s, z) =>
            {
                bd.selection("SELECT RELEASE_LOCK('" + idJoueur + "')");
                bd.selection("SELECT GET_LOCK('" + idJoueur + "',10)");
            };

            bgWorker.RunWorkerAsync();

        }

        protected override void OnClosed(EventArgs e)
        {/*
            System.Threading.Thread ThreadBD = new System.Threading.Thread(new System.Threading.ThreadStart(() => bd.Update("UPDATE  Joueurs SET  estConnecte =  0 WHERE  nomUtilisateur  ='" + Player.NomUtilisateur + "'")));
            ThreadBD.Start();
            bool test = bd.Update("UPDATE  Joueurs SET  estConnecte =  0 WHERE  nomUtilisateur  ='" + Player.NomUtilisateur + "'");
            */
            bd.selection("SELECT RELEASE_LOCK('" + idJoueur + "')");

            System.Threading.Thread.Sleep(1000);
            if (pgchat != null)
            {
                pgchat.aTimer.Stop();
                pgchat.chat.CloseConnection();
            }

            if (pgchat.fenetreChat != null)
            {
                pgchat.fenetreChat.pgCht.aTimer.Stop();
                pgchat.fenetreChat.pgCht.chat.CloseConnection();
                pgchat.fenetreChat.Close();
            }

            //  pgperso.timer.Stop();
            if (System.Windows.Application.Current.Windows.Cast<Window>().FirstOrDefault(x => x.GetType() == typeof(PageInventaire)) != null)
                System.Windows.Application.Current.Windows.Cast<Window>().FirstOrDefault(x => x.GetType() == typeof(PageInventaire)).Close();
            if (System.Windows.Application.Current.Windows.Cast<Window>().FirstOrDefault(x => x.GetType() == typeof(PageDocumentation)) != null)
                System.Windows.Application.Current.Windows.Cast<Window>().FirstOrDefault(x => x.GetType() == typeof(PageDocumentation)).Close();
            base.OnClosed(e);

        }



        #region Marché
        /// <summary>
        /// action du bouton d'achat du marché
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAchat_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult m = System.Windows.MessageBox.Show("Voulez vous vraiment acheter l'objet : " + lblItem.Content + ". Au cout de " + lblPrix.Content + " Kamas ?", "Achat", MessageBoxButton.YesNo, MessageBoxImage.Information);// affichage d'un message box te demandant situ veut acheter ceci
            if (m == MessageBoxResult.Yes)
            {// si oui 
                Player.Kamas -= (int)lblPrix.Content; // je change l'argent du joueur
                string up1 = "UPDATE  Joueurs SET  argent =  " + Player.Kamas.ToString() + " WHERE  nomUtilisateur  ='" + Player.NomUtilisateur + "';COMMIT;";
                bd.Update(up1);// et dans la bd 
                string sel1 = "SELECT je.quantite,je.idJoueurEquipement FROM joueursequipements je INNER JOIN joueurs j ON je.idJoueur = j.idJoueur  INNER JOIN Equipements e ON e.idEquipement = je.idEquipement WHERE e.nom ='" + lblItem.Content.ToString() + "' AND j.nomUtilisateur = '" + Player.NomUtilisateur + "'";
                List<string> rep = bd.selection(sel1)[0];
                // je regarde s'il a deja cette item dans son inventaire 
                if (rep[0] == "rien")// si non je l'ajoute en bd
                {
                    string ins1 = "INSERT INTO  JoueursEquipements (idJoueur ,idEquipement ,quantite ,quantiteEquipe) VALUES ( (SELECT idJoueur FROM Joueurs WHERE nomUtilisateur = '" + Player.NomUtilisateur + "'),(SELECT idEquipement FROM Equipements WHERE nom = '" + lblItem.Content.ToString() + "') ,1, 0);COMMIT;";
                    bd.insertion(ins1);
                }
                else// ou je change la quantité posseder
                {
                    string up2 = "UPDATE JoueursEquipements SET  quantite =  " + (Convert.ToInt16(rep[0]) + 1).ToString() + " WHERE  idJoueurEquipement =" + rep[1] + ";COMMIT;";
                    bd.Update(up2);
                }

                if (rep[0] == "rien")// et sur lui
                {
                    string sel2 = "SELECT * FROM Equipements WHERE nom = '" + lblItem.Content.ToString() + "'";
                    string sel3 = "SELECT * FROM Joueurs WHERE nomUtilisateur='" + Player.NomUtilisateur + "'";
                    Player.Inventaire.Add(new Equipement(bd.selection(sel2)[0], true, Convert.ToInt32(bd.selection(sel3)[0][0])));
                }
                else// change la quantité sur le joueur
                    Player.Inventaire.First(x => x.Nom == lblItem.Content.ToString()).Quantite++;
            }
            lblKamas.Content = Player.Kamas;// actualise son argent

            if (Player.Kamas < (int)lblPrix.Content)
                btnAchat.IsEnabled = false;// active le bouton ou non s'il a encore assé d'argent
            else
                btnAchat.IsEnabled = true;
        }

        /// <summary>
        /// action d'un click d'une image dans le marché
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_MouseUp(object sender, MouseButtonEventArgs e)
        {

            #region Abracadabra
            btnAchat.Visibility = Visibility.Visible;
            lblPri.Visibility = Visibility.Visible;
            lblMoney.Visibility = Visibility.Visible;
            lblKamas.Visibility = Visibility.Visible;
            tabControlStats.Visibility = Visibility.Visible;
            #endregion

            if (imgCurrent.Source == (((ImageItem)sender).imgItem.Source))// si tu veut les description de l'item que tu a deja
                return;

            lblKamas.Content = Player.Kamas;
            LstCaras.Clear();//réinitialisation des list d'infos
            LstStats.Clear();
            LstConds.Clear();

            imgCurrent.Source = ((ImageItem)sender).imgItem.Source;// met l'article choisi en gros
            string info = "SELECT * FROM Equipements  WHERE nom ='" + ((ImageItem)sender).txtNom.Text.ToString() + "'";

            Equipement item = new Equipement(bd.selection(info)[0], true, 0);// crée l'item
            lblItem.Content = item.Nom;

            lblPrix.Content = item.Prix;
            if (Player.Kamas < (int)lblPrix.Content)
                btnAchat.IsEnabled = false;
            else
                btnAchat.IsEnabled = true;

            txtBDesc.Text = item.Desc;

            // affiche les info
            if (item.EstArme)
            {
                tbCara.Visibility = Visibility.Visible;
                foreach (Effet effet in item.LstEffets)
                    LstStats.Add(effet.NomSimplifier + " : " + effet.DmgMin + " à " + effet.DmgMax);
                LstCaras.Add("Pa requis : " + item.Pa);
                LstCaras.Add("Portée : " + item.ZonePortee.Nom + " de " + (item.ZonePortee.PorteeMax == item.ZonePortee.PorteeMin ? item.ZonePortee.PorteeMax.ToString() : item.ZonePortee.PorteeMin.ToString() + " à " + item.ZonePortee.PorteeMax.ToString()));
                //LstCaras.Add("Zone d'effet : " + item.ZoneEffet.Nom + " de " + (item.ZoneEffet.PorteeMax == item.ZoneEffet.PorteeMin ? item.ZoneEffet.PorteeMax.ToString() : item.ZoneEffet.PorteeMin.ToString() + " à " + item.ZoneEffet.PorteeMax.ToString()));

            }
            else
                tbCara.Visibility = Visibility.Hidden;

            foreach (Statistique stat in item.LstStatistiques)
                LstStats.Add(stat.NomSimple + " : " + stat.Valeur.ToString());

            foreach (Condition cond in item.LstConditions)
                if (cond.Stat.Nom == Statistique.element.experience)
                {
                    lblLvl.Content = "Catégorie : " + item.Type + "  Niv." + cond.Stat.toLevel().ToString();
                    LstConds.Add("Niveau requis : " + cond.Stat.toLevel().ToString());
                }
                else
                    LstConds.Add(cond.Stat.NomSimple + " " + cond.Signe + "  " + cond.Stat.Valeur.ToString());
        }
        /// <summary>
        /// rempli les combobox
        /// </summary>
        private void fillSortCbo()
        {
            List<string> type = new List<string>();
            List<string> entitesNom = new List<string>();
            type.Add("Tous");
            foreach (List<string> typeNom in bd.selection("SELECT nom FROM typesEquipements"))
                type.Add(typeNom[0]);
            cboTrie.ItemsSource = type;
            cboTrie.SelectedIndex = 0;

            foreach (Entite perso in Player.LstEntites)
                entitesNom.Add(perso.Nom);// TODO ne marchera pas si crée un perso marche
        }

        /// <summary>
        /// quand tu change de trie ceci change les items afficher
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboTrie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string type = ((System.Windows.Controls.ComboBox)sender).SelectedValue.ToString();
            LstImgItems.Clear();
            string query = "SELECT * FROM Equipements  e INNER JOIN ConditionsEquipements c ON c.idEquipement = e.idEquipement INNER JOIN TypesEquipements t ON t.idTypeEquipement = e.idTypeEquipement WHERE idCondition = 21 " + (type == "Tous" ? "" : "AND t.nom ='" + type + "'") + " ORDER BY c.valeur  LIMIT 10 OFFSET 0";// les 10 premier item triée par ordre de niveau
            string count = "SELECT COUNT(*) FROM Equipements  e INNER JOIN ConditionsEquipements c ON c.idEquipement = e.idEquipement INNER JOIN TypesEquipements t ON t.idTypeEquipement = e.idTypeEquipement WHERE idCondition = 21 " + (type == "Tous" ? "" : "AND t.nom ='" + type + "'");

            List<string>[] items = bd.selection(query);
            dckLink.Children.Clear();
            if (items[0][0] != "rien")
            {
                createPageLinks(Convert.ToInt32(bd.selection(count)[0][0]), 1);// créations des numeros de pages
                retrieveItem(items);// affidhe les items
            }
        }

        /// <summary>
        /// action d'un click sur un numero de page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_link_click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT * FROM Equipements  e INNER JOIN ConditionsEquipements c ON c.idEquipement = e.idEquipement INNER JOIN TypesEquipements t ON t.idTypeEquipement = e.idTypeEquipement WHERE idCondition = 21 " + ((string)cboTrie.SelectedValue == "Tous" ? "" : "AND t.nom ='" + (string)cboTrie.SelectedValue + "'") + " ORDER BY c.valeur  LIMIT 10 OFFSET " + ((Convert.ToInt16(((System.Windows.Controls.Button)sender).Content) - 1) * 10).ToString();// choisi avec un offset les xieme articles a afficher
            string count = "SELECT COUNT(*) FROM Equipements  e INNER JOIN ConditionsEquipements c ON c.idEquipement = e.idEquipement INNER JOIN TypesEquipements t ON t.idTypeEquipement = e.idTypeEquipement WHERE idCondition = 21 " + ((string)cboTrie.SelectedValue == "Tous" ? "" : "AND t.nom ='" + (string)cboTrie.SelectedValue + "'");

            LstImgItems.Clear();
            dckLink.Children.Clear();
            List<string>[] items = bd.selection(query);
            if (items[0][0] != "rien")
            {
                createPageLinks(Convert.ToInt32(bd.selection(count)[0][0]), Convert.ToInt16(((System.Windows.Controls.Button)sender).Content));
                retrieveItem(items);
            }
        }

        /// <summary>
        /// crée les numeros des pages 
        /// </summary>
        /// <param name="nbPages"></param>
        /// <param name="actuel"></param>
        private void createPageLinks(int nbPages, int actuel)
        {            //<Button Style="{StaticResource LinkButton}" Content="Clicky" />
            if (nbPages % 10 == 0)
                nbPages = nbPages / 10;
            else
                nbPages = (nbPages / 10) + 1;


            for (int i = 0; i < nbPages; i++)
            {
                System.Windows.Controls.Button btn = new System.Windows.Controls.Button();
                btn.Style = (Style)FindResource("LinkButton");
                btn.Click += btn_link_click;
                btn.Content = (i + 1).ToString();
                if (i + 1 == actuel)
                {
                    btn.Foreground = Brushes.Purple;
                    btn.IsEnabled = false;
                }
                dckLink.Children.Add(btn);
                if (i != nbPages - 1)// page actuelle tu la désactive et la met d'une autre couleurs
                {
                    System.Windows.Controls.Label lbl = new System.Windows.Controls.Label();
                    lbl.Content = " - ";
                    lbl.Height = 21;
                    lbl.VerticalAlignment = VerticalAlignment.Center;
                    dckLink.Children.Add(lbl);
                }
            }
        }

        /// <summary>
        /// ajoute les items dans le marché
        /// </summary>
        /// <param name="items"></param>
        private void retrieveItem(List<string>[] items)
        {
            foreach (List<string> item in items)
            {
                Equipement equip = new Equipement(item, false, 0);
                ImageItem i = new ImageItem(equip, false, 0);
                i.MouseDown += image_MouseUp;
                LstImgItems.Add(i);
            }
        }

        #endregion


        #region Michael("Francis")/Perso
        // ***************************************************
        //Onglet Personnage
        // ***************************************************
        public void TabPerso_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Entite perso in Player.LstEntites)
            {

                TabItem onglet = new TabItem();
                onglet.Header = perso.Nom;
                onglet.Content = new PagePerso(perso, Player);
                tCPerso.Items.Add(onglet);
            }

            tCPerso.SelectedIndex = 0;
            if (tCPerso.Items.Count <= 4)
            {
                TabItem onglet = new TabItem();
                onglet.Header = "+";
                onglet.Content = new pageCpersonage(Player);
                tCPerso.Items.Add(onglet);
            }
        }

        #endregion

        #region UserControls

        private void PgAdmin_Selected(object sender, RoutedEventArgs e)
        {


        }




        private void PgArchive_Selected(object sender, RoutedEventArgs e)
        {
            if (!controlArchive.HasContent)
                controlArchive.Content = new pageArchive(idJoueur);
        }

        private void PgArene_Selected(object sender, RoutedEventArgs e)
        {
            if (!controlArene.HasContent)
                controlArene.Content = new pageArene(idJoueur, Player.LstEntites);
            else
            {
                ((pageArene)controlArene.Content).RefreshPersos(Player.LstEntites);
            }

        }

        private void PgGestion_Selected(object sender, RoutedEventArgs e)
        {
            if (!controlGestion.HasContent)
                controlGestion.Content = new pageGestion(Player, idJoueur);
        }
        #endregion


        private void TabItem_Unselected(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Application.Current.Windows.Cast<Window>().FirstOrDefault(x => x.GetType() == typeof(PageInventaire)) != null)
                System.Windows.Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(PageInventaire)).Close();

        }

        private void PGDoc_Selected(object sender, RoutedEventArgs e)
        {
            if(PGDoc.Content == null)
            PGDoc.Content = new PageDoc();
        }

        private void TabItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (System.Windows.Application.Current.Windows.Cast<Window>().FirstOrDefault(x => x.GetType() == typeof(FenetreScript)) == null)
            {
                FenetreScript fs = new FenetreScript();
                fs.Show();
            }
        }

        private void TabItem_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
