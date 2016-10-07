using System;

namespace GofusSharp
{
    public class Envoutement
    {
        public Statistique.type Stat { get; internal set; }
        public int Valeur { get; internal set; }
        public int TourRestants { get; internal set; }
        public int IdLanceur { get; internal set; }
        public Envoutement(Statistique.type Stat, int Valeur, int TourRestants, int IdLanceur)
        {
            this.Stat = Stat;
            this.Valeur = Valeur;
            this.TourRestants = TourRestants;
            this.IdLanceur = IdLanceur;
        }

        internal bool PasserTour()
        {
            TourRestants--;
            if (TourRestants <= 0)
                return true;
            return false;
        }
    }
}
