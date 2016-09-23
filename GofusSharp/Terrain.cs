namespace GofusSharp
{
    public class Terrain
    {
        public Case[][] TabCases { get; internal set; }
        public Terrain(Case[][] TabCases)
        {
            this.TabCases = TabCases;
        }
        

        public int DistanceEntreCases(Case case1, Case case2)
        {
            return Math.Abs(case1.X - case2.X) + Math.Abs(case1.Y - case2.Y);
        }
    }
}
