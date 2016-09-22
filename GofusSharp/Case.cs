namespace GofusSharp
{
    public class Case
    {
        private int X { get; }
        private int Y { get; }
        private int Contenu { get; }
        public Case(int X, int Y, int Contenu)
        {
            this.X = X;
            this.Y = Y;
            this.Contenu = Contenu;
        }
    }
}
