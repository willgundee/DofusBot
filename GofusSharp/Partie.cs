using System.Linq;
namespace GofusSharp
{
    public class Partie
    {
        public int IdPartie { get; internal set; }
        public Terrain TerrainPartie { get; internal set; }
        public Liste<Entite> ListAttaquants { get; internal set; }
        public Liste<Entite> ListDefendants { get; internal set; }
        public Liste<EntiteInconnu> ListEntites { get; internal set; }
        public System.Random Seed { get; internal set; }
        public int valeurSeed { get; internal set; }
        internal Partie(int IdPartie, Terrain TerrainPartie, Liste<Entite> ListAttaquants, Liste<Entite> ListDefendants, int valeurSeed)
        {
            this.IdPartie = IdPartie;
            this.TerrainPartie = TerrainPartie;
            this.ListAttaquants = ListAttaquants;
            this.ListDefendants = ListDefendants;
            this.valeurSeed = valeurSeed;
            Seed = new System.Random(valeurSeed);
            GenererTerrain(10, 10);
            ListEntites = new Liste<EntiteInconnu>();
            DebuterPartie();
        }
        internal Partie(int IdPartie, Liste<Entite> ListAttaquants, Liste<Entite> ListDefendants)
        {
            this.IdPartie = IdPartie;
            this.ListAttaquants = ListAttaquants;
            this.ListDefendants = ListDefendants;
            int total = 0;
            foreach (Entite entite in ListAttaquants)
                total += entite.IdEntite;
            foreach (Entite entite in ListDefendants)
                total += entite.IdEntite;
            valeurSeed = System.DateTime.Now.Millisecond - total;
            Seed = new System.Random(valeurSeed);
            GenererTerrain(10, 10);
            PlacerJoueurs();
            PlacerObstacles();
            ListEntites = new Liste<EntiteInconnu>();
            DebuterPartie();
        }

        private void GenererTerrain(int Largeur, int Hauteur)
        {
            TerrainPartie = new Terrain(Largeur, Hauteur);
            for (int i = 0; i < Largeur; i++)
            {
                for (int j = 0; j < Hauteur; j++)
                {
                    TerrainPartie.TabCases[i][j] = new Case(i, j, Case.type.vide);
                }
            }
        }

        private void PlacerObstacles()
        {
            //14% des case sont des obstacles
            ListeChainee<Case> caseObstacle = new ListeChainee<Case>();
            bool coince = false;
            do
            {
                for (int i = 0; i < TerrainPartie.Hauteur * TerrainPartie.Largeur / 7; i++)
                {
                    caseObstacle.AjouterFin(new Case(Seed.Next(0, TerrainPartie.Largeur), Seed.Next(0, TerrainPartie.Hauteur), Case.type.obstacle));
                    while (TerrainPartie.TabCases[caseObstacle.Last.Valeur.X][caseObstacle.Last.Valeur.Y].Contenu != Case.type.vide)
                    {
                        caseObstacle.Last.Valeur.X = Seed.Next(0, TerrainPartie.Largeur);
                        caseObstacle.Last.Valeur.Y = Seed.Next(0, TerrainPartie.Hauteur);
                    }
                }
                foreach (Case obstacle in caseObstacle)
                    TerrainPartie.TabCases[obstacle.X][obstacle.Y].Contenu = Case.type.obstacle;
                foreach (Entite entite in ListAttaquants.Concat(ListDefendants))
                {
                    if (TerrainPartie.CheminEntreCases(ListAttaquants.First().Position, entite.Position) == null)
                    {
                        coince = true;
                        foreach (Case obstacle in caseObstacle)
                            TerrainPartie.TabCases[obstacle.X][obstacle.Y].Contenu = Case.type.vide;
                        break;
                    }
                }
            } while (coince);
        }

