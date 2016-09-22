namespace GofusSharp
{
    public class Equipement
    {
        public int IdEquipement { get; protected set; }
        public Condition[] TabConditions { get; protected set; }
        public Statistique[] TabStatistiques { get; protected set; }
        public string Nom { get; protected set; }
        public string Type { get; protected set; }
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
