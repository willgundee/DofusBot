namespace GofusSharp
{
    public class Entite : EntiteInconnu
    {
        public int PV { get; internal set; }
        public int PV_MAX { get; internal set; }
        public int PA { get; internal set; }
        public int PA_MAX { get; internal set; }
        public int PM { get; internal set; }
        public int PM_MAX { get; internal set; }
        public Statistique[] TabStatistiques { get; internal set; }
        public Script ScriptEntite { get; internal set; }
        public Terrain TerrainEntite { get; internal set; }
        public Entite(int IdEntite, Classe ClasseEntite, string Nom, float Experience, Case Position, Statistique[] TabStatistiques, Script ScriptEntite, Terrain TerrainEntite) : base(IdEntite, ClasseEntite, Nom, Experience, Position)
        {
            this.IdEntite = IdEntite;
            this.TabStatistiques = TabStatistiques;
            this.ScriptEntite = ScriptEntite;
            this.ClasseEntite = ClasseEntite;
            this.Nom = Nom;
            this.Experience = Experience;

            this.TerrainEntite = TerrainEntite;
        }

        public bool UtiliserSort(Sort sort, EntiteInconnu cible) {

            return false;
        }



        private bool CalculerPortee(Zone.type ) {

        }

        public int AvancerVers(EntiteInconnu cible) {
            int PM_Debut = PM;
            while (PM > 0) {
                if (Math.Abs(Position.X - cible.Position.X) >= Math.Abs(Position.Y - cible.Position.Y)) {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.Position.X - Position.X > 0 ? 1 : -1)][Position.Y])) {
                        if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Position.Y - Position.Y > 0 ? 1 : -1)])) {
                            return PM_Debut - PM;
                        }
                    }
                } else {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Position.Y - Position.Y > 0 ? 1 : -1)])) {
                        if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.Position.X - Position.X > 0 ? 1 : -1)][Position.Y])) {
                            return PM_Debut - PM;
                        }
                    }
                }
            }
            return PM_Debut - PM;
        }
        public int AvancerVers(EntiteInconnu cible, int PM_Alouer) {
            int PM_Debut = PM;
            while (PM > 0 && PM_Alouer > 0) {
                if (Math.Abs(Position.X - cible.Position.X) >= Math.Abs(Position.Y - cible.Position.Y)) {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.Position.X - Position.X > 0 ? 1 : -1)][Position.Y])) {
                        if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Position.Y - Position.Y > 0 ? 1 : -1)])) {
                            return PM_Debut - PM;
                        }
                    }
                } else {
                    if (!ChangerPosition(TerrainEntite.TabCases[Position.X][Position.Y + (cible.Position.Y - Position.Y > 0 ? 1 : -1)])) {
                        if (!ChangerPosition(TerrainEntite.TabCases[Position.X + (cible.Position.X - Position.X > 0 ? 1 : -1)][Position.Y])) {
                            return PM_Debut - PM;
                        }
                    }
                }
                PM--;
                PM_Alouer--;
            }
            return PM_Debut - PM;
        }

        internal bool ChangerPosition(Case nextPosition)
        {
            if (nextPosition.Contenu == Case.type.vide)
            {
                Position.Contenu = Case.type.vide;
                nextPosition.Contenu = Case.type.joueur;
                Position = nextPosition;
                PM--;
                return true;
            }
            return false;
        }
    }
}
