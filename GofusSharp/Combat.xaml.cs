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

[assembly: InternalsVisibleTo("Gofus")]
namespace GofusSharp
{
    /// <summary>
    /// Logique d'interaction pour Combat.xaml
    /// </summary>
    internal partial class Combat : Window
    {
        internal Partie CombatCourant { get; set; }

        private bool AutoScroll = true;

        public Combat(List<Gofus.Entite> lstJoueurAtt, List<Gofus.Entite> lstJoueurDef, int seed)
        {
            InitializeComponent();
            this.Show();
            CreerPartie(lstJoueurAtt, lstJoueurDef, seed);
            //System.Windows.Forms.MessageBox.Show(JsonConvert.SerializeObject(CombatCourant));
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

        //private void Action(Terrain terrain, Entite joueur, System.Collections.ObjectModel.ReadOnlyCollection<EntiteInconnu> ListEntites)
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
                UpdateInfo();
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
                    Close();
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
                    Close();
                }
            }
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

    }
}
