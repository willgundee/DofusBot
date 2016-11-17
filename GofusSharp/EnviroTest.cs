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
            Sort Bavouille = null;
            Sort Mordillement = null;
            EntiteInconnu ennemi = null;
            Perso.PeutUtiliserSort
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

            }
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
