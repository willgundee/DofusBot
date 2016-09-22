namespace GofusSharp
{
    public class Partie
    {
        private int IdPartie { get; }
        private Terrain TerrainPartie { get; }
        private Entite[] TabAttaquants { get; }
        private Entite[] TabDefendants { get; }
        private int Seed { get; }
        public Partie(int IdPartie, Terrain TerrainPartie, Entite[] TabAttaquants, Entite[] TabDefendants, int Seed)
        {
            this.IdPartie = IdPartie;
            this.TerrainPartie = TerrainPartie;
            this.TabAttaquants = TabAttaquants;
            this.TabDefendants = TabDefendants;
            this.Seed = Seed;
        }
    }
}
