namespace GofusSharp
{
    public class EntiteInconnu
    {
        public int IdEntite { get; protected set; }
        public Classe ClasseEntite { get; protected set; }
        public string Nom { get; protected set; }
        public float Experience { get; protected set; }
        public EntiteInconnu(int IdEntite, Classe ClasseEntite, string Nom, float Experience)
        {
            this.IdEntite = IdEntite;
            this.ClasseEntite = ClasseEntite;
            this.Nom = Nom;
            this.Experience = Experience;
        }
        public EntiteInconnu(Entite entite)
        {
            IdEntite = entite.IdEntite;
            ClasseEntite = entite.ClasseEntite;
            Nom = entite.Nom;
            Experience = entite.Experience;
        }
    }
}
