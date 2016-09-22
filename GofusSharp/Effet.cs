﻿namespace GofusSharp
{
    public class Effet
    {
        private string Nom { get; }
        private int ValeurMin { get; }
        private int ValeurMax { get; }
        public Effet(string Nom, int ValeurMin, int ValeurMax)
        {
            this.Nom = Nom;
            this.ValeurMin = ValeurMin;
            this.ValeurMax = ValeurMax;
        }
    }
}
