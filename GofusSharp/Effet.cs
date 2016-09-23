namespace GofusSharp
{
    public class Effet
    {
        public enum type { pousse, pousse_lanceur, tire, tire_lanceur, teleportation, ATT, ATT_neutre, ATT_air, ATT_feu, ATT_terre, ATT_eau}
        public type Nom { get; private set; }
        public int ValeurMin { get; private set; }
        public int ValeurMax { get; private set; }
        public Effet(type Nom, int ValeurMin, int ValeurMax)
        {
            this.Nom = Nom;
            this.ValeurMin = ValeurMin;
            this.ValeurMax = ValeurMax;
        }
    }
}
