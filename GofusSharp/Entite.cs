namespace GofusSharp
{
    public class Entite
    {
        public int IdEntite { get; protected set; }
        public ListeChainee LstStatistiques { get; protected set; }
        public Script ScriptEntite { get; protected set; }
        public Classe ClasseEntite { get; protected set; }
        public string Nom { get; protected set; }
        public float Experience { get; protected set; }
        public Entite(int IdEntite, ListeChainee LstStatistiques, Script ScriptEntite, Classe ClasseEntite, string Nom, float Experience)
        {
            this.IdEntite = IdEntite;
            this.LstStatistiques = LstStatistiques;
            this.ScriptEntite = ScriptEntite;
            this.ClasseEntite = ClasseEntite;
            this.Nom = Nom;
            this.Experience = Experience;
        }
    }
}
