namespace GofusSharp
{
    public class EntiteInconnu
    {
        public enum type { attaquant, defendant }
        public int IdEntite { get; internal set; }
        public Classe ClasseEntite { get; internal set; }
        public string Nom { get; internal set; }
        public float Experience { get; internal set; }
        public Case Position { get; internal set; }
        public type Equipe { get; internal set; }
        public int PV { get; internal set; }
        public int PV_MAX { get; internal set; }
        public EntiteInconnu(int IdEntite, Classe ClasseEntite, string Nom, float Experience, Case Position, type Equipe)
        {
            this.IdEntite = IdEntite;
            this.ClasseEntite = ClasseEntite;
            this.Nom = Nom;
            this.Experience = Experience;
            this.Position = Position;
            this.Equipe = Equipe;
        }
        public EntiteInconnu(Entite entite)
        {
            IdEntite = entite.IdEntite;
            ClasseEntite = entite.ClasseEntite;
            Nom = entite.Nom;
            Experience = entite.Experience;
            Position = entite.Position;
        }

    }
}
