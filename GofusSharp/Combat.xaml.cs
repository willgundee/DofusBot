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
        public Combat(string script1, string script2)
        {
            InitializeComponent();
            this.Show();
            fakePartie(script1, script2);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Label lbl = new Label();
                    lbl.HorizontalAlignment = HorizontalAlignment.Center;
                    lbl.VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetRow(lbl, i);
                    Grid.SetColumn(lbl, j);
                    grd_Terrain.Children.Add(lbl);
                }
            }
            UpdateInfo();
        }
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
                    Label lbl = new Label();
                    lbl.HorizontalAlignment = HorizontalAlignment.Center;
                    lbl.VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetRow(lbl, i);
                    Grid.SetColumn(lbl, j);
                    grd_Terrain.Children.Add(lbl);
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


        private void fakePartie(string script1, string script2)
        {
            Liste<Statistique> listStatistiqueAtt = new Liste<Statistique>();
            listStatistiqueAtt.Add(new Statistique(Statistique.type.PA, 6));
            listStatistiqueAtt.Add(new Statistique(Statistique.type.PM, 3));
            listStatistiqueAtt.Add(new Statistique(Statistique.type.vie, 100));
            listStatistiqueAtt.Add(new Statistique(Statistique.type.initiative, 101));
            listStatistiqueAtt.Add(new Statistique(Statistique.type.force, 30));
            listStatistiqueAtt.Add(new Statistique(Statistique.type.sagesse, 40));
            listStatistiqueAtt.Add(new Statistique(Statistique.type.intelligence, 20));
            listStatistiqueAtt.Add(new Statistique(Statistique.type.agilite, 10));
            listStatistiqueAtt.Add(new Statistique(Statistique.type.chance, 50));
            Liste<Effet> tabEffetAtt1 = new Liste<Effet> { new Effet(Effet.type.teleportation, 0, 0) };
            Zone zoneEffetAtt1 = new Zone(Zone.type.carre, 0, 0);
            Zone zonePorteeAtt1 = new Zone(Zone.type.cercle, 1, 5);
            Liste<Effet> tabEffetAtt2 = new Liste<Effet> { new Effet(Effet.type.ATT_neutre, 10, 15), new Effet(Effet.type.pousse, 4, 4) };
            Zone zoneEffetAtt2 = new Zone(Zone.type.carre, 0, 0);
            Zone zonePorteeAtt2 = new Zone(Zone.type.croix, 1, 1);
            Liste<Sort> tabSortAtt = new Liste<Sort> { new Sort(tabEffetAtt1, "bond", false, true, true, zonePorteeAtt1, zoneEffetAtt1, 3, 5, Sort.nom_sort.bond), new Sort(tabEffetAtt2, "intimidation", true, false, false, zonePorteeAtt2, zoneEffetAtt2, -2, 2, Sort.nom_sort.intimidation) };
            Classe classeAtt = new Classe(tabSortAtt, "iop");
            Liste<Statistique> statItemAtt = new Liste<Statistique> { new Statistique(Statistique.type.force, 70) };
            Liste<Equipement> tabEquipAtt = new Liste<Equipement> { new Equipement(statItemAtt, "Coiffe bouftou", Equipement.type.chapeau) };
            Liste<Statistique> listStatistiqueDef = new Liste<Statistique>();
            listStatistiqueDef.Add(new Statistique(Statistique.type.PA, 6));
            listStatistiqueDef.Add(new Statistique(Statistique.type.PM, 3));
            listStatistiqueDef.Add(new Statistique(Statistique.type.vie, 100));
            listStatistiqueDef.Add(new Statistique(Statistique.type.initiative, 101));
            listStatistiqueDef.Add(new Statistique(Statistique.type.force, 30));
            listStatistiqueDef.Add(new Statistique(Statistique.type.sagesse, 40));
            listStatistiqueDef.Add(new Statistique(Statistique.type.intelligence, 20));
            listStatistiqueDef.Add(new Statistique(Statistique.type.agilite, 10));
            listStatistiqueDef.Add(new Statistique(Statistique.type.chance, 50));
            Liste<Effet> tabEffetDef1 = new Liste<Effet> { new Effet(Effet.type.teleportation, 0, 0) };
            Zone zoneEffetDef1 = new Zone(Zone.type.carre, 0, 0);
            Zone zonePorteeDef1 = new Zone(Zone.type.cercle, 1, 5);
            Liste<Effet> tabEffetDef2 = new Liste<Effet> { new Effet(Effet.type.ATT_neutre, 10, 15), new Effet(Effet.type.pousse, 4, 4) };
            Zone zoneEffetDef2 = new Zone(Zone.type.carre, 0, 0);
            Zone zonePorteeDef2 = new Zone(Zone.type.croix, 1, 1);
            Liste<Sort> tabSortDef = new Liste<Sort> { new Sort(tabEffetDef1, "bond", false, true, true, zonePorteeDef1, zoneEffetDef1, 3, 5, Sort.nom_sort.bond), new Sort(tabEffetDef2, "intimidation", true, false, false, zonePorteeDef2, zoneEffetDef2, -2, 2, Sort.nom_sort.intimidation) };
            Classe classeDef = new Classe(tabSortDef, "iop");
            Liste<Statistique> statItemDef = new Liste<Statistique> { new Statistique(Statistique.type.force, 70) };
            Liste<Equipement> tabEquipDef = new Liste<Equipement> { new Equipement(statItemDef, "Coiffe bouftou", Equipement.type.chapeau), new Arme(statItemAtt, "Marteau bouftous", Equipement.type.arme, tabEffetAtt2, zonePorteeAtt2, zoneEffetAtt2, Arme.typeArme.marteau, 5) };
            Terrain terrain = new Terrain(10, 10);
            Liste<Entite> ListAttaquants = new Liste<Entite>();
            ListAttaquants.Add(new Personnage(10, classeAtt, "Trebor", 10000, EntiteInconnu.type.attaquant, listStatistiqueAtt, script1, tabEquipAtt, terrain));
            Liste<Entite> ListDefendants = new Liste<Entite>();
            ListDefendants.Add(new Personnage(11, classeDef, "Robert", 9000, EntiteInconnu.type.defendant, listStatistiqueDef, script2, tabEquipDef, terrain));
            CombatCourant = new Partie(ListAttaquants, ListDefendants);
        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            foreach (Entite entite in Liste<Entite>.ConcatAlternate(CombatCourant.ListAttaquants, CombatCourant.ListDefendants))
            {
                if (entite.Etat == EntiteInconnu.typeEtat.mort)
                    continue;
                CombatCourant.DebuterAction(entite);
                if (entite is Personnage)
                    Action(CombatCourant.TerrainPartie, entite as Personnage, CombatCourant.ListEntites/*.AsReadOnly()*/);
                else
                    Action(CombatCourant.TerrainPartie, entite as Entite, CombatCourant.ListEntites/*.AsReadOnly()*/);
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
            foreach (Label lbl in grd_Terrain.Children)
            {
                lbl.Content = CombatCourant.TerrainPartie.TabCases[Grid.GetRow(lbl)][Grid.GetColumn(lbl)].Contenu.ToString().First().ToString();

                switch (CombatCourant.TerrainPartie.TabCases[Grid.GetRow(lbl)][Grid.GetColumn(lbl)].Contenu)
                {
                    case Case.type.vide:
                        lbl.Background = Brushes.White;
                        break;
                    case Case.type.joueur:
                        lbl.Background = Brushes.Chartreuse;
                        break;
                    case Case.type.obstacle:
                        lbl.Background = Brushes.Red;
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
