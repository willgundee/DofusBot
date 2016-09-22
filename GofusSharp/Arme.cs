namespace GofusSharp
{
    public class Arme : Equipement
    {
        public Effet[] TabEffets { get; private set; }
        public Zone ZonePortee { get; private set; }
        public Zone ZoneEffet { get; private set; }
        public Arme(int IdEquipement, Condition[] TabConditions, Statistique[] TabStatistiques, string Nom, string Type, Effet[] TabEffets, Zone ZonePortee, Zone ZoneEffet) : base(IdEquipement, TabConditions, TabStatistiques, Nom, Type)
        {
            this.TabEffets = TabEffets;
            this.ZonePortee = ZonePortee;
            this.ZoneEffet = ZoneEffet;
        }
    }
}
