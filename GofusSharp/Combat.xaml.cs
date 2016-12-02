﻿using Microsoft.CSharp;
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
using System.Windows.Documents;
using System.Windows.Controls.Primitives;

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
        internal DelUpdate DelUpd { get; set; }
        public bool CombatTerminer { get; set; }

        internal ManualResetEvent mrse = new ManualResetEvent(false);

        internal delegate void DelUpdate();

        private delegate void DelThreadEnd();

        private delegate void DelAfficheRes(long idPartie);

        private Gofus.BDService bd = new Gofus.BDService();

        internal delegate void Log(string text);

        internal Thread TAction;

        private bool AutoScroll = true;

        public Combat(List<Gofus.Entite> lstJoueurAtt, List<Gofus.Entite> lstJoueurDef, int seed, long idPartie, bool premiereGeneration = true)
        {
            InitializeComponent();
            CombatTerminer = false;
            Debug.FCombat = this;
            Generation = premiereGeneration;
            DelLog = UpdateLog;
            DelUpd = UpdateInfo;
            mrse.Set();
            IdPartie = idPartie;
            if (Generation)
            {
                GenererPartie(lstJoueurAtt, lstJoueurDef, seed);
            }
            Speed = 3;
            txtNum.Text = Speed.ToString();
            Show();
            CreerPartie(lstJoueurAtt, lstJoueurDef, seed);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Canvas cnvs = new Canvas();
                    StackPanel sPnl = new StackPanel();
                    sPnl.HorizontalAlignment = HorizontalAlignment.Center;
                    sPnl.VerticalAlignment = VerticalAlignment.Center;
                    Border border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(1);
                    Grid.SetRow(cnvs, i);
                    Grid.SetColumn(cnvs, j);
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    grd_Terrain.Children.Add(border);
                    grd_Terrain.Children.Add(cnvs);
                    cnvs.Children.Add(sPnl);
                }
            }
            Random RandRock = new Random(CombatCourant.TerrainPartie.TabCases.First().First().X);
            foreach (Canvas cnvs in grd_Terrain.Children.Cast<FrameworkElement>().Where(x => x is Canvas))
            {
                StackPanel sPnl = cnvs.Children.Cast<StackPanel>().First();
                if (CombatCourant.TerrainPartie.TabCases[Grid.GetRow(cnvs)][Grid.GetColumn(cnvs)].Contenu == Case.type.obstacle)
                {
                    Image ImageSprite = new Image();
                    ImageSource SourceImageObstacle = new BitmapImage(new Uri("pack://application:,,,/Resources/GofusSharp/Roche" + RandRock.Next(1, 3) + ".png"));
                    ImageSprite.Source = SourceImageObstacle;
                    sPnl.Children.Add(ImageSprite);
                    ImageSprite.Height = grd_Terrain.RowDefinitions.First().ActualHeight;
                    ImageSprite.Width = grd_Terrain.ColumnDefinitions.First().ActualWidth;
                }
            }
            spl_Info.Children.Add(CreerInfoPartie());
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

        private void GenererPartie(List<Gofus.Entite> lstJoueurAtt, List<Gofus.Entite> lstJoueurDef, int seed)
        {
            System.Windows.Input.Mouse.SetCursor(System.Windows.Input.Cursors.Wait);
            Gofus.BDService BD = new Gofus.BDService();
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
            while (true)
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
                        BD.Update("UPDATE Parties SET attaquantAGagne = false WHERE idPartie = " + IdPartie + ";");
                        // lvlsUp[0] = Si le gagnant a lvlup , lvlsUp[0] = Si le perdant a lvlup
                        attributionGain(IdPartie);
                        Generation = false;
                        return;
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
                        BD.Update("UPDATE Parties SET attaquantAGagne = true WHERE idPartie = " + IdPartie + ";");
                        // lvlsUp[0] = Si le gagnant a lvlup , lvlsUp[0] = Si le perdant a lvlup
                        attributionGain(IdPartie);
                        Generation = false;
                        return;
                    }
                }
                CombatCourant.Tour++;
                if (CombatCourant.Tour >= 64)
                {
                    BD.Update("UPDATE Parties SET attaquantAGagne = null WHERE idPartie = " + IdPartie + ";");
                    // lvlsUp[0] = Si le gagnant a lvlup , lvlsUp[0] = Si le perdant a lvlup
                    attributionGain(IdPartie);
                    Generation = false;
                    return;
                }
            }
        }


        private void attributionGain(long IdPartie)
        {
            Gofus.BDService BD = new Gofus.BDService();
            double exp = 0;
            double gain = 0;
            bool infoPartie = true;
            string infoParti = BD.selection("SELECT attaquantAGagne FROM Parties WHERE idPartie = " + IdPartie)[0][0];

            bool perdantLvlUp = false;
            bool gagnantLvlUp = false;
            #region NULL
            if (infoParti == "")//si la parti est null 
            {
                foreach (Entite item in CombatCourant.ListAttaquants)
                    if (item is Personnage)
                    {
                        infoPartie = true;
                        double expG;
                        double gainG;
                        List<string>[] bd = BD.selection("SELECT valeur FROM StatistiquesEntites WHERE idEntite=" + item.IdEntite + " AND idTypeStatistique=(SELECT idTypeStatistique FROM TypesStatistiques WHERE NOM='" + Gofus.Statistique.element.experience.ToString() + "')");
                        List<string>[] cash = BD.selection("SELECT argent FROM Joueurs WHERE idJoueur=( SELECT idJoueur FROM Entites WHERE idEntite = " + item.IdEntite + ")");
                        expG = Convert.ToDouble(bd[0][0]) + 1;
                        gainG = 1 + Convert.ToDouble(cash[0][0]);

                        //Check si le gagnant a lvl up
                        LvlUp(Convert.ToDouble(bd[0][0]), expG, item.IdEntite);

                        BD.Update("UPDATE StatistiquesEntites SET valeur = " + expG + " WHERE idEntite = " + item.IdEntite + " AND idTypeStatistique=(SELECT idTypeStatistique FROM TypesStatistiques WHERE NOM='" + Gofus.Statistique.element.experience.ToString() + "')");
                        BD.Update("UPDATE Joueurs SET argent = " + gainG + " WHERE idJoueur=( SELECT idJoueur FROM Entites WHERE idEntite = " + item.IdEntite + ")");
                    }

                foreach (Entite item in CombatCourant.ListDefendants)
                    if (item is Personnage)
                    {
                        double expP;
                        double gainP;
                        List<string>[] bd = BD.selection("SELECT valeur FROM StatistiquesEntites WHERE idEntite=" + item.IdEntite + " AND idTypeStatistique=(SELECT idTypeStatistique FROM TypesStatistiques WHERE NOM='" + Gofus.Statistique.element.experience.ToString() + "')");
                        List<string>[] cash = BD.selection("SELECT argent FROM Joueurs WHERE idJoueur=( SELECT idJoueur FROM Entites WHERE idEntite = " + item.IdEntite + ")");
                        expP = Convert.ToDouble(bd[0][0]) + 1;
                        gainP = 1 + Convert.ToDouble(cash[0][0]);

                        //Check si le perdant a lvl up
                        LvlUp(Convert.ToDouble(bd[0][0]), expP, item.IdEntite);

                        BD.Update("UPDATE StatistiquesEntites SET valeur = " + Math.Floor(Convert.ToDouble(expP)) + " WHERE idEntite = " + item.IdEntite + " AND idTypeStatistique=(SELECT idTypeStatistique FROM TypesStatistiques WHERE NOM='" + Gofus.Statistique.element.experience.ToString() + "')");
                        BD.Update("UPDATE Joueurs SET argent = " + Math.Floor(Convert.ToDouble(gainP)) + " WHERE idJoueur=( SELECT idJoueur FROM Entites WHERE idEntite = " + item.IdEntite + ")");
                    }

            }
            #endregion
            else
            {
                infoPartie = Convert.ToBoolean(infoParti);
                //si l'attaquant a gagné
                if (infoPartie)
                {
                    gain = CombatCourant.ListDefendants[0].RetourneNiveau() * 100;
                    exp = CombatCourant.ListAttaquants[0].RetourneNiveau() * CombatCourant.ListDefendants[0].RetourneNiveau() * 32;
                    foreach (Entite item in CombatCourant.ListAttaquants)
                        if (item is Personnage)
                        {
                            double expG;
                            double gainG;
                            List<string>[] bd = BD.selection("SELECT valeur FROM StatistiquesEntites WHERE idEntite=" + item.IdEntite + " AND idTypeStatistique=(SELECT idTypeStatistique FROM TypesStatistiques WHERE NOM='" + Gofus.Statistique.element.experience.ToString() + "')");
                            List<string>[] cash = BD.selection("SELECT argent FROM Joueurs WHERE idJoueur=( SELECT idJoueur FROM Entites WHERE idEntite = " + item.IdEntite + ")");
                            expG = Convert.ToDouble(bd[0][0]) + exp;
                            gainG = gain + Convert.ToDouble(cash[0][0]);

                            //Check si le gagnant a lvl up
                            LvlUp(Convert.ToDouble(bd[0][0]), expG, item.IdEntite);

                            BD.Update("UPDATE StatistiquesEntites SET valeur = " + Math.Floor(Convert.ToDouble(expG)) + " WHERE idEntite = " + item.IdEntite + " AND idTypeStatistique=(SELECT idTypeStatistique FROM TypesStatistiques WHERE NOM='" + Gofus.Statistique.element.experience.ToString() + "')");
                            BD.Update("UPDATE Joueurs SET argent = " + Math.Floor(Convert.ToDouble(gainG)) + " WHERE idJoueur=( SELECT idJoueur FROM Entites WHERE idEntite = " + item.IdEntite + ")");
                        }

                    foreach (Entite item in CombatCourant.ListDefendants)
                        if (item is Personnage)
                        {
                            double expP;
                            double gainP;
                            List<string>[] bd = BD.selection("SELECT valeur FROM StatistiquesEntites WHERE idEntite=" + item.IdEntite + " AND idTypeStatistique=(SELECT idTypeStatistique FROM TypesStatistiques WHERE NOM='" + Gofus.Statistique.element.experience.ToString() + "')");
                            List<string>[] cash = BD.selection("SELECT argent FROM Joueurs WHERE idJoueur=( SELECT idJoueur FROM Entites WHERE idEntite = " + item.IdEntite + ")");
                            expP = Convert.ToDouble(bd[0][0]) + (exp / 10);
                            gainP = (gain / 10) + Convert.ToDouble(cash[0][0]);

                            //Check si le perdant a lvl up
                            LvlUp(Convert.ToDouble(bd[0][0]), expP, item.IdEntite);

                            BD.Update("UPDATE StatistiquesEntites SET valeur = " + Math.Floor(Convert.ToDouble(expP)) + " WHERE idEntite = " + item.IdEntite + " AND idTypeStatistique=(SELECT idTypeStatistique FROM TypesStatistiques WHERE NOM='" + Gofus.Statistique.element.experience.ToString() + "')");
                            BD.Update("UPDATE Joueurs SET argent = " + Math.Floor(Convert.ToDouble(gainP)) + " WHERE idJoueur=( SELECT idJoueur FROM Entites WHERE idEntite = " + item.IdEntite + ")");
                        }
                }//fin du if(attaquantAGangé)
                else // si le deffendant a gangé
                {
                    gain = CombatCourant.ListAttaquants[0].RetourneNiveau() * 100;
                    exp = CombatCourant.ListAttaquants[0].RetourneNiveau() * CombatCourant.ListDefendants[0].RetourneNiveau() * 32;
                    foreach (Entite item in CombatCourant.ListDefendants)
                        if (item is Personnage)
                        {
                            double expG;
                            double gainG;
                            List<string>[] bd = BD.selection("SELECT valeur FROM StatistiquesEntites WHERE idEntite=" + item.IdEntite + " AND idTypeStatistique=(SELECT idTypeStatistique FROM TypesStatistiques WHERE NOM='" + Gofus.Statistique.element.experience.ToString() + "')");
                            List<string>[] cash = BD.selection("SELECT argent FROM Joueurs WHERE idJoueur=( SELECT idJoueur FROM Entites WHERE idEntite = " + item.IdEntite + ")");

                            expG = Convert.ToDouble(bd[0][0]) + exp;
                            gainG = gain + Convert.ToDouble(cash[0][0]);

                            //Check si le gagnant a lvl up
                            LvlUp(Convert.ToDouble(bd[0][0]), expG, item.IdEntite);

                            BD.Update("UPDATE StatistiquesEntites SET valeur = " + Math.Floor(Convert.ToDouble(expG)) + " WHERE idEntite = " + item.IdEntite + " AND idTypeStatistique=(SELECT idTypeStatistique FROM TypesStatistiques WHERE NOM='" + Gofus.Statistique.element.experience.ToString() + "')");
                            BD.Update("UPDATE Joueurs SET argent = " + Math.Floor(Convert.ToDouble(gainG)) + " WHERE idJoueur=( SELECT idJoueur FROM Entites WHERE idEntite = " + item.IdEntite + ")");
                        }

                    foreach (Entite item in CombatCourant.ListAttaquants)
                        if (item is Personnage)
                        {
                            double expP;
                            double gainP;
                            List<string>[] bd = BD.selection("SELECT valeur FROM StatistiquesEntites WHERE idEntite=" + item.IdEntite + " AND idTypeStatistique=(SELECT idTypeStatistique FROM TypesStatistiques WHERE NOM='" + Gofus.Statistique.element.experience.ToString() + "')");
                            List<string>[] cash = BD.selection("SELECT argent FROM Joueurs WHERE idJoueur=( SELECT idJoueur FROM Entites WHERE idEntite = " + item.IdEntite + ")");
                            expP = Convert.ToDouble(bd[0][0]) + (exp / 10);
                            gainP = (gain / 10) + Convert.ToDouble(cash[0][0]);

                            //Check si le perdant a lvl up
                            LvlUp(Convert.ToDouble(bd[0][0]), expP, item.IdEntite);


                            BD.Update("UPDATE StatistiquesEntites SET valeur = " + Math.Floor(Convert.ToDouble(expP)) + " WHERE idEntite = " + item.IdEntite + " AND idTypeStatistique=(SELECT idTypeStatistique FROM TypesStatistiques WHERE NOM='" + Gofus.Statistique.element.experience.ToString() + "')");
                            BD.Update("UPDATE Joueurs SET argent = " + Math.Floor(Convert.ToDouble(gainP)) + " WHERE idJoueur=( SELECT idJoueur FROM Entites WHERE idEntite = " + item.IdEntite + ")");
                        }
                }
            }
        }
        // -----------------------------------
        private void LvlUp(double av, double apr, int idEntite)
        {
            int avLevel = Gofus.Statistique.toLevel(av);
            int apLevel = Gofus.Statistique.toLevel(apr);
            if (avLevel - apLevel != 0)
            {
                string capitalLibre = bd.selection("SELECT capitalLibre FROM Entites WHERE idEntite = " + idEntite)[0][0];
                string valeur = bd.selection("SELECT valeur FROM StatistiquesEntites WHERE idEntite = " + idEntite + " AND idTypeStatistique = (SELECT idTypeStatistique FROM TypesStatistiques WHERE NOM = '" + Gofus.Statistique.element.vie.ToString() + "')")[0][0];
                bd.Update("UPDATE Entites SET capitalLibre = " + capitalLibre + " + " + (5 * (apLevel - avLevel)) + " WHERE idEntite = " + idEntite);
                bd.Update("UPDATE StatistiquesEntites SET valeur = " + valeur + " + " + (5 * (apLevel - avLevel)) + " WHERE idEntite = " + idEntite + " AND idTypeStatistique=(SELECT idTypeStatistique FROM TypesStatistiques WHERE NOM='" + Gofus.Statistique.element.vie.ToString() + "')");
            }
        }

        // -----------------------------------
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
                return;
            }
            //mettre la fonction compilé dans une variable

            MethodInfo mi = results.CompiledAssembly.GetType("Arene.Action").GetMethod("Execution");

            mi.Invoke(null, new object[] { terrain, joueur, ListEntites });
        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            if (!CombatTerminer)
            {
                btn_Next.IsEnabled = false;
                TAction = new Thread(new ThreadStart(() => AsyncWork()));
                TAction.Start();
            }
        }

        private void UpdateInfo()
        {
            foreach (Canvas cnvs in grd_Terrain.Children.Cast<FrameworkElement>().Where(x => x is Canvas))
            {
                StackPanel sPnl = cnvs.Children.Cast<StackPanel>().First();
                switch (CombatCourant.TerrainPartie.TabCases[Grid.GetRow(cnvs)][Grid.GetColumn(cnvs)].Contenu)
                {
                    case Case.type.vide:
                        sPnl.Children.Clear();
                        break;
                    case Case.type.joueur:
                        sPnl.Children.Clear();
                        if (CombatCourant.ListAttaquants.Concat(CombatCourant.ListDefendants).Where(x => x.Etat == EntiteInconnu.typeEtat.mort).Count() != 0)
                            break;
                        Image ImageSprite = new Image();
                        Entite perso = CombatCourant.ListAttaquants.Concat(CombatCourant.ListDefendants).Where(x => x.Position.X == Grid.GetRow(cnvs) && x.Position.Y == Grid.GetColumn(cnvs)).First();
                        ImageSource SourceImageClasse;
                        try
                        {
                            SourceImageClasse = new BitmapImage(new Uri("pack://application:,,,/Resources/" + perso.ClasseEntite.Nom + ".png"));
                        }
                        catch (Exception)
                        {
                            try
                            {
                                SourceImageClasse = new BitmapImage(new Uri("pack://application:,,,/Resources/" + perso.ClasseEntite.Nom + ".jpg"));
                            }
                            catch (Exception)
                            {
                                SourceImageClasse = new BitmapImage(new Uri("pack://application:,,,/Resources/monstre.png"));
                            }
                        }
                        ImageSprite.Source = SourceImageClasse;
                        ImageSprite.ToolTip = CreerToolTip(perso);
                        ToolTipService.SetShowDuration(ImageSprite, int.MaxValue);
                        sPnl.Children.Add(ImageSprite);
                        ImageSprite.Height = grd_Terrain.RowDefinitions.First().ActualHeight;
                        ImageSprite.Width = grd_Terrain.ColumnDefinitions.First().ActualWidth;
                        ImageSprite.HorizontalAlignment = HorizontalAlignment.Left;
                        ImageSprite.MouseDown += ImageSprite_MouseDown;
                        TextBlock TBName = new TextBlock();
                        TBName.Text = perso.Nom;
                        TBName.Foreground = Brushes.Red;
                        sPnl.Children.Add(TBName);
                        break;
                }
            }
            if (spl_Info.Children.Cast<StackPanel>().First().Children.Cast<TextBlock>().FirstOrDefault(x => x.Tag != null) != null)
            {
                int idPerso = (int)(spl_Info.Children.Cast<StackPanel>().First().Children.Cast<TextBlock>().First(x => x.Tag != null).Tag);
                Entite perso = CombatCourant.ListAttaquants.Concat(CombatCourant.ListDefendants).Where(x => x.IdEntite == idPerso).First();
                spl_Info.Children.Clear();
                spl_Info.Children.Add(CreerInfo(perso));
            }
            else
            {
                spl_Info.Children.Clear();
                spl_Info.Children.Add(CreerInfoPartie());
            }
        }

        private void ImageSprite_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                Image img = sender as Image;
                StackPanel sPnl = img.Parent as StackPanel;
                Canvas cnvs = sPnl.Parent as Canvas;
                Entite perso = CombatCourant.ListAttaquants.Concat(CombatCourant.ListDefendants).Where(x => x.Position.X == Grid.GetRow(cnvs) && x.Position.Y == Grid.GetColumn(cnvs)).First();
                StackPanel info = CreerInfo(perso);
                spl_Info.Children.Clear();
                spl_Info.Children.Add(info);
            }
            catch (Exception)
            {

            }
        }

        private StackPanel CreerInfoPartie()
        {
            StackPanel info = new StackPanel();

            TextBlock tb_tour = new TextBlock();
            tb_tour.Text = "Tour : " + CombatCourant.Tour;
            info.Children.Add(tb_tour);
            TextBlock tb_id = new TextBlock();
            tb_id.Text = "Id partie : " + IdPartie;
            info.Children.Add(tb_id);
            TextBlock tb_seed = new TextBlock();
            tb_seed.Text = "Seed : " + CombatCourant.valeurSeed;
            info.Children.Add(tb_seed);
            TextBlock tb_Att = new TextBlock(new Bold(new Run("Nom Attaquant : ")));
            info.Children.Add(tb_Att);
            foreach (Entite ent_att in CombatCourant.ListAttaquants)
            {
                TextBlock tb_ent = new TextBlock();
                tb_ent.Text = ent_att.Nom;
                info.Children.Add(tb_ent);
            }
            TextBlock tb_Def = new TextBlock(new Bold(new Run("Nom Defendant : ")));
            info.Children.Add(tb_Def);
            foreach (Entite ent_def in CombatCourant.ListDefendants)
            {
                TextBlock tb_ent = new TextBlock();
                tb_ent.Text = ent_def.Nom;
                info.Children.Add(tb_ent);
            }

            return info;
        }

        private StackPanel CreerInfo(Entite perso)
        {
            StackPanel info = CreerToolTip(perso);

            TextBlock Etat = new TextBlock();
            Etat.Text = "Etat: " + perso.Etat;
            info.Children.Insert(1, Etat);

            TextBlock Equipe = new TextBlock();
            Equipe.Text = "Equipe: " + perso.Equipe;
            info.Children.Insert(1, Equipe);

            TextBlock Position = new TextBlock();
            try
            {
                Position.Text = "Position: X: " + perso.Position.X + " Y: " + perso.Position.Y;
            }
            catch (Exception)
            {
                Position.Text = "Position: Inexistant";
            }
            info.Children.Insert(1, Position);

            TextBlock Niveau = new TextBlock();
            Niveau.Text = "Niveau: " + perso.RetourneNiveau();
            info.Children.Insert(1, Niveau);

            TextBlock idPerso = new TextBlock();
            idPerso.Text = "ID: " + perso.IdEntite;
            idPerso.Tag = perso.IdEntite;
            info.Children.Insert(1, idPerso);

            return info;
        }

        private StackPanel CreerToolTip(Entite perso)
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
            infoPerso.Children.Add(Stat);

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

            if (perso.ListEnvoutements.Count() != 0)
            {

                TextBlock Envout = new TextBlock();
                Envout.Text = "Envoutement";
                Envout.FontWeight = FontWeights.Bold;
                infoPerso.Children.Add(Envout);

                foreach (Envoutement e in perso.ListEnvoutements)
                {
                    TextBlock Env = new TextBlock();
                    Env.Text = e.Stat.ToString() + ": " + e.Valeur + " pour " + e.TourRestants + " tour" + (e.TourRestants == 1 ? "" : "s");
                    infoPerso.Children.Add(Env);
                }
            }
            return infoPerso;
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
                Speed = Convert.ToDouble(tb_num.Text.Replace('.', ','));
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
                Dispatcher.Invoke(DelUpd);
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
                    CombatTerminer = true;
                    DelAfficheRes FenRes = OuvrirResultat;
                    Dispatcher.Invoke(FenRes, new object[] { IdPartie });
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
                    CombatTerminer = true;
                    DelAfficheRes FenRes = OuvrirResultat;
                    Dispatcher.Invoke(FenRes, new object[] { IdPartie });
                }
            }
            CombatCourant.Tour++;
            Dispatcher.Invoke(DelUpd);
            if (CombatCourant.Tour >= 64)
            {
                CombatTerminer = true;
                DelAfficheRes FenRes = OuvrirResultat;
                Dispatcher.Invoke(FenRes, new object[] { IdPartie });
            }
            DelThreadEnd ThEnd = ThreadEnd;
            Dispatcher.Invoke(ThEnd);
        }

        internal void UpdateLog(string text)
        {
            tb_Log.Text += text;
        }
        internal void ThreadEnd()
        {
            if (chb_AutoPlay.IsChecked == false)
                btn_Next.IsEnabled = true;
            else if (!CombatTerminer)
            {
                Thread.Sleep((int)(1000 / Debug.FCombat.Speed));
                TAction = new Thread(new ThreadStart(() => AsyncWork()));
                TAction.Start();
            }
        }

        internal void OuvrirResultat(long idPartie)
        {
            Gofus.Resultat resultat = new Gofus.Resultat(IdPartie);
            resultat.ShowDialog();
        }

        private void chb_AutoPlay_Checked(object sender, RoutedEventArgs e)
        {
            btn_Next.IsEnabled = false;
            if ((TAction == null || !TAction.IsAlive) && !CombatTerminer)
            {
                TAction = new Thread(new ThreadStart(() => AsyncWork()));
                TAction.Start();
            }
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
                    btn_Next.IsEnabled = false;
                    mrse.Reset();
                    StartStop.Content = "Jouer";
                    break;
                case "Jouer":
                    if (chb_AutoPlay.IsChecked == false)
                        btn_Next.IsEnabled = true;
                    StartStop.Content = "Pause";
                    mrse.Set();
                    break;
            }
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!(e.OriginalSource is Image))
            {
                spl_Info.Children.Clear();
                spl_Info.Children.Add(CreerInfoPartie());
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Generation = true;
            Speed = 1000;
            chb_AutoPlay.IsChecked = false;
            mrse.Set();
        }
    }
}