        private void PlacerJoueurs()
        {
            int posL;
            int posH;
            //attaquant a gauche/defendant a droite
            foreach (Entite entite in ListAttaquants)
            {
                do
                {
                    posL = Seed.Next(1, TerrainPartie.Largeur / 2) - 1;
                    posH = Seed.Next(1, TerrainPartie.Hauteur) - 1;
                } while (TerrainPartie.TabCases[posL][posH].Contenu != Case.type.vide);
                TerrainPartie.TabCases[posL][posH].Contenu = Case.type.joueur;
                entite.Position = TerrainPartie.TabCases[posL][posH];
            }
            foreach (Entite entite in ListDefendants)
            {
                do
                {
                    posL = Seed.Next(TerrainPartie.Largeur / 2, TerrainPartie.Largeur) - 1;
                    posH = Seed.Next(1, TerrainPartie.Hauteur) - 1;
                } while (TerrainPartie.TabCases[posL][posH].Contenu != Case.type.vide);
                TerrainPartie.TabCases[posL][posH].Contenu = Case.type.joueur;
                entite.Position = TerrainPartie.TabCases[posL][posH];
            }
        }

        private void DebuterPartie()
        {
            foreach (Entite entite in ListAttaquants.Concat(ListDefendants))
            {
                entite.TerrainEntite = TerrainPartie;
                int vie = new int();
                int vitalite = new int();
                foreach (Statistique stat in entite.ListStatistiques)
                {
                    switch (stat.Nom)
                    {
                        case Statistique.type.vie:
                            vie = stat.Valeur;
                            break;
                        case Statistique.type.vitalite:
                            vitalite = stat.Valeur;
                            break;
                        case Statistique.type.PA:
                            entite.PA_MAX = stat.Valeur;
                            break;
                        case Statistique.type.PM:
                            entite.PM_MAX = stat.Valeur;
                            break;
                    }
                }
                entite.PV_MAX = vie + (vitalite * (entite.ClasseEntite.Nom != Classe.type.sacrieur ? 1 : 2));
                entite.PV = entite.PV_MAX;
                ListEntites.Add(new EntiteInconnu(entite));
            }
        }

        public void DebuterAction(Entite entite)
        {
            entite.PM = entite.PM_MAX;
            entite.PA = entite.PA_MAX;
            foreach (Envoutement buff in entite.ListEnvoutements)
            {
                switch (buff.Stat)
                {
                    case Statistique.type.PA:
                        entite.PA += buff.Valeur;
                        break;
                    case Statistique.type.PM:
                        entite.PM += buff.Valeur;
                        break;
                }
            }
            entite.ListEntites = ListEntites;
            foreach (EntiteInconnu entiteInconnu in ListEntites)
            {
                foreach (Envoutement buff in entiteInconnu.ListEnvoutements)
                {
                    if (buff.IdLanceur == entite.IdEntite)
                    {
                        if (buff.PasserTour())
                        {
                            entiteInconnu.ListEnvoutements.Remove(buff);
                        }
                    }
                }
            }
        }

        public void SyncroniserJoueur()
        {
            foreach (EntiteInconnu entiteInconnu in ListEntites)
            {
                bool existe = false;
                foreach (Entite entite in ListAttaquants.Concat(ListDefendants))
                {
                    if (entite.IdEntite == entiteInconnu.IdEntite)
                    {
                        entite.TerrainEntite = TerrainPartie;
                        entite.Position = entiteInconnu.Position;
                        entite.PV = entiteInconnu.PV;
                        entite.PV_MAX = entiteInconnu.PV_MAX;
                        entite.ListEnvoutements = entiteInconnu.ListEnvoutements;
                        entite.Etat = entiteInconnu.Etat;
                        existe = true;
                        break;
                    }
                }
                if (!existe)
                {
                    //Entite newInvoc = new Entite(entiteInconnu, new Script(3, "//Placeholder"), TerrainPartie, entite.Proprietaire);
                    //if (newInvoc.Equipe == EntiteInconnu.type.attaquant)
                    //{
                    //    foreach (Entite entiteProp in ListAttaquants)
                    //    {
                    //        if (entiteProp.IdEntite == newInvoc.Proprietaire)
                    //        {
                    //            ListAttaquants.Insert(ListAttaquants.FindIndex(entiteProp), newInvoc);
                    //            break;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    foreach (Entite entiteProp in ListDefendants)
                    //    {
                    //        if (entiteProp.IdEntite == newInvoc.Proprietaire)
                    //        {
                    //            ListDefendants.Add(newInvoc, ListDefendants.TrouverPosition(entiteProp));
                    //            break;
                    //        }
                    //    }
                    //}
                }
            }
        }
    }
}
