using GofusSharp;

namespace test
{
    class EnviroTest
    {
        public void Execution(Terrain terrain, Entite Perso, Liste<EntiteInconnu> ListEntites)
        {
            #region chafer archer
            /*
            Sort flecheHuile = null;
            Sort flecheFeu = null;
            EntiteInconnu ennemi = Perso.EnnemiLePlusProche(ListEntites);
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
            {
                switch (item.Nom)
                {
                    case "Flèche d'huile":
                        flecheHuile = item;
                        break;
                    case "Flèche de feu":
                        flecheFeu = item;
                        break;
                }
            }

            while (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) >= 8 && Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0], 1) == 1)
            {

            }

            if (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) <= 8)
            {
                Perso.UtiliserSort(flecheFeu, Perso.EnnemiLePlusProche(ListEntites));
            }
            Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites));
            */
            #endregion
            #region krurorre
            /*
            Sort chafouette = null;
            Sort charnaque = null;
            Sort coupChaferFort = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
                switch (item.Nom)
                {
                    case "Chafouette":
                        chafouette = item;
                        break;
                    case "Charnaque":
                        charnaque = item;
                        break;
                    case "Coup mortel du chafer":
                        coupChaferFort = item;
                        break;
                }
            Debug.Log(Debug.TourCourant());
            if (Debug.TourCourant() % 2 == 1)
            {
                Perso.UtiliserSort(charnaque, Perso);
            }
            while (Perso.PM != 0 && Perso.PA >= coupChaferFort.CoutPA)
            {
                while (Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0]) == 1)
                {

                }
                while (Perso.PeutUtiliserSort(chafouette, Perso.EnnemiLePlusProche(ListEntites)))
                {
                    Perso.UtiliserSort(chafouette, Perso.EnnemiLePlusProche(ListEntites));
                }
                while (Perso.PeutUtiliserSort(coupChaferFort, Perso.EnnemiLePlusProche(ListEntites)))
                {
                    Perso.UtiliserSort(coupChaferFort, Perso.EnnemiLePlusProche(ListEntites));
                }
            }
            Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites));
            */
            #endregion
            #region chafer lancier
            /*
            Sort Empalement = null;
            Sort Euphorie = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
                switch (item.Nom)
                {
                    case "Empalement":
                        Empalement = item;
                        break;
                    case "Euphorie malsaine":
                        Euphorie = item;
                        break;
                }
            Debug.Log(Debug.TourCourant());
            if (Debug.TourCourant() % 3 == 1)
            {
                Perso.UtiliserSort(Euphorie, Perso);
            }
            while (Perso.PM != 0 && Perso.PA >= Empalement.CoutPA)
            {
                while (Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0]) == 1)
                {
                }
                if (Perso.PeutUtiliserSort(Empalement, Perso.EnnemiLePlusProche(ListEntites)))
                {
                    Perso.UtiliserSort(Empalement, Perso.EnnemiLePlusProche(ListEntites));
                }
            }
            Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites));
            */
            #endregion
            #region chafer

            Sort CoupChafer = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
            {
                switch (item.Nom)
                {
                    case "Coup du chafer":
                        CoupChafer = item;
                        break;
                }
            }
            while (Perso.PM != 0 && Perso.PA >= CoupChafer.CoutPA)
            {
                while (Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0]) == 1)
                {
                }
                if (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) == 1)
                {
                    Perso.UtiliserSort(CoupChafer, Perso.EnnemiLePlusProche(ListEntites));
                }
            }
            Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites));

            #endregion
            #region pissenlit diabolique
            /*
            Sort effleurement = null;
            Sort herbeSauvage = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
            {
                switch (item.Nom)
                {
                    case "Effleurement":
                        effleurement = item;
                        break;
                    case "Herbe sauvage":
                        herbeSauvage = item;
                        break;
                }
            }

                while (Perso.PM != 0 && Perso.PA >= 3)
                {
                    while (Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0]) == 1)
                    {
                    }
                if (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) == 1)
                {
                    Perso.UtiliserSort(effleurement, Perso.EnnemiLePlusProche(ListEntites));
                    Perso.UtiliserSort(herbeSauvage, Perso.EnnemiLePlusProche(ListEntites));
                }
            }
                Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites));
                */
            #endregion
            #region sanglier
            /*
            Sort embrochement = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
            {
                switch (item.Nom)
                {
                    case "Embrochement":
                        embrochement = item;
                        break;
                }
            }

            while (Perso.PM != 0 && Perso.PA >= embrochement.CoutPA)
            {
                while (Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0], 1) == 1)
                {
                    while (Perso.PeutUtiliserSort(embrochement, Perso.EnnemiLePlusProche(ListEntites)))
                    {
                        Perso.UtiliserSort(embrochement, Perso.EnnemiLePlusProche(ListEntites));
                    }
                }
                while (Perso.PeutUtiliserSort(embrochement, Perso.EnnemiLePlusProche(ListEntites)))
                {
                    Perso.UtiliserSort(embrochement, Perso.EnnemiLePlusProche(ListEntites));
                }
            }
            */
            #endregion
            #region tofu
            /* 
             Sort beco = null;
             foreach (Sort item in Perso.ClasseEntite.ListSorts)
             {
                 switch (item.Nom)
                 {
                     case "Béco du tofu":
                         beco = item;
                         break;
                 }
             }
             while (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) != 1 && Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0], 1) == 1)
             { }

             if (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) == 1)
             {
                 Perso.UtiliserSort(beco, Perso.EnnemiLePlusProche(ListEntites));
             }
             Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites));
             */
            #endregion
            #region champ champ
            /*
            Sort poison = null;
            Sort frappe = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
            {
                switch (item.Nom)
                {
                    case "Frappe":
                        frappe = item;
                        break;
                    case "Poison_Sauvage":
                        poison = item;
                        break;
                }
            }
            while (Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0], 1) == 1)
            {
                if (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) <= 8)
                {
                    Perso.UtiliserSort(poison, Perso.EnnemiLePlusProche(ListEntites));
                    Perso.UtiliserSort(poison, Perso.EnnemiLePlusProche(ListEntites));
                    Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites));
                }
                if (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) - Perso.PM >= 0)
                {
                    Perso.AvancerVers(Perso.EnnemiLePlusProche(ListEntites));
                }
            }
            if (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) == 1)
            {
                Perso.UtiliserSort(frappe, Perso.EnnemiLePlusProche(ListEntites));
                Perso.UtiliserSort(frappe, Perso.EnnemiLePlusProche(ListEntites));
            }
            */
            #endregion champ champ
            #region boufton noir
            /*
            Sort Crachouille = null;
            Sort Mordillement = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
            {
                switch (item.Nom)
                {
                    case "Crachouille":
                        Crachouille = item;
                        break;
                    case "Mordillement":
                        Mordillement = item;
                        break;
                }
            }
             while (Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0], 1) == 1)
            {
                if (terrain.DistanceEntreCases(Perso.Position, (Perso.EnnemiLePlusProche(ListEntites)).Position) < 5 && Perso.PA == Perso.PA_MAX)
                {
                    Perso.UtiliserSort(Crachouille, Perso.EnnemiLePlusProche(ListEntites));
                }
            }
            if (terrain.DistanceEntreCases(Perso.Position, (Perso.EnnemiLePlusProche(ListEntites)).Position) == 1)
            {
                Perso.UtiliserSort(Mordillement, Perso.EnnemiLePlusProche(ListEntites));
            }
            */
            #endregion
            #region bouftou
            /*
            Sort MorsureBouftou = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
            {
                switch (item.Nom)
                {
                    case "Morsure du bouftou":
                        MorsureBouftou = item;
                        break;
                }
            }
            while (Perso.PM != 0 && Perso.PA >= MorsureBouftou.CoutPA)
            {
                while (Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0], 1) == 1)
                {
                }

                while (Perso.PeutUtiliserSort(MorsureBouftou, Perso.EnnemiLePlusProche(ListEntites)))
                {
                    Perso.UtiliserSort(MorsureBouftou, Perso.EnnemiLePlusProche(ListEntites));
                }
            }
            */
            #endregion
            #region tournesol
            /*
            Sort racine = null;
            Sort poison = null;
            EntiteInconnu ennemi = Perso.EnnemiLePlusProche(ListEntites);
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
            {
                switch (item.Nom)
                {
                    case "Racine_barbelee":
                        racine = item;
                        break;
                    case "Poison_Sauvage":
                        poison = item;
                        break;
                }
            }

            while(terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) >=8 && Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0], 1) == 1)
            {

            }

            if (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) <= 8)
            {
                Perso.UtiliserSort(racine, Perso.EnnemiLePlusProche(ListEntites));
                Perso.UtiliserSort(poison, Perso.EnnemiLePlusProche(ListEntites));
            }
            Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites));
            */
            #endregion
            #region boufton blanc
            /*
            Sort Bavouille = null;
            Sort Mordillement = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
            {
                switch (item.Nom)
                {
                    case "Bavouille":
                        Bavouille = item;
                        break;
                    case "Mordillement":
                        Mordillement = item;
                        break;
                }
            }
            while (Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0], 1) == 1)
            {
                if (terrain.DistanceEntreCases(Perso.Position, (Perso.EnnemiLePlusProche(ListEntites)).Position) < 5 && Perso.PA == Perso.PA_MAX)
                {
                    Perso.UtiliserSort(Bavouille, Perso.EnnemiLePlusProche(ListEntites));
                }
            }
            if (terrain.DistanceEntreCases(Perso.Position, (Perso.EnnemiLePlusProche(ListEntites)).Position) == 1)
            {
                Perso.UtiliserSort(Mordillement, Perso.EnnemiLePlusProche(ListEntites));
            }
            */
            #endregion
        }
    }
}
