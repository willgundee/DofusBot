using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GofusSharp;
using System.Windows.Media.Imaging;
using System.Threading;
using System.ComponentModel;

[assembly: InternalsVisibleTo("Gofus")]
namespace GofusSharp
{
    /// <summary>
    /// Logique d'interaction pour Combat.xaml
    /// </summary>
    internal partial class Combat : Window
    {
        internal Partie CombatCourant { get; set; }
        internal Partie CombatGeneration { get; set; }
        internal bool Generation { get; set; }
        internal double Speed { get; set; }

        internal BackgroundWorker bw = new BackgroundWorker();


        private bool AutoScroll = true;

        public Combat(List<Gofus.Entite> lstJoueurAtt, List<Gofus.Entite> lstJoueurDef, int seed, long idPartie, bool premiereGeneration = true)
        {
            InitializeComponent();
            Generation = premiereGeneration;
            if (Generation)
            {
                Gofus.BDService BD = new Gofus.BDService();
                bool? resultat = GenererPartie(lstJoueurAtt, lstJoueurDef, seed);
                BD.Update("UPDATE Parties SET attaquantAGagne = " + (resultat == null?"null":(resultat == true?"true":"false")) + " WHERE idPartie = " + idPartie + ";");
                Generation = false;
            }
            Speed = 1.5;
            txtNum.Text = Speed.ToString();
            Show();
            CreerPartie(lstJoueurAtt, lstJoueurDef, seed);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    StackPanel sPnl = new StackPanel();
                    sPnl.HorizontalAlignment = HorizontalAlignment.Center;
                    sPnl.VerticalAlignment = VerticalAlignment.Center;
                    Border border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(2);
                    Grid.SetRow(sPnl, i);
                    Grid.SetColumn(sPnl, j);
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    grd_Terrain.Children.Add(border);
                    grd_Terrain.Children.Add(sPnl);
                }
            }
            UpdateInfo();
        }
        private void CreerPartie(List<Gofus.Entite> lstJoueurAtt, List<Gofus.Entite> lstJoueurDef, int seed)
        {
            Liste<Entite> ListEntiteAtt = new Liste<Entite>();
            Liste<Entite> ListEntiteDef = new Liste<Entite>();
            Terrain terrain = new Terrain(10, 10);
            foreach (Gofus.Entite entite in lstJoueurAtt)
            {
                if (entite.EstPersonnage)
                    ListEntiteAtt.Add(new Personnage(entite, EntiteInconnu.type.attaquant, terrain));
                else
                    ListEntiteAtt.Add(new Entite(entite, EntiteInconnu.type.attaquant, terrain));
            }
            foreach (Gofus.Entite entite in lstJoueurDef)
            {
                if (entite.EstPersonnage)
                    ListEntiteDef.Add(new Personnage(entite, EntiteInconnu.type.defendant, terrain));
                else
                    ListEntiteDef.Add(new Entite(entite, EntiteInconnu.type.defendant, terrain));
            }

            CombatCourant = new Partie(terrain, ListEntiteAtt, ListEntiteDef, seed);
        }

        private bool? GenererPartie(List<Gofus.Entite> lstJoueurAtt, List<Gofus.Entite> lstJoueurDef, int seed)
        {
            Liste<Entite> ListEntiteAtt = new Liste<Entite>();
            Liste<Entite> ListEntiteDef = new Liste<Entite>();
            Terrain terrain = new Terrain(10, 10);
            foreach (Gofus.Entite entite in lstJoueurAtt)
            {
                if (entite.EstPersonnage)
                    ListEntiteAtt.Add(new Personnage(entite, EntiteInconnu.type.attaquant, terrain));
                else
                    ListEntiteAtt.Add(new Entite(entite, EntiteInconnu.type.attaquant, terrain));
            }
            foreach (Gofus.Entite entite in lstJoueurDef)
            {
                if (entite.EstPersonnage)
                    ListEntiteDef.Add(new Personnage(entite, EntiteInconnu.type.defendant, terrain));
                else
                    ListEntiteDef.Add(new Entite(entite, EntiteInconnu.type.defendant, terrain));
            }

            CombatGeneration = new Partie(terrain, ListEntiteAtt, ListEntiteDef, seed);
            for (int i = 0; i < 64; i++)
            {
                foreach (Entite entite in Liste<Entite>.ConcatAlternate(CombatGeneration.ListAttaquants, CombatGeneration.ListDefendants))
                {
                    if (entite.Etat == EntiteInconnu.typeEtat.mort)
                        continue;
                    CombatGeneration.DebuterAction(entite);
                    if (entite is Personnage)
                        Action(CombatGeneration.TerrainPartie, entite as Personnage, CombatGeneration.ListEntites);
                    else
                        Action(CombatGeneration.TerrainPartie, entite as Entite, CombatGeneration.ListEntites);
                    CombatGeneration.SyncroniserJoueur();
                    bool vivante = false;
                    foreach (Entite entiteAtt in CombatGeneration.ListAttaquants)
                    {
                        if (entiteAtt.Etat == EntiteInconnu.typeEtat.vivant)
                        {
                            vivante = true;
                            break;
                        }
                    }
                    if (!vivante)
                    {
                        return false;
                    }
                    vivante = false;
                    foreach (Entite entiteDef in CombatGeneration.ListDefendants)
                    {
                        if (entiteDef.Etat == EntiteInconnu.typeEtat.vivant)
                        {
                            vivante = true;
                            break;
                        }
                    }
                    if (!vivante)
                    {
                        return true;
                    }
                }
            }
            return null;
        }

        private void Action(Terrain terrain, Personnage joueur, Liste<EntiteInconnu> ListEntites)
        {
            //code dynamique 
            string code = @"
                using GofusSharp;
                namespace Arene
                {
                    public class Action
                    {
                        public static void Execution(Terrain terrain, Personnage Perso, Liste<EntiteInconnu> ListEntites)
                        {
                            user_code
                        }
                    }
                }
            ";

            //je remplace le mot user_code pour ce qui ce trouve dans la text box
            string finalCode = code.Replace("user_code", joueur.ScriptEntite);
            //initialisation d'un compilateur de code C#
            CSharpCodeProvider provider = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
            //initialisation des paramètres du compilateur de code C#
            CompilerParameters parameters = new CompilerParameters() { GenerateInMemory = true };
            //ajout des lien de bibliothèque dynamique (dll)
            parameters.ReferencedAssemblies.Add("GofusSharp.dll");
            //compilation du code 
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, finalCode);
            //recherche d'érreurs de compilation
            if (results.Errors.HasErrors)
            {
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                return;
            }
            //mettre la fonction compilé dans une variable

            MethodInfo mi = results.CompiledAssembly.GetType("Arene.Action").GetMethod("Execution");
            Thread TAction;
            if (!Generation)
            {
                //bw.WorkerSupportsCancellation = true;
                //bw.WorkerReportsProgress = true;
                //bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                //bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
                //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                //bw.RunWorkerAsync(new object[] { mi, terrain, joueur, ListEntites });
                mi.Invoke(null, new object[] { terrain, joueur, ListEntites });
                //btn_Next.IsEnabled = false;
                //TAction = new Thread(new ThreadStart(() => mi.Invoke(null, new object[] { terrain, joueur, ListEntites })));
                //TAction.Start();
            }
            else
                mi.Invoke(null, new object[] { terrain, joueur, ListEntites });
        }
        
        private void Action(Terrain terrain, Entite joueur, Liste<EntiteInconnu> ListEntites)
        {
            //code dynamique 
            string code = @"
                using GofusSharp;
                namespace Arene
                {
                    public class Action
                    {
                        public static void Execution(Terrain terrain, Entite Perso, Liste<EntiteInconnu> ListEntites)
                        {
                            user_code
                        }
                    }
                }
            ";

            //je remplace le mot user_code pour ce qui ce trouve dans la text box
            string finalCode = code.Replace("user_code", joueur.ScriptEntite);
            //initialisation d'un compilateur de code C#
            CSharpCodeProvider provider = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
            //initialisation des paramètres du compilateur de code C#
            CompilerParameters parameters = new CompilerParameters() { GenerateInMemory = true };
            //ajout des lien de bibliothèque dynamique (dll)
            parameters.ReferencedAssemblies.Add("GofusSharp.dll");
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
            //mettre la fonction compilé dans une variable

            MethodInfo mi = results.CompiledAssembly.GetType("Arene.Action").GetMethod("Execution");

            mi.Invoke(null, new object[] { terrain, joueur, ListEntites });
        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            btn_Next.IsEnabled = false;
            bw.RunWorkerAsync();
            //foreach (Entite entite in Liste<Entite>.ConcatAlternate(CombatCourant.ListAttaquants, CombatCourant.ListDefendants))
            //{
            //    if (entite.Etat == EntiteInconnu.typeEtat.mort)
            //        continue;
            //    CombatCourant.DebuterAction(entite);
            //    if (entite is Personnage)
            //        Action(CombatCourant.TerrainPartie, entite as Personnage, CombatCourant.ListEntites);
            //    else
            //        Action(CombatCourant.TerrainPartie, entite as Entite, CombatCourant.ListEntites);
            //    CombatCourant.SyncroniserJoueur();
            //    UpdateInfo();
            //    bool vivante = false;
            //    foreach (Entite entiteAtt in CombatCourant.ListAttaquants)
            //    {
            //        if (entiteAtt.Etat == EntiteInconnu.typeEtat.vivant)
            //        {
            //            vivante = true;
            //            break;
            //        }
            //    }
            //    if (!vivante)
            //    {
            //        System.Windows.Forms.MessageBox.Show("L'équipe defendante a gagnée");
            //        Close();
            //    }
            //    vivante = false;
            //    foreach (Entite entiteDef in CombatCourant.ListDefendants)
            //    {
            //        if (entiteDef.Etat == EntiteInconnu.typeEtat.vivant)
            //        {
            //            vivante = true;
            //            break;
            //        }
            //    }
            //    if (!vivante)
            //    {
            //        System.Windows.Forms.MessageBox.Show("L'équipe attaquante a gagnée");
            //        Close();
            //    }
            //}
        }

        private void UpdateInfo()
        {
            foreach (StackPanel sPnl in grd_Terrain.Children.Cast<FrameworkElement>().Where(x => x is StackPanel))
            {
                //sPnl.Content = CombatCourant.TerrainPartie.TabCases[Grid.GetRow(sPnl)][Grid.GetColumn(sPnl)].Contenu.ToString().First().ToString();

                Image ImageSprite = new Image();
                switch (CombatCourant.TerrainPartie.TabCases[Grid.GetRow(sPnl)][Grid.GetColumn(sPnl)].Contenu)
                {
                    case Case.type.vide:
                        sPnl.Children.Clear();
                        break;
                    case Case.type.joueur:
                        ImageSource SourceImageClasse = new BitmapImage(new Uri(@"..\..\Resources\GofusSharp\cra.png", UriKind.Relative));
                        ImageSprite.Source = SourceImageClasse;
                        sPnl.Children.Add(ImageSprite);
                        break;
                    case Case.type.obstacle:
                        ImageSource SourceImageObstacle = new BitmapImage(new Uri(@"..\..\Resources\GofusSharp\Roche0.png", UriKind.Relative));
                        ImageSprite.Source = SourceImageObstacle;
                        sPnl.Children.Add(ImageSprite);
                        break;
                }
            }
            StringBuilder info = new StringBuilder();
            info.Append("Attaquant");
            info.Append("\nPoint de vie: " + CombatCourant.ListAttaquants.First().PV);
            if (CombatCourant.ListAttaquants.First().Etat == EntiteInconnu.typeEtat.vivant)
                info.Append("\nPosition: X: " + CombatCourant.ListAttaquants.First().Position.X + " Y: " + CombatCourant.ListAttaquants.First().Position.Y);
            else
                info.Append("\nPosition: X: 0 Y: 0");
            info.Append("\nEtat: " + CombatCourant.ListAttaquants.First().Etat);
            tb_perso0.Text = info.ToString();
            info.Clear();
            info.Append("Deffendant");
            info.Append("\nPoint de vie: " + CombatCourant.ListDefendants.First().PV);
            if (CombatCourant.ListDefendants.First().Etat == EntiteInconnu.typeEtat.vivant)
                info.Append("\nPosition: X: " + CombatCourant.ListDefendants.First().Position.X + " Y: " + CombatCourant.ListDefendants.First().Position.Y);
            else
                info.Append("\nPosition: X: 0 Y: 0");
            info.Append("\nEtat: " + CombatCourant.ListDefendants.First().Etat);
            tb_perso1.Text = info.ToString();
        }

        private void srv_Log_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer log = sender as ScrollViewer;
            // User scroll event : set or unset autoscroll mode
            if (e.ExtentHeightChange == 0)
            {   // Content unchanged : user scroll event
                if (log.VerticalOffset == log.ScrollableHeight)
                {   // Scroll bar is in bottom
                    // Set autoscroll mode
                    AutoScroll = true;
                }
                else
                {   // Scroll bar isn't in bottom
                    // Unset autoscroll mode
                    AutoScroll = false;
                }
            }

            // Content scroll event : autoscroll eventually
            if (AutoScroll && e.ExtentHeightChange != 0)
            {   // Content changed and autoscroll mode set
                // Autoscroll
                log.ScrollToVerticalOffset(log.ExtentHeight);
            }
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (Entite entite in Liste<Entite>.ConcatAlternate(CombatCourant.ListAttaquants, CombatCourant.ListDefendants))
            {
                if (entite.Etat == EntiteInconnu.typeEtat.mort)
                    continue;
                CombatCourant.DebuterAction(entite);
                if (entite is Personnage)
                    Action(CombatCourant.TerrainPartie, entite as Personnage, CombatCourant.ListEntites);
                else
                    Action(CombatCourant.TerrainPartie, entite as Entite, CombatCourant.ListEntites);
                CombatCourant.SyncroniserJoueur();
                bw.ReportProgress(0, new object[] { "update" });
                bool vivante = false;
                foreach (Entite entiteAtt in CombatCourant.ListAttaquants)
                {
                    if (entiteAtt.Etat == EntiteInconnu.typeEtat.vivant)
                    {
                        vivante = true;
                        break;
                    }
                }
                if (!vivante)
                {
                    System.Windows.Forms.MessageBox.Show("L'équipe defendante a gagnée");
                    bw.ReportProgress(0, "end");
                }
                vivante = false;
                foreach (Entite entiteDef in CombatCourant.ListDefendants)
                {
                    if (entiteDef.Etat == EntiteInconnu.typeEtat.vivant)
                    {
                        vivante = true;
                        break;
                    }
                }
                if (!vivante)
                {
                    System.Windows.Forms.MessageBox.Show("L'équipe attaquante a gagnée");
                    bw.ReportProgress(0, "end");
                }
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            dynamic info = e.UserState;
            switch (info[0] as string)
            {
                case "update":
                    UpdateInfo();
                    break;
                case "end":
                    bw.CancelAsync();
                    Close();
                    break;
                case "log":
                    tb_Log.Text += info[1];
                    break;
                default:
                    break;
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btn_Next.IsEnabled = true;
        }
    }
}
