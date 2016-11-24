using System;
using System.Linq;
using System.Windows;

namespace GofusSharp
{
    public class EntiteInconnu
    {
        public enum type { attaquant, defendant }
        public enum typeEtat { vivant, mort }
        private Entite VraiEnti { get; set; }
        public typeEtat Etat
        {
            get
            {
                if (this is Entite)
                    return etat;
                else
                    return VraiEnti.Etat;
            }
            internal set
            {
                if (this is Entite)
                    etat = value;
                else
                    VraiEnti.Etat = value;
            }
        }
        private typeEtat etat;
        public int IdEntite
        {
            get
            {
                if (this is Entite)
                    return idEntite;
                else
                    return VraiEnti.IdEntite;
            }
            internal set
            {
                if (this is Entite)
                    idEntite = value;
                else
                    VraiEnti.IdEntite = value;
            }
        }
        private int idEntite;
        public Classe ClasseEntite
        {
            get
            {
                if (this is Entite)
                    return classeEntite;
                else
                    return VraiEnti.ClasseEntite;
            }
            internal set
            {
                if (this is Entite)
                    classeEntite = value;
                else
                    VraiEnti.ClasseEntite = value;
            }
        }
        private Classe classeEntite;
        public string Nom
        {
            get
            {
                if (this is Entite)
                    return nom;
                else
                    return VraiEnti.Nom;
            }
            internal set
            {
                if (this is Entite)
                    nom = value;
                else
                    VraiEnti.Nom = value;
            }
        }
        private string nom;
        public double Experience
        {
            get
            {
                if (this is Entite)
                    return experience;
                else
                    return VraiEnti.Experience;
            }
            internal set
            {
                if (this is Entite)
                    experience = value;
                else
                    VraiEnti.Experience = value;
            }
        }
        private double experience;
        public Case Position
        {
            get
            {
                if (this is Entite)
                    return position;
                else
                    return VraiEnti.Position;
            }
            internal set
            {
                if (this is Entite)
                    position = value;
                else
                    VraiEnti.Position = value;
            }
        }
        private Case position;
        public type Equipe
        {
            get
            {
                if (this is Entite)
                    return equipe;
                else
                    return VraiEnti.Equipe;
            }
            internal set
            {
                if (this is Entite)
                    equipe = value;
                else
                    VraiEnti.Equipe = value;
            }
        }
        private type equipe;
        public int PV
        {
            get
            {
                if (this is Entite)
                    return pv;
                else
                    return VraiEnti.PV;
            }
            internal set
            {
                if (this is Entite)
                    pv = value;
                else
                    VraiEnti.PV = value;
            }
        }
        private int pv;
        public int PV_MAX
        {
            get
            {
                if (this is Entite)
                    return pv_MAX;
                else
                    return VraiEnti.PV_MAX;
            }
            internal set
            {
                if (this is Entite)
                    pv_MAX = value;
                else
                    VraiEnti.PV_MAX = value;
            }
        }
        private int pv_MAX;
        public int PA
        {
            get
            {
                if (this is Entite)
                    return pa;
                else
                    return VraiEnti.PA;
            }
            internal set
            {
                if (this is Entite)
                    pa = value;
                else
                    VraiEnti.PA = value;
            }
        }
        private int pa;
        public int PA_MAX
        {
            get
            {
                if (this is Entite)
                    return pa_MAX;
                else
                    return VraiEnti.PA_MAX;
            }
            internal set
            {
                if (this is Entite)
                    pa_MAX = value;
                else
                    VraiEnti.PA_MAX = value;
            }
        }
        private int pa_MAX;
        public int PM
        {
            get
            {
                if (this is Entite)
                    return pm;
                else
                    return VraiEnti.PM;
            }
            internal set
            {
                if (this is Entite)
                    pm = value;
                else
                    VraiEnti.PM = value;
            }
        }
        private int pm;
        public int PM_MAX
        {
            get
            {
                if (this is Entite)
                    return pm_MAX;
                else
                    return VraiEnti.PM_MAX;
            }
            internal set
            {
                if (this is Entite)
                    pm_MAX = value;
                else
                    VraiEnti.PM_MAX = value;
            }
        }
        private int pm_MAX;
        public int Proprietaire
        {
            get
            {
                if (this is Entite)
                    return proprietaire;
                else
                    return VraiEnti.Proprietaire;
            }
            internal set
            {
                if (this is Entite)
                    proprietaire = value;
                else
                    VraiEnti.Proprietaire = value;
            }
        }
        private int proprietaire;
        public Liste<Statistique> ListStatistiques
        {
            get
            {
                if (this is Entite)
                    return listStatistiques;
                else
                    return VraiEnti.ListStatistiques;
            }
            internal set
            {
                if (this is Entite)
                    listStatistiques = value;
                else
                    VraiEnti.ListStatistiques = value;
            }
        }
        private Liste<Statistique> listStatistiques;
        public Liste<Envoutement> ListEnvoutements
        {
            get
            {
                if (this is Entite)
                    return listEnvoutements;
                else
                    return VraiEnti.ListEnvoutements;
            }
            internal set
            {
                if (this is Entite)
                    listEnvoutements = value;
                else
                    VraiEnti.ListEnvoutements = value;
            }
        }
        private Liste<Envoutement> listEnvoutements;

        public EntiteInconnu() { }
        internal EntiteInconnu(Gofus.Entite entite, type Equipe)
        {
            VraiEnti = new Entite();
            IdEntite = entite.IdEntite;
            ClasseEntite = new Classe(entite.ClasseEntite);
            Nom = entite.Nom;
            Experience = entite.LstStats.First(x => x.Nom == Gofus.Statistique.element.experience).Valeur;
            this.Equipe = Equipe;
            ListStatistiques = new Liste<Statistique>();
            foreach (Gofus.Statistique stat in entite.LstStats)
            {
                if (stat.Nom != Gofus.Statistique.element.experience)
                    ListStatistiques.Add(new Statistique(stat));
            }
            ListEnvoutements = new Liste<Envoutement>();
        }

        internal EntiteInconnu(Entite entite)
        {
            VraiEnti = entite;
            //IdEntite = entite.IdEntite;
            //ClasseEntite = entite.ClasseEntite;
            //Nom = entite.Nom;
            //Experience = entite.Experience;
            //Position = entite.Position;
            //Equipe = entite.Equipe;
            //PA = entite.PA;
            //PV = entite.PV;
            //PM = entite.PM;
            //PA_MAX = entite.PA_MAX;
            //PV_MAX = entite.PV_MAX;
            //PM_MAX = entite.PM_MAX;
            //Proprietaire = entite.Proprietaire;
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
            if (dommageRecu < 0)
                dommageRecu = 0;
            if (!Debug.FCombat.Generation)
            {
                Debug.FCombat.Dispatcher.Invoke(Debug.FCombat.DelLog, new object[] { "\n" + Nom + " à perdu " + dommageRecu.ToString() + " point de vie" });
            }
            PV -= dommageRecu;
            if (PV <= 0)
            {
                PV = 0;
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

        public int RetourneNiveau()
        {
            for (int i = 1; i < 200; i++)
                if (Experience >= Statistique.dictLvl[i] && Experience < Statistique.dictLvl[i + 1])
                    return i;
            if (Experience >= Statistique.dictLvl[200])
                return 200;
            return 0; //si toutes fuck up
        }
    }
}
