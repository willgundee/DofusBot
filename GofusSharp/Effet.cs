namespace GofusSharp
{
    public class Effet
    {
        public string Nom { get; private set; }
        public int ValeurMin { get; private set; }
        public int ValeurMax { get; private set; }
        public Effet(string Nom, int ValeurMin, int ValeurMax)
        {
            this.Nom = Nom;
            this.ValeurMin = ValeurMin;
            this.ValeurMax = ValeurMax;
        }
    }
}
