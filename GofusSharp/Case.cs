namespace GofusSharp
{
    public class Case
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Contenu { get; private set; }
        public Case(int X, int Y, int Contenu)
        {
            this.X = X;
            this.Y = Y;
            this.Contenu = Contenu;
        }
    }
}
