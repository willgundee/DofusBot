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
using System.Globalization;
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
        private Dictionary<int, double> dictLvl = new Dictionary<int, double>()
        {
            #region les levels
            {1,0},
            {2,110},
            {3,650},
            {4,1500},
            {5,2800},
            {6,4800},
            {7,7300},
            {8,10500},
            {9,14500},
            {10,19200},
            {11,25200},
            {12,32600},
            {13,41000},
            {14,50500},
            {15,61000},
            {16,75000},
            {17,91000},
            {18,115000},
            {19,142000},
            {20,171000},
            {21,202000},
            {22,235000},
            {23,270000},
            {24,310000},
            {25,353000},
            {26,398500},
            {27,448000},
            {28,503000},
            {29,561000},
            {30,621600},
            {31,687000},
            {32,755000},
            {33,829000},
            {34,910000},
            {35,1000000},
            {36,1100000},
            {37,1240000},
            {38,1400000},
            {39,1580000},
            {40,1780000},
            {41,2000000},
            {42,2250000},
            {43,2530000},
            {44,2850000},
            {45,3200000},
            {46,3570000},
            {47,3960000},
            {48,4400000},
            {49,4860000},
            {50,5350000},
            {51,5860000},
            {52,6390000},
            {53,6950000},
            {54,7530000},
            {55,8130000},
            {56,8765100},
            {57,9420000},
            {58,10150000},
            {59,10894000},
            {60,11655000},
            {61,12450000},
            {62,13280000},
            {63,14130000},
            {64,15170000},
            {65,16251000},
            {66,17377000},
            {67,18553000},
            {68,19778000},
            {69,21055000},
            {70,22385000},
            {71,23529000},
            {72,25209000},
            {73,26707000},
            {74,28264000},
            {75,29882000},
            {76,31563000},
            {77,33307000},
            {78,35118000},
            {79,36997000},
            {80,38945000},
            {81,40965000},
            {82,43059000},
            {83,45229000},
            {84,47476000},
            {85,49803000},
            {86,52211000},
            {87,54704000},
            {88,57284000},
            {89,59952000},
            {90,62712000},
            {91,65565000},
            {92,68514000},
            {93,71561000},
            {94,74710000},
            {95,77963000},
            {96,81323000},
            {97,84792000},
            {98,88374000},
            {99,92071000},
            {100,95886000},
            {101,99823000},
            {102,103885000},
            {103,108075000},
            {104,112396000},
            {105,116853000},
            {106,121447000},
            {107,126184000},
            {108,131066000},
            {109,136098000},
            {110,141283000},
            {111,146626000},
            {112,152130000},
            {113,157800000},
            {114,163640000},
            {115,169655000},
            {116,175848000},
            {117,182225000},
            {118,188791000},
            {119,195550000},
            {120,202507000},
            {121,209667000},
            {122,217037000},
            {123,224620000},
            {124,232424000},
            {125,240452000},
            {126,248712000},
            {127,257209000},
            {128,265949000},
            {129,274939000},
            {130,284186000},
            {131,293694000},
            {132,303473000},
            {133,313527000},
            {134,323866000},
            {135,334495000},
            {136,345423000},
            {137,356657000},
            {138,368206000},
            {139,380076000},
            {140,392278000},
            {141,404818000},
            {142,417706000},
            {143,430952000},
            {144,444564000},
            {145,458551000},
            {146,472924000},
            {147,487693000},
            {148,502867000},
            {149,518458000},
            {150,534476000},
            {151,551000000},
            {152,567839000},
            {153,585206000},
            {154,603047000},
            {155,621374000},
            {156,640199000},
            {157,659536000},
            {158,679398000},
            {159,699798000},
            {160,720751000},
            {161,742272000},
            {162,764374000},
            {163,787074000},
            {164,810387000},
            {165,834329000},
            {166,858917000},
            {167,884167000},
            {168,910098000},
            {169,936727000},
            {170,964073000},
            {171,992154000},
            {172,1020991000},
            {173,1050603000},
            {174,1081010000},
            {175,1112235000},
            {176,1144298000},
            {177,1177222000},
            {178,1211030000},
            {179,1245745000},
            {180,1281393000},
            {181,1317997000},
            {182,1355584000},
            {183,1404179000},
            {184,1463811000},
            {185,1534506000},
            {186,1616294000},
            {187,1709205000},
            {188,1813267000},
            {189,1928513000},
            {190,2054975000},
            {191,2192686000},
            {192,2341679000},
            {193,2501990000},
            {194,2673655000},
            {195,2856710000},
            {196,3051194000},
            {197,3257146000},
            {198,3474606000},
            {199,3703616000},
            {200,5555424000}
             #endregion
        };
        public Chat chat;
        public Joueur Player { get; set; }
        public Thread trdEnvoie { get; private set; }

        public ObservableCollection<PagePerso> pgperso; 

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

            pgperso = new ObservableCollection<PagePerso>();
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
            long envoie = -1;

            trdEnvoie = new Thread(() => {
                envoie = chat.envoyerMessageModLess();
            });
            trdEnvoie = Thread.CurrentThread;

            if (envoie != -1)
            {
                trdEnvoie = new Thread(() =>
                {
                    threadRefresh();
                });
                trdEnvoie = Thread.CurrentThread;
            }
            else
            {
                System.Windows.MessageBox.Show("Erreur d'envois du message..");
            }
        }

        private void threadRefresh()
        {
            chat.refreshChat();
            Scroll.ScrollToEnd();
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


            fenetreChat = new ChatWindow(chat.id);
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
            TreeNode treeNode_1 = new TreeNode("keyword");
            TreeNode treeNode_2 = new TreeNode("classGofus");
            TreeNode treeNode_3 = new TreeNode("Math", treeNodeTab_math);
            TreeNode treeNode_4 = new TreeNode("chaine", treeNodeTab_chaine);
            TreeNode treeNode_5 = new TreeNode("simpleVar", treeNodeTab_simpleVar);
            TreeNode treeNode_6 = new TreeNode("tab", treeNodeTab_tab);
            TreeNode treeNode_7 = new TreeNode("fonctionVoid");
            TreeNode treeNode_8 = new TreeNode("Liste", treeNodeTab_Liste);

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

            TreeNode[] treeNode_root = new TreeNode[] {
            treeNode_1,
            treeNode_2,
            treeNode_3,
            treeNode_4,
            treeNode_5,
            treeNode_6,
            treeNode_7,
            treeNode_8};



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
            SendMessage(new WindowInteropHelper(Window.GetWindow(rtb_lineNumber)).Handle, (int)Message.WM_VSCROLL, new UIntPtr(wParam), new UIntPtr(0));
        }
