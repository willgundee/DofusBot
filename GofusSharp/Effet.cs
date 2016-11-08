namespace GofusSharp
{
    public class Effet
    {
        public enum type { pousse, repousse, repousse_lanceur, tire, tire_lanceur, teleportation, ATT_neutre, ATT_air, ATT_feu, ATT_terre, ATT_eau, envoutement, pose_piege, pose_glyphe, invocation, soin}
        public type Nom { get; internal set; }
        public int ValeurMin { get; internal set; }
        public int ValeurMax { get; internal set; }
        public int NbTour { get; internal set; }
        public Statistique.type Stat { get; internal set; }
        internal Effet(Gofus.Effet effet)
        {
            Nom = (type)System.Enum.Parse(typeof(type), effet.Nom.ToString());
            ValeurMin = effet.DmgMin;
            ValeurMax = effet.DmgMax;
        }
        internal Effet(type Nom, int ValeurMin, int ValeurMax)
        {
            this.Nom = Nom;
            this.ValeurMin = ValeurMin;
            this.ValeurMax = ValeurMax;
        }
        internal Effet(type Nom, int ValeurMin, int ValeurMax, int NbTour, Statistique.type Stat)
        {
            this.Nom = Nom;
            this.ValeurMin = ValeurMin;
            this.ValeurMax = ValeurMax;
            this.NbTour = NbTour;
            this.Stat = Stat;
        }

    }
}
