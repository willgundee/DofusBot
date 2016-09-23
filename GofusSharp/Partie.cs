namespace GofusSharp
{
    public class Partie
    {
        public int IdPartie { get; internal set; }
        public Terrain TerrainPartie { get; internal set; }
        public Entite[] TabAttaquants { get; internal set; }
        public Entite[] TabDefendants { get; internal set; }
        internal int Seed { get; set; }
        public Partie(int IdPartie, Terrain TerrainPartie, Entite[] TabAttaquants, Entite[] TabDefendants, int Seed)
        {
            this.IdPartie = IdPartie;
            this.TerrainPartie = TerrainPartie;
            this.TabAttaquants = TabAttaquants;
            this.TabDefendants = TabDefendants;
            this.Seed = Seed;
        }
        
        internal void DebuterPartie() {
            foreach (Entite attaquant in TabAttaquants) {
                int vie = new int();
                int vitalite = new int();
                foreach (Statistique stat in attaquant.TabStatistiques) {
                    switch (stat.Nom) {
                        case Statistique.type.vie:
                            vie = stat.Valeur;
                            break;
                        case Statistique.type.vitalite:
                            vitalite = stat.Valeur;
                            break;
                        case Statistique.type.PA:
                            attaquant.PA_MAX = stat.Valeur;
                            break;
                        case Statistique.type.PM:
                            attaquant.PM_MAX = stat.Valeur;
                            break;
                    }
                }
                attaquant.PV_MAX = vie + (vitalite * (attaquant.ClasseEntite.Nom != Classe.type.sacrieur ? 1 : 2));
                attaquant.PV = attaquant.PV_MAX;
            }
            foreach (Entite defendant in TabDefendants) {
                int vie = new int();
                int vitalite = new int();
                foreach (Statistique stat in defendant.TabStatistiques) {
                    switch (stat.Nom) {
                        case Statistique.type.vie:
                            vie = stat.Valeur;
                            break;
                        case Statistique.type.vitalite:
                            vitalite = stat.Valeur;
                            break;
                        case Statistique.type.PA:
                            defendant.PA_MAX = stat.Valeur;
                            break;
                        case Statistique.type.PM:
                            defendant.PM_MAX = stat.Valeur;
                            break;
                    }
                }
                defendant.PV_MAX = vie + (vitalite * (defendant.ClasseEntite.Nom != Classe.type.sacrieur ? 1 : 2));
                defendant.PV = defendant.PV_MAX;
            }
        }

        internal void DebuterTour() {
            foreach (Entite attaquant in TabAttaquants) {
                attaquant.PM = attaquant.PM_MAX;
                attaquant.PA = attaquant.PA_MAX;
            }
            foreach (Entite defendant in TabDefendants) {
                defendant.PM = defendant.PM_MAX;
                defendant.PA = defendant.PA_MAX;
            }
        }
        
    }
}
