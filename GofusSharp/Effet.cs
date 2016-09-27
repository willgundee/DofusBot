namespace GofusSharp
{
    public class Effet
    {
        public enum type { pousse, pousse_lanceur, tire, tire_lanceur, teleportation, ATT_neutre, ATT_air, ATT_feu, ATT_terre, ATT_eau, envoutement, pose_piege, pose_glyphe, invocation, soin}
        public type Nom { get; internal set; }
        public int ValeurMin { get; internal set; }
        public int ValeurMax { get; internal set; }
        public int NbTour { get; internal set; }
        public Statistique.type Stat { get; internal set; }
        public Effet(type Nom, int ValeurMin, int ValeurMax)
        {
            this.Nom = Nom;
            this.ValeurMin = ValeurMin;
            this.ValeurMax = ValeurMax;
        }
        public Effet(type Nom, int ValeurMin, int ValeurMax, int NbTour, Statistique.type Stat)
        {
            this.Nom = Nom;
            this.ValeurMin = ValeurMin;
            this.ValeurMax = ValeurMax;
            this.NbTour = NbTour;
            this.Stat = Stat;
        }

    }
}
