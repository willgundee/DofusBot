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
                    if(caseT.Contenu == Case.type.obstacle)
                        caseAvecObstacle.AjouterFin(caseT);
                }
            }
            return caseAvecObstacle;
        }

        public ListeChainee<Case> CheminEntreCases(Case Depart, Case Destination)
        {
            ListeChainee<Case> chemin = new ListeChainee<Case>();
            ListeChainee<Case> CulDeSac = new ListeChainee<Case>();
            chemin.AjouterFin(Depart);
            while (chemin.Last.Valeur != Destination)
            {
                if (TrouverProchaineCase(chemin, CulDeSac, Destination) == null)
                {
                    CulDeSac.AjouterFin(chemin.Last.Valeur);
                    chemin.EnleverDernier();
                }
                else
                {
                    chemin.AjouterFin(TrouverProchaineCase(chemin, CulDeSac, Destination));
                }
            }
            return chemin;
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
