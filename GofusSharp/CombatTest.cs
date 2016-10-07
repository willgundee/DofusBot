//csc /target:library /out:GofusSharp.dll *.cs
namespace GofusSharp
{
    public class CombatTest
    {
        public Partie PartieTest { get; private set; }
        public CombatTest()
        {

            //while (true)
            //{
            //    PartieTest.DebuterAction();
            //    Action(PartieTest.TerrainPartie, PartieTest.ListAttaquants.First.Valeur as Personnage, PartieTest.ListEntites);
            //    PartieTest.SyncroniserJoueur();
            //    if (PartieTest.ListAttaquants.First.Valeur.PV <= 0 || PartieTest.ListDefendants.First.Valeur.PV <= 0)
            //        break;
            //    PartieTest.DebuterAction();
            //    Action(PartieTest.TerrainPartie, PartieTest.ListDefendants.First.Valeur as Personnage, PartieTest.ListEntites);
            //    PartieTest.SyncroniserJoueur();
            //    if (PartieTest.ListAttaquants.First.Valeur.PV <= 0 || PartieTest.ListDefendants.First.Valeur.PV <= 0)
            //        break;
            //}
            //if (PartieTest.ListAttaquants.First.Valeur.PV <= 0)
            //    System.Windows.Forms.MessageBox.Show("Robert est le gagnant avec " + PartieTest.ListDefendants.First.Valeur.PV.ToString() + " PV restant!!!");
            //else if (PartieTest.ListDefendants.First.Valeur.PV <= 0)
            //    System.Windows.Forms.MessageBox.Show("Trebor est le gagnant avec " + PartieTest.ListAttaquants.First.Valeur.PV.ToString() + " PV restant!!!");
        }

        public string combat(int nbTour)
        {
            fakePartie();
            for (int i = 0; i < nbTour; i++)
            {
                Noeud<Entite> EntAtt = PartieTest.ListAttaquants.First;
                Noeud<Entite> EntDef = PartieTest.ListDefendants.First;
                while (EntAtt != PartieTest.ListAttaquants.Last.Next && EntDef != PartieTest.ListDefendants.Last.Next)
                {
                    if (EntAtt != PartieTest.ListAttaquants.Last.Next)
                    {
                        if (EntAtt.Valeur.Etat == EntiteInconnu.typeEtat.mort)
                        {
                            EntAtt = EntAtt.Next;
                            break;
                        }
                        PartieTest.DebuterAction(EntAtt.Valeur);
                        PartieTest.SyncroniserJoueur();

                        System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(() => { Action(PartieTest.TerrainPartie, EntAtt.Valeur as Personnage, PartieTest.ListEntites); });
                        t.Start();
                        int c = 0;
                        while (!t.IsCompleted && c < 1000000000)
                        {
                            c++;
                        }
                        System.Windows.Forms.MessageBox.Show(c.ToString());

                        PartieTest.SyncroniserJoueur();
                        EntAtt = EntAtt.Next;
                    }
                    if (EntDef != PartieTest.ListDefendants.Last.Next)
                    {
                        if (EntDef.Valeur.Etat == EntiteInconnu.typeEtat.mort)
                        {
                            EntDef = EntDef.Next;
                            break;
                        }
                        PartieTest.DebuterAction(EntDef.Valeur);
                        PartieTest.SyncroniserJoueur();
                        Action(PartieTest.TerrainPartie, EntDef.Valeur as Personnage, PartieTest.ListEntites);
                        PartieTest.SyncroniserJoueur();
                        EntDef = EntDef.Next;
                    }
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
                        return "L'équipe attaquante a gagnée";
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
                        return "L'équipe defendante a gagnée";
                }
            }
            return "Partie nulle";
        }
        public void Action(Terrain terrain, Personnage joueur, ListeChainee<EntiteInconnu> ListEntites)
        {
            while (true)
            {

            }
            Noeud<EntiteInconnu> entite = ListEntites.First;
            while (entite.Valeur.Equipe == joueur.Equipe)
                entite = entite.Next;
            if (terrain.DistanceEntreCases(joueur.Position, entite.Valeur.Position) > 1)
            {
                int result = 1;
                while (result != 0 && result != -1)
                {
                    result = joueur.AvancerVers(terrain.CheminEntreCases(joueur.Position, entite.Valeur.Position).First.Next.Valeur, 1);
                }
            }
            joueur.Attaquer(entite.Valeur);
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
            Terrain terrain = new Terrain(10, 5);
            ListeChainee<Entite> ListAttaquants = new ListeChainee<Entite>();
            ListAttaquants.AjouterFin(new Personnage(10, classeAtt, "Trebor", 10000, EntiteInconnu.type.attaquant, listStatistiqueAtt, scriptAtt, tabEquipAtt, terrain));
            ListeChainee<Entite> ListDefendants = new ListeChainee<Entite>();
            ListDefendants.AjouterFin(new Personnage(11, classeDef, "Robert", 9000, EntiteInconnu.type.defendant, listStatistiqueDef, scriptDef, tabEquipDef, terrain));
            PartieTest = new Partie(1, ListAttaquants, ListDefendants);
        }

    }
}
