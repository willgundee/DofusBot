namespace GofusSharp
{
    public class Terrain
    {
        public Case[][] TabCases { get; internal set; }
        public int Largeur { get; internal set; }
        public int Hauteur { get; internal set; }
        public Terrain(Case[][] TabCases)
        {
            this.TabCases = TabCases;
        }
        public Terrain(int Largeur, int Hauteur)
        {
            this.Largeur = Largeur;
            this.Hauteur = Hauteur;

            TabCases = new Case[Largeur][];
            for (int i = 0; i < Largeur; i++)
            {
                TabCases[i] = new Case[Hauteur];
            }
        }
        public int DistanceEntreCases(Case case1, Case case2)
        {
            return Math.Abs(case1.X - case2.X) + Math.Abs(case1.Y - case2.Y);
        }

        public ListeChainee<Case> CaseAvecObstacle()
        {
            ListeChainee<Case> caseAvecObstacle = new ListeChainee<Case>();
            foreach (Case[] tabcase in TabCases)
            {
                foreach (Case caseT in tabcase)
                {
                    if (caseT.Contenu == Case.type.obstacle)
                        caseAvecObstacle.AjouterFin(caseT);
                }
            }
            return caseAvecObstacle;
        }

        private class CaseAStar
        {
            public Case LaCase { get; set; }
            public int Cout { get; set; }
            public int Heuristique { get; set; }
            public CaseAStar(Case LaCase, int Cout, int Heuristique)
            {
                this.LaCase = LaCase;
                this.Cout = Cout;
                this.Heuristique = Heuristique;
            }
            public CaseAStar(Case LaCase)
            {
                this.LaCase = LaCase;
                this.Cout = 0;
                this.Heuristique = 0;
            }
        }

        public ListeChainee<Case> CheminEntreCases(Case Depart, Case Destination)
        {
            CaseAStar depart = new CaseAStar(Depart);
            ListeChainee<CaseAStar> fermee = new ListeChainee<CaseAStar>();
            ListeChainee<CaseAStar> ouverte = new ListeChainee<CaseAStar>();
            ouverte.AjouterFin(depart);

            while (ouverte.First != null)
            {
                CaseAStar meilleur = ouverte.First.Valeur;
                foreach (CaseAStar score in ouverte)
                    if (score.Score_f < meilleur.Score_f)
                        meilleur = score;
                if (meilleur.LaCase == Destination)
                    return ReconstruireChemin(meilleur);
                ouverte.Enlever(meilleur);
                fermee.AjouterFin(meilleur);
                foreach (Case voisin in CaseVoisines(meilleur.LaCase))
                {
                    foreach (CaseAStar caseAStar in fermee)
                        if (caseAStar.LaCase == voisin)
                            continue;
                    int tentative_score_g = meilleur.Score_g + DistanceEntreCases(meilleur.LaCase, voisin);
                    CaseAStar caseVoisineExistante = null;
                    foreach (CaseAStar caseAStar in ouverte)
                        if (caseAStar.LaCase == voisin)
                            caseVoisineExistante = caseAStar;
                    if (caseVoisineExistante == null)
                    {
                        caseVoisineExistante = new CaseAStar(voisin);
                        ouverte.AjouterFin(caseVoisineExistante);
                    }
                    else if (tentative_score_g >= caseVoisineExistante.Score_g)
                        continue;
                    caseVoisineExistante.viensDe = meilleur;
                    caseVoisineExistante.Score_g = tentative_score_g;
                    caseVoisineExistante.Score_f = caseVoisineExistante.Score_g + DistanceEntreCases(caseVoisineExistante.LaCase, Destination);
                }
            }
            return null;
        }
        private ListeChainee<Case> ReconstruireChemin(CaseAStar Courant)
        {
            ListeChainee<Case> chemin = new ListeChainee<Case>();
            while (Courant != null)
            {
                chemin.AjouterDebut(Courant.LaCase);
                Courant = Courant.viensDe;
            }
            return chemin;
        }
        public ListeChainee<Case> CaseVoisines(Case caseCible)
        {
            ListeChainee<Case> caseVoisines = new ListeChainee<Case>();
            if (caseCible.Y - 1 >= 0 && TabCases[caseCible.X][caseCible.Y - 1].Contenu != Case.type.obstacle)
                caseVoisines.AjouterFin(TabCases[caseCible.X][caseCible.Y - 1]);
            if (caseCible.X - 1 >= 0 && TabCases[caseCible.X - 1][caseCible.Y].Contenu != Case.type.obstacle)
                caseVoisines.AjouterFin(TabCases[caseCible.X - 1][caseCible.Y]);
            if (TabCases.Length > caseCible.X + 1 && TabCases[caseCible.X + 1][caseCible.Y].Contenu != Case.type.obstacle)
                caseVoisines.AjouterFin(TabCases[caseCible.X + 1][caseCible.Y]);
            if (TabCases[caseCible.X].Length > caseCible.Y + 1 && TabCases[caseCible.X][caseCible.Y + 1].Contenu != Case.type.obstacle)
                caseVoisines.AjouterFin(TabCases[caseCible.X][caseCible.Y + 1]);
            return caseVoisines;
        }

        private int ComparerDeuxCase(CaseAStar c1, CaseAStar c2)
        {
            if (c1.Heuristique < c2.Heuristique)
                return 1;
            else if (c1.Heuristique == c2.Heuristique)
                return 0;
            else
                return -1;
        }

        //internal Case TrouverProchaineCase(ListeChainee<Case> Chemin, ListeChainee<Case> CulDeSac, Case Destination)
        //{
        //    if (Math.Abs(Chemin.Last.Valeur.X - Destination.X) >= Math.Abs(Chemin.Last.Valeur.Y - Destination.Y))
        //    {
        //        if (TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y].Contenu == Case.type.obstacle || CulDeSac.Contient(TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y]))
        //        {
        //            if (TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)].Contenu == Case.type.obstacle || CulDeSac.Contient(TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)]))
        //            {
        //                return null;
        //            }
        //            return TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)];
        //        }
        //        return TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y];
        //    }
        //    else
        //    {
        //        if (TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)].Contenu == Case.type.obstacle || CulDeSac.Contient(TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)]))
        //        {
        //            if (TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y].Contenu == Case.type.obstacle || CulDeSac.Contient(TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y]))
        //            {
        //                return null;
        //            }
        //            return TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y];
        //        }
        //        return TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)];
        //    }
        //}
    }
}
