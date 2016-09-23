namespace GofusSharp
{
    public class Arme : Equipement
    {
        public Effet[] TabEffets { get; internal set; }
        public Zone ZonePortee { get; internal set; }
        public Zone ZoneEffet { get; internal set; }
        public Arme(int IdEquipement, Condition[] TabConditions, Statistique[] TabStatistiques, string Nom, type Type, Effet[] TabEffets, Zone ZonePortee, Zone ZoneEffet) : base(IdEquipement, TabConditions, TabStatistiques, Nom, Type)
        {
            this.TabEffets = TabEffets;
            this.ZonePortee = ZonePortee;
            this.ZoneEffet = ZoneEffet;
        }
    }
}
