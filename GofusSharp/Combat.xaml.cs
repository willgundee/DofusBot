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
        internal bool Generation { get; set; }
        internal double Speed { get; set; }
        internal long IdPartie { get; set; }
        internal Log DelLog { get; set; }

        private delegate void DelUpdate();

        

        internal delegate void Log(string text);

        internal Thread TAction;

        private bool AutoScroll = true;

        public Combat(List<Gofus.Entite> lstJoueurAtt, List<Gofus.Entite> lstJoueurDef, int seed, long idPartie, bool premiereGeneration = true)
        {
            InitializeComponent();
            Generation = premiereGeneration;
            DelLog = UpdateLog;
            IdPartie = idPartie;
            if (Generation)
            {
                Gofus.BDService BD = new Gofus.BDService();
                bool? resultat = GenererPartie(lstJoueurAtt, lstJoueurDef, seed);
                BD.Update("UPDATE Parties SET attaquantAGagne = " + (resultat == null?"null":(resultat == true?"true":"false")) + " WHERE idPartie = " + idPartie + ";");
                //TODO: LVL UP !
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
            Random RandRock = new Random(CombatCourant.TerrainPartie.TabCases.First().First().X);
            foreach (StackPanel sPnl in grd_Terrain.Children.Cast<FrameworkElement>().Where(x => x is StackPanel))
            {
                if (CombatCourant.TerrainPartie.TabCases[Grid.GetRow(sPnl)][Grid.GetColumn(sPnl)].Contenu == Case.type.obstacle)
                {
                    Image ImageSprite = new Image();
                    ImageSource SourceImageObstacle = new BitmapImage(new Uri(@"..\..\Resources\GofusSharp\Roche" + RandRock.Next(1,3) + ".png", UriKind.Relative));
                    ImageSprite.Source = SourceImageObstacle;
                    sPnl.Children.Add(ImageSprite);
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

            CombatCourant = new Partie(terrain, ListEntiteAtt, ListEntiteDef, seed);
            for (int i = 0; i < 64; i++)
            {
                foreach (Entite entite in Liste<Entite>.ConcatAlternate(CombatCourant.ListAttaquants, CombatCourant.ListDefendants))
                {
                    if (entite.Etat == EntiteInconnu.typeEtat.mort)
                        continue;
                    CombatCourant.DebuterAction(entite);
                    Liste<EntiteInconnu> ListEntites = new Liste<EntiteInconnu>();
                    foreach (Entite entiteI in CombatCourant.ListAttaquants.Concat(CombatCourant.ListDefendants))
                        ListEntites.Add(new EntiteInconnu(entiteI));
                    if (entite is Personnage)
                        Action(CombatCourant.TerrainPartie, entite as Personnage, ListEntites);
                    else
                        Action(CombatCourant.TerrainPartie, entite as Entite, ListEntites);
                    CombatCourant.FinirAction(entite);
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
                        return false;
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
            TAction = new Thread(new ThreadStart(() => AsyncWork()));
            TAction.Start();
        }

        private void UpdateInfo()
        {
            foreach (StackPanel sPnl in grd_Terrain.Children.Cast<FrameworkElement>().Where(x => x is StackPanel))
            {
                switch (CombatCourant.TerrainPartie.TabCases[Grid.GetRow(sPnl)][Grid.GetColumn(sPnl)].Contenu)
                {
                    case Case.type.vide:
                        sPnl.Children.Clear();
                        break;
                    case Case.type.joueur:
                        if (CombatCourant.ListAttaquants.Concat(CombatCourant.ListDefendants).Where(x => x.Etat == EntiteInconnu.typeEtat.mort).Count() != 0)
                            break;
                        Image ImageSprite = new Image();
                        Entite perso = CombatCourant.ListAttaquants.Concat(CombatCourant.ListDefendants).Where(x => x.Position.X == Grid.GetRow(sPnl) && x.Position.Y == Grid.GetColumn(sPnl)).First();
                        ImageSource SourceImageClasse = new BitmapImage(new Uri(@"..\..\Resources\GofusSharp\" + perso.ClasseEntite.Nom + @".png", UriKind.Relative));
                        ImageSprite.Source = SourceImageClasse;
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

        private void AjouterToolTip(Image img, Entite perso)
        {
            StackPanel infoPerso = new StackPanel();


            TextBlock Nom = new TextBlock();
            Nom.Text = perso.Nom;
            Nom.FontWeight = FontWeights.Bold;

            TextBlock PV = new TextBlock();
            PV.Text = "PV: " + perso.PV + "/" + perso.PV_MAX;

            TextBlock PA = new TextBlock();
            PA.Text = "PA: " + perso.PA + "/" + perso.PA_MAX;

            TextBlock PM = new TextBlock();
            PM.Text = "PM: " + perso.PM + "/" + perso.PM_MAX;

            infoPerso.Children.Add(Nom);
            infoPerso.Children.Add(PV);
            infoPerso.Children.Add(PA);
            infoPerso.Children.Add(PM);


            TextBlock Stat = new TextBlock();
            Stat.Text = "Résistance";
            Stat.FontWeight = FontWeights.Bold;

            foreach (Statistique resis in perso.ListStatistiques)
            {
                TextBlock res = new TextBlock();
                switch (resis.Nom)
                {
                    case Statistique.type.RES_neutre:
                        res.Text = "Neutre: " + resis.Valeur;
                        infoPerso.Children.Add(res);
                        break;
                    case Statistique.type.RES_feu:
                        res.Text = "Feu: " + resis.Valeur;
                        infoPerso.Children.Add(res);
                        break;
                    case Statistique.type.RES_air:
                        res.Text = "Air: " + resis.Valeur;
                        infoPerso.Children.Add(res);
                        break;
                    case Statistique.type.RES_terre:
                        res.Text = "Terre: " + resis.Valeur;
                        infoPerso.Children.Add(res);
                        break;
                    case Statistique.type.RES_eau:
                        res.Text = "Eau: " + resis.Valeur;
                        infoPerso.Children.Add(res);
                        break;
                    case Statistique.type.RES_Pourcent_neutre:
                        res.Text = "%Neutre: " + resis.Valeur;
                        infoPerso.Children.Add(res);
                        break;
                    case Statistique.type.RES_Pourcent_feu:
                        res.Text = "%Feu: " + resis.Valeur;
                        infoPerso.Children.Add(res);
                        break;
                    case Statistique.type.RES_Pourcent_air:
                        res.Text = "%Air: " + resis.Valeur;
                        infoPerso.Children.Add(res);
                        break;
                    case Statistique.type.RES_Pourcent_terre:
                        res.Text = "%Terre: " + resis.Valeur;
                        infoPerso.Children.Add(res);
                        break;
                    case Statistique.type.RES_Pourcent_eau:
                        res.Text = "%Eau: " + resis.Valeur;
                        infoPerso.Children.Add(res);
                        break;
                }
            }


            TextBlock Envout = new TextBlock();
            Envout.Text = "Envoutement";
            Envout.FontWeight = FontWeights.Bold;

            foreach (Envoutement e in perso.ListEnvoutements)
            {
                TextBlock Env = new TextBlock();
                Env.Text = e.Stat.ToString() + ": " + e.Valeur + " pour " + e.TourRestants + " tour" + (e.TourRestants == 1?"":"s");
                infoPerso.Children.Add(Env);
            }

            img.ToolTip = infoPerso;
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

        private void txtNum_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb_num = sender as TextBox;
            try
            {
                Speed = Convert.ToDouble(tb_num.Text);
                Speed = Math.Round(Speed, 1);
                if (Speed < 0.1)
                    Speed = 0.1;
                if (Speed > 20)
                    Speed = 20;
                tb_num.Text = Speed.ToString();
            }
            catch (Exception)
            {
                tb_num.Text = Speed.ToString();
            }
            
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            Speed += 0.5;
            if (Speed < 0.1)
                Speed = 0.1;
            if (Speed > 20)
                Speed = 20;
            txtNum.Text = Speed.ToString();
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            Speed -= 0.5;
            if (Speed < 0.1)
                Speed = 0.1;
            if (Speed > 20)
                Speed = 20;
            txtNum.Text = Speed.ToString();
        }

        private void AsyncWork()
        {
            //les stats des liste entite sont vide a partir d'ici
            foreach (Entite entite in Liste<Entite>.ConcatAlternate(CombatCourant.ListAttaquants, CombatCourant.ListDefendants))
            {
                if (entite.Etat == EntiteInconnu.typeEtat.mort)
                    continue;
                CombatCourant.DebuterAction(entite);
                Liste<EntiteInconnu> ListEntites = new Liste<EntiteInconnu>();
                foreach (Entite entiteI in CombatCourant.ListAttaquants.Concat(CombatCourant.ListDefendants))
                    ListEntites.Add(new EntiteInconnu(entiteI));
                if (entite is Personnage)
                    Action(CombatCourant.TerrainPartie, entite as Personnage, ListEntites);
                else
                    Action(CombatCourant.TerrainPartie, entite as Entite, ListEntites);
                CombatCourant.FinirAction(entite);
                DelUpdate du = UpdateInfo;
                Dispatcher.Invoke(du);
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
                    /*TODO ouvrir la fenetre resultat avec le param IdPartie
                    
                    Gofus.Résultat resultat = new Gofus.Résultat();
                    resultat.Show();*/

                    
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
                    /*
                     Gofus.Résultat resultat = new Gofus.Résultat();
                     double i = Convert.ToInt32(CombatCourant.Seed);
                     resultat.Show(i);*/




                }
            }
        }

        internal void UpdateLog(string text)
        {
            tb_Log.Text += text;
        }

        private void chb_AutoPlay_Checked(object sender, RoutedEventArgs e)
        {
            btn_Next.IsEnabled = false;
        }

        private void chb_AutoPlay_Unchecked(object sender, RoutedEventArgs e)
        {
            if (btn_StartStop.Content.ToString() == "Pause")
                btn_Next.IsEnabled = true;
        }

        private void btn_StartStop_Click(object sender, RoutedEventArgs e)
        {
            Button StartStop = sender as Button;
            switch (StartStop.Content.ToString())
            {
                case "Pause":
                    StartStop.Content = "Jouer";
                    btn_Next.IsEnabled = false;
                    break;
                case "Jouer":
                    StartStop.Content = "Pause";
                    if (chb_AutoPlay.IsChecked == false)
                        btn_Next.IsEnabled = true;
                    break;
            }
        }
    }
}
