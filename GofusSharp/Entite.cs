namespace GofusSharp
{
    public class Entite : EntiteInconnu
    {
        public ListeChainee LstStatistiques { get; protected set; }
        public Script ScriptEntite { get; protected set; }
        public Entite(int IdEntite, Classe ClasseEntite, string Nom, float Experience, ListeChainee LstStatistiques, Script ScriptEntite) : base(IdEntite, ClasseEntite, Nom, Experience)
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
