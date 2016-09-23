namespace GofusSharp
{
    public class Equipement
    {
        public enum type { chapeau, gant, botte, ceinture, cape, amulette, arme}
        public int IdEquipement { get; protected set; }
        public Condition[] TabConditions { get; protected set; }
        public Statistique[] TabStatistiques { get; protected set; }
        public string Nom { get; protected set; }
        public type Type { get; protected set; }
        public Equipement(int IdEquipement, Condition[] TabConditions, Statistique[] TabStatistiques, string Nom, type Type)
        {
            this.IdEquipement = IdEquipement;
            this.TabConditions = TabConditions;
            this.TabStatistiques = TabStatistiques;
            this.Nom = Nom;
            this.Type = Type;
        }
    }
}
