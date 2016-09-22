namespace GofusSharp
{
    public class Entite
    {
        protected int IdEntite { get; }
        protected ListeChainee LstStatistiques { get; }
        protected Script ScriptEntite { get; }
        protected Classe ClasseEntite { get; }
        protected string Nom { get; }
        protected float Experience { get; }
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
