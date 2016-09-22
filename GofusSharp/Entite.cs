namespace GofusSharp
{
    public class Entite
    {
        protected int IdEntite { get; }
        protected Statistique[] TabStatistiques { get; }
        protected Script ScriptEntite { get; }
        protected Classe ClasseEntite { get; }
        protected string Nom { get; }
        protected float Experience { get; }
        public Entite(int IdEntite, Statistique[] TabStatistiques, Script ScriptEntite, Classe ClasseEntite, string Nom, float Experience)
        {
            this.IdEntite = IdEntite;
            this.TabStatistiques = TabStatistiques;
            this.ScriptEntite = ScriptEntite;
            this.ClasseEntite = ClasseEntite;
            this.Nom = Nom;
            this.Experience = Experience;
        }
    }
}
