using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using GofusSharp;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Windows.Media;
using System.Threading;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Shapes;
using Gofus;

namespace test
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

        private Window _dragdropWindow = null;
        ObservableCollection<ImageItem> LstImgItems;
        ObservableCollection<string> LstStats;
        ObservableCollection<string> LstConds;
        ObservableCollection<string> LstCaras;
        ObservableCollection<ImageItem> LstInventaire;
        ObservableCollection<DescItem> LstDesc;
        ObservableCollection<pArchives> LstArchive;
        System.Windows.Controls.ListBox dragSource = null;

        #endregion

        List<string> lstAvatars;

        public Joueur Player { get; set; }

        public FenetreRapport rapport;

        public int idJoueur { get; set; }

        public ObservableCollection<Gofus.pageSort> pgSort;

        #region Marc_Chat,FenetreChat,DispatcherTimer,ThreadEnvoie
        public Chat chat;
        DispatcherTimer aTimer;
        public ChatWindow fenetreChat;
        public Thread trdEnvoie { get; private set; }
        #endregion


        public MainWindow(int id)
        {
            //CombatTest combat = new CombatTest();
            InitializeComponent();
            Player = new Joueur(bd.selection("SELECT * FROM Joueurs WHERE idJoueur = " + id)[0]);
            idJoueur = id;
            lstAvatars = new List<string>();
            GenererAvatars();


            btnQuitterSalle.IsEnabled = false;


            string URI = lstAvatars[Player.Avatar];
            iAvatar.Source = new BitmapImage(new Uri(URI));

            lblEtat.Content = "État : Non connecté à la salle";
            lblEtat.Foreground = new SolidColorBrush(Colors.Orange);

            ctb_main.CreateTreeView(generateTree());
            ctb_main.UpdateSyntaxHightlight();
            ctb_main.UpdateTreeView();
            pgSort = new ObservableCollection<Gofus.pageSort>();

            LstArchive = new ObservableCollection<pArchives>();
            archive.ItemsSource = LstArchive;

            #region Lou
            LstImgItems = new ObservableCollection<ImageItem>();
            LstInventaire = new ObservableCollection<ImageItem>();
            LstDesc = new ObservableCollection<DescItem>();
            LstStats = new ObservableCollection<string>();
            LstConds = new ObservableCollection<string>();
            LstCaras = new ObservableCollection<string>();

            itmCtrlEquip.ItemsSource = LstImgItems;
            lbxStats.ItemsSource = LstStats;
            lbxCond.ItemsSource = LstConds;
            lbxCara.ItemsSource = LstCaras;
            lbxInventaire.ItemsSource = LstInventaire;
            itmCtrlDesc.ItemsSource = LstDesc;

            fillSortCbo();
            #endregion

            txt_AncienCourriel.Text = Player.Courriel;
            txt_nomUtilisateur.Text = Player.NomUtilisateur;


            #region Marc_TimerTick_Chat

            this.chat = new Chat();
            chat.nomUtilisateur = Player.NomUtilisateur;
            chat.getId();

            btnEnvoyerMessage.IsEnabled = false;


            aTimer = new DispatcherTimer();
            aTimer.Tick += new EventHandler(Timer_Tick);
            aTimer.Interval = new TimeSpan(0, 0, 1);
            #endregion

        }

        void GenererAvatars()
        {

            for (int J = 1; J < 94; J++)
            {
                string ajout;
                if (J > 10)
                    ajout = "";
                else
                    ajout = "0";
                string path = "http://staticns.ankama.com/dofus/www/game/items/200/180" + ajout + J.ToString() + ".png";
                lstAvatars.Add(path);
            }
        }

        /*   protected override void OnClosed(EventArgs e)
           {
               base.OnClosed(e);

               System.Windows.Application.Current.Shutdown();
           }
           */

        #region Marc_Chat(Timer,WPF,EnvoieMessage,Refresh)
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Updating the Label which displays the current second
            if (this != null)
            {

                ObservableCollection<string> messages = new ObservableCollection<string>();
                Thread trdRefresh = new Thread(() =>
                {

                    messages = chat.refreshChat();
                    System.Windows.Application.Current.Dispatcher.Invoke(new System.Action(() =>
                    {
                        txtboxHistorique.Text = "";
                        foreach (string m in messages)
                        {
                            txtboxHistorique.Text += m;
                        }
                    }));
                });
                trdRefresh.Start();
                Thread.Yield();

                CommandManager.InvalidateRequerySuggested();
            }
            else
            {
                aTimer.Stop();
            }
        }

        private void BtnEnvoyer_Click(object sender, RoutedEventArgs e)
        {
            string text = txtMessage.Text;
            trdEnvoie = new Thread(() => { chat.envoyerMessage(text); });
            trdEnvoie.Start();
            Thread.Yield();
        }


        private void txtMessage_TextChange(object sender, TextChangedEventArgs e)
        {


            if (txtMessage.Text.ToString() == "")
            {

                btnEnvoyerMessage.IsEnabled = false;

            }
            else
            {
                if (aTimer.IsEnabled)
                    btnEnvoyerMessage.IsEnabled = true;
            }
        }

        private void btnRejoindreSalle_Click(object sender, RoutedEventArgs e)
        {
            aTimer.Start();
            lblEtat.Content = "État : Connecter à la salle.";
            lblEtat.Foreground = new SolidColorBrush(Colors.ForestGreen);
            txtMessage.IsEnabled = true;
            btnRejoindreSalle.IsEnabled = false;
            btnQuitterSalle.IsEnabled = true;

        }

        private void btnQuitterSalle_Click(object sender, RoutedEventArgs e)
        {
            aTimer.Stop();
            txtMessage.Text = "";
            txtboxHistorique.Text = "";
            lblEtat.Content = "État : Déconnecter.";
            lblEtat.Foreground = new SolidColorBrush(Colors.Orange);

            btnEnvoyerMessage.IsEnabled = false;
            txtMessage.IsEnabled = false;

            btnRejoindreSalle.IsEnabled = true;
            btnQuitterSalle.IsEnabled = false;

        }

        public void MainWindow_ChatWindowClosing(object sender, System.EventArgs e)
        {
            fenetreChat = null;
        }

        private void BtnModLess_Click(object sender, RoutedEventArgs e)
        {

            if (fenetreChat != null)
            {
                fenetreChat.Activate();
            }
            else
            {
                fenetreChat = new ChatWindow(chat.nomUtilisateur);
                fenetreChat.Closed += MainWindow_ChatWindowClosing;
                fenetreChat.Show();
            }
        }
        #endregion

        #region Marc_OngletGestionCompte
        /// ***************************************************
        /// / ONGLET OPTIONS
        // ***************************************************
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool valide = true;
            StringBuilder UpdSt = new StringBuilder();
            UpdSt.Append("UPDATE Joueurs SET ");
            /* Faire un update si toute est legit*/
            if (txt_mdp.Password != "" && txt_mdp.Password == txtConfirmation.Password && txtConfirmation.Password != "" || txt_Courriel.Text != "")
            {
                /* Update */
                lbl_Mdp.Foreground = new SolidColorBrush(Colors.Black);
                lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Black);

                if (txt_Courriel.Text != "")
                {
                    if (txt_Courriel.Text != txt_AncienCourriel.Text)
                    {
                        lbl_Courriel.Foreground = new SolidColorBrush(Colors.Black);
                        UpdSt.Append("courriel = '" + txt_Courriel.Text + "'");
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Votre nouveau courriel doit être différent de l'ancien", "Courriel");
                        lbl_Courriel.Foreground = new SolidColorBrush(Colors.Red);
                        valide = false;
                    }

                }
                if (txt_mdp.Password != "" && txt_mdp.Password == txtConfirmation.Password && txtConfirmation.Password != "")
                {
                    string reqid = "SELECT motDePasse from Joueurs WHERE NomUtilisateur = '" + Player.NomUtilisateur + "';";
                    List<string>[] idResult = bd.selection(reqid);
                    string mdp = idResult[0][0];

                    if (txt_AncienMdp.Password == mdp)
                    {
                        UpdSt.Append(" , motDePasse = '" + txt_mdp.Password + "'");
                    }
                    else
                    {
                        lbl_AncienMdp.Foreground = new SolidColorBrush(Colors.Red);
                        System.Windows.Forms.MessageBox.Show("Votre Ancien mot de passe n'est pas valide", "Ancien mot de passe");
                        valide = false;
                    }


                }

            }
            else
            {
                if (txt_mdp.Password == "" && txtConfirmation.Password == "")
                {
                    /* Aucune modification effectué*/
                    lbl_Mdp.Foreground = new SolidColorBrush(Colors.Black);
                    lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Black);
                    lbl_Courriel.Foreground = new SolidColorBrush(Colors.Black);
                    valide = false;
                }
                else if (txt_mdp.Password != "")
                {
                    /* Erreur de confirmation*/
                    lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Red);
                    valide = false;
                    System.Windows.Forms.MessageBox.Show("Votre Confirmation doit être identique à votre nouveau mot de passe", "Confirmation");
                }
                else if (txt_mdp.Password == "" & txtConfirmation.Password != "")
                {
                    /* Mot de passe vide*/
                    lbl_Mdp.Foreground = new SolidColorBrush(Colors.Red);
                    valide = false;
                    System.Windows.Forms.MessageBox.Show("Votre Confirmation doit être identique à votre nouveau mot de passe", "Champs mot de passe vide");
                }
            }


            if (valide)
            {
                UpdSt.Append(" WHERE nomUtilisateur = '" + Player.NomUtilisateur + "';");
                string st = UpdSt.ToString();
                if (bd.Update(st))
                {
                    System.Windows.Forms.MessageBox.Show("Mise à jour avec succès de vos infos!!");
                }
                txt_AncienCourriel.Text = txt_Courriel.Text;
                Player.Courriel = txt_AncienCourriel.Text;
                txt_mdp.Password = "";
                txtConfirmation.Password = "";
                txt_Courriel.Text = "";
                lbl_Courriel.Foreground = new SolidColorBrush(Colors.Black);
                lbl_Mdp.Foreground = new SolidColorBrush(Colors.Black);
                lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Black);
                lbl_AncienMdp.Foreground = new SolidColorBrush(Colors.Black);

            }


        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            txtConfirmation.Password = "";
            txt_mdp.Password = "";
            txt_Courriel.Text = "";

            lbl_Mdp.Foreground = new SolidColorBrush(Colors.Black);
            lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void btnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            Authentification a = new Authentification();

            a.Show();
            Close();
        }

        public void MainWindow_RapportClosing(object sender, System.EventArgs e)
        {
            rapport = null;
        }



        private void btnSuggestion_Click(object sender, RoutedEventArgs e)
        {
            if (rapport != null)
            {
                rapport.Activate();
            }
            else
            {
                rapport = new FenetreRapport(idJoueur);
                rapport.Closed += MainWindow_RapportClosing;
                rapport.ShowDialog();
            }

        }

        private void Change_Avatar(object sender, MouseButtonEventArgs e)
        {
            ChangerAvatar();
        }


        private void ChangerAvatar()
        {

            choixAvatar choisir = new choixAvatar(lstAvatars, Player.Avatar);
            choisir.ShowDialog();

            string URI = lstAvatars[choisir.idAvatar];
            iAvatar.Source = new BitmapImage(new Uri(URI));
            Player.Avatar = choisir.idAvatar;


            bool upd = bd.Update("UPDATE  Joueurs SET  Avatar =  " + Player.Avatar + " WHERE  nomUtilisateur  ='" + Player.NomUtilisateur + "';COMMIT");
        }

        private void btnChange_Avatar(object sender, RoutedEventArgs e)
        {
            ChangerAvatar();
        }


        #endregion

        #region truc trop long de ced
        //**************************************************************************************************
        private void btn_run_Click(object sender, RoutedEventArgs e)
        {
            //code dynamique 
            string code = @"
                using GofusSharp;
                namespace Arene
                {
                    public class Combat
                    {
                        public static void Action(Terrain terrain, Entite Perso, Liste<EntiteInconnu> ListEntites)
                        {
                            user_code
                        }
                    }
                }
            ";

            //je remplace le mot user_code pour ce qui ce trouve dans la text box
            string finalCode = code.Replace("user_code", ctb_main.Text);
            //initialisation d'un compilateur de code C#
            CSharpCodeProvider provider = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
            //initialisation des paramètres du compilateur de code C#
            CompilerParameters parameters = new CompilerParameters();
            //ajout des lien de bibliothèque dynamique (dll)
            //parameters.ReferencedAssemblies.Add("WindowsBase.dll");
            parameters.ReferencedAssemblies.Add("GofusSharp.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            //System.Windows.Forms.MessageBox.Show(  );
            //compilation du code 
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, finalCode);
            //recherche d'érreurs de compilation
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(string.Format("Erreur (Ligne {0}): {1}", (error.Line - 8).ToString(), error.ErrorText));
                }
                System.Windows.Forms.MessageBox.Show(sb.ToString());
                return;
            }
            string codeAI = @"
            EntiteInconnu ennemi = null;
            foreach (EntiteInconnu entite in ListEntites)
            {
                if (entite.Equipe != Perso.Equipe)
                {
                    ennemi = entite;
                    break;
                }
            }
            if (terrain.DistanceEntreCases(Perso.Position, ennemi.Position) > 1)
            {
                int result = 1;
                while (result != 0 && result != -1)
                {
                    result = Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, ennemi.Position)[0], 1);
                }
            }
            Perso.UtiliserSort(Perso.ClasseEntite.TabSorts[1], ennemi);";
            Combat combat = new Combat(ctb_main.Text, codeAI);
        }

        //**************************************************************************************************
        public int maxLC = 0; //maxLineCount - should be public
        private void ctb_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            int linecount = ctb_main.Text.Split('\n').Count();
            if (linecount != maxLC)
            {
                rtb_lineNumber.Document.Blocks.Clear();
                for (int i = 1; i < linecount + 1; i++)
                {
                    if (i == 1)
                        rtb_lineNumber.AppendText(i.ToString());
                    else
                        rtb_lineNumber.AppendText(Environment.NewLine + i.ToString());
                }
                maxLC = linecount;
            }
            ctb_main_VScroll(new object(), new EventArgs());
        }


        #region generate tree
        private TreeNode[] generateTree()
        {
            //#############################################################################################################

            TreeNode[] treeNodeTab_method_chaine = new TreeNode[] {
                new TreeNode("Clone()"),
                new TreeNode("CompareTo()"),
                new TreeNode("Contains()"),
                new TreeNode("EndsWith()"),
                new TreeNode("Equals()"),
                new TreeNode("GetHashCode()"),
                new TreeNode("GetType()"),
                new TreeNode("GetTypeCode()"),
                new TreeNode("IndexOf()"),
                new TreeNode("ToLower()"),
                new TreeNode("ToUpper()"),
                new TreeNode("Insert()"),
                new TreeNode("IsNormalized()"),
                new TreeNode("LastIndexOf()"),
                new TreeNode("Remove()"),
                new TreeNode("Replace()"),
                new TreeNode("Split()"),
                new TreeNode("StartsWith()"),
                new TreeNode("Substring()"),
                new TreeNode("ToCharArray()"),
                new TreeNode("Trim()")
            };
            foreach (TreeNode Tnode in treeNodeTab_method_chaine)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "method";
                Tnode.Text = "system";
            }
            TreeNode[] treeNodeTab_attribut_chaine = new TreeNode[] {
                new TreeNode("Length")
            };
            foreach (TreeNode Tnode in treeNodeTab_attribut_chaine)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }
            TreeNode[] treeNodeTab_chaine = new TreeNode[treeNodeTab_method_chaine.Length + treeNodeTab_attribut_chaine.Length];
            treeNodeTab_method_chaine.CopyTo(treeNodeTab_chaine, 0);
            treeNodeTab_attribut_chaine.CopyTo(treeNodeTab_chaine, treeNodeTab_method_chaine.Length);
            //#############################################################################################################
            TreeNode[] treeNodeTab_simpleVar = new TreeNode[] {
                new TreeNode("CompareTo()"),
                new TreeNode("Equals()"),
                new TreeNode("GetHashCode()"),
                new TreeNode("GetType()"),
                new TreeNode("GetTypeCode()"),
                new TreeNode("ToString()", treeNodeTab_chaine)
            };
            foreach (TreeNode Tnode in treeNodeTab_simpleVar)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "class";
                Tnode.Text = "system";
            }
            //############################################################################################################# 
            TreeNode[] treeNodeTab_method_tab = new TreeNode[] {
                new TreeNode("Clone()"),
                new TreeNode("CopyTo()"),
                new TreeNode("Equals()"),
                new TreeNode("GetEnumerator()"),
                new TreeNode("GetHashCode()"),
                new TreeNode("GetLength()"),
                new TreeNode("GetLongLength()"),
                new TreeNode("GetLowerBound()"),
                new TreeNode("GetType()"),
                new TreeNode("GetUpperBound()"),
                new TreeNode("GetValue()"),
                new TreeNode("Initialize()"),
                new TreeNode("SetValue()"),
                new TreeNode("ToString()")
            };

            foreach (TreeNode Tnode in treeNodeTab_method_tab)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "method";
                Tnode.Text = "system";
            }
            TreeNode[] treeNodeTab_attribut_tab = new TreeNode[] {
                new TreeNode("IsFixedSize"),
                new TreeNode("IsReadOnly"),
                new TreeNode("IsSynchronized"),
                new TreeNode("Length"),
                new TreeNode("LongLength"),
                new TreeNode("Rank"),
                new TreeNode("SyncRoot")
            };
            foreach (TreeNode Tnode in treeNodeTab_attribut_tab)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }

            TreeNode[] treeNodeTab_tab = new TreeNode[treeNodeTab_method_tab.Length + treeNodeTab_attribut_tab.Length];
            treeNodeTab_method_tab.CopyTo(treeNodeTab_tab, 0);
            treeNodeTab_attribut_tab.CopyTo(treeNodeTab_tab, treeNodeTab_method_tab.Length);

            //#############################################################################################################
            TreeNode[] treeNodeTab_method_Liste = new TreeNode[] {
                new TreeNode("Add()"),
                new TreeNode("AddRange()"),
                new TreeNode("BinarySearch()"),
                new TreeNode("Clear()"),
                new TreeNode("Contains()"),
                new TreeNode("ConvertAll()"),
                new TreeNode("CopyTo()"),
                new TreeNode("Exists()"),
                new TreeNode("Find()"),
                new TreeNode("FindAll()"),
                new TreeNode("FindIndex()"),
                new TreeNode("FindLast()"),
                new TreeNode("ForEach()"),
                new TreeNode("GetEnumerator()"),
                new TreeNode("GetRange()"),
                new TreeNode("IndexOf()"),
                new TreeNode("Insert()"),
                new TreeNode("InsertRange()"),
                new TreeNode("LastIndexOf()"),
                new TreeNode("Remove()"),
                new TreeNode("RemoveAll()"),
                new TreeNode("RemoveAt()"),
                new TreeNode("RemoveRange()"),
                new TreeNode("Reverse()"),
                new TreeNode("Sort()"),
                new TreeNode("ToArray()"),
                new TreeNode("TrimExcess()"),
                new TreeNode("TrueForAll()")
            };

            foreach (TreeNode Tnode in treeNodeTab_method_tab)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "method";
                Tnode.Text = "system";
            }

            TreeNode[] treeNodeTab_attribut_Liste = new TreeNode[] {
                new TreeNode("Capacity"),
                new TreeNode("Count")
            };
            foreach (TreeNode Tnode in treeNodeTab_attribut_tab)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }

            TreeNode[] treeNodeTab_Liste = new TreeNode[treeNodeTab_method_Liste.Length + treeNodeTab_attribut_Liste.Length];
            treeNodeTab_method_Liste.CopyTo(treeNodeTab_Liste, 0);
            treeNodeTab_attribut_Liste.CopyTo(treeNodeTab_Liste, treeNodeTab_method_Liste.Length);

            //#############################################################################################################
            TreeNode[] treeNodeTab_method_math = new TreeNode[] {
                new TreeNode("Abs()", treeNodeTab_simpleVar),
                new TreeNode("Acos()", treeNodeTab_simpleVar),
                new TreeNode("Asin()", treeNodeTab_simpleVar),
                new TreeNode("Atan()", treeNodeTab_simpleVar),
                new TreeNode("Atan2()", treeNodeTab_simpleVar),
                new TreeNode("BigMul()", treeNodeTab_simpleVar),
                new TreeNode("Ceiling()", treeNodeTab_simpleVar),
                new TreeNode("Cos()", treeNodeTab_simpleVar),
                new TreeNode("Cosh()", treeNodeTab_simpleVar),
                new TreeNode("DivRem()", treeNodeTab_simpleVar),
                new TreeNode("Exp()", treeNodeTab_simpleVar),
                new TreeNode("Floor()", treeNodeTab_simpleVar),
                new TreeNode("Log()", treeNodeTab_simpleVar),
                new TreeNode("Log10()", treeNodeTab_simpleVar),
                new TreeNode("Max()", treeNodeTab_simpleVar),
                new TreeNode("Min()", treeNodeTab_simpleVar),
                new TreeNode("Pow()", treeNodeTab_simpleVar),
                new TreeNode("Round()", treeNodeTab_simpleVar),
                new TreeNode("Sign()", treeNodeTab_simpleVar),
                new TreeNode("Sin()", treeNodeTab_simpleVar),
                new TreeNode("Sinh()", treeNodeTab_simpleVar),
                new TreeNode("Sqrt()", treeNodeTab_simpleVar),
                new TreeNode("Tan()", treeNodeTab_simpleVar),
                new TreeNode("Tanh()", treeNodeTab_simpleVar),
                new TreeNode("Truncate()", treeNodeTab_simpleVar),
                new TreeNode("Sinh()", treeNodeTab_simpleVar),
                new TreeNode("Sqrt()", treeNodeTab_simpleVar),
                new TreeNode("Tan()", treeNodeTab_simpleVar),
                new TreeNode("Tanh()", treeNodeTab_simpleVar),
                new TreeNode("Truncate()", treeNodeTab_simpleVar)
            };

            foreach (TreeNode Tnode in treeNodeTab_method_math)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "method";
                Tnode.Text = "system";
            }


            TreeNode[] treeNodeTab_attribut_math = new TreeNode[] {
                new TreeNode("E", treeNodeTab_simpleVar),
                new TreeNode("PI", treeNodeTab_simpleVar)
            };
            foreach (TreeNode Tnode in treeNodeTab_attribut_math)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }

            TreeNode[] treeNodeTab_math = new TreeNode[treeNodeTab_method_math.Length + treeNodeTab_attribut_math.Length];
            treeNodeTab_method_math.CopyTo(treeNodeTab_math, 0);
            treeNodeTab_attribut_math.CopyTo(treeNodeTab_math, treeNodeTab_method_math.Length);

            //#############################################################################################################
            TreeNode[] treeNodeTab_keyword = new TreeNode[] {
                new TreeNode("abstract"),
                new TreeNode("as"),
                new TreeNode("base"),
                new TreeNode("bool"),
                new TreeNode("break"),
                new TreeNode("byte"),
                new TreeNode("case"),
                new TreeNode("catch"),
                new TreeNode("char"),
                new TreeNode("checked"),
                new TreeNode("class"),
                new TreeNode("const"),
                new TreeNode("continue"),
                new TreeNode("decimal"),
                new TreeNode("default"),
                new TreeNode("delegate"),
                new TreeNode("do"),
                new TreeNode("double"),
                new TreeNode("else"),
                new TreeNode("enum"),
                new TreeNode("event"),
                new TreeNode("explicit"),
                new TreeNode("extern"),
                new TreeNode("false"),
                new TreeNode("finally"),
                new TreeNode("fixed"),
                new TreeNode("float"),
                new TreeNode("for"),
                new TreeNode("foreach"),
                new TreeNode("goto"),
                new TreeNode("if"),
                new TreeNode("implicit"),
                new TreeNode("in"),
                new TreeNode("int"),
                new TreeNode("interface"),
                new TreeNode("internal"),
                new TreeNode("is"),
                new TreeNode("lock"),
                new TreeNode("long"),
                new TreeNode("namespace"),
                new TreeNode("new"),
                new TreeNode("null"),
                new TreeNode("object"),
                new TreeNode("operator"),
                new TreeNode("out"),
                new TreeNode("override"),
                new TreeNode("params"),
                new TreeNode("private"),
                new TreeNode("protected"),
                new TreeNode("public"),
                new TreeNode("readonly"),
                new TreeNode("ref"),
                new TreeNode("return"),
                new TreeNode("sbyte"),
                new TreeNode("sealed"),
                new TreeNode("short"),
                new TreeNode("sizeof"),
                new TreeNode("stackalloc"),
                new TreeNode("static"),
                new TreeNode("string"),
                new TreeNode("struct"),
                new TreeNode("switch"),
                new TreeNode("this"),
                new TreeNode("throw"),
                new TreeNode("true"),
                new TreeNode("try"),
                new TreeNode("typeof"),
                new TreeNode("uint"),
                new TreeNode("ulong"),
                new TreeNode("unchecked"),
                new TreeNode("unsafe"),
                new TreeNode("ushort"),
                new TreeNode("using"),
                new TreeNode("virtual"),
                new TreeNode("void"),
                new TreeNode("volatile"),
                new TreeNode("while")
            };
            foreach (TreeNode Tnode in treeNodeTab_keyword)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_T_zone_enum = new TreeNode[] {
                new TreeNode("cercle"),
                new TreeNode("ligne_verticale"),
                new TreeNode("ligne_horizontale"),
                new TreeNode("carre"),
                new TreeNode("croix"),
                new TreeNode("T"),
                new TreeNode("X"),
                new TreeNode("demi_cercle"),
                new TreeNode("cone"),
                new TreeNode("tous")
            };
            foreach (TreeNode Tnode in treeNodeTab_T_zone_enum)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            TreeNode[] treeNodeTab_T_zone = new TreeNode[] {
                new TreeNode("type",treeNodeTab_T_zone_enum)
            };
            foreach (TreeNode Tnode in treeNodeTab_T_zone)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_T_stat_enum = new TreeNode[] {
                new TreeNode("vie"),
                new TreeNode("force"),
                new TreeNode("intelligence"),
                new TreeNode("chance"),
                new TreeNode("agilite"),
                new TreeNode("vitalite"),
                new TreeNode("sagesse"),
                new TreeNode("PA"),
                new TreeNode("PM"),
                new TreeNode("portee"),
                new TreeNode("invocation"),
                new TreeNode("prospection"),
                new TreeNode("initiative"),
                new TreeNode("fuite"),
                new TreeNode("DMG_neutre"),
                new TreeNode("DMG_feu"),
                new TreeNode("DMG_air"),
                new TreeNode("DMG_terre"),
                new TreeNode("DMG_eau"),
                new TreeNode("DMG_poussee"),
                new TreeNode("DMG_piege"),
                new TreeNode("RES_neutre"),
                new TreeNode("RES_feu"),
                new TreeNode("RES_air"),
                new TreeNode("RES_terre"),
                new TreeNode("RES_eau"),
                new TreeNode("RES_poussee"),
                new TreeNode("RES_Pourcent_neutre"),
                new TreeNode("RES_Pourcent_feu"),
                new TreeNode("RES_Pourcent_air"),
                new TreeNode("RES_Pourcent_terre"),
                new TreeNode("RES_Pourcent_eau"),
                new TreeNode("retrait_PA"),
                new TreeNode("retrait_PM"),
                new TreeNode("esquive_PA"),
                new TreeNode("esquive_PM"),
                new TreeNode("soin"),
                new TreeNode("renvoie_DMG"),
                new TreeNode("tacle"),
                new TreeNode("puissance"),
                new TreeNode("puissance_piege"),
                new TreeNode("reduction_physique"),
                new TreeNode("reduction_magique ")
            };
            foreach (TreeNode Tnode in treeNodeTab_T_stat_enum)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            TreeNode[] treeNodeTab_T_stat = new TreeNode[] {
                new TreeNode("type",treeNodeTab_T_zone_enum)
            };
            foreach (TreeNode Tnode in treeNodeTab_T_stat)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_T_equip_enum = new TreeNode[] {
                new TreeNode("chapeau"),
                new TreeNode("anneau"),
                new TreeNode("botte"),
                new TreeNode("ceinture"),
                new TreeNode("cape"),
                new TreeNode("amulette"),
                new TreeNode("arme")
            };
            foreach (TreeNode Tnode in treeNodeTab_T_equip_enum)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            TreeNode[] treeNodeTab_T_equip = new TreeNode[] {
                new TreeNode("type",treeNodeTab_T_equip_enum)
            };
            foreach (TreeNode Tnode in treeNodeTab_T_equip)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_T_Enti_enum_1 = new TreeNode[] {
                new TreeNode("attaquant"),
                new TreeNode("defendant")
            };
            foreach (TreeNode Tnode in treeNodeTab_T_Enti_enum_1)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            TreeNode[] treeNodeTab_T_Enti_enum_2 = new TreeNode[] {
                new TreeNode("vivant"),
                new TreeNode("mort")
            };
            foreach (TreeNode Tnode in treeNodeTab_T_Enti_enum_2)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            TreeNode[] treeNodeTab_T_enti = new TreeNode[] {
                new TreeNode("type",treeNodeTab_T_Enti_enum_1),
                new TreeNode("typeEtat",treeNodeTab_T_Enti_enum_2)
            };
            foreach (TreeNode Tnode in treeNodeTab_T_enti)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_T_effet_enum = new TreeNode[] {
                new TreeNode("pousse"),
                new TreeNode("pousse_lanceur"),
                new TreeNode("tire_lanceur"),
                new TreeNode("teleportation"),
                new TreeNode("ATT_neutre"),
                new TreeNode("ATT_air"),
                new TreeNode("ATT_feu"),
                new TreeNode("ATT_terre"),
                new TreeNode("ATT_eau"),
                new TreeNode("envoutement"),
                new TreeNode("pose_piege"),
                new TreeNode("pose_glyphe"),
                new TreeNode("invocation"),
                new TreeNode("soin")
            };
            foreach (TreeNode Tnode in treeNodeTab_T_effet_enum)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            TreeNode[] treeNodeTab_T_effet = new TreeNode[] {
                new TreeNode("type",treeNodeTab_T_effet_enum)
            };
            foreach (TreeNode Tnode in treeNodeTab_T_effet)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_T_classe_enum = new TreeNode[] {
                new TreeNode("ecaflip"),
                new TreeNode("eniripsa"),
                new TreeNode("iop"),
                new TreeNode("cra"),
                new TreeNode("feca"),
                new TreeNode("sacrieur"),
                new TreeNode("sadida"),
                new TreeNode("osamoda"),
                new TreeNode("enutrof"),
                new TreeNode("sram"),
                new TreeNode("xelor"),
                new TreeNode("pandawa")
            };
            foreach (TreeNode Tnode in treeNodeTab_T_classe_enum)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            TreeNode[] treeNodeTab_T_classe = new TreeNode[] {
                new TreeNode("type",treeNodeTab_T_classe_enum)
            };
            foreach (TreeNode Tnode in treeNodeTab_T_classe)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_T_sort_enum = new TreeNode[] {
                new TreeNode("felintion"),
                new TreeNode("chance_d_ecaflip"),
                new TreeNode("bond_du_felin"),
                new TreeNode("bluff"),
                new TreeNode("pile_ou_face"),
                new TreeNode("perception"),
                new TreeNode("contrecoup"),
                new TreeNode("griffe_invocatrice"),
                new TreeNode("tout_ou_rien"),
                new TreeNode("roulette"),
                new TreeNode("topkaj"),
                new TreeNode("langue_rapeuse"),
                new TreeNode("roue_de_la_fortune"),
                new TreeNode("esprit_felin"),
                new TreeNode("trefle"),
                new TreeNode("odorat"),
                new TreeNode("reflexes"),
                new TreeNode("griffe_joueuse"),
                new TreeNode("griffe_de_ceangal"),
                new TreeNode("rekopdestin_d_ecaflip"),
                new TreeNode("invocation_de_dopeul_ecaflip"),
                new TreeNode("mot_blessant"),
                new TreeNode("mot_alternatif"),
                new TreeNode("mot_d_amitie"),
                new TreeNode("mot_decisif"),
                new TreeNode("mot_interdit"),
                new TreeNode("mot_de_frayeur"),
                new TreeNode("mot_stimulant"),
                new TreeNode("mot_turbulent"),
                new TreeNode("mot_de_jouvence"),
                new TreeNode("mot_selectif"),
                new TreeNode("mot_eclatant"),
                new TreeNode("mot_de_prevention"),
                new TreeNode("mot_de_regeneration"),
                new TreeNode("mot_d_immobilisation"),
                new TreeNode("mot_deroutant"),
                new TreeNode("mot_tournoyant"),
                new TreeNode("mot_fracassant"),
                new TreeNode("mot_de_silence"),
                new TreeNode("mot_d_envol"),
                new TreeNode("mot_de_revitalisation"),
                new TreeNode("mot_de_reconstitution"),
                new TreeNode("invocation_de_dopeul_eniripsa"),
                new TreeNode("brokle"),
                new TreeNode("pression"),
                new TreeNode("bond"),
                new TreeNode("intimidation"),
                new TreeNode("vitalite"),
                new TreeNode("epee_divine"),
                new TreeNode("epeedesctructrice"),
                new TreeNode("poutch"),
                new TreeNode("souffle"),
                new TreeNode("concentration"),
                new TreeNode("couper"),
                new TreeNode("friction"),
                new TreeNode("duel"),
                new TreeNode("epee_du_jugement"),
                new TreeNode("puissance"),
                new TreeNode("precipitation"),
                new TreeNode("tempete_de_puissance"),
                new TreeNode("epee_celeste"),
                new TreeNode("epee_du_iop"),
                new TreeNode("epee_du_destin"),
                new TreeNode("colere_du_iop"),
                new TreeNode("invocation_de_dopeul_iop"),
                new TreeNode("fleche_de_dispersion"),
                new TreeNode("fleche_magique"),
                new TreeNode("fleche_empoisonne"),
                new TreeNode("fleche_de_recul"),
                new TreeNode("fleche_glacee"),
                new TreeNode("fleche_enflammee"),
                new TreeNode("tir_eloigne"),
                new TreeNode("fleche_d_expiation"),
                new TreeNode("œil_de_taupe"),
                new TreeNode("tir_critique"),
                new TreeNode("fleche_d_immobilisation"),
                new TreeNode("fleche_punitive"),
                new TreeNode("tir_puissant"),
                new TreeNode("fleche_harcelante"),
                new TreeNode("fleche_persecutrice"),
                new TreeNode("fleche_cinglante"),
                new TreeNode("fleche_destructrice"),
                new TreeNode("fleche_absorbante"),
                new TreeNode("fleche_ralentissante"),
                new TreeNode("fleche_explosive"),
                new TreeNode("maitrise_de_l_arc"),
                new TreeNode("invocation_de_dopeul_cra"),
                new TreeNode("mise_en_garde"),
                new TreeNode("aveuglement"),
                new TreeNode("attaque_naturelle"),
                new TreeNode("rempart"),
                new TreeNode("typhon"),
                new TreeNode("bulle"),
                new TreeNode("barricade"),
                new TreeNode("glyphe_agressif"),
                new TreeNode("lethargie"),
                new TreeNode("attaque_nuageuse"),
                new TreeNode("bastion"),
                new TreeNode("retour_du_baton"),
                new TreeNode("teleglyphe"),
                new TreeNode("glyphe_de_repulsion"),
                new TreeNode("treve"),
                new TreeNode("glyphe_d_aveuglement"),
                new TreeNode("frisson"),
                new TreeNode("glyphe_optique"),
                new TreeNode("glyphe_gravitationnel"),
                new TreeNode("glyphe_enflamme"),
                new TreeNode("bouclier_feca"),
                new TreeNode("invocation_de_dopeul_feca"),
                new TreeNode("douleur_partage"),
                new TreeNode("chatiment_force"),
                new TreeNode("attirance"),
                new TreeNode("pied_du_sacrieur"),
                new TreeNode("derobade"),
                new TreeNode("detour"),
                new TreeNode("assaut"),
                new TreeNode("chatiment_agile"),
                new TreeNode("dissolution"),
                new TreeNode("chatiment_ose"),
                new TreeNode("chatiment_spirituel"),
                new TreeNode("sacrifice"),
                new TreeNode("absorption"),
                new TreeNode("chatiment_vitalesque"),
                new TreeNode("cooperation"),
                new TreeNode("transposition"),
                new TreeNode("punition"),
                new TreeNode("furie"),
                new TreeNode("epee_volante"),
                new TreeNode("transfert_de_vie"),
                new TreeNode("folie_sanguinaire"),
                new TreeNode("invocation_de_dopeul_sacrieur"),
                new TreeNode("ronce"),
                new TreeNode("la_bloqueuse"),
                new TreeNode("poison_paralysant"),
                new TreeNode("sacrifice_poupesque"),
                new TreeNode("larme"),
                new TreeNode("la_folle"),
                new TreeNode("ronce_apaisante"),
                new TreeNode("puissance_sylvestre"),
                new TreeNode("la_sacrifiee"),
                new TreeNode("tremblement"),
                new TreeNode("connaissance_des_poupees"),
                new TreeNode("ronces_multiples"),
                new TreeNode("arbre"),
                new TreeNode("vent_empoisonne"),
                new TreeNode("la_gonflable"),
                new TreeNode("ronce_agressives"),
                new TreeNode("herbe_folle"),
                new TreeNode("feu_de_brousse"),
                new TreeNode("ronce_insolente"),
                new TreeNode("la_surpuissante"),
                new TreeNode("invocation_de_dopeul_sadida"),
                new TreeNode("laisse_spirituelle"),
                new TreeNode("griffe_spectrale"),
                new TreeNode("resistance_naturelle"),
                new TreeNode("invocation_de_tofu"),
                new TreeNode("beneditcion_animale"),
                new TreeNode("deplacement_felin"),
                new TreeNode("invocation_de_bouftou"),
                new TreeNode("crapaud"),
                new TreeNode("invocation_de_prespic"),
                new TreeNode("fouet"),
                new TreeNode("piqure_motivante"),
                new TreeNode("corbeau"),
                new TreeNode("griffe_cinglante"),
                new TreeNode("soin_animal"),
                new TreeNode("invocation_de_sanglier"),
                new TreeNode("frappe_du_craqueleur"),
                new TreeNode("cris_de_l_ours"),
                new TreeNode("invocation_de_bwork_mage"),
                new TreeNode("crocs_du_mulou"),
                new TreeNode("invocation_de_craqueleur"),
                new TreeNode("invocation_de_dragonnet_rouge"),
                new TreeNode("invocation_de_dopeul_osamodas"),
                new TreeNode("retraite_anticipee"),
                new TreeNode("sac_anime"),
                new TreeNode("lancer_de_pelle"),
                new TreeNode("lancer_de_pieces"),
                new TreeNode("pelle_fantomatique"),
                new TreeNode("acceleration"),
                new TreeNode("boite_de_pandore"),
                new TreeNode("remblai"),
                new TreeNode("cle_reductrice"),
                new TreeNode("force_de_l_age"),
                new TreeNode("pelle_animee"),
                new TreeNode("cupidite"),
                new TreeNode("roulage_de_pelle"),
                new TreeNode("maladresse"),
                new TreeNode("maladresse_de_masse"),
                new TreeNode("chance"),
                new TreeNode("pelle_de_jugement"),
                new TreeNode("pelle_massacrante"),
                new TreeNode("corruption"),
                new TreeNode("desinvocation"),
                new TreeNode("coffre_anime"),
                new TreeNode("invocation_de_dopeul_enutrof"),
                new TreeNode("poisse"),
                new TreeNode("sournoiserie"),
                new TreeNode("piege_sournois"),
                new TreeNode("invisibilite"),
                new TreeNode("poison_insidieux"),
                new TreeNode("fourvoiment"),
                new TreeNode("coup_sournois"),
                new TreeNode("double_sram"),
                new TreeNode("pulsion_de_chakra"),
                new TreeNode("piege_de_masse"),
                new TreeNode("invisibilite_d_autrui"),
                new TreeNode("piege_empoisonne"),
                new TreeNode("concentration_de_chakra"),
                new TreeNode("piege_d_immobilisation"),
                new TreeNode("piege_de_silence"),
                new TreeNode("piege_repulsif"),
                new TreeNode("peur"),
                new TreeNode("arnaque"),
                new TreeNode("reperage"),
                new TreeNode("attaque_mortelle"),
                new TreeNode("piege_mortel"),
                new TreeNode("invocation_de_dopeul_sram"),
                new TreeNode("ralentissement"),
                new TreeNode("rembobinage"),
                new TreeNode("aiguille"),
                new TreeNode("gelure"),
                new TreeNode("sablier_de_xelor"),
                new TreeNode("rayon_obscur"),
                new TreeNode("teleportation"),
                new TreeNode("fletrissement"),
                new TreeNode("flou"),
                new TreeNode("poussiere_temporelle"),
                new TreeNode("vol_du_temps"),
                new TreeNode("aiguille_chercheuse"),
                new TreeNode("devoument"),
                new TreeNode("fuite"),
                new TreeNode("demotivation"),
                new TreeNode("contre"),
                new TreeNode("momification"),
                new TreeNode("horloge"),
                new TreeNode("frappe_de_xelor"),
                new TreeNode("cadran_de_xelor"),
                new TreeNode("invocation_de_dopeul_xelor")
            };
            foreach (TreeNode Tnode in treeNodeTab_T_sort_enum)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            TreeNode[] treeNodeTab_T_sort = new TreeNode[] {
                new TreeNode("nom_sort",treeNodeTab_T_sort_enum)
            };
            foreach (TreeNode Tnode in treeNodeTab_T_sort)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_T_case_enum = new TreeNode[] {
                new TreeNode("vide"),
                new TreeNode("joueur"),
                new TreeNode("obstacle"),
                new TreeNode("piege")
            };
            foreach (TreeNode Tnode in treeNodeTab_T_case_enum)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            TreeNode[] treeNodeTab_T_case = new TreeNode[] {
                new TreeNode("type",treeNodeTab_T_case_enum)
            };
            foreach (TreeNode Tnode in treeNodeTab_T_case)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_T_arme_enum = new TreeNode[] {
                new TreeNode("arc"),
                new TreeNode("baguette"),
                new TreeNode("baton"),
                new TreeNode("dague"),
                new TreeNode("faux"),
                new TreeNode("hache"),
                new TreeNode("marteau"),
                new TreeNode("outil"),
                new TreeNode("pelle"),
                new TreeNode("pioche"),
                new TreeNode("epee")
            };
            foreach (TreeNode Tnode in treeNodeTab_T_arme_enum)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            TreeNode[] treeNodeTab_T_arme = new TreeNode[] {
                new TreeNode("type",treeNodeTab_T_arme_enum)
            };
            foreach (TreeNode Tnode in treeNodeTab_T_arme)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_gofus = new TreeNode[] {
                new TreeNode("Arme", treeNodeTab_T_arme),
                new TreeNode("Case", treeNodeTab_T_case),
                new TreeNode("Classe", treeNodeTab_T_classe),
                new TreeNode("Effet", treeNodeTab_T_effet),
                new TreeNode("Entite", treeNodeTab_T_enti),
                new TreeNode("EntiteInconnu"),
                new TreeNode("Envoutement"),
                new TreeNode("Equipement", treeNodeTab_T_equip),
                new TreeNode("Liste"),
                new TreeNode("Personnage"),
                new TreeNode("Sort", treeNodeTab_T_sort),
                new TreeNode("Statistique", treeNodeTab_T_stat),
                new TreeNode("Terrain"),
                new TreeNode("Zone", treeNodeTab_T_zone)
            };
            foreach (TreeNode Tnode in treeNodeTab_gofus)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_stat = new TreeNode[] {
                new TreeNode("Nom"),
                new TreeNode("Valeur", treeNodeTab_simpleVar)
            };
            foreach (TreeNode Tnode in treeNodeTab_stat)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_equip = new TreeNode[] {
                new TreeNode("IdEquipement", treeNodeTab_simpleVar),
                new TreeNode("Nom", treeNodeTab_chaine),
                new TreeNode("TabStatistiques", treeNodeTab_tab),
                new TreeNode("Type")
            };
            foreach (TreeNode Tnode in treeNodeTab_equip)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_case = new TreeNode[] {
                new TreeNode("X", treeNodeTab_simpleVar),
                new TreeNode("Y", treeNodeTab_simpleVar),
                new TreeNode("Contenu")
            };
            foreach (TreeNode Tnode in treeNodeTab_case)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_zone = new TreeNode[] {
                new TreeNode("PorteeMin", treeNodeTab_simpleVar),
                new TreeNode("PorteeMax", treeNodeTab_simpleVar),
                new TreeNode("Type")
            };
            foreach (TreeNode Tnode in treeNodeTab_zone)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_sort = new TreeNode[] {
                new TreeNode("IdSort", treeNodeTab_simpleVar),
                new TreeNode("TabEffets", treeNodeTab_tab),
                new TreeNode("Nom", treeNodeTab_chaine),
                new TreeNode("LigneDeVue", treeNodeTab_simpleVar),
                new TreeNode("PorteeModifiable", treeNodeTab_simpleVar),
                new TreeNode("CelluleLibre", treeNodeTab_simpleVar),
                new TreeNode("ZonePortee", treeNodeTab_zone),
                new TreeNode("ZoneEffet", treeNodeTab_zone),
                new TreeNode("TabEffets", treeNodeTab_tab),
                new TreeNode("TauxDeRelance", treeNodeTab_simpleVar),
                new TreeNode("CoutPA", treeNodeTab_simpleVar)
            };
            foreach (TreeNode Tnode in treeNodeTab_sort)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_classe = new TreeNode[] {
                new TreeNode("IdClasse", treeNodeTab_simpleVar),
                new TreeNode("TabSorts", treeNodeTab_tab),
                new TreeNode("Nom")
            };
            foreach (TreeNode Tnode in treeNodeTab_classe)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode[] treeNodeTab_method_perso = new TreeNode[] {
                new TreeNode("UtiliserSort()", treeNodeTab_simpleVar),
                new TreeNode("AvancerVers()", treeNodeTab_simpleVar),
                new TreeNode("Attaquer()", treeNodeTab_simpleVar)
            };

            foreach (TreeNode Tnode in treeNodeTab_method_perso)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "method";
                Tnode.Text = "system";
            }

            TreeNode[] treeNodeTab_attribut_perso = new TreeNode[] {
                new TreeNode("Equipe"),
                new TreeNode("Etat"),
                new TreeNode("IdEntite", treeNodeTab_simpleVar),
                new TreeNode("ClasseEntite", treeNodeTab_classe),
                new TreeNode("Nom", treeNodeTab_chaine),
                new TreeNode("Experience", treeNodeTab_simpleVar),
                new TreeNode("PV", treeNodeTab_simpleVar),
                new TreeNode("PV_MAX", treeNodeTab_simpleVar),
                new TreeNode("PA", treeNodeTab_simpleVar),
                new TreeNode("PA_MAX", treeNodeTab_simpleVar),
                new TreeNode("PM", treeNodeTab_simpleVar),
                new TreeNode("PM_MAX", treeNodeTab_simpleVar),
                new TreeNode("Proprietaire", treeNodeTab_simpleVar),
                new TreeNode("ListStatistiques", treeNodeTab_Liste),
                new TreeNode("ListEnvoutements", treeNodeTab_Liste)
            };
            foreach (TreeNode Tnode in treeNodeTab_attribut_perso)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }

            TreeNode[] treeNodeTab_perso = new TreeNode[treeNodeTab_method_perso.Length + treeNodeTab_attribut_perso.Length];
            treeNodeTab_method_perso.CopyTo(treeNodeTab_perso, 0);
            treeNodeTab_attribut_perso.CopyTo(treeNodeTab_perso, treeNodeTab_method_perso.Length);
            //#############################################################################################################
            TreeNode[] treeNodeTab_method_perso_i = new TreeNode[] {
                new TreeNode("UtiliserSort()", treeNodeTab_simpleVar),
                new TreeNode("AvancerVers()", treeNodeTab_simpleVar),
                new TreeNode("Attaquer()", treeNodeTab_simpleVar)
            };

            foreach (TreeNode Tnode in treeNodeTab_method_perso_i)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "method";
                Tnode.Text = "system";
            }

            TreeNode[] treeNodeTab_attribut_perso_i = new TreeNode[] {
                new TreeNode("Equipe"),
                new TreeNode("Etat"),
                new TreeNode("IdEntite", treeNodeTab_simpleVar),
                new TreeNode("ClasseEntite", treeNodeTab_classe),
                new TreeNode("Nom", treeNodeTab_chaine),
                new TreeNode("Experience", treeNodeTab_simpleVar),
                new TreeNode("PV", treeNodeTab_simpleVar),
                new TreeNode("PV_MAX", treeNodeTab_simpleVar),
                new TreeNode("PA", treeNodeTab_simpleVar),
                new TreeNode("PA_MAX", treeNodeTab_simpleVar),
                new TreeNode("PM", treeNodeTab_simpleVar),
                new TreeNode("PM_MAX", treeNodeTab_simpleVar),
                new TreeNode("Proprietaire", treeNodeTab_simpleVar),
                new TreeNode("ListStatistiques", treeNodeTab_Liste),
                new TreeNode("ListEnvoutements", treeNodeTab_Liste)
            };
            foreach (TreeNode Tnode in treeNodeTab_attribut_perso_i)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }

            TreeNode[] treeNodeTab_perso_i = new TreeNode[treeNodeTab_method_perso_i.Length + treeNodeTab_attribut_perso_i.Length];
            treeNodeTab_method_perso_i.CopyTo(treeNodeTab_perso_i, 0);
            treeNodeTab_attribut_perso_i.CopyTo(treeNodeTab_perso_i, treeNodeTab_method_perso_i.Length);
            //#############################################################################################################
            TreeNode[] treeNodeTab_method_terrain = new TreeNode[] {
                new TreeNode("DistanceEntreCases()", treeNodeTab_simpleVar),
                new TreeNode("CaseAvecObstacle()", treeNodeTab_Liste),
                new TreeNode("CheminEntreCases()", treeNodeTab_Liste),
                new TreeNode("CaseVoisines()", treeNodeTab_Liste)
            };

            foreach (TreeNode Tnode in treeNodeTab_method_terrain)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "method";
                Tnode.Text = "system";
            }

            TreeNode[] treeNodeTab_attribut_terrain = new TreeNode[] {
                new TreeNode("TabCases", treeNodeTab_tab),
                new TreeNode("Largeur", treeNodeTab_simpleVar),
                new TreeNode("Hauteur", treeNodeTab_simpleVar)
            };
            foreach (TreeNode Tnode in treeNodeTab_attribut_terrain)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }

            TreeNode[] treeNodeTab_terrain = new TreeNode[treeNodeTab_method_terrain.Length + treeNodeTab_attribut_terrain.Length];
            treeNodeTab_method_terrain.CopyTo(treeNodeTab_terrain, 0);
            treeNodeTab_attribut_terrain.CopyTo(treeNodeTab_terrain, treeNodeTab_method_terrain.Length);
            //#############################################################################################################
            TreeNode treeNode_1 = new TreeNode("keyword");
            TreeNode treeNode_2 = new TreeNode("classGofus");
            TreeNode treeNode_3 = new TreeNode("Math", treeNodeTab_math);
            TreeNode treeNode_4 = new TreeNode("chaine", treeNodeTab_chaine);
            TreeNode treeNode_5 = new TreeNode("simpleVar", treeNodeTab_simpleVar);
            TreeNode treeNode_6 = new TreeNode("tab", treeNodeTab_tab);
            TreeNode treeNode_7 = new TreeNode("fonctionVoid");
            TreeNode treeNode_8 = new TreeNode("Liste", treeNodeTab_Liste);
            TreeNode treeNode_9 = new TreeNode("Perso", treeNodeTab_perso);
            TreeNode treeNode_10 = new TreeNode("ListeEntite", treeNodeTab_Liste);
            TreeNode treeNode_11 = new TreeNode("terrain", treeNodeTab_terrain);

            treeNode_1.Name = "keyword";
            treeNode_1.Tag = "class";
            treeNode_1.Text = "system";

            treeNode_2.Name = "classGofus";
            treeNode_2.Tag = "class";
            treeNode_2.Text = "system";

            treeNode_3.Name = "Math";
            treeNode_3.Tag = "class";
            treeNode_3.Text = "system";

            treeNode_4.Name = "chaine";
            treeNode_4.Tag = "class";
            treeNode_4.Text = "system";

            treeNode_5.Name = "simpleVar";
            treeNode_5.Tag = "class";
            treeNode_5.Text = "system";

            treeNode_6.Name = "tab";
            treeNode_6.Tag = "class";
            treeNode_6.Text = "system";

            treeNode_7.Name = "fonctionVoid";
            treeNode_7.Tag = "class";
            treeNode_7.Text = "system";

            treeNode_8.Name = "Liste";
            treeNode_8.Tag = "class";
            treeNode_8.Text = "system";

            treeNode_9.Name = "Perso";
            treeNode_9.Tag = "property";
            treeNode_9.Text = "system";

            treeNode_10.Name = "ListeEntite";
            treeNode_10.Tag = "property";
            treeNode_10.Text = "system";

            treeNode_11.Name = "terrain";
            treeNode_11.Tag = "property";
            treeNode_11.Text = "system";

            TreeNode[] treeNode_root = new TreeNode[] {
            treeNode_1,
            treeNode_2,
            treeNode_3,
            treeNode_4,
            treeNode_5,
            treeNode_6,
            treeNode_7,
            treeNode_8,
            treeNode_9,
            treeNode_10,
            treeNode_11};



            TreeNode[] treeNode_Intellisense = new TreeNode[treeNode_root.Length + treeNodeTab_keyword.Length + treeNodeTab_gofus.Length];
            treeNodeTab_keyword.CopyTo(treeNode_Intellisense, 0);
            treeNode_root.CopyTo(treeNode_Intellisense, treeNodeTab_keyword.Length);
            treeNodeTab_gofus.CopyTo(treeNode_Intellisense, treeNodeTab_keyword.Length + treeNode_root.Length);
            return treeNode_Intellisense;
        }
        #endregion

        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);
        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, UIntPtr wParam, UIntPtr lParam);

        private void ctb_main_VScroll(object sender, EventArgs e)
        {
            int nPos = GetScrollPos(ctb_main.Handle, (int)ScrollBarType.SbVert);
            nPos <<= 16;
            uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
            SendMessage(new WindowInteropHelper(Window.GetWindow(rtb_lineNumber)).Handle, (int)Message.WM_VSCROLL, new UIntPtr(wParam), new UIntPtr(0));
        }

        #endregion

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

            cboTrieInventaire.ItemsSource = type;
            cboTrieInventaire.SelectedIndex = 0;

            foreach (Entite perso in Player.LstEntites)
                entitesNom.Add(perso.Nom);// TODO ne marchera pas si crée un perso marche
            cboChoixEntite.ItemsSource = entitesNom;
            cboChoixEntite.SelectedIndex = 0;
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

        #region ced
        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
            string codeAI = @"
            EntiteInconnu ennemi = null;
            foreach (EntiteInconnu entite in ListEntites)
            {
                if (entite.Equipe != Perso.Equipe)
                {
                    ennemi = entite;
                    break;
                }
            }
            if (terrain.DistanceEntreCases(Perso.Position, ennemi.Position) > 1)
            {
                int result = 1;
                while (result != 0 && result != -1)
                {
                    result = Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, ennemi.Position)[0], 1);
                }
            }
            Perso.UtiliserSort(Perso.ClasseEntite.TabSorts[1], ennemi);";
            Combat combat = new Combat(codeAI, codeAI);
        }

        private void btn_debug_Click(object sender, RoutedEventArgs e)
        {

            //code dynamique 
            string code = @"
                using GofusSharp;
                namespace Arene
                {
                    public class Combat
                    {
                        public static void Action(Terrain terrain, Entite Perso, Liste<EntiteInconnu> ListEntites)
                        {
                            user_code
                        }
                    }
                }
            ";

            //je remplace le mot user_code pour ce qui ce trouve dans la text box
            string finalCode = code.Replace("user_code", ctb_main.Text);
            //initialisation d'un compilateur de code C#
            CSharpCodeProvider provider = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
            //initialisation des paramètres du compilateur de code C#
            CompilerParameters parameters = new CompilerParameters();
            //ajout des lien de bibliothèque dynamique (dll)
            //parameters.ReferencedAssemblies.Add("WindowsBase.dll");
            parameters.ReferencedAssemblies.Add("GofusSharp.dll");
            //System.Windows.Forms.MessageBox.Show(  );
            //compilation du code 
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, finalCode);
            //recherche d'érreurs de compilation
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(string.Format("Erreur (Ligne {0}): {1}", (error.Line - 8).ToString(), error.ErrorText));
                }
                System.Windows.Forms.MessageBox.Show(sb.ToString());
                return;
                //throw new InvalidOperationException(sb.ToString());
            }
            System.Windows.Forms.MessageBox.Show("Aucune érreur de compilation");
        }
        #endregion

        #region Inventaire
        /// <summary>
        /// action d'un click sur un item dans l'inventaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_desc(object sender, MouseButtonEventArgs e)
        {
            LstDesc.Clear();
            string nom = (((ImageItem)sender).imgItem).Name.Replace("_", " ");
            LstDesc.Add(new DescItem(new Equipement(bd.selection("SELECT * FROM Equipements WHERE nom ='" + nom + "'")[0], true, 0)));
        }

        /// <summary>
        /// action quand on click sur l'onglet inventaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabItem_Selected_Inventaire(object sender, RoutedEventArgs e)
        {
            refreshInv();
            cboChoixEntite.SelectedIndex = -1;// # not legit
            cboChoixEntite.SelectedIndex = 0;
        }

        /// <summary>
        /// quand on change le type de trie de l'inventaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboTrieInventaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((System.Windows.Controls.ComboBox)sender).SelectedIndex != -1)
            {
                string type = ((System.Windows.Controls.ComboBox)sender).SelectedValue.ToString();

                LstInventaire.Clear();

                foreach (Equipement item in Player.Inventaire)
                    if (type == "Tous" && item.Quantite - item.QuantiteEquipe != 0)
                    {
                        ImageItem i = new ImageItem(item, false, item.Quantite - item.QuantiteEquipe);
                        i.PreviewMouseDoubleClick += image_desc;
                        LstInventaire.Add(i);
                    }
                    else
                    {
                        if (item.Type == type && item.Quantite - item.QuantiteEquipe != 0)
                        {
                            ImageItem i = new ImageItem(item, false, item.Quantite - item.QuantiteEquipe);
                            i.PreviewMouseDoubleClick += image_desc;

                            LstInventaire.Add(i);
                        }
                    }
                if (LstInventaire.Count <= 3 * 6)
                    lbxInventaire.Style = (Style)FindResource("RowFix");
                else
                    lbxInventaire.Style = (Style)FindResource("RowOverflow");
            }
        }

        /// <summary>
        /// pour avoir la description d'un item qui est équiper sur soi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgInv_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                LstDesc.Clear();
                if (convertPathToNoItem((sender as Image).Source.ToString()) != "vide")
                    LstDesc.Add(new DescItem(new Equipement(bd.selection("SELECT * FROM Equipements WHERE noImage =" + Convert.ToInt32(convertPathToNoItem((sender as Image).Source.ToString())))[0], true, 0)));
                lbxInventaire.SelectedIndex = -1;
            }
        }

        public void resetImagesEquipements()
        {
            BitmapImage link = new BitmapImage(new Uri("resources/vide.png",UriKind.Relative));
            imgAmuletteInv.Source = link;
            imgBotteInv.Source = link;
            imgAnneau1Inv.Source = link;
            imgAnneau2Inv.Source = link;
            imgArmeInv.Source = link;
            imgCeintureInv.Source = link;
            imgCapeInv.Source = link;
        }
        /// <summary>
        /// Affiche les équipement du personnages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboChoixEntite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//emplacement possible : tete, cou, pied, ano1, ano2, arme, hanche, dos.
            if (((System.Windows.Controls.ComboBox)sender).SelectedIndex != -1)
            {
                resetImagesEquipements();
                string nomPerso = ((System.Windows.Controls.ComboBox)sender).SelectedValue.ToString();
                lblNomEntite.Content = nomPerso;
                List<string>[] info = bd.selection("SELECT e.noImage,ee.emplacement FROM equipementsentites ee INNER JOIN Equipements e ON e.idEquipement = ee.idEquipement INNER JOIN Entites et ON ee.idEntite = et.idEntite WHERE et.nom = '" + nomPerso + "'");
                if (info[0][0] != "rien")
                    foreach (List<string> line in info)
                    {
                        BitmapImage link = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/items/200/" + line[0] + ".png"));
                        switch (line[1])//l'emplacement de l'équipement
                        {
                            case "tête":
                                imgChapeauInv.Source = link;
                                break;
                            case "cou":
                                imgAmuletteInv.Source = link;
                                break;
                            case "pied":
                                imgBotteInv.Source = link;
                                break;
                            case "ano1":
                                imgAnneau1Inv.Source = link;
                                break;
                            case "ano2":
                                imgAnneau2Inv.Source = link;
                                break;
                            case "arme":
                                imgArmeInv.Source = link;
                                break;
                            case "hanche":
                                imgCeintureInv.Source = link;
                                break;
                            case "dos":
                                imgCapeInv.Source = link;
                                break;
                        }
                    }
            }
        }

        /// <summary>
        /// truc pas legit pour refaire la list de l'inventaire
        /// </summary>
        private void refreshInv()
        {
            int i = cboTrieInventaire.SelectedIndex;
            cboTrieInventaire.SelectedIndex = -1;
            cboTrieInventaire.SelectedIndex = i;
        }
        /// <summary>
        /// je trouvais la fonction longue donc je lui ai donné un long nom 
        /// </summary>
        /// <param name="path">la source de l'image</param>
        /// <returns></returns>
        private string convertPathToNoItem(string path)
        {
            return System.IO.Path.GetFileNameWithoutExtension(path.Split('/').Last());
        }

        #region Drag&Drop
        /// <summary>
        /// le Drag du Drag&Drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbxInventaire_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataContext = this;
            System.Windows.Controls.ListBox parent = (System.Windows.Controls.ListBox)sender;
            dragSource = parent;
            ImageItem data = (ImageItem)GetDataFromListBox(dragSource, e.GetPosition(parent));

            #region drop pas icitte !
            imgCapeInv.AllowDrop = false;
            imgChapeauInv.AllowDrop = false;
            imgBotteInv.AllowDrop = false;
            imgAnneau1Inv.AllowDrop = false;
            imgAnneau2Inv.AllowDrop = false;
            imgAmuletteInv.AllowDrop = false;
            imgArmeInv.AllowDrop = false;
            #endregion

            SolidColorBrush color = Brushes.Orange;
            if (data != null)
            {
                Equipement itemDrag = Player.Inventaire.First(x => x.NoImg == convertPathToNoItem(data.imgItem.Source.ToString()));
                switch (itemDrag.Type)
                {
                    case "Cape":
                        imgCapeInv.AllowDrop = true;
                        borderCape.BorderBrush = color;
                        break;
                    case "Chapeau":
                        imgChapeauInv.AllowDrop = true;
                        borderCoiffe.BorderBrush = color;
                        break;
                    case "Botte":
                        imgBotteInv.AllowDrop = true;
                        borderBotte.BorderBrush = color;
                        break;
                    case "Ceinture":
                        imgCeintureInv.AllowDrop = true;
                        borderCeinture.BorderBrush = color;
                        break;
                    case "Anneau":
                        imgAnneau1Inv.AllowDrop = true;
                        imgAnneau2Inv.AllowDrop = true;
                        borderAno1.BorderBrush = color;
                        borderAno2.BorderBrush = color;
                        break;
                    case "Amulette":
                        imgAmuletteInv.AllowDrop = true;
                        borderAmu.BorderBrush = color;
                        break;
                    default://arme
                        imgArmeInv.AllowDrop = true;
                        borderCac.BorderBrush = color;
                        break;
                }
                //image qui suit le curseur http://stackoverflow.com/questions/3129443/wpf-4-drag-and-drop-with-visual-element-as-cursor
                System.Windows.DataObject dragData = new System.Windows.DataObject("image", data);
                CreateDragDropWindow(data.imgItem);
                var effet = DragDrop.DoDragDrop(parent, dragData, System.Windows.DragDropEffects.Move);
                if (effet == System.Windows.DragDropEffects.None)
                {//drop fail
                    if (this._dragdropWindow != null)
                    {
                        this._dragdropWindow.Close();
                        this._dragdropWindow = null;
                    }
                    #region Pouf Transparent !
                    borderCape.BorderBrush = Brushes.Transparent;
                    borderCoiffe.BorderBrush = Brushes.Transparent;
                    borderBotte.BorderBrush = Brushes.Transparent;
                    borderCeinture.BorderBrush = Brushes.Transparent;
                    borderAno1.BorderBrush = Brushes.Transparent;
                    borderAno2.BorderBrush = Brushes.Transparent;
                    borderAmu.BorderBrush = Brushes.Transparent;
                    borderCac.BorderBrush = Brushes.Transparent;
                    #endregion
                }
            }
        }

        /// <summary>
        /// suis la position du curseur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_GiveFeedback(object sender, System.Windows.GiveFeedbackEventArgs e)
        {
            // update the position of the visual feedback item
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            this._dragdropWindow.Left = w32Mouse.X;
            this._dragdropWindow.Top = w32Mouse.Y;
        }
        /// <summary>
        /// crée l'image qui suis le curseur une window
        /// </summary>
        /// <param name="dragElement"></param>
        private void CreateDragDropWindow(Visual dragElement)
        {
            this._dragdropWindow = new Window();
            _dragdropWindow.WindowStyle = WindowStyle.None;
            _dragdropWindow.AllowsTransparency = true;
            _dragdropWindow.AllowDrop = false;
            _dragdropWindow.Background = null;
            _dragdropWindow.IsHitTestVisible = false;
            _dragdropWindow.SizeToContent = SizeToContent.WidthAndHeight;
            _dragdropWindow.Topmost = true;
            _dragdropWindow.ShowInTaskbar = false;

            Rectangle r = new Rectangle();
            r.Width = ((FrameworkElement)dragElement).ActualWidth;
            r.Height = ((FrameworkElement)dragElement).ActualHeight;
            r.Fill = new VisualBrush(dragElement);
            this._dragdropWindow.Content = r;

            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);

            this._dragdropWindow.Left = w32Mouse.X;
            this._dragdropWindow.Top = w32Mouse.Y;
            this._dragdropWindow.Show();
        }

        /// <summary>
        /// renvoi l'information de ta position dans la listbox
        /// </summary>
        /// <param name="source"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private static object GetDataFromListBox(System.Windows.Controls.ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    if (element == source)
                        return null;
                }
                if (data != DependencyProperty.UnsetValue)
                    return data;
            }
            return null;
        }

        /// <summary>
        /// Le Drop du Drag&Drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgInv_Drop(object sender, System.Windows.DragEventArgs e)
        {
            Image cible = (Image)sender;
            ImageItem data = e.Data.GetData("image") as ImageItem;
            Equipement itemDejaEquipe = null;

            Equipement itemVoulantEtreEquiper = Player.Inventaire.First(x => x.NoImg == convertPathToNoItem(data.imgItem.Source.ToString()));


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

            if (Player.LstEntites.First(x => x.Nom == cboChoixEntite.SelectedValue.ToString()).peutEquiper(itemVoulantEtreEquiper))
            {

                cible.Source = data.imgItem.Source;
                Player.Inventaire.First(x => x.Nom == itemVoulantEtreEquiper.Nom).Quantite--;
                if (itemDejaEquipe != null)
                    Player.Inventaire.First(x => x.Nom == itemDejaEquipe.Nom).Quantite++;
                /* else
                     Player.Inventaire.Add(itemVoulantEtreEquiper);*/
            }

            if (this._dragdropWindow != null)
            {
                this._dragdropWindow.Close();
                this._dragdropWindow = null;
            }

            refreshInv();
        }

        #region Truc pour invoquer des squelettes
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]

        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        #endregion

        #endregion

        #endregion

        #region Michael/Perso
        // ***************************************************
        //Onglet Personnage
        // ***************************************************
        private void TabPerso_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Entite perso in Player.LstEntites)
            {

                TabItem onglet = new TabItem();
                onglet.Header = perso.Nom;
                onglet.Content = new PagePerso(perso, Player);
                tCPerso.Items.Add(onglet);
            }
            tCPerso.SelectedIndex = 0;
            if (tCPerso.Items.Count <=4)
            {
                TabItem onglet = new TabItem();
                onglet.Header = "+";
                onglet.Content = new pageCpersonage(Player);
                tCPerso.Items.Add(onglet);
            }



        }
        #endregion    

        private void PGSort_Selected(object sender, RoutedEventArgs e)
        {

            TabItem ong = new TabItem();
            ong.Content= new pageSort();
            PGSort.Items.Add(ong);

        }
    }
}