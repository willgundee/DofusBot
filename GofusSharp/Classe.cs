namespace GofusSharp
{
    public class Classe
    {
        public enum type {ecaflip, eniripsa, iop, cra, feca, sacrieur, sadida, osamoda, enutrof, sram, xelor, pandawa}
        public int IdClasse { get; private set; }
        public Sort[] TabSorts { get; private set; }
        public type Nom { get; private set; }
        public Classe(int IdClasse, Sort[] TabSorts, type Nom)
        {
            this.IdClasse = IdClasse;
            this.TabSorts = TabSorts;
            this.Nom = Nom;
        }
    }
}
