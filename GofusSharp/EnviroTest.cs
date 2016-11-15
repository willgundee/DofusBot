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
            foreach (EntiteInconnu entite in ListEntites)
            {
                if (entite.Equipe != Perso.Equipe)
                {
                    ennemi = entite;
                    break;
                }
            }
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
            while (terrain.DistanceEntreCases(Perso.Position, ennemi.Position) > 1)
            {
                Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, ennemi.Position)[0], 1);
                if (terrain.DistanceEntreCases(Perso.Position, ennemi.Position) < 5) 
                    Perso.UtiliserSort(Bavouille, ennemi.Position);

            }

            if (terrain.DistanceEntreCases(Perso.Position, ennemi.Position) == 1)
                Perso.UtiliserSort(Mordillement, ennemi.Position);/*
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
            while (terrain.DistanceEntreCases(Perso.Position, Perso.EnemiLePlusProche(ListEntites).Position) != 1)
            {
                Perso.AvancerVers(terrain.CheminEntreCases(Perso.Position, Perso.EnemiLePlusProche(ListEntites).Position)[0], 1);
                if (terrain.DistanceEntreCases(Perso.Position, Perso.EnemiLePlusProche(ListEntites).Position) > 1 || terrain.DistanceEntreCases(Perso.Position, Perso.EnemiLePlusProche(ListEntites).Position) < 5)
                    Perso.UtiliserSort(Bavouille, Perso.EnemiLePlusProche(ListEntites).Position);

            }

            if (terrain.DistanceEntreCases(Perso.Position, Perso.EnemiLePlusProche(ListEntites).Position) == 1)
                Perso.UtiliserSort(Mordillement, Perso.EnemiLePlusProche(ListEntites).Position);
                */

        }
    }
}
