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


            /*Sort CoupChafer = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
                switch (item.Nom)
                {
                    case "Coup du chafer":
                        CoupChafer = item;
                        break;
                }
            if (terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position).Count == 6)
                Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites), 1);
            else
            {
                while (Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0]) == 1)
                    while (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) == 1)
                        Perso.UtiliserSort(CoupChafer, Perso.EnnemiLePlusProche(ListEntites));
                Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites));
            }*/


            /*
            Sort effleurement = null;
            Sort herbeSauvage = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
                switch (item.Nom)
                {
                    case "Effleurement":
                        effleurement = item;
                        break;
                    case "Herbe sauvage":
                        herbeSauvage = item;
                        break;
                }
            if (terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position).Count == 5)
                Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites), 1);
            else
            {
                while (Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0]) == 1)
                    while (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) == 1)
                    {
                        Perso.UtiliserSort(effleurement, Perso.EnnemiLePlusProche(ListEntites));
                        Perso.UtiliserSort(herbeSauvage, Perso.EnnemiLePlusProche(ListEntites));
                    }
                Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites));
            }
            */

            /*Sort embrochement = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
                switch (item.Nom)
                {
                    case "Embrochement":
                        embrochement = item;
                        break;
                }
            while(Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0]) == 1)
                while (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) == 1)
                    Perso.UtiliserSort(embrochement, Perso.EnnemiLePlusProche(ListEntites));
*/

            /*Sort beco = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
                switch (item.Nom)
                {
                    case "Béco du tofu":
                        beco = item;
                        break;
                }
            Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0]);
            if (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) == 1)
                Perso.UtiliserSort(beco, Perso.EnnemiLePlusProche(ListEntites));
            Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites));
            */
            /*Sort poison = null;
        Sort frappe = null;
        foreach (Sort item in Perso.ClasseEntite.ListSorts)
            switch (item.Nom)
            {
                case "Frappe":
                    frappe = item;
                    break;
                case "Poison Sauvage":
                    poison = item;
                    break;
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
                Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0]);
                Perso.UtiliserSort(frappe, Perso.EnnemiLePlusProche(ListEntites));
                Perso.UtiliserSort(frappe, Perso.EnnemiLePlusProche(ListEntites));
            }
        }*/

            /*
            Sort MorsureBouftou = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
                switch (item.Nom)
                {
                    case "Morsure du bouftou":
                        MorsureBouftou = item;
                        break;
                }
            Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0]);
            if (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) == 1)
                Perso.UtiliserSort(MorsureBouftou, Perso.EnnemiLePlusProche(ListEntites));
                */
            /*Sort racine = null;
            Sort poison = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
                switch (item.Nom)
                {
                    case "Racine barbelée":
                        racine = item;
                        break;
                    case "Poison Sauvage":
                        poison = item;
                        break;
                }

            if (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) == 9)
                Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0], 1);
            if (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) < 8)
            {
                Perso.UtiliserSort(racine, Perso.EnnemiLePlusProche(ListEntites));
                Perso.UtiliserSort(poison, Perso.EnnemiLePlusProche(ListEntites));
            }
            Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites));

            Sort Crachouille = null;
            Sort Mordillement = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
                switch (item.Nom)
                {
                    case "Crachouille":
                        Crachouille = item;
                        break;
                    case "Mordillement":
                        Mordillement = item;
                        break;
                }
            while (Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position)[0], 1) == 1)
            {
                if (terrain.DistanceEntreCases(Perso.Position, (Perso.EnnemiLePlusProche(ListEntites)).Position) == 1)
                    Perso.UtiliserSort(Mordillement, Perso.EnnemiLePlusProche(ListEntites));
                if (terrain.DistanceEntreCases(Perso.Position, (Perso.EnnemiLePlusProche(ListEntites)).Position) < 5)
                    Perso.UtiliserSort(Crachouille, Perso.EnnemiLePlusProche(ListEntites));
            }*/

            /* EntiteInconnu ennemi = Perso.EnnemiLePlusProche(ListEntites);
             Perso.AvancerVers(ennemi);
             Perso.Attaquer(ennemi);*/
            //Perso.UtiliserSort(Sort.nom_sort.pression/*Le nom du sort*/,ennemi);
            /*Sort racine = null;
            Sort poison = null;
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
                switch (item.Nom)
                {
                    case "Racine barbelée":
                        racine = item;
                        break;
                    case "Poison Sauvage":
                        poison = item;
                        break;
                }
            while (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) > 8)
                    Perso.AvancerVers(Perso.EnnemiLePlusProche(ListEntites), 1);

            if (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) == 9)
                Perso.AvancerVers(Perso.EnnemiLePlusProche(ListEntites), 1);
            if (terrain.DistanceEntreCases(Perso.Position, Perso.EnnemiLePlusProche(ListEntites).Position) <= 8)
            {
                Perso.UtiliserSort(racine, Perso.EnnemiLePlusProche(ListEntites));
                Perso.UtiliserSort(poison, Perso.EnnemiLePlusProche(ListEntites));
            }
            Perso.SEloignerDe(Perso.EnnemiLePlusProche(ListEntites));*/


            /*Sort Bavouille = null;
            Sort Mordillement = null;
            EntiteInconnu ennemi = null;
            //Perso.PeutUtiliserSort
            foreach (EntiteInconnu entite in ListEntites)
                if (entite.Equipe != Perso.Equipe)
                {
                    ennemi = entite;
                    break;
                }
            foreach (Sort item in Perso.ClasseEntite.ListSorts)
                switch (item.Nom)
                {
                    case "Bavouille":
                        Bavouille = item;
                        break;
                    case "Mordillement":
                        Mordillement = item;
                        break;
                }
            while (Perso.AvancerVers(Perso.EnnemiLePlusProche(ListEntites),1) == 1)
            {//tant que tu peut avancer
                if (terrain.DistanceEntreCases(Perso.Position, (Perso.EnnemiLePlusProche(ListEntites)).Position) == 1)
                    Perso.UtiliserSort(Mordillement, Perso.EnnemiLePlusProche(ListEntites));
                if (terrain.DistanceEntreCases(Perso.Position, (Perso.EnnemiLePlusProche(ListEntites)).Position) < 5)
                {
                    Perso.UtiliserSort(Bavouille, Perso.EnnemiLePlusProche(ListEntites));
                }

            }*/
            /*
                        if (terrain.DistanceEntreCases(Perso.Position, ennemi.Position) > 1)
                        {
                            int result = 1;
                            while (result != 0 && result != -1)
                            {
                                result = Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, ennemi.Position)[0], 1);
                                if (terrain.DistanceEntreCases(Perso.Position, ennemi.Position) < 5)
                                    if (Perso.PA >= 6)
                                        Perso.UtiliserSort(Bavouille, ennemi);
                            }
                        }

                        if (terrain.DistanceEntreCases(Perso.Position, ennemi.Position) == 1)
                            Perso.UtiliserSort(Mordillement, ennemi);*/
            /* EntiteInconnu ennemi = null;
             foreach (EntiteInconnu entite in ListEntites)
             {
                 if (entite.Equipe != Perso.Equipe)
                 {
                     ennemi = entite;
                     break;
                 }
             }
             if (terrain.DistanceEntreCases(Perso.Position, ennemi.Position) > 1)
             {
                 int result = 1;
                 while (result != 0 && result != -1)
                 {
                     result = Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, ennemi.Position)[0], 1);
                 }
             }
             while (Perso.UtiliserSort(Sort.nom_sort.pression, ennemi))
             {

             }*/



        }
    }
}
