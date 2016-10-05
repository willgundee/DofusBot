namespace GofusSharp
{
    public class Partie
    {
        public int IdPartie { get; internal set; }
        public Terrain TerrainPartie { get; internal set; }
        public ListeChainee<Entite> ListAttaquants { get; internal set; }
        public ListeChainee<Entite> ListDefendants { get; internal set; }
        public ListeChainee<EntiteInconnu> ListEntites { get; internal set; }
        public System.Random Seed { get; internal set; }
        public int valeurSeed { get; internal set; }
        public Partie(int IdPartie, Terrain TerrainPartie, ListeChainee<Entite> ListAttaquants, ListeChainee<Entite> ListDefendants, int valeurSeed)
        {
            this.IdPartie = IdPartie;
            this.TerrainPartie = TerrainPartie;
            this.ListAttaquants = ListAttaquants;
            this.ListDefendants = ListDefendants;
            this.valeurSeed = valeurSeed;
            Seed = new System.Random(valeurSeed);
            ListAttaquants.Last.Next = ListDefendants.First;
            ListDefendants.First.Previous = ListAttaquants.Last;
            GenererTerrain(10, 5, 0);
            ListEntites = new ListeChainee<EntiteInconnu>();
            DebuterPartie();
        }
        public Partie(int IdPartie, ListeChainee<Entite> ListAttaquants, ListeChainee<Entite> ListDefendants)
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
            ListAttaquants.Last.Next = ListDefendants.First;
            ListDefendants.First.Previous = ListAttaquants.Last;
            GenererTerrain(10, 5, 0);
            PlacerJoueurs();
            PlacerObstacles();
            ListEntites = new ListeChainee<EntiteInconnu>();
            DebuterPartie();
        }

        private void GenererTerrain(int Largeur, int Hauteur, int NbObstacle)
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
                    caseObstacle.AjouterFin(new Case(Seed.Next(0,TerrainPartie.Largeur),Seed.Next(0, TerrainPartie.Hauteur),Case.type.obstacle));
                    while (TerrainPartie.TabCases[caseObstacle.Last.Valeur.X][caseObstacle.Last.Valeur.Y].Contenu != Case.type.vide)
                    {
                        caseObstacle.Last.Valeur.X = Seed.Next(0, TerrainPartie.Largeur);
                        caseObstacle.Last.Valeur.Y = Seed.Next(0, TerrainPartie.Hauteur);
                    }
                }
                foreach (Case obstacle in caseObstacle)
                    TerrainPartie.TabCases[obstacle.X][obstacle.Y].Contenu = Case.type.obstacle;
                Noeud<Entite> entite = ListAttaquants.First;
                while (entite != null)
                {
                    if(TerrainPartie.CheminEntreCases(ListAttaquants.First.Valeur.Position, entite.Valeur.Position) == null)
                    {
                        coince = true;
                        foreach (Case obstacle in caseObstacle)
                            TerrainPartie.TabCases[obstacle.X][obstacle.Y].Contenu = Case.type.vide;
                        break;
                    }
                }
            } while (!coince);
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

        internal void DebuterPartie()
        {
            Noeud<Entite> entite = ListAttaquants.First;
            while (entite != null)
            {
                int vie = new int();
                int vitalite = new int();
                foreach (Statistique stat in entite.Valeur.ListStatistiques)
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
                            entite.Valeur.PA_MAX = stat.Valeur;
                            break;
                        case Statistique.type.PM:
                            entite.Valeur.PM_MAX = stat.Valeur;
                            break;
                    }
                }
                entite.Valeur.PV_MAX = vie + (vitalite * (entite.Valeur.ClasseEntite.Nom != Classe.type.sacrieur ? 1 : 2));
                entite.Valeur.PV = entite.Valeur.PV_MAX;
                ListEntites.AjouterFin(new EntiteInconnu(entite.Valeur));
                entite = entite.Next;
            }
        }

        public void DebuterAction()
        {
            Noeud<Entite> entite = ListAttaquants.First;
            while (entite != null)
            {
                entite.Valeur.PM = entite.Valeur.PM_MAX;
                entite.Valeur.PA = entite.Valeur.PA_MAX;
                entite.Valeur.ListEntites = ListEntites;
                entite = entite.Next;
            }
        }

        public void SyncroniserJoueur()
        {
            Noeud<Entite> entite = ListAttaquants.First;
            foreach (EntiteInconnu entiteInconnu in ListEntites)
            {
                bool existe = false;
                while (entite != null)
                {
                    if (entite.Valeur.IdEntite == entiteInconnu.IdEntite)
                    {
                        entite.Valeur.Position = entiteInconnu.Position;
                        entite.Valeur.PV = entiteInconnu.PV;
                        entite.Valeur.PV_MAX = entiteInconnu.PV_MAX;
                        entite.Valeur.ListEnvoutements = entiteInconnu.ListEnvoutements;
                        existe = true;
                        break;
                    }
                    entite = entite.Next;
                }
                if (!existe)
                {
                    Entite newInvoc = new Entite(entiteInconnu , new Script(3, "//Placeholder"), TerrainPartie, entite.Valeur.Proprietaire);
                    if (newInvoc.Equipe == EntiteInconnu.type.attaquant)
                    {
                        foreach (Entite entiteProp in ListAttaquants)
                        {
                            if (entiteProp.IdEntite == newInvoc.Proprietaire)
                            {
                                ListAttaquants.AjouterApres(newInvoc, ListAttaquants.TrouverPosition(entiteProp));
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (Entite entiteProp in ListDefendants)
                        {
                            if (entiteProp.IdEntite == newInvoc.Proprietaire)
                            {
                                ListDefendants.AjouterApres(newInvoc, ListDefendants.TrouverPosition(entiteProp));
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
