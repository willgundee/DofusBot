namespace GofusSharp
{
    public class Noeud<T>
    {
        public T Valeur { get; set; }
        public Noeud<T> Next { get; set; }
        public Noeud<T> Previous { get; set; }
        public Noeud(T Valeur)
        {
            this.Valeur = Valeur;
            Next = null;
            Previous = null;
        }
    }
}
