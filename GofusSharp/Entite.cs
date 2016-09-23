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
        protected Terrain TerrainEntite { get; set; }
        public Entite(int IdEntite, Classe ClasseEntite, string Nom, float Experience, Case Position, Statistique[] TabStatistiques, Script ScriptEntite, Terrain TerrainEntite) : base(IdEntite, ClasseEntite, Nom, Experience, Position)
        {
            this.IdEntite = IdEntite;
            this.TabStatistiques = TabStatistiques;
            this.ScriptEntite = ScriptEntite;
            this.ClasseEntite = ClasseEntite;
            this.Nom = Nom;
            this.Experience = Experience;
        }

        public int AvancerVers(EntiteInconnu cible)
        {
            int PM_depart = PM;
            while (PM > 0)
            {
                if (Math.Abs(Position.X - cible.Position.X) >= Math.Abs(Position.Y - cible.Position.Y))
                {

                }
            }
            return 0;
        }

        private bool ChangerPosition(Case nextPosition)
        {
            if (nextPosition.Contenu == Case.type.vide)
            {
                Position.ChangerContenu(Case.type.vide);
                nextPosition.ChangerContenu(Case.type.joueur);
                PM--;
                return true;
            }
            return false;
        }
    }
}
