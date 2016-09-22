namespace GofusSharp
{
    public class Noeud
    {
        public object Valeur { get; set; }
        public Noeud Next { get; set; }
        public Noeud Previous { get; set; }
        public Noeud(object Valeur)
        {
            this.Valeur = Valeur;
            Next = null;
            Previous = null;
        }
    }
}
