﻿namespace GofusSharp
{
    public class Equipement
    {
        public enum type { chapeau, gant, botte, ceinture, cape, amulette, arme}
        public int IdEquipement { get; internal set; }
        public Condition[] TabConditions { get; internal set; }
        public Statistique[] TabStatistiques { get; internal set; }
        public string Nom { get; internal set; }
        public type Type { get; internal set; }
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