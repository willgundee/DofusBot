namespace GofusSharp
{
    class CombatTest
    {
        public Partie PartieTest { get; private set; }
        public CombatTest()
        {
            fakePartie();
            foreach (Case[] tabcase in PartieTest.TerrainPartie.TabCases) {
                foreach (Case cAse in tabcase) {
                    System.Windows.Forms.MessageBox.Show("X:"+cAse.X.ToString()+"Y:"+ cAse.Y.ToString() + "valeur:" + cAse.Contenu.ToString());
                }
            }
            PartieTest.DebuterPartie();
            PartieTest.DebuterTour();
            Action(PartieTest.TerrainPartie, PartieTest.TabAttaquants[0], new EntiteInconnu(PartieTest.TabDefendants[0]));
            foreach (Case[] tabcase in PartieTest.TerrainPartie.TabCases) {
                foreach (Case cAse in tabcase) {
                    System.Windows.Forms.MessageBox.Show("X:" + cAse.X.ToString() + "Y:" + cAse.Y.ToString() + "valeur:" + cAse.Contenu.ToString());
                }
            }
        }

        public void Action(Terrain terrain, Entite joueur, EntiteInconnu ennemie)
        {
            joueur.AvancerVers(ennemie);
        }

        private void fakePartie()
        {
            Statistique[] tabStatistiqueAtt = new Statistique[] { new Statistique(Statistique.type.PM, 3), new Statistique(Statistique.type.PA, 6), new Statistique(Statistique.type.vie, 100), new Statistique(Statistique.type.initiative, 101), new Statistique(Statistique.type.force, 30), new Statistique(Statistique.type.sagesse, 40), new Statistique(Statistique.type.intelligence, 20), new Statistique(Statistique.type.agilite, 10), new Statistique(Statistique.type.chance, 50) };
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
            Statistique[] tabStatistiqueDef = new Statistique[] { new Statistique(Statistique.type.PM, 3), new Statistique(Statistique.type.PA, 6), new Statistique(Statistique.type.vie, 100), new Statistique(Statistique.type.initiative, 101), new Statistique(Statistique.type.force, 30), new Statistique(Statistique.type.sagesse, 40), new Statistique(Statistique.type.intelligence, 20), new Statistique(Statistique.type.agilite, 10), new Statistique(Statistique.type.chance, 50) };
            Script scriptDef = new Script(2, "//PlaceHolder");
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
            Case[][] tabCases = new Case[][] { new Case[] { new Case(0, 0, Case.type.joueur), new Case(0, 1, Case.type.vide), new Case(0, 2, Case.type.vide) }, new Case[] { new Case(1, 0, Case.type.vide), new Case(1, 1, 0), new Case(1, 2, Case.type.vide) }, new Case[] { new Case(2, 0, Case.type.vide), new Case(2, 1, Case.type.vide), new Case(2, 2, Case.type.joueur) } };
            Terrain terrain = new Terrain(tabCases);
            PartieTest = new Partie(1, terrain, new Personnage[] { new Personnage(10, classeAtt, "Trebor", 10000, terrain.TabCases[0][0], tabStatistiqueAtt, scriptAtt, tabEquipAtt, terrain) }, new Personnage[] { new Personnage(10, classeDef, "Robert", 9000, terrain.TabCases[2][2], tabStatistiqueDef, scriptDef, tabEquipDef, terrain) }, 123123);
        }

    }
}
