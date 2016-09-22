namespace GofusSharp
{
    public class Equipement
    {
        protected int IdEquipement { get; }
        protected Condition[] TabConditions { get; }
        protected Statistique[] TabStatistiques { get; }
        protected string Nom { get; }
        protected string Type { get; }
        public Equipement(int IdEquipement, Condition[] TabConditions, Statistique[] TabStatistiques, string Nom, string Type)
        {
            this.IdEquipement = IdEquipement;
            this.TabConditions = TabConditions;
            this.TabStatistiques = TabStatistiques;
            this.Nom = Nom;
            this.Type = Type;
        }
    }
}
