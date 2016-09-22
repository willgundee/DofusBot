namespace GofusSharp
{
    public class Partie
    {
        public int IdPartie { get; private set; }
        public Terrain TerrainPartie { get; private set; }
        public Entite[] TabAttaquants { get; private set; }
        public Entite[] TabDefendants { get; private set; }
        public int Seed { get; private set; }
        public Partie(int IdPartie, Terrain TerrainPartie, Entite[] TabAttaquants, Entite[] TabDefendants, int Seed)
        {
            this.IdPartie = IdPartie;
            this.TerrainPartie = TerrainPartie;
            this.TabAttaquants = TabAttaquants;
            this.TabDefendants = TabDefendants;
            this.Seed = Seed;
        }
        public int Generer()
        {
            IdPartie = 1;

            return 0;
        }
    }
}
