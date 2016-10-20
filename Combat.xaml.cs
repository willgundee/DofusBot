using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GofusSharp
{
    /// <summary>
    /// Logique d'interaction pour Combat.xaml
    /// </summary>
    public partial class Combat : Window
    {
        private Partie PartieTest { get; set; }
        public Combat()
        {
            InitializeComponent();
            this.Show();
            fakePartie();
            PartieTest.DebuterPartie();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Label lbl = new Label();
                    Grid.SetRow(lbl, i);
                    Grid.SetColumn(lbl, j);
                    Binding b = new Binding("Case");
                    b.Source = PartieTest.TerrainPartie.TabCases[i][j];
                    b.Mode = BindingMode.OneWay;
                    b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    lbl.Content = new Binding(PartieTest.TerrainPartie.TabCases[i][j].Contenu.ToString());
                    //lbl.SetBinding(ContentProperty, b);
                    grd_Terrain.Children.Add(lbl);
                    DataContext = this;
                }
            }
            for (int i = 0; i < 64; i++)
            {
                foreach (Entite entite in Liste<Entite>.ConcatAlternate(PartieTest.ListAttaquants, PartieTest.ListDefendants))
                {
                    
                    if (entite.Etat == EntiteInconnu.typeEtat.mort)
                        continue;
                    PartieTest.DebuterAction(entite);
                    PartieTest.SyncroniserJoueur();
                    Action(PartieTest.TerrainPartie, entite as Personnage, PartieTest.ListEntites.AsReadOnly());
                    PartieTest.SyncroniserJoueur();

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
                        System.Windows.Forms.MessageBox.Show("L'équipe attaquante a gagnée");
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
                        System.Windows.Forms.MessageBox.Show("L'équipe defendante a gagnée");
                        Close();
                    }
                }
            }
            System.Windows.Forms.MessageBox.Show("Partie nulle");
            Close();
        }

        public void Action(Terrain terrain, Personnage joueur, System.Collections.ObjectModel.ReadOnlyCollection<EntiteInconnu> ListEntites)
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
            joueur.Attaquer(ennemi);
        }

        public void Action(Terrain terrain, Entite joueur, System.Collections.Generic.IEnumerator<EntiteInconnu> ListEntites)
        {
            while (ListEntites.Current.Equipe == joueur.Equipe)
                ListEntites.MoveNext();
            if (terrain.DistanceEntreCases(joueur.Position, ListEntites.Current.Position) > 1)
            {
                joueur.AvancerVers(ListEntites.Current);
            }
            joueur.UtiliserSort(joueur.ClasseEntite.TabSorts[1], ListEntites.Current);
        }


        private void fakePartie()
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
            Script scriptAtt = new Script(1, "//PlaceHolder");
            Effet[] tabEffetAtt1 = new Effet[] { new Effet(Effet.type.teleportation, 0, 0) };
            Zone zoneEffetAtt1 = new Zone(Zone.type.carre, 0, 0);
            Zone zonePorteeAtt1 = new Zone(Zone.type.cercle, 1, 5);
            Effet[] tabEffetAtt2 = new Effet[] { new Effet(Effet.type.pousse, 4, 4), new Effet(Effet.type.ATT_neutre, 10, 15) };
            Zone zoneEffetAtt2 = new Zone(Zone.type.carre, 0, 0);
            Zone zonePorteeAtt2 = new Zone(Zone.type.croix, 1, 1);
            Sort[] tabSortAtt = new Sort[] { new Sort(1, tabEffetAtt1, "bond", false, true, true, zonePorteeAtt1, zoneEffetAtt1, 3, 5), new Sort(2, tabEffetAtt2, "intimidation", true, false, false, zonePorteeAtt2, zoneEffetAtt2, -2, 2) };
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
            Script scriptDef = new Script(2, "//PlaceHolder");
            Effet[] tabEffetDef1 = new Effet[] { new Effet(Effet.type.teleportation, 0, 0) };
            Zone zoneEffetDef1 = new Zone(Zone.type.carre, 0, 0);
            Zone zonePorteeDef1 = new Zone(Zone.type.cercle, 1, 5);
            Effet[] tabEffetDef2 = new Effet[] { new Effet(Effet.type.pousse, 4, 4), new Effet(Effet.type.ATT_neutre, 10, 15) };
            Zone zoneEffetDef2 = new Zone(Zone.type.carre, 0, 0);
            Zone zonePorteeDef2 = new Zone(Zone.type.croix, 1, 1);
            Sort[] tabSortDef = new Sort[] { new Sort(1, tabEffetDef1, "bond", false, true, true, zonePorteeDef1, zoneEffetDef1, 3, 5), new Sort(2, tabEffetDef2, "intimidation", true, false, false, zonePorteeDef2, zoneEffetDef2, -2, 2) };
            Classe classeDef = new Classe(1, tabSortDef, Classe.type.iop);
            Statistique[] statItemDef = new Statistique[] { new Statistique(Statistique.type.force, 70) };
            Equipement[] tabEquipDef = new Equipement[] { new Equipement(1, statItemDef, "Coiffe bouftou", Equipement.type.chapeau), new Arme(2, statItemAtt, "Marteau bouftous", Equipement.type.arme, tabEffetAtt2, zonePorteeAtt2, zoneEffetAtt2, Arme.typeArme.marteau) };
            Terrain terrain = new Terrain(10, 10);
            Liste<Entite> ListAttaquants = new Liste<Entite>();
            ListAttaquants.Add(new Personnage(10, classeAtt, "Trebor", 10000, EntiteInconnu.type.attaquant, listStatistiqueAtt, scriptAtt, tabEquipAtt, terrain));
            Liste<Entite> ListDefendants = new Liste<Entite>();
            ListDefendants.Add(new Personnage(11, classeDef, "Robert", 9000, EntiteInconnu.type.defendant, listStatistiqueDef, scriptDef, tabEquipDef, terrain));
            PartieTest = new Partie(1, ListAttaquants, ListDefendants);
        }
    }
}
