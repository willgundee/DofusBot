namespace GofusSharp
{
    class CombatTest
    {
        public Partie PartieTest { get; private set; }
        public CombatTest()
        {
            fakePartie();
            PartieTest.DebuterPartie();
            while (true)
            {
                PartieTest.DebuterAction();
                Action(PartieTest.TerrainPartie, PartieTest.ListAttaquants.First.Valeur, PartieTest.ListEntites);
                PartieTest.SyncroniserJoueur();
                if (PartieTest.ListAttaquants.First.Valeur.PV <= 0 || PartieTest.ListDefendants.First.Valeur.PV <= 0)
                    break;
                PartieTest.DebuterAction();
                Action(PartieTest.TerrainPartie, PartieTest.ListDefendants.First.Valeur, PartieTest.ListEntites);
                PartieTest.SyncroniserJoueur();
                if (PartieTest.ListAttaquants.First.Valeur.PV <= 0 || PartieTest.ListDefendants.First.Valeur.PV <= 0)
                    break;
            }
        }

        public void Action(Terrain terrain, Entite joueur, ListeChainee<EntiteInconnu> ListEntites)
        {
            Noeud<EntiteInconnu> entite = ListEntites.First;
            while (entite.Valeur.Equipe == joueur.Equipe)
                entite = entite.Next;
            if (terrain.DistanceEntreCases(joueur.Position, entite.Valeur.Position) > 1)
            {
                joueur.AvancerVers(entite.Valeur);
            }
            joueur.UtiliserSort(joueur.ClasseEntite.TabSorts[1], entite.Valeur);
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
            Condition[] condItemAtt = new Condition[] { new Condition(Condition.type.exp_min, 5000) };
            Equipement[] tabEquipAtt = new Equipement[] { new Equipement(1, condItemAtt, statItemAtt, "Coiffe bouftou", Equipement.type.chapeau) };
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
            Condition[] condItemDef = new Condition[] { new Condition(Condition.type.exp_min, 5000) };
            Equipement[] tabEquipDef = new Equipement[] { new Equipement(1, condItemDef, statItemDef, "Coiffe bouftou", Equipement.type.chapeau) };
            Case[][] tabCases = new Case[][] { new Case[] { new Case(0, 0, Case.type.joueur), new Case(0, 1, Case.type.vide), new Case(0, 2, Case.type.vide) }, new Case[] { new Case(1, 0, Case.type.vide), new Case(1, 1, 0), new Case(1, 2, Case.type.vide) }, new Case[] { new Case(2, 0, Case.type.vide), new Case(2, 1, Case.type.vide), new Case(2, 2, Case.type.joueur) } };
            Terrain terrain = new Terrain(tabCases);
            ListeChainee<Entite> ListAttaquants = new ListeChainee<Entite>();
            ListAttaquants.AjouterFin(new Personnage(10, classeAtt, "Trebor", 10000, terrain.TabCases[0][0], EntiteInconnu.type.attaquant, listStatistiqueAtt, scriptAtt, tabEquipAtt, terrain));
            ListeChainee<Entite> ListDefendants = new ListeChainee<Entite>();
            ListDefendants.AjouterFin(new Personnage(11, classeDef, "Robert", 9000, terrain.TabCases[2][2], EntiteInconnu.type.defendant, listStatistiqueDef, scriptDef, tabEquipDef, terrain));
            PartieTest = new Partie(1, terrain, ListAttaquants, ListDefendants, 123123);
        }

    }
}
