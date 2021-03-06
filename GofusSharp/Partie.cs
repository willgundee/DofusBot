﻿using System.Linq;
namespace GofusSharp
{
    public class Partie
    {
        public Terrain TerrainPartie { get; internal set; }
        public Liste<Entite> ListAttaquants { get; internal set; }
        public Liste<Entite> ListDefendants { get; internal set; }
        public int Tour { get; internal set; }
        internal System.Random Seed { get; set; }
        internal int valeurSeed { get; set; }
        internal Partie(Terrain TerrainPartie, Liste<Entite> ListAttaquants, Liste<Entite> ListDefendants, int valeurSeed)
        {
            this.ListAttaquants = ListAttaquants;
            this.ListDefendants = ListDefendants;
            this.valeurSeed = valeurSeed;
            this.TerrainPartie = TerrainPartie;
            Tour = 0;
            Seed = new System.Random(valeurSeed);
            GenererTerrain();
            PlacerJoueurs();
            PlacerObstacles();
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
        private void GenererTerrain()
        {
            for (int i = 0; i < TerrainPartie.TabCases.Count(); i++)
            {
                for (int j = 0; j < TerrainPartie.TabCases[0].Count(); j++)
                {
                    TerrainPartie.TabCases[i][j] = new Case(i, j, Case.type.vide);
                }
            }
        }

        private void PlacerObstacles()
        {
            //14% des case sont des obstacles
            Liste<Case> caseObstacle = new Liste<Case>();
            bool coince = false;
            do
            {
                for (int i = 0; i < TerrainPartie.Hauteur * TerrainPartie.Largeur / 7; i++)
                {
                    caseObstacle.Add(new Case(Seed.Next(0, TerrainPartie.Largeur), Seed.Next(0, TerrainPartie.Hauteur), Case.type.obstacle));
                    while (TerrainPartie.TabCases[caseObstacle.Last().X][caseObstacle.Last().Y].Contenu != Case.type.vide)
                    {
                        caseObstacle.Last().X = Seed.Next(0, TerrainPartie.Largeur);
                        caseObstacle.Last().Y = Seed.Next(0, TerrainPartie.Hauteur);
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
                entite.PV_MAX = vie + (vitalite * (entite.ClasseEntite.Nom != "sacrieur" ? 1 : 2));
                entite.PV = entite.PV_MAX;
                entite.PA = entite.PA_MAX;
                entite.PM = entite.PM_MAX;
                //ListEntites.Add(new EntiteInconnu(entite));
            }
        }

        internal void DebuterAction(Entite entite)
        {
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
            entite.ListEntites = new Liste<EntiteInconnu>();
            foreach (Entite entiteCour in ListAttaquants.Concat(ListDefendants))
                entite.ListEntites.Add(new EntiteInconnu(entiteCour));
            foreach (Entite entiteCour in ListAttaquants.Concat(ListDefendants))
            {
                foreach (Envoutement buff in entiteCour.ListEnvoutements)
                {
                    if (buff.IdLanceur == entite.IdEntite)
                    {
                        buff.PasserTour();
                    }
                }
                entiteCour.ListEnvoutements.RemoveAll(x => x.TourRestants <= 0);
            }
        }

        internal void FinirAction(Entite entite)
        {
            entite.PM = entite.PM_MAX;
            entite.PA = entite.PA_MAX;
        }
    }
}
