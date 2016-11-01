using System;
using System.Linq;
using System.Windows;

namespace GofusSharp
{
    public class EntiteInconnu
    {
        public enum type { attaquant, defendant }
        public enum typeEtat { vivant, mort }
        public typeEtat Etat { get; internal set; }
        public int IdEntite { get; internal set; }
        public Classe ClasseEntite { get; internal set; }
        public string Nom { get; internal set; }
        public float Experience { get; internal set; }
        public Case Position { get; internal set; }
        public type Equipe { get; internal set; }
        public int PV { get; internal set; }
        public int PV_MAX { get; internal set; }
        public int PA { get; internal set; }
        public int PA_MAX { get; internal set; }
        public int PM { get; internal set; }
        public int PM_MAX { get; internal set; }
        public int Proprietaire { get; internal set; }
        public Liste<Statistique> ListStatistiques { get; internal set; }
        public Liste<Envoutement> ListEnvoutements { get; internal set; }
        internal EntiteInconnu(int IdEntite, Classe ClasseEntite, string Nom, float Experience, type Equipe)
        {
            this.IdEntite = IdEntite;
            this.ClasseEntite = ClasseEntite;
            this.Nom = Nom;
            this.Experience = Experience;
            this.Equipe = Equipe;
            ListEnvoutements = new Liste<Envoutement>();
        }
        internal EntiteInconnu(int IdEntite, Classe ClasseEntite, string Nom, float Experience, Case Position, type Equipe, int proprietaire)
        {
            this.IdEntite = IdEntite;
            this.ClasseEntite = ClasseEntite;
            this.Nom = Nom;
            this.Experience = Experience;
            this.Position = Position;
            this.Equipe = Equipe;
            ListEnvoutements = new Liste<Envoutement>();
        }
        internal EntiteInconnu(Entite entite)
        {
            IdEntite = entite.IdEntite;
            ClasseEntite = entite.ClasseEntite;
            Nom = entite.Nom;
            Experience = entite.Experience;
            Position = entite.Position;
            Equipe = entite.Equipe;
            PA = entite.PA;
            PV = entite.PV;
            PM = entite.PM;
            PA_MAX = entite.PA_MAX;
            PV_MAX = entite.PV_MAX;
            PM_MAX = entite.PM_MAX;
            Proprietaire = entite.Proprietaire;
            ListEnvoutements = entite.ListEnvoutements;
            ListStatistiques = new Liste<Statistique>();
            foreach (Statistique stat in entite.ListStatistiques)
            {
                switch (stat.Nom)
                {
                    case Statistique.type.tacle:
                    case Statistique.type.RES_neutre:
                    case Statistique.type.RES_feu:
                    case Statistique.type.RES_air:
                    case Statistique.type.RES_terre:
                    case Statistique.type.RES_eau:
                    case Statistique.type.RES_poussee:
                    case Statistique.type.RES_Pourcent_neutre:
                    case Statistique.type.RES_Pourcent_feu:
                    case Statistique.type.RES_Pourcent_air:
                    case Statistique.type.RES_Pourcent_terre:
                    case Statistique.type.RES_Pourcent_eau:
                    case Statistique.type.esquive_PA:
                    case Statistique.type.esquive_PM:
                    case Statistique.type.renvoie_DMG:
                    case Statistique.type.reduction_magique:
                    case Statistique.type.reduction_physique:
                        ListStatistiques.Add(stat);
                        break;
                }
            }
        }

        internal bool recevoirDommages(int dommageRecu)
        {
            (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(Combat)) as Combat).tb_Log.Text += "\n" + Nom + " à perdu " + dommageRecu.ToString() + " point de vie";
            PV -= dommageRecu;
            if (PV <= 0)
            {
                Etat = typeEtat.mort;
                Position.Contenu = Case.type.vide;
                Position = null;
                return true;
            }
            return false;
        }

        internal bool ChangerPosition(Case nextPosition)
        {
            if (nextPosition.Contenu == Case.type.vide)
            {
                Position.Contenu = Case.type.vide;
                nextPosition.Contenu = Case.type.joueur;
                Position = nextPosition;
                return true;
            }
            return false;
        }
    }
}
