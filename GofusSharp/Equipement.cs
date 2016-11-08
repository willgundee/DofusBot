using System.Globalization;
using System.Text;

namespace GofusSharp
{
    public class Equipement
    {
        public enum type { chapeau, anneau, botte, ceinture, cape, amulette, arme}
        public Liste<Statistique> ListStatistiques { get; internal set; }
        public string Nom { get; internal set; }
        public type Type { get; internal set; }
        internal Equipement(Gofus.Equipement equip)
        {
            ListStatistiques = new Liste<Statistique>();
            foreach (Gofus.Statistique stat in equip.LstStatistiques)
            {
                ListStatistiques.Add(new Statistique(stat));
            }
            Nom = equip.Nom;
            try
            {
                Type = (type)System.Enum.Parse(typeof(type), RemoveDiacritics(equip.Type.Replace(' ', '_').Replace('\'', '_').ToLower()));
            }
            catch (System.Exception)
            {
                Type = type.arme;
            }
        }
        internal Equipement(Liste<Statistique> ListStatistiques, string Nom, type Type)
        {
            this.ListStatistiques = ListStatistiques;
            this.Nom = Nom;
            this.Type = Type;
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
