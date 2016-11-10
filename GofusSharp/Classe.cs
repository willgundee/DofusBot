namespace GofusSharp
{
    public class Classe
    {
        public Liste<Sort> ListSorts { get; internal set; }
        public string Nom { get; internal set; }
        internal Classe(Gofus.Classe classe)
        {
            ListSorts = new Liste<Sort>();
            foreach (Gofus.Sort sort in classe.LstSorts)
            {
                ListSorts.Add(new Sort(sort));
            }
            Nom = classe.Nom;
        }
        internal Classe(Liste<Sort> ListSorts, string Nom)
        {
            this.ListSorts = ListSorts;
            this.Nom = Nom;
        }
    }
}
