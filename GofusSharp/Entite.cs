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
            if (CaseEstDansZone(sort.ZonePortee.Type,sort.ZonePortee.PorteeMin, sort.ZonePortee.PorteeMax, Position, cible.Position))
            {

            }
            return false;
        }

        internal int InfligerEffet(Effet effet, Zone zoneEffet, Case source)
        {
            switch (effet.Nom)
            {
                case Effet.type.pousse:
                    break;
                case Effet.type.pousse_lanceur:
                    break;
                case Effet.type.tire:
                    break;
                case Effet.type.tire_lanceur:
                    break;
                case Effet.type.teleportation:
                    break;
                case Effet.type.ATT_neutre:
                    break;
                case Effet.type.ATT_air:
                    break;
                case Effet.type.ATT_feu:
                    break;
                case Effet.type.ATT_terre:
                    break;
                case Effet.type.ATT_eau:
                    break;
                case Effet.type.envoutement:
                    break;
                case Effet.type.pose_piege:
                    break;
                case Effet.type.pose_glyphe:
                    break;
                case Effet.type.invocation:
                    break;
                default:
                    break;
            }
        }

        private bool CaseEstDansZone(Zone.type TypeZone, int porteeMin, int porteeMax, Case source, Case cible) {
            switch (TypeZone)
            {
                case Zone.type.cercle:
                    int portee = TerrainEntite.DistanceEntreCases(source, cible);
                    if (portee >= porteeMin && portee <= porteeMax)
                    {
                        return true;
                    }
                    break;
                case Zone.type.ligne_verticale:
                    if (cible.X == source.X && Math.Abs(cible.Y - source.Y) >= porteeMin && Math.Abs(cible.Y - source.Y) <= porteeMax && (source.X > Position.X && source.X <= cible.X || source.X < Position.X && source.X >= cible.X) || cible.Y == source.Y && Math.Abs(cible.X - source.X) >= porteeMin && Math.Abs(cible.X - source.X) <= porteeMax && (source.Y > Position.Y && source.Y <= cible.Y || source.Y < Position.Y && source.Y >= cible.Y))
                    {
                        return true;
                    }
                    break;
                case Zone.type.ligne_horizontale:
                    if (cible.X == source.X && Math.Abs(cible.Y - source.Y) >= porteeMin && Math.Abs(cible.Y - source.Y) <= porteeMax && source.Y == Position.Y || cible.Y == source.Y && Math.Abs(cible.X - source.X) >= porteeMin && Math.Abs(cible.X - source.X) <= porteeMax && source.X == Position.X)
                    {
                        return true;
                    }
                    break;
                case Zone.type.carre:
                    if (Math.Abs(cible.Y - source.Y) >= porteeMin && Math.Abs(cible.Y - source.Y) <= porteeMax && Math.Abs(cible.X - source.X) >= porteeMin && Math.Abs(cible.X - source.X) <= porteeMax)
                    {
                        return true;
                    }
                    break;
                case Zone.type.croix:
                    if ((cible.X == source.X && Math.Abs(cible.Y - source.Y) >= porteeMin && Math.Abs(cible.Y - source.Y) <= porteeMax || cible.Y == source.Y) && Math.Abs(cible.X - source.X) >= porteeMin && Math.Abs(cible.X - source.X) <= porteeMax)
                    {
                        return true;
                    }
                    break;
                case Zone.type.X:
                    if (cible.X - cible.Y == source.X - source.Y || cible.X + cible.Y == source.X + source.Y)
                    {
                        return true;
                    }
                    break;
                case Zone.type.T:
                    break;
                case Zone.type.demi_cercle:
                    break;
                case Zone.type.cone:
                    break;
                case Zone.type.tous:
                    return true;
                    break;
                default:
                    break;
            }
            return false;
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
