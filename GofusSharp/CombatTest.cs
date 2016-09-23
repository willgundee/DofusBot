namespace GofusSharp
{
    class CombatTest
    {
        public Partie PartieTest { get; private set; }
        public CombatTest()
        {
            Action(PartieTest.TerrainPartie, PartieTest.TabAttaquants[0], new EntiteInconnu(PartieTest.TabDefendants[0]))
        }

        public void Action(Terrain terrain, Entite joueur, EntiteInconnu ennemie)
        {

        }

        private void fakePartie()
        {
            ListeChainee listStatistiqueAtt = new ListeChainee();
            listStatistiqueAtt.AjouterFin(new Statistique(Statistique.type.PM, 3));
            listStatistiqueAtt.AjouterFin(new Statistique(Statistique.type.PA, 6));
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
            Effet[] tabEffetAtt2 = new Effet[] { new Effet(Effet.type.pousse, 4, 4), new Effet(Effet.type.ATT, 10, 15) };
            Zone zoneEffetAtt2 = new Zone(Zone.type.carre, 0, 0);
            Zone zonePorteeAtt2 = new Zone(Zone.type.ligne, 1, 1);
            Sort[] tabSortAtt = new Sort[] { new Sort(1, tabEffetAtt1, "bond", false, true, true, zonePorteeAtt1, zoneEffetAtt1, 3), new Sort(2, tabEffetAtt2, "intimidation", true, false, false, zonePorteeAtt2, zoneEffetAtt2, -2) };
            Classe classeAtt = new Classe(1, tabSortAtt, Classe.type.iop);
            Statistique[] statItemAtt = new Statistique[] { new Statistique(Statistique.type.force, 70) };
            Condition[] condItemAtt = new Condition[] { new Condition(Condition.type.exp_min, 5000) };
            Equipement[] tabEquipAtt = new Equipement[] { new Equipement(1, condItemAtt, statItemAtt, "Coiffe bouftou", Equipement.type.chapeau) };
            ListeChainee listStatistiqueDef = new ListeChainee();
            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.PM, 3));
            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.PA, 6));
            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.vie, 100));
            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.initiative, 101));
            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.force, 30));
            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.sagesse, 40));
            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.intelligence, 20));
            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.agilite, 10));
            listStatistiqueDef.AjouterFin(new Statistique(Statistique.type.chance, 50));
            Script scriptDef = new Script(1, "//PlaceHolder");
            Effet[] tabEffetDef1 = new Effet[] { new Effet(Effet.type.teleportation, 0, 0) };
            Zone zoneEffetDef1 = new Zone(Zone.type.carre, 0, 0);
            Zone zonePorteeDef1 = new Zone(Zone.type.cercle, 1, 5);
            Effet[] tabEffetDef2 = new Effet[] { new Effet(Effet.type.pousse, 4, 4), new Effet(Effet.type.ATT, 10, 15) };
            Zone zoneEffetDef2 = new Zone(Zone.type.carre, 0, 0);
            Zone zonePorteeDef2 = new Zone(Zone.type.ligne, 1, 1);
            Sort[] tabSortDef = new Sort[] { new Sort(1, tabEffetDef1, "bond", false, true, true, zonePorteeDef1, zoneEffetDef1, 3), new Sort(2, tabEffetDef2, "intimidation", true, false, false, zonePorteeDef2, zoneEffetDef2, -2) };
            Classe classeDef = new Classe(1, tabSortDef, Classe.type.iop);
            Statistique[] statItemDef = new Statistique[] { new Statistique(Statistique.type.force, 70) };
            Condition[] condItemDef = new Condition[] { new Condition(Condition.type.exp_min, 5000) };
            Equipement[] tabEquipDef = new Equipement[] { new Equipement(1, condItemDef, statItemDef, "Coiffe bouftou", Equipement.type.chapeau) };
            Case[][] tabCases = new Case[][] { new Case[] { new Case(0, 0, 10), new Case(0, 1, 0), new Case(0, 2, 0) }, new Case[] { new Case(1, 0, 0), new Case(1, 1, 0), new Case(1, 2, 0) }, new Case[] { new Case(2, 0, 0), new Case(2, 1, 0), new Case(2, 2, 11) } };
            PartieTest = new Partie(1, new Terrain(tabCases), new Personnage[] { new Personnage(10, listStatistiqueAtt, scriptAtt, classeAtt, "Trebor", 10000, tabEquipAtt) }, new Personnage[] { new Personnage(11, listStatistiqueDef, scriptDef, classeDef, "Robert", 9000, tabEquipDef) }, 123123);
        }

    }
}
