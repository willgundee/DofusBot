namespace GofusSharp
{
    public class Classe
    {
        public int IdClasse { get; private set; }
        public Sort[] TabSorts { get; private set; }
        public string Nom { get; private set; }
        public Classe(int IdClasse, Sort[] TabSorts, string Nom)
        {
            this.IdClasse = IdClasse;
            this.TabSorts = TabSorts;
            this.Nom = Nom;
        }
    }
}
