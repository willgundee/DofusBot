namespace GofusSharp
{
    public class Condition
    {
        public enum type { exp_min, exp_max, classe,PA_min, PA_max, PM_min, PM_max, force_min, intelligence_min, chance_min, agilite_min, vitalite_min, sagesse_min, force_max, intelligence_max, chance_max, agilite_max, vitalite_max, sagesse_max }
        public type Type { get; internal set; }
        public int Valeur { get; internal set; }
        public Condition(type Type, int Valeur)
        {
            this.Type = Type;
            this.Valeur = Valeur;
        }
    }
}
