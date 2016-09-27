namespace GofusSharp
{
    public class Statistique
    {
        public enum type { vie, force, intelligence, chance, agilite, vitalite, sagesse, PA, PM, portee, invocation, prospection, initiative, fuite, DMG_neutre, DMG_feu, DMG_air, DMG_terre, DMG_eau, DMG_poussee, DMG_piege, RES_neutre, RES_feu, RES_air, RES_terre, RES_eau, RES_poussee, RES_Pourcent_neutre, RES_Pourcent_feu, RES_Pourcent_air, RES_Pourcent_terre, RES_Pourcent_eau, retrait_PA, retrait_PM, esquive_PA, esquive_PM, soin, renvoie_DMG, tacle, puissance, puissance_piege, reduction_physique, reduction_magique }
        public type Nom { get; internal set; }
        public int Valeur { get; internal set; }
        public Statistique(type Nom, int Valeur)
        {
            this.Nom = Nom;
            this.Valeur = Valeur;
        }
    }
}
