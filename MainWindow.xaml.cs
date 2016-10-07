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
        DispatcherTimer aTimer;
        private ChatWindow fenetreChat;


        public MainWindow()
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


          
            this.chat = new Chat();
            btnEnvoyerMessage.IsEnabled = false;


            aTimer = new System.Windows.Threading.DispatcherTimer();
            aTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            aTimer.Interval = new TimeSpan(0, 0, 2);



            tb_lineNumber.Font = new System.Drawing.Font("Courier New", 8);
            tb_lineNumber.ReadOnly = true;
            tb_lineNumber.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            tb_lineNumber.Cursor = System.Windows.Forms.Cursors.Arrow;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            System.Windows.Application.Current.Shutdown();
        }



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
            string richText = ctb_main.Text;
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
            int linecount = ctb_main.Lines.Count();
            if (linecount != maxLC)
            {
                tb_lineNumber.Clear();
                for (int i = 1; i < linecount + 1; i++)
                {
                    if (i == 1)
                        tb_lineNumber.AppendText(i.ToString());
                    else
                        tb_lineNumber.AppendText(Environment.NewLine + i.ToString());
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
            int nPos = GetScrollPos(ctb_main.Handle, (int)ScrollBarType.SbVert);
            nPos <<= 16;
            uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
            SendMessage(tb_lineNumber.Handle, (int)Message.WM_VSCROLL, new UIntPtr(wParam), new UIntPtr(0));
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
            #endregion

            //SELECT * FROM Equipements e  INNER JOIN TypesEquipements t ON t.idTypeEquipement = e.idTypeEquipement INNER JOIN StatistiquesEquipements s ON s.idEquipement = e.idEquipement INNER JOIN TypesStatistiques ts ON ts.idTypeStatistique = s.idTypeStatistique INNER JOIN EffetsEquipements ee ON ee.idEquipement = e.idEquipement INNER JOIN Effets et ON et.idEffet = ee.idEffet WHERE e.nom ='Marteau du bouftou'
            imgCurrent.Source = ((Image)sender).Source;
            string nomItem = ((Image)sender).Name.Replace("_", " ");
            string info = "SELECT * FROM Equipements e INNER JOIN TypesEquipements t ON t.idTypeEquipement = e.idTypeEquipement WHERE e.nom ='" + nomItem + "'";
            string stats = "SELECT ts.nom ,valeur FROM Equipements e INNER JOIN TypesEquipements t ON t.idTypeEquipement = e.idTypeEquipement INNER JOIN StatistiquesEquipements s ON s.idEquipement = e.idEquipement INNER JOIN TypesStatistiques ts ON ts.idTypeStatistique = s.idTypeStatistique WHERE e.nom ='" + nomItem + "'";
            string dmg = "SELECT valeurMin,valeurMax,et.nom FROM Equipements e INNER JOIN EffetsEquipements ee ON ee.idEquipement = e.idEquipement INNER JOIN Effets  et ON et.idEffet = ee.idEffet WHERE e.nom ='" + nomItem + "'";
            string eft = "SELECT t.nom,z.porteeMin,z.porteeMax FROM Equipements e INNER JOIN Zones z ON z.idZone = e.idZoneEffet INNER JOIN TypesZones t ON t.idTypeZone = z.idTypeZone WHERE e.nom ='" + nomItem + "'";
            string po = "SELECT t.nom,z.porteeMin,z.porteeMax FROM Equipements e INNER JOIN Zones z ON z.idZone = e.idZonePorte INNER JOIN TypesZones t ON t.idTypeZone = z.idTypeZone WHERE e.nom ='" + nomItem + "'";
            /*List<string>[] infoItem = bd.selection(info);
            List<string>[] statsItem = bd.selection(stats);*/
            //set des info dans les champs statics
            Equipement item = new Equipement(bd.selection(info)[0], bd.selection(stats));
            if (item.EstArme)
                item = new Equipement(bd.selection(info)[0], bd.selection(stats), bd.selection(po)[0], bd.selection(eft)[0], bd.selection(dmg));
            lblItem.Content = item.Nom;
            lblPrix.Content = item.Prix;
            txtBDesc.Text = item.Desc;
            //elneves tout les stats de l'item

            grdStats.Children.Clear();
            // ajoutes les nouvelles

            grdStats.Children.Add(CreateLbl("Type :", 0, 0));
            grdStats.Children.Add(CreateLbl(item.Type, 0, 1));

            for (int i = 0; i < item.LstStatistiques.Count(); i++)
            {//ajout des stats
                grdStats.Children.Add(CreateLbl(item.LstStatistiques[i].Nom + " :", i + 1, 0));
                grdStats.Children.Add(CreateLbl(item.LstStatistiques[i].Valeur.ToString(), i + 1, 1));
            }

            if (item.EstArme)
                for (int x = 0; x < item.LstEffet.Count(); x++)
                {//ajout des dommages
                    grdStats.Children.Add(CreateLbl(item.LstEffet[x].NomSimplifier + " :", x, 2));
                    grdStats.Children.Add(CreateLbl(item.LstEffet[x].DmgMin.ToString() + " à " +item.LstEffet[x].DmgMax.ToString(), x, 3));

                }

        }

        private System.Windows.Controls.Label CreateLbl(string content, int row, int col)
        {
            System.Windows.Controls.Label lbl = new System.Windows.Controls.Label();
            lbl.Content = content;
            Grid.SetColumn(lbl, col);
            Grid.SetRow(lbl, row);
            return lbl;
        }

        private void TabItemMarche_Selected(object sender, RoutedEventArgs e)
        {
            //TODO: Lorsque plus de données faire un limit/offset et des numero de page
            string equips = "SELECT * FROM Equipements e INNER JOIN TypesEquipements t ON t.idTypeEquipement = e.idTypeEquipement WHERE idZonePorte IS NULL LIMIT 10 ";
            string armes = "SELECT * FROM Equipements e INNER JOIN TypesEquipements t ON t.idTypeEquipement = e.idTypeEquipement WHERE idZonePorte IS NOT NULL LIMIT 10";
            List<string>[] repArmes = bd.selection(armes);
            List<string>[] repEquip = bd.selection(equips);

            short col = 0;
            short row = 0;//géneration des images des armes
            foreach (List<string> item in repArmes)
            {
                Equipement equip = new Equipement(item);
                Image img = CreateImg(equip.NoImg, equip.Nom);
                if (col == 5)
                {
                    col = 0;
                    row++;
                }
                Grid.SetRow(img, row);
                Grid.SetColumn(img, col);
                col++;
                grdArmes.Children.Add(img);
            }

            col = 0;
            row = 0;// génération des images des équipements
            foreach (List<string> item in repEquip)
            {
                Equipement equip = new Equipement(item);
                Image img = CreateImg(equip.NoImg, equip.Nom);
                if (col == 5)
                {
                    col = 0;
                    row++;
                }
                Grid.SetRow(img, row);
                Grid.SetColumn(img, col);
                col++;
                grdEquips.Children.Add(img);
            }

        }

        private Image CreateImg(string Noimg, string nom)
        {
            Image img = new Image();
            System.Windows.Media.ImageSource path = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/items/200/" + Noimg + ".png"));
            img.Width = 100;
            img.Height = 100;
            img.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            img.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            img.MouseUp += image_MouseUp; // ajoute l'evenement mouse_up
            img.Source = path;
            img.Name = nom.Replace(" ", "_");
            return img;
        }

        private void btnAchat_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Coming soon !");
        }

        private void TabItemMarche_Unselected(object sender, RoutedEventArgs e)
        {
            grdArmes.Children.Clear();
            grdEquips.Children.Clear();
        }

        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
            CombatTest lol = new CombatTest();
        }





        //**************************************************************************************************
    }
}