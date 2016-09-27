namespace GofusSharp
{
    public class Partie
    {
        public int IdPartie { get; internal set; }
        public Terrain TerrainPartie { get; internal set; }
        internal ListeChainee<Entite> ListAttaquants { get; set; }
        internal ListeChainee<Entite> ListDefendants { get; set; }
        internal ListeChainee<EntiteInconnu> ListEntites { get; set; }
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
            ListEntites = new ListeChainee<EntiteInconnu>();
        }

        internal void DebuterPartie()
        {
            Noeud<Entite> entite = ListAttaquants.First;
            while(entite != null)
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
                ListEntites.AjouterFin(new EntiteInconnu(entite.Valeur));
                entite = entite.Next;
            }
        }

        internal void DebuterAction()
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
            Noeud<EntiteInconnu> entiteInconnu = ListEntites.First;
            Noeud<Entite> entite = ListAttaquants.First;
            while (entiteInconnu != null)
            {
                bool existe = false;
                while (entite != null)
                {
                    if (entite.Valeur.IdEntite == entiteInconnu.Valeur.IdEntite)
                    {
                        entite.Valeur.Position = entiteInconnu.Valeur.Position;
                        entite.Valeur.PV = entiteInconnu.Valeur.PV;
                        entite.Valeur.PV_MAX = entiteInconnu.Valeur.PV_MAX;
                        entite.Valeur.ListEnvoutements = entiteInconnu.Valeur.ListEnvoutements;
                        Noeud<Statistique> statInconnu = entiteInconnu.Valeur.ListStatistiques.First;
                        Noeud<Statistique> stat = entite.Valeur.ListStatistiques.First;
                        while (statInconnu != null)
                        {
                            while (stat != null)
                            {
                                if (statInconnu.Valeur.Nom == stat.Valeur.Nom)
                                {
                                    statInconnu.Valeur.Valeur = stat.Valeur.Valeur;
                                    break;
                                }
                                stat = stat.Next;
                            }
                            statInconnu = statInconnu.Next;
                        }
                        existe = true;
                        break;
                    }
                    if (!existe)
                    {
                        Entite newInvoc = new Entite(entiteInconnu.Valeur.IdEntite, entiteInconnu.Valeur.ClasseEntite, entiteInconnu.Valeur.Nom, entiteInconnu.Valeur.Experience, entiteInconnu.Valeur.Position, entiteInconnu.Valeur.Equipe, entiteInconnu.Valeur.ListStatistiques,new Script(3, "Placeholder"), TerrainPartie);
                        if (true)
                        {

                        }
                    }
                    entite = entite.Next;
                }
                entiteInconnu = entiteInconnu.Next;
            }
        }
    }
}