#endregion


        #region Marché

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

            //SELECT * FROM Equipements e  INNER JOIN TypesEquipements t ON t.idTypeEquipement = e.idTypeEquipement INNER JOIN StatistiquesEquipements s ON s.idEquipement = e.idEquipement INNER JOIN TypesStatistiques ts ON ts.idTypeStatistique = s.idTypeStatistique INNER JOIN EffetsEquipements ee ON ee.idEquipement = e.idEquipement INNER JOIN Effets et ON et.idEffet = ee.idEffet WHERE e.nom ='Marteau du bouftou'
            if (imgCurrent.Source == ((Image)sender).Source)
                return;
            imgCurrent.Source = ((Image)sender).Source;
            string nomItem = ((Image)sender).Name.Replace("_", " ");
            double expRequis = 0;
            string info = "SELECT * FROM Equipements  WHERE nom ='" + nomItem + "'";
            /*List<string>[] infoItem = bd.selection(info);
            List<string>[] statsItem = bd.selection(stats);*/
            //set des info dans les champs statics
            Equipement item = new Equipement(bd.selection(info)[0],true);
            lblItem.Content = item.Nom;

            NumberFormatInfo nfi = (NumberFormatInfo)
            CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";
            lblPrix.Content = Convert.ToInt32(item.Prix).ToString("n",nfi);// n | x
            txtBDesc.Text = item.Desc;
            //elneves tout les stats de l'item

            grdStats.Children.Clear();
            // ajoutes les nouvelles

            grdStats.Children.Add(CreateLbl("Type :", 0, 0));
            grdStats.Children.Add(CreateLbl(item.Type, 0, 1));
            short col = 0;
            short row = 1;
            for (int i = 0; i < item.LstStatistiques.Count(); i++)
            {//ajout des stats
                grdStats.Children.Add(CreateLbl(item.LstStatistiques[i].NomSimple + " :", row, col));
                grdStats.Children.Add(CreateLbl(item.LstStatistiques[i].Valeur.ToString(), row, col +1));
                row++;
                if (row == 5)
                {
                    row = 0;
                    col = 2;
                }

            }

            if (item.EstArme)
                for (int x = 0; x < item.LstEffets.Count(); x++)
                {//ajout des dommages
                    grdStats.Children.Add(CreateLbl(item.LstEffets[x].NomSimplifier + " :", x, 2));
                    grdStats.Children.Add(CreateLbl(item.LstEffets[x].DmgMin.ToString() + " à " + item.LstEffets[x].DmgMax.ToString(), x, 3));

                }
            grdCond.Children.Clear();

            grdCond.Children.Add(CreateLbl("Niveau requis : ", 0, 0));
            foreach (Condition cd in item.LstConditions)
                if (cd.Stat.Nom == Statistique.element.experience)
                    expRequis = cd.Stat.Valeur;

            grdCond.Children.Add(CreateLbl(toLevel(expRequis).ToString(), 0, 1));

            for (int w = 0; w < item.LstConditions.Count(); w++)
                if (item.LstConditions[w].Stat.Nom != Statistique.element.experience)
                {
                    grdCond.Children.Add(CreateLbl(item.LstConditions[w].Stat.NomSimple + " " + item.LstConditions[w].Signe, w + 1, 0));
                    grdCond.Children.Add(CreateLbl(item.LstConditions[w].Stat.Valeur.ToString(), w + 1, 1));
                }

        }
        private int toLevel(double exp)
        {
            for (int i = 1; i < 200; i++)
                if (exp >= dictLvl[i] && exp < dictLvl[i + 1])
                    return i;
            if (exp >= dictLvl[200])
                return 200;
            return 0;//si tout fucktop
        }

        private System.Windows.Controls.Label CreateLbl(string content, int row, int col)
        {
            System.Windows.Controls.Label lbl = new System.Windows.Controls.Label();
            lbl.Content = content;
            Grid.SetColumn(lbl, col);
            Grid.SetRow(lbl, row);
            return lbl;
        }

        private void addPages(int nbPages)
        {

        }

        private void TabItemMarche_Loaded(object sender, RoutedEventArgs e)
        {

            /* Thread th = new Thread(new ThreadStart(loadItem));
             th.SetApartmentState(ApartmentState.STA);

             th.Start();
             th.Join();*/
            loadItem();
            //TODO: Lorsque plus de données faire un limit/offset et des numero de page
            //SELECT * FROM ConditionsEquipements c INNER JOIN Equipements e ON c.idEquipement = e.idEquipement WHERE idCondition = 21 ORDER BY c.valeur
            // string equips = "SELECT * FROM Equipements WHERE idZonePorte IS NULL LIMIT 10 ";
          

        }

        public void loadItem()
        {
            string equips = "SELECT * FROM Equipements  e INNER JOIN ConditionsEquipements c ON c.idEquipement = e.idEquipement WHERE idCondition = 21  AND idZonePorte IS NULL ORDER BY c.valeur DESC LIMIT 10";
            string armes = "SELECT * FROM Equipements WHERE idZonePorte IS NOT NULL LIMIT 10";
            int nbPages = Convert.ToInt16(bd.selection(" SELECT COUNT(*) FROM Equipements WHERE idZonePorte IS NULL")[0][0]);
            List<string>[] repArmes = bd.selection(armes);
            List<string>[] repEquip = bd.selection(equips);

            short col = 0;
            short row = 0;//géneration des images des armes
            foreach (List<string> item in repArmes)
            {
                Equipement equip = new Equipement(item, false);
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
                Equipement equip = new Equipement(item, false);
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
            ImageSource path = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/items/200/" + Noimg + ".png"));
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

        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
            Combat combat = new Combat();
        }
        #endregion
        #region Marc/Tchat
        /// ***************************************************
        /// / ONGLET OPTIONS
        // ***************************************************
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            /* Faire un update si toute est legit*/
            if (txt_mdp.Password != "" && txt_mdp.Password == txtConfirmation.Password && txtConfirmation.Password != "" || txt_Courriel.Text != "")
            {
                /* Update */
                lbl_Mdp.Foreground = new SolidColorBrush(Colors.Black);
                lbl_Confirmation.Foreground = new SolidColorBrush(Colors.Black);
                StringBuilder UpdSt = new StringBuilder();
                UpdSt.Append("UPDATE Joueurs SET ");
                if (txt_Courriel.Text != "")
                {
                    UpdSt.Append("courriel = '" + txt_Courriel.Text + "'");
         
                }
                if (txt_mdp.Password != "" && txt_mdp.Password == txtConfirmation.Password && txtConfirmation.Password != "")
                {
                    UpdSt.Append(" , motDePasse = '" + txt_mdp.Password + "'");

                }
                UpdSt.Append(" WHERE nomUtilisateur = '" + Player.NomUtilisateur + "';");

                

                string st = UpdSt.ToString();
                if (bd.Update(st))
                {
                    System.Windows.Forms.MessageBox.Show("Mise à jour avec succès de vos infos!!");
                }

                txt_mdp.Password = "";
                txtConfirmation.Password = "";
                txt_Courriel.Text = "";

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
        #endregion
        #region /Perso
        // ***************************************************
        //Onglet Personnage
        // ***************************************************

        private void TabPersonage_Selected(object sender, RoutedEventArgs e)
        {
            //le nom du perso 
            foreach (Entite perso in Player.LstEntites)
            {
                pgperso.Add( new PagePerso (perso, Player));
            }
            tCPerso.ItemsSource=pgperso;
        }

        #endregion



        //**************************************************************************************************
    }
}