using System.Globalization;
using System.Text;

namespace GofusSharp
{
    public class Arme : Equipement
    {
        public enum typeArme { arc, baguette, baton, dague, faux, hache, marteau, outil, pelle, pioche, epee }
        public Liste<Effet> ListEffets { get; internal set; }
        public Zone ZonePortee { get; internal set; }
        public Zone ZoneEffet { get; internal set; }
        public typeArme TypeArme { get; internal set; }
        public int CoutPA { get; internal set; }
        internal Arme(Gofus.Equipement equip) : base(equip)
        {
            ListEffets = new Liste<Effet>();
            foreach (Gofus.Effet effet in equip.LstEffets)
            {
                ListEffets.Add(new Effet(effet));
            }
            ZonePortee = new Zone(equip.ZonePortee);
            ZoneEffet = new Zone(equip.ZoneEffet);
            TypeArme = (typeArme)System.Enum.Parse(typeof(typeArme), RemoveDiacritics(equip.Type.Replace(' ', '_').Replace('\'', '_').ToLower()));
            CoutPA = equip.Pa;
        }
        internal Arme(Liste<Statistique> ListStat, string Nom, type Type, Liste<Effet> ListEffets, Zone ZonePortee, Zone ZoneEffet, typeArme TypeArme, int CoutPA) : base(ListStat, Nom, Type)
        {
            this.ListEffets = ListEffets;
            this.ZonePortee = ZonePortee;
            this.ZoneEffet = ZoneEffet;
            this.TypeArme = TypeArme;
            this.CoutPA = CoutPA;
        }
        private string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();
            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
