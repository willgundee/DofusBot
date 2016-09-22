namespace GofusSharp
{
    public class Statistique
    {
        private string Nom { get; }
        private int Valeur { get; }
        public Statistique(string Nom, int Valeur)
        {
            this.Nom = Nom;
            this.Valeur = Valeur;
        }
    }
}
