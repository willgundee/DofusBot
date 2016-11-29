using GofusSharp;

namespace test
{
    class EnviroTest
    {
        //        EntiteInconnu ennemi = null;
        //foreach (EntiteInconnu entite in ListEntites)
        //{
        //    if (entite.Equipe != Perso.Equipe)
        //    {
        //        ennemi = entite;
        //        break;
        //    }
        //}
        //if (terrain.DistanceEntreCases(Perso.Position, ennemi.Position) > 1)
        //{
        //    int result = 1;
        //    while (result != 0 && result != -1)
        //    {
        //        result = Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, ennemi.Position)[0], 1);
        //    }
        //}
        //while(Perso.UtiliserSort(Sort.nom_sort.pression, ennemi))
        //{

        //}

        public void Execution(Terrain terrain, Entite Perso, Liste<EntiteInconnu> ListEntites)
        {
            #region chafer archer

            #endregion
            #region krurorre

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
            // Perso.CasesPourUtiliserSort
            if (Debug.TourCourant() % 3 == 0)
                Perso.UtiliserSort(Euphorie, Perso);
            while (Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0]) == 1)
                while (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) == 1)
                    Perso.UtiliserSort(Empalement, Perso.EnnemiLePlusProche(ListEntites));
                    */
            #endregion
            #region chafer
            /*
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
            if (terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position).Count == 6)
            {
                Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites), 1);
            }
            else
            {
                while (Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0]) == 1)
                {
                    while (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) == 1)
                    {
                        Perso.UtiliserSort(CoupChafer, Perso.EnnemiLePlusProche(ListEntites));
                    }
                }
                Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites));
            }
            */
            #endregion
            #region pissenlit diabolique
            
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
            if (terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position).Count == 5)
            {
                Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites), 1);
            }
            else
            {
                while (Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0]) == 1)
                {
                    while (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) == 1)
                    {
                        Perso.UtiliserSort(effleurement, Perso.EnnemiLePlusProche(ListEntites));
                        Perso.UtiliserSort(herbeSauvage, Perso.EnnemiLePlusProche(ListEntites));
                    }
                }
                Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites));
            }

            #endregion
            #region sanglier
            
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
                Debug.Log(embrochement.CoutPA);
                Debug.Log(Perso.PM);
                Debug.Log(Perso.PA);
                while ( Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0], 1) == 1)
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
                while(Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0],1)==1);
                if (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) == 1)
                {
                    Perso.UtiliserSort(MorsureBouftou, Perso.EnnemiLePlusProche(ListEntites));
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
