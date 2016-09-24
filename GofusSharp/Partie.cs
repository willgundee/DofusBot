namespace GofusSharp
{
    public class Partie
    {
        public int IdPartie { get; internal set; }
        public Terrain TerrainPartie { get; internal set; }
        public ListeChainee<Entite> ListAttaquants { get; internal set; }
        public ListeChainee<Entite> ListDefendants { get; internal set; }
        public ListeChainee<Entite> ListEntites { get; internal set; }
        internal int Seed { get; set; }
        public Partie(int IdPartie, Terrain TerrainPartie, ListeChainee<Entite> ListAttaquants, ListeChainee<Entite> ListDefendants, int Seed)
        {
            this.IdPartie = IdPartie;
            this.TerrainPartie = TerrainPartie;
            this.ListAttaquants = ListAttaquants;
            this.ListDefendants = ListDefendants;
            this.Seed = Seed;
            ListAttaquants.Last.Next = ListDefendants.First;
            ListDefendants.First.Previous = ListAttaquants.Last;
        }

        internal void DebuterPartie()
        {
            Noeud<Entite> entite = ListAttaquants.First;
            while(entite.Next != null)
            {
                int vie = new int();
                int vitalite = new int();
                foreach (Statistique stat in entite.Valeur.TabStatistiques)
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
            }
        }

        internal void DebuterTour()
        {
            Noeud<Entite> entite = ListAttaquants.First;
            while (entite.Next != null)
            {
                entite.Valeur.PM = entite.Valeur.PM_MAX;
                entite.Valeur.PA = entite.Valeur.PA_MAX;
            }
        }
    }
}
