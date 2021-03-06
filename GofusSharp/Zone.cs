﻿namespace GofusSharp
{
    public class Zone
    {
        public enum type {cercle, ligne_verticale, ligne_horizontale, carre, croix, T, X, demi_cercle, cone, tous}
        public type Type { get; internal set; }
        public int PorteeMin { get; internal set; }
        public int PorteeMax { get; internal set; }
        internal Zone(Gofus.Zone zone)
        {
            Type = (type)System.Enum.Parse(typeof(type), zone.Nom.ToString()); ;
            PorteeMin = zone.PorteeMin;
            PorteeMax = zone.PorteeMax;
        }
        internal Zone(type Type, int PorteeMin, int PorteeMax)
        {
            this.Type = Type;
            this.PorteeMin = PorteeMin;
            this.PorteeMax = PorteeMax;
        }
    }
}
