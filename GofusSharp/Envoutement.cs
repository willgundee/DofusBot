namespace GofusSharp
{
    public class Envoutement
    {
        public Statistique.type Stat { get; internal set; }
        public int TourRestants { get; internal set; }
        public int IdLanceur { get; internal set; }
        public Envoutement(Statistique.type Stat, int TourRestants, int IdLanceur)
        {
            this.Stat = Stat;
            this.TourRestants = TourRestants;
            this.IdLanceur = IdLanceur;
        }
    }
}
