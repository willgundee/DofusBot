namespace GofusSharp
{
    public class Case
    {
        public enum type {vide, joueur, obstacle, piege }
        public int X { get; internal set; }
        public int Y { get; internal set; }
        public type Contenu { get; internal set; }
        public Case(int X, int Y, type Contenu)
        {
            this.X = X;
            this.Y = Y;
            this.Contenu = Contenu;
        }
    }
}
