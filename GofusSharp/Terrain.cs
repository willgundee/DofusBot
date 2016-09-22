namespace GofusSharp
{
    public class Terrain
    {
        private Case[][] TabCases { get; }
        public Terrain(Case[][] TabCases)
        {
            this.TabCases = TabCases;
        }
    }
}
