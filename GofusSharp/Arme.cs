namespace GofusSharp
{
    public class Arme : Equipement
    {
        private Effet[] TabEffets { get; }
        private Zone ZonePortee { get; }
        private Zone ZoneEffet { get; }
        public Arme(int IdEquipement, Condition[] TabConditions, Statistique[] TabStatistiques, string Nom, string Type, Effet[] TabEffets, Zone ZonePortee, Zone ZoneEffet) : base(IdEquipement, TabConditions, TabStatistiques, Nom, Type)
        {
            this.TabEffets = TabEffets;
            this.ZonePortee = ZonePortee;
            this.ZoneEffet = ZoneEffet;
        }
    }
}
