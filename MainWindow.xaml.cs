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
        public Chat chat;
        public ObservableCollection<ImageItem> LstImgItems;
        ObservableCollection<string> LstStats;
        ObservableCollection<string> LstConds;
        ObservableCollection<string> LstCaras;

        public Joueur Player { get; set; }
        DispatcherTimer aTimer;
        private ChatWindow fenetreChat;

        public MainWindow(int id)
        {
            /*  BDService bd = new BDService();
              List<string>[] rep = bd.selection("SELECT * FROM classes");

              foreach (List<string> iop in rep)
                  foreach (string item in iop)
                      System.Windows.Forms.MessageBox.Show(item);*/



            //CombatTest combat = new CombatTest();
            InitializeComponent();
            ctb_main.CreateTreeView(generateTree());
            ctb_main.UpdateSyntaxHightlight();
            ctb_main.UpdateTreeView();

            #region linking Marché
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

            Player = new Joueur(bd.selection("SELECT * FROM Joueurs WHERE idJoueur = " + id)[0]);

            this.chat = new Chat();
            btnEnvoyerMessage.IsEnabled = false;


            aTimer = new DispatcherTimer();
            aTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            aTimer.Interval = new TimeSpan(0, 0, 2);
            //dgStats.ItemsSource=

        }

        /*   protected override void OnClosed(EventArgs e)
           {
               base.OnClosed(e);

               System.Windows.Application.Current.Shutdown();
           }
           */

        // POUR LE CHAT -------------------------------------------------------------------------------------------------------------------
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Updating the Label which displays the current second
            if (this != null)
            {
                chat.refreshChat();
                // Forcing the CommandManager to raise the RequerySuggested event
                CommandManager.InvalidateRequerySuggested();
            }
            else
            {
                aTimer.Stop();
            }
        }

        private void BtnEnvoyer_Click(object sender, RoutedEventArgs e)
        {
            long envois = chat.envoyerMessage();
            if (envois != -1)
            {
                chat.refreshChat();
                Scroll.ScrollToEnd();
            }
            else
            {
                System.Windows.MessageBox.Show("Erreur d'envois du message..");
            }
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
            txtMessage.IsEnabled = true;
        }

        private void btnQuitterSalle_Click(object sender, RoutedEventArgs e)
        {
            aTimer.Stop();
            txtMessage.Text = "";
            txtboxHistorique.Text = "";
            btnEnvoyerMessage.IsEnabled = false;
            txtMessage.IsEnabled = false;


        }

        private void BtnModLess_Click(object sender, RoutedEventArgs e)
        {


            fenetreChat = new ChatWindow();
            fenetreChat.Show();

        }

        #region truc trop long de ced
        //--------------------------------------------------------------------------------------------------------
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
                        public static string Generer()
                        {
                            Partie PartieTest = Arene.Combat.faussePartie();
                            while (true)
                            {
                                PartieTest.DebuterAction();
                                Action(PartieTest.TerrainPartie, PartieTest.ListAttaquants.First.Valeur, PartieTest.ListEntites);
                                PartieTest.SyncroniserJoueur();
                                if (PartieTest.ListAttaquants.First.Valeur.PV <= 0)
                                    return " + "\"Robert est le gagnant avec \" + PartieTest.ListDefendants.First.Valeur.PV.ToString() + \" PV restant!!!\"" + @";
                                else if(PartieTest.ListDefendants.First.Valeur.PV <= 0)
                                    return " + "\"Trebor est le gagnant avec \" + PartieTest.ListAttaquants.First.Valeur.PV.ToString() + \" PV restant!!!\"" + @";
                                PartieTest.DebuterAction();
                                Action(PartieTest.TerrainPartie, PartieTest.ListDefendants.First.Valeur, PartieTest.ListEntites);
                                PartieTest.SyncroniserJoueur();
                                if (PartieTest.ListAttaquants.First.Valeur.PV <= 0)
                                    return " + "\"Robert est le gagnant avec \" + PartieTest.ListDefendants.First.Valeur.PV.ToString() + \" PV restant!!!\"" + @";
                                else if(PartieTest.ListDefendants.First.Valeur.PV <= 0)
                                    return " + "\"Trebor est le gagnant avec \" + PartieTest.ListAttaquants.First.Valeur.PV.ToString() + \" PV restant!!!\"" + @";
                            }
                        }
                        public static void Action(Terrain terrain, Entite joueur, ListeChainee<EntiteInconnu> ListEntites)
                        {
                            user_code
                        }
                        public static Partie faussePartie()
                        {
                            ListeChainee<Statistique> listStatistiqueAtt = new ListeChainee<Statistique>();
                            listStatistiqueAtt.AjouterFin(new Statistique(Statistique.type.PA, 6));
                            listStatistiqueAtt.AjouterFin(new Statistique(Statistique.type.PM, 3));
                            listStatistiqueAtt.AjouterFin(new Statistique(Statistique.type.vie, 100));
                            listStatistiqueAtt.AjouterFin(new Statistique(Statistique.type.initiative, 101));
                            listStatistiqueAtt.AjouterFin(new Statistique(Statistique.type.force, 30));
                            listStatistiqueAtt.AjouterFin(new Statistique(Statistique.type.sagesse, 40));
                            listStatistiqueAtt.AjouterFin(new Statistique(Statistique.type.intelligence, 20));
                            listStatistiqueAtt.AjouterFin(new Statistique(Statistique.type.agilite, 10));
                            listStatistiqueAtt.AjouterFin(new Statistique(Statistique.type.chance, 50));
                            Script scriptAtt = new Script(1, " + "\"PlaceHolder\"" + @");
                            Effet[] tabEffetAtt1 = new Effet[] { new Effet(Effet.type.teleportation, 0, 0) };
                            Zone zoneEffetAtt1 = new Zone(Zone.type.carre, 0, 0);
                            Zone zonePorteeAtt1 = new Zone(Zone.type.cercle, 1, 5);
                            Effet[] tabEffetAtt2 = new Effet[] { new Effet(Effet.type.pousse, 4, 4), new Effet(Effet.type.ATT_neutre, 10, 15) };
                            Zone zoneEffetAtt2 = new Zone(Zone.type.carre, 0, 0);
                            Zone zonePorteeAtt2 = new Zone(Zone.type.croix, 1, 1);
                            Sort[] tabSortAtt = new Sort[] { new Sort(1, tabEffetAtt1, " + "\"bond\"" + @", false, true, true, zonePorteeAtt1, zoneEffetAtt1, 3, 5), new Sort(2, tabEffetAtt2, " + "\"intimidation\"" + @", true, false, false, zonePorteeAtt2, zoneEffetAtt2, -2, 2) };
                            Classe classeAtt = new Classe(1, tabSortAtt, Classe.type.iop);
                            Statistique[] statItemAtt = new Statistique[] { new Statistique(Statistique.type.force, 70) };
                            Equipement[] tabEquipAtt = new Equipement[] { new Equipement(1, statItemAtt, " + "\"Coiffe bouftou\"" + @", Equipement.type.chapeau) };
                            ListeChainee<Statistique> listStatistiqueDef = new ListeChainee<Statistique>();
                            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.PA, 6));
                            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.PM, 3));
                            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.vie, 100));
                            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.initiative, 101));
                            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.force, 30));
                            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.sagesse, 40));
                            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.intelligence, 20));
                            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.agilite, 10));
                            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.chance, 50));
                            Script scriptDef = new Script(2, " + "\"PlaceHolder\"" + @");
                            Effet[] tabEffetDef1 = new Effet[] { new Effet(Effet.type.teleportation, 0, 0) };
                            Zone zoneEffetDef1 = new Zone(Zone.type.carre, 0, 0);
                            Zone zonePorteeDef1 = new Zone(Zone.type.cercle, 1, 5);
                            Effet[] tabEffetDef2 = new Effet[] { new Effet(Effet.type.pousse, 4, 4), new Effet(Effet.type.ATT_neutre, 10, 15) };
                            Zone zoneEffetDef2 = new Zone(Zone.type.carre, 0, 0);
                            Zone zonePorteeDef2 = new Zone(Zone.type.croix, 1, 1);
                            Sort[] tabSortDef = new Sort[] { new Sort(1, tabEffetDef1, " + "\"bond\"" + @", false, true, true, zonePorteeDef1, zoneEffetDef1, 3, 5), new Sort(2, tabEffetDef2, " + "\"intimidation\"" + @", true, false, false, zonePorteeDef2, zoneEffetDef2, -2, 2) };
                            Classe classeDef = new Classe(1, tabSortDef, Classe.type.iop);
                            Statistique[] statItemDef = new Statistique[] { new Statistique(Statistique.type.force, 70) };
                            Equipement[] tabEquipDef = new Equipement[] { new Equipement(1, statItemDef, " + "\"Coiffe bouftou\"" + @", Equipement.type.chapeau) };
                            Case[][] tabCases = new Case[][] { new Case[] { new Case(0, 0, Case.type.joueur), new Case(0, 1, Case.type.vide), new Case(0, 2, Case.type.vide) }, new Case[] { new Case(1, 0, Case.type.vide), new Case(1, 1, 0), new Case(1, 2, Case.type.vide) }, new Case[] { new Case(2, 0, Case.type.vide), new Case(2, 1, Case.type.vide), new Case(2, 2, Case.type.joueur) } };
                            Terrain terrain = new Terrain(tabCases);
                            ListeChainee<Entite> ListAttaquants = new ListeChainee<Entite>();
                            ListAttaquants.AjouterFin(new Personnage(10, classeAtt, " + "\"Trebor\"" + @", 10000, terrain.TabCases[0][0], EntiteInconnu.type.attaquant, listStatistiqueAtt, scriptAtt, tabEquipAtt, terrain));
                            ListeChainee < Entite > ListDefendants = new ListeChainee<Entite>();
                            ListDefendants.AjouterFin(new Personnage(11, classeDef, " + "\"Robert\"" + @", 9000, terrain.TabCases[2][2], EntiteInconnu.type.defendant, listStatistiqueDef, scriptDef, tabEquipDef, terrain));
                            return new Partie(1, terrain, ListAttaquants, ListDefendants, 123123);
                        }
                    }
                }
            ";

            //je remplace le mot user_code pour ce qui ce trouve dans la text box
            string richText = new TextRange(ctb_main.Document.ContentStart, ctb_main.Document.ContentEnd).Text;
            string finalCode = code.Replace("user_code", richText);
            //initialisation d'un compilateur de code C#
            CSharpCodeProvider provider = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
            //initialisation des paramètres du compilateur de code C#
            CompilerParameters parameters = new CompilerParameters();
            //ajout des lien de bibliothèque dynamique (dll)
            //parameters.ReferencedAssemblies.Add("WindowsBase.dll");
            parameters.ReferencedAssemblies.Add("GofusSharp.dll");
            parameters.ReferencedAssemblies.Add("MySql.Data.dll");
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
            //mettre la fonction compilé dans une variable
            Type binaryFunction = results.CompiledAssembly.GetType("Arene.Combat");
            //invoqué la fonction compilée avec la variable
            System.Windows.Forms.MessageBox.Show(binaryFunction.GetMethod("Generer").Invoke(null, null).ToString());
        }

        //**************************************************************************************************
        public int maxLC = 0; //maxLineCount - should be public
        private void ctb_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            int linecount = new TextRange(ctb_main.Document.ContentStart, ctb_main.Document.ContentEnd).Text.Split('\n').Count();
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


        //**************************************************************************************************
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
            TreeNode treeNode_1 = new TreeNode("keyword");
            TreeNode treeNode_2 = new TreeNode("classGofus");
            TreeNode treeNode_3 = new TreeNode("Math", treeNodeTab_math);
            TreeNode treeNode_4 = new TreeNode("chaine", treeNodeTab_chaine);
            TreeNode treeNode_5 = new TreeNode("simpleVar", treeNodeTab_simpleVar);
            TreeNode treeNode_6 = new TreeNode("tab", treeNodeTab_tab);
            TreeNode treeNode_7 = new TreeNode("fonctionVoid");

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

            TreeNode[] treeNode_root = new TreeNode[] {
            treeNode_1,
            treeNode_2,
            treeNode_3,
            treeNode_4,
            treeNode_5,
            treeNode_6,
            treeNode_7};



            TreeNode[] treeNode_Intellisense = new TreeNode[treeNode_root.Length + treeNodeTab_keyword.Length];
            treeNodeTab_keyword.CopyTo(treeNode_Intellisense, 0);
            treeNode_root.CopyTo(treeNode_Intellisense, treeNodeTab_keyword.Length);
            return treeNode_Intellisense;
        }
        //**************************************************************************************************
        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);
        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, UIntPtr wParam, UIntPtr lParam);


        private void ctb_main_VScroll(object sender, EventArgs e)
        {
            int nPos = GetScrollPos(new WindowInteropHelper(Window.GetWindow(ctb_main)).Handle, (int)ScrollBarType.SbVert);
            nPos <<= 16;
            uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
            SendMessage(new WindowInteropHelper(Window.GetWindow(rtb_lineNumber)).Handle, (int)Message.WM_VSCROLL, new UIntPtr(wParam), new UIntPtr(0));
        }

        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
            CombatTest lol = new CombatTest();
            System.Windows.Forms.MessageBox.Show(lol.combat(64));
        }
        #endregion


        #region Marché
        private void btnAchat_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult m = System.Windows.MessageBox.Show("Voulez vous vraiment acheter l'objet : " + lblItem.Content + ". Au cout de " +  lblPrix.Content + " Kamas ?", "Achat", MessageBoxButton.YesNo,MessageBoxImage.Information);
            if (m == MessageBoxResult.Yes)
                if (Player.Kamas < (int)lblPrix.Content)
                {
                    System.Windows.MessageBox.Show("Il semblerais que vous n'avez pas assez de Kamas ! Vous ne voulez pas être endetter de " + lblPrix.Content + " Kamas ?", "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    Player.Kamas -= (int)lblPrix.Content;
                    //TODO: insertion de l'item dans l'inventaire du joueur
                }
        }

        private void image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //TODO : add border to selected item
            /* Border border = new Border();
             border.BorderBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
             border.BorderThickness = new Thickness(1);
             // ((Image)sender).Parent as border;*/
            #region Abracadabra
            btnAchat.Visibility = Visibility.Visible;
            lblPri.Visibility = Visibility.Visible;
            imgKamas.Visibility = Visibility.Visible;
            tabControlStats.Visibility = Visibility.Visible;
            #endregion

            if (imgCurrent.Source == (((ImageItem)sender).imgItem.Source))
                return;

            LstCaras.Clear();
            LstStats.Clear();
            LstConds.Clear();

            imgCurrent.Source = ((ImageItem)sender).imgItem.Source;
            string info = "SELECT * FROM Equipements  WHERE nom ='" + ((ImageItem)sender).txtNom.Text + "'";

            Equipement item = new Equipement(bd.selection(info)[0], true);
            lblItem.Content = item.Nom;

            lblPrix.Content = item.Prix;
            txtBDesc.Text = item.Desc;

            // ajoutes les nouvelles
            if (item.EstArme)
                foreach (Effet effet in item.LstEffets)
                    LstStats.Add(effet.NomSimplifier + " : " + effet.DmgMin + " à " + effet.DmgMax);

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

        private void fillSortCbo()
        {
            List<string> type = new List<string>();
            type.Add("Tous");
            foreach (List<string> typeNom in bd.selection("SELECT nom FROM typesEquipements"))
                type.Add(typeNom[0]);
            cboTrie.ItemsSource = type;
            cboTrie.SelectedIndex = 0;
            cboTrie.SelectionChanged += cboTrie_SelectionChanged;
        }

        private void cboTrie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string type = ((System.Windows.Controls.ComboBox)sender).SelectedValue.ToString();
            LstImgItems.Clear();
            string query = "SELECT * FROM Equipements  e INNER JOIN ConditionsEquipements c ON c.idEquipement = e.idEquipement INNER JOIN TypesEquipements t ON t.idTypeEquipement = e.idTypeEquipement WHERE idCondition = 21 " + (type == "Tous" ? "" : "AND t.nom ='" + type + "'") + " ORDER BY c.valeur  LIMIT 10 OFFSET 0";
            string count = "SELECT COUNT(*) FROM Equipements  e INNER JOIN ConditionsEquipements c ON c.idEquipement = e.idEquipement INNER JOIN TypesEquipements t ON t.idTypeEquipement = e.idTypeEquipement WHERE idCondition = 21 " + (type == "Tous" ? "" : "AND t.nom ='" + type + "'") + " ORDER BY c.valeur  LIMIT 10 OFFSET 0";

            List<string>[] items = bd.selection(query);
                dckLink.Children.Clear();
            if (items[0][0] != "rien")
            {
                createPageLinks(Convert.ToInt32(bd.selection(count)[0][0]));
                retrieveItem(items);
            }
        }
        private void btn_link_click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT * FROM Equipements  e INNER JOIN ConditionsEquipements c ON c.idEquipement = e.idEquipement INNER JOIN TypesEquipements t ON t.idTypeEquipement = e.idTypeEquipement WHERE idCondition = 21 " + ((string)cboTrie.SelectedValue == "Tous" ? "" : "AND t.nom ='" + (string)cboTrie.SelectedValue + "'") + " ORDER BY c.valeur  LIMIT 10 OFFSET " + ((Convert.ToInt16(((System.Windows.Controls.Button)sender).Content) - 1) * 10).ToString();
            LstImgItems.Clear();
            List<string>[] items = bd.selection(query);
            if (items[0][0] != "rien")
                retrieveItem(items);
        }
        private void createPageLinks(int nbPages)
        {            //<Button Style="{StaticResource LinkButton}" Content="Clicky" />
            nbPages = nbPages / 10;

            for (int i = 0; i < nbPages + 1; i++)
            {
                System.Windows.Controls.Button btn = new System.Windows.Controls.Button();
                btn.Style = (Style)FindResource("LinkButton");
                btn.Click += btn_link_click;
                btn.Content = (i + 1).ToString();
                dckLink.Children.Add(btn);
                if (i != nbPages)
                {
                    System.Windows.Controls.Label lbl = new System.Windows.Controls.Label();
                    lbl.Content = " - ";
                    lbl.Height = 21;
                    lbl.VerticalAlignment = VerticalAlignment.Center;
                    dckLink.Children.Add(lbl);
                }
            }
        }

        private void retrieveItem(List<string>[] items)
        {
            short col = 0;
            short row = 0;

            foreach (List<string> item in items)
            {
                Equipement equip = new Equipement(item, false);
                ImageItem i = new ImageItem(equip);
                i.MouseDown += image_MouseUp;
                Grid.SetColumn(i, col);
                Grid.SetRow(i, row);
                if (col == 4)
                {
                    col = -1;
                    row++;
                }
                col++;
                LstImgItems.Add(i);
            }
        }
        #endregion

        /// ***************************************************
        /// / ONGLET OPTIONS
        // ***************************************************
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            /* Faire un update si toute est legit*/
            if (txt_mdp.Password != "" && txt_mdp.Password == txtConfirmation.Password && txtConfirmation.Password != "")
            {
                /* Update */
                lbl_Mdp.Foreground = new SolidColorBrush(Colors.Black);
                lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                if (txt_mdp.Password == "" && txtConfirmation.Password == "")
                {
                    /* Aucune modification effectué*/
                    lbl_Mdp.Foreground = new SolidColorBrush(Colors.Black);
                    lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Black);
                }
                else if (txt_mdp.Password != "")
                {
                    /* Erreur de confirmation*/
                    lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Red);

                }
                else if (txt_mdp.Password == "" & txtConfirmation.Password != "")
                {
                    /* Mot de passe vide*/
                    lbl_Mdp.Foreground = new SolidColorBrush(Colors.Red);
                }
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

        private void btnSuggestion_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Bientôt disponnible !");
        }

        private void InlineUIContainer_Unloaded_Debut(object sender, RoutedEventArgs e)
        {
            (sender as InlineUIContainer).Unloaded -= new RoutedEventHandler(InlineUIContainer_Unloaded_Debut);

            TextBlock tb = new TextBlock();
            tb.FontFamily = new FontFamily("Courier New");
            tb.FontSize = 14;
            tb.Text = "public void Action(Terrain terrain, Personnage joueur, System.Collections.ObjectModel.ReadOnlyCollection<EntiteInconnu> ListEntites){";

            TextPointer tp = ctb_main.CaretPosition.GetInsertionPosition(LogicalDirection.Forward);
            InlineUIContainer iuic = new InlineUIContainer(tb, tp);
            iuic.Unloaded += new RoutedEventHandler(InlineUIContainer_Unloaded_Debut);
        }

        private void InlineUIContainer_Unloaded_Fin(object sender, RoutedEventArgs e)
        {
            (sender as InlineUIContainer).Unloaded -= new RoutedEventHandler(InlineUIContainer_Unloaded_Fin);

            TextBlock tb = new TextBlock();
            tb.FontFamily = new FontFamily("Courier New");
            tb.FontSize = 14;
            tb.Text = "}";

            TextPointer tp = ctb_main.CaretPosition.GetInsertionPosition(LogicalDirection.Forward);
            InlineUIContainer iuic = new InlineUIContainer(tb, tp);
            iuic.Unloaded += new RoutedEventHandler(InlineUIContainer_Unloaded_Fin);
        }

        private void rtb_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var newPointer = ctb_main.Selection.Start.InsertLineBreak();
                ctb_main.Selection.Select(newPointer, newPointer);

                e.Handled = true;
            }
        }

        // ***************************************************
        //Onglet Personnage
        // ***************************************************

        private void TabPersonage_Selected(object sender, RoutedEventArgs e)
        {
            //le nom du perso

            int nbScript = Player.LstScripts.Count;
            for (int i = 0; i < nbScript; i++)
            {
                cbScript.Items.Add(Player.LstScripts[i].Nom);

            }


            foreach (Entite perso in Player.LstEntites)
            {
                //todo création de plusieurs onglets personnage
                ongletPerso.Header = perso.Nom;
                lblNomClasse.Content = perso.ClasseEntite.Nom;
                string SourceImgClasse = "resources/" + perso.ClasseEntite.Nom;

                BitmapImage path = new BitmapImage();
                path.BeginInit();
                path.UriSource = new Uri(SourceImgClasse + ".png", UriKind.Relative);
                path.EndInit();
                Imgclasse.Source = path;

                cbScript.SelectedItem = perso.ScriptEntite.Nom;


                dgStats.ItemsSource = perso.LstStats;
                dgDommage.ItemsSource = perso.LstStats;
                dgResistance.ItemsSource = perso.LstStats;

            }

        }

        private void AjustementLignes(Entite perso)
        {

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
        //**************************************************************************************************
    }
}