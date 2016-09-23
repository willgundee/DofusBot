namespace GofusSharp
{
    public class Classe
    {
        public enum type {ecaflip, eniripsa, iop, cra, feca, sacrieur, sadida, osamoda, enutrof, sram, xelor, pandawa}
        public int IdClasse { get; internal set; }
        public Sort[] TabSorts { get; internal set; }
        public type Nom { get; internal set; }
        public Classe(int IdClasse, Sort[] TabSorts, type Nom)
        {
            this.IdClasse = IdClasse;
            this.TabSorts = TabSorts;
            this.Nom = Nom;
        }
    }
}
