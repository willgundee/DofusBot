namespace GofusSharp
{
    public class Terrain
    {
        public Case[][] TabCases { get; private set; }
        public Terrain(Case[][] TabCases)
        {
            this.TabCases = TabCases;
        }
    }
}
