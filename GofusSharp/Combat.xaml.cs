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


[assembly: InternalsVisibleTo("Gofus")]
namespace GofusSharp
{
    /// <summary>
    /// Logique d'interaction pour Combat.xaml
    /// </summary>
    internal partial class Combat : Window
    {
        public static int lel = 1;
        private Partie PartieTest { get; set; }

        private Boolean AutoScroll = true;
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

        private void Action(Terrain terrain, Personnage joueur, System.Collections.ObjectModel.ReadOnlyCollection<EntiteInconnu> ListEntites)
        {
            //code dynamique 
            string code = @"
                using GofusSharp;
                namespace Arene
                {
                    public class Action
                    {
                        public static void Execution(Terrain terrain, Personnage Perso, System.Collections.ObjectModel.ReadOnlyCollection<EntiteInconnu> ListEntites)
                        {
                            user_code
                        }
                    }
                }
            ";

            //je remplace le mot user_code pour ce qui ce trouve dans la text box
            string finalCode = code.Replace("user_code", joueur.ScriptEntite.Texte);
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

        private void Action(Terrain terrain, Entite joueur, System.Collections.ObjectModel.ReadOnlyCollection<EntiteInconnu> ListEntites)
        {
            EntiteInconnu ennemi = null;
            foreach (EntiteInconnu entite in ListEntites)
            {
                if (entite.Equipe != joueur.Equipe)
                {
                    ennemi = entite;
                    break;
                }
            }
            if (terrain.DistanceEntreCases(joueur.Position, ennemi.Position) > 1)
            {
                int result = 1;
                while (result != 0 && result != -1)
                {
                    result = joueur.AvancerVers(terrain.CheminEntreCases(joueur.Position, ennemi.Position).First(), 1);
                }
            }
            joueur.UtiliserSort(joueur.ClasseEntite.TabSorts[1], ennemi);

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
            Script scriptAtt = new Script(1, script1);
            Effet[] tabEffetAtt1 = new Effet[] { new Effet(Effet.type.teleportation, 0, 0) };
            Zone zoneEffetAtt1 = new Zone(Zone.type.carre, 0, 0);
            Zone zonePorteeAtt1 = new Zone(Zone.type.cercle, 1, 5);
            Effet[] tabEffetAtt2 = new Effet[] { new Effet(Effet.type.pousse, 4, 4), new Effet(Effet.type.ATT_neutre, 10, 15) };
            Zone zoneEffetAtt2 = new Zone(Zone.type.carre, 0, 0);
            Zone zonePorteeAtt2 = new Zone(Zone.type.croix, 1, 1);
            Sort[] tabSortAtt = new Sort[] { new Sort(1, tabEffetAtt1, "bond", false, true, true, zonePorteeAtt1, zoneEffetAtt1, 3, 5, Sort.nom_sort.bond), new Sort(2, tabEffetAtt2, "intimidation", true, false, false, zonePorteeAtt2, zoneEffetAtt2, -2, 2, Sort.nom_sort.intimidation) };
            Classe classeAtt = new Classe(1, tabSortAtt, Classe.type.iop);
            Statistique[] statItemAtt = new Statistique[] { new Statistique(Statistique.type.force, 70) };
            Equipement[] tabEquipAtt = new Equipement[] { new Equipement(1, statItemAtt, "Coiffe bouftou", Equipement.type.chapeau) };
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
            Script scriptDef = new Script(2, script2);
            Effet[] tabEffetDef1 = new Effet[] { new Effet(Effet.type.teleportation, 0, 0) };
            Zone zoneEffetDef1 = new Zone(Zone.type.carre, 0, 0);
            Zone zonePorteeDef1 = new Zone(Zone.type.cercle, 1, 5);
            Effet[] tabEffetDef2 = new Effet[] { new Effet(Effet.type.pousse, 4, 4), new Effet(Effet.type.ATT_neutre, 10, 15) };
            Zone zoneEffetDef2 = new Zone(Zone.type.carre, 0, 0);
            Zone zonePorteeDef2 = new Zone(Zone.type.croix, 1, 1);
            Sort[] tabSortDef = new Sort[] { new Sort(1, tabEffetDef1, "bond", false, true, true, zonePorteeDef1, zoneEffetDef1, 3, 5, Sort.nom_sort.bond), new Sort(2, tabEffetDef2, "intimidation", true, false, false, zonePorteeDef2, zoneEffetDef2, -2, 2, Sort.nom_sort.intimidation) };
            Classe classeDef = new Classe(1, tabSortDef, Classe.type.iop);
            Statistique[] statItemDef = new Statistique[] { new Statistique(Statistique.type.force, 70) };
            Equipement[] tabEquipDef = new Equipement[] { new Equipement(1, statItemDef, "Coiffe bouftou", Equipement.type.chapeau), new Arme(2, statItemAtt, "Marteau bouftous", Equipement.type.arme, tabEffetAtt2, zonePorteeAtt2, zoneEffetAtt2, Arme.typeArme.marteau, 5) };
            Terrain terrain = new Terrain(10, 10);
            Liste<Entite> ListAttaquants = new Liste<Entite>();
            ListAttaquants.Add(new Personnage(10, classeAtt, "Trebor", 10000, EntiteInconnu.type.attaquant, listStatistiqueAtt, scriptAtt, tabEquipAtt, terrain));
            Liste<Entite> ListDefendants = new Liste<Entite>();
            ListDefendants.Add(new Personnage(11, classeDef, "Robert", 9000, EntiteInconnu.type.defendant, listStatistiqueDef, scriptDef, tabEquipDef, terrain));
            PartieTest = new Partie(1, ListAttaquants, ListDefendants);
        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            foreach (Entite entite in Liste<Entite>.ConcatAlternate(PartieTest.ListAttaquants, PartieTest.ListDefendants))
            {
                if (entite.Etat == EntiteInconnu.typeEtat.mort)
                    continue;
                PartieTest.DebuterAction(entite);
                Action(PartieTest.TerrainPartie, entite as Personnage, PartieTest.ListEntites.AsReadOnly());
                PartieTest.SyncroniserJoueur();
                UpdateInfo();
                bool vivante = false;
                foreach (Entite entiteAtt in PartieTest.ListAttaquants)
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
                foreach (Entite entiteDef in PartieTest.ListDefendants)
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
                lbl.Content = PartieTest.TerrainPartie.TabCases[Grid.GetRow(lbl)][Grid.GetColumn(lbl)].Contenu.ToString().First().ToString();

                switch (PartieTest.TerrainPartie.TabCases[Grid.GetRow(lbl)][Grid.GetColumn(lbl)].Contenu)
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
            info.Append("\nPoint de vie: " + PartieTest.ListAttaquants.First().PV);
            if (PartieTest.ListAttaquants.First().Etat == EntiteInconnu.typeEtat.vivant)
                info.Append("\nPosition: X: " + PartieTest.ListAttaquants.First().Position.X + " Y: " + PartieTest.ListAttaquants.First().Position.Y);
            else
                info.Append("\nPosition: X: 0 Y: 0");
            info.Append("\nEtat: " + PartieTest.ListAttaquants.First().Etat);
            tb_perso0.Text = info.ToString();
            info.Clear();
            info.Append("Deffendant");
            info.Append("\nPoint de vie: " + PartieTest.ListDefendants.First().PV);
            if (PartieTest.ListDefendants.First().Etat == EntiteInconnu.typeEtat.vivant)
                info.Append("\nPosition: X: " + PartieTest.ListDefendants.First().Position.X + " Y: " + PartieTest.ListDefendants.First().Position.Y);
            else
                info.Append("\nPosition: X: 0 Y: 0");
            info.Append("\nEtat: " + PartieTest.ListDefendants.First().Etat);
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
