namespace GofusSharp
{
    public class Statistique
    {
        public string Nom { get; private set; }
        public int Valeur { get; private set; }
        public Statistique(string Nom, int Valeur)
        {
            this.Nom = Nom;
            this.Valeur = Valeur;
        }
    }
}
