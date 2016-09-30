namespace GofusSharp
{
    public class Terrain
    {
        public Case[][] TabCases { get; internal set; }
        public Terrain(Case[][] TabCases)
        {
            this.TabCases = TabCases;
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
            public int Score_g { get; set; }
            public int Score_f { get; set; }
            public CaseAStar(Case LaCase, int Score_g, int Score_f)
            {
                this.LaCase = LaCase;
                this.Score_g = Score_g;
                this.Score_f = Score_f;
            }
        }

        public ListeChainee<Case> CheminEntreCases(Case Depart, Case Destination)
        {
            ListeChainee<CaseAStar> fermee = new ListeChainee<CaseAStar>();
            ListeChainee<CaseAStar> ouverte = new ListeChainee<CaseAStar>();
            ListeChainee<Case> chemin = new ListeChainee<Case>();
            ouverte.AjouterFin(new CaseAStar(Depart, 0, DistanceEntreCases(Depart, Destination)));

            while (ouverte.Count != 0)
            {
                CaseAStar courant = ouverte.First.Valeur;
                foreach (CaseAStar score in ouverte)
                    if (score.Score_f < courant.Score_f)
                        courant = score;
                if (courant.LaCase == Destination)
                    return chemin;
                ouverte.Enlever(courant);
                fermee.AjouterFin(courant);
                foreach (Case voisin in CaseVoisines(courant.LaCase))
                {
                    if (fermee.Contient(voisin))
                        continue;
                    int tentative_score_g = score_g.TrouverA()
                }
            }


            //ListeChainee<Case> chemin = new ListeChainee<Case>();
            //ListeChainee<Case> CulDeSac = new ListeChainee<Case>();
            //chemin.AjouterFin(Depart);
            //while (chemin.Last.Valeur != Destination)
            //{
            //    if (TrouverProchaineCase(chemin, CulDeSac, Destination) == null)
            //    {
            //        CulDeSac.AjouterFin(chemin.Last.Valeur);
            //        chemin.EnleverDernier();
            //    }
            //    else
            //    {
            //        chemin.AjouterFin(TrouverProchaineCase(chemin, CulDeSac, Destination));
            //    }
            //}
            //return chemin;
        }
        public ListeChainee<Case> CaseVoisines(Case caseCible)
        {
            ListeChainee<Case> caseVoisines = new ListeChainee<Case>();
            if (caseCible.Y - 1 >= 0)
                caseVoisines.AjouterFin(TabCases[caseCible.X][caseCible.Y - 1]);
            if (caseCible.X - 1 >= 0)
                caseVoisines.AjouterFin(TabCases[caseCible.X - 1][caseCible.Y]);
            if (TabCases.Length >= caseCible.X + 1)
                caseVoisines.AjouterFin(TabCases[caseCible.X + 1][caseCible.Y]);
            if (TabCases[caseCible.X].Length >= caseCible.Y + 1)
                caseVoisines.AjouterFin(TabCases[caseCible.X][caseCible.Y + 1]);
            return caseVoisines;
        }

        internal Case TrouverProchaineCase(ListeChainee<Case> Chemin, ListeChainee<Case> CulDeSac, Case Destination)
        {
            if (Math.Abs(Chemin.Last.Valeur.X - Destination.X) >= Math.Abs(Chemin.Last.Valeur.Y - Destination.Y))
            {
                if (TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y].Contenu == Case.type.obstacle || CulDeSac.Contient(TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y]))
                {
                    if (TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)].Contenu == Case.type.obstacle || CulDeSac.Contient(TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)]))
                    {
                        return null;
                    }
                    return TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)];
                }
                return TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y];
            }
            else
            {
                if (TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)].Contenu == Case.type.obstacle || CulDeSac.Contient(TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)]))
                {
                    if (TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y].Contenu == Case.type.obstacle || CulDeSac.Contient(TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y]))
                    {
                        return null;
                    }
                    return TabCases[Chemin.Last.Valeur.X + (Destination.X - Chemin.Last.Valeur.X > 0 ? 1 : -1)][Chemin.Last.Valeur.Y];
                }
                return TabCases[Chemin.Last.Valeur.X][Chemin.Last.Valeur.Y + (Destination.Y - Chemin.Last.Valeur.Y > 0 ? 1 : -1)];
            }
        }
    }
}
