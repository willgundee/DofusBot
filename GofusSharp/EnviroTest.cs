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

        public void Execution(Terrain terrain, Personnage Perso, Liste<EntiteInconnu> ListEntites)
        {
            //Mettre l'ennemi le plus proche dans une variable
            EntiteInconnu ennemi = Perso.EnnemiLePlusProche(ListEntites);
            //Mettre la premiere case qui constitue le chemin entre moi et l'ennemi
            Case caseChemin = terrain.CheminEntreCases(Perso.Position, ennemi.Position)[0];
            //avancer vers cette case si je peux
            while (Perso.AvancerVers(caseChemin, 1) == 1) {
                caseChemin = terrain.CheminEntreCases(Perso.Position, ennemi.Position)[0];
            }
            //tant que mon personnage est capable d'attaquer l'ennemi
            while (Perso.PeutAttaquer(ennemi)) {
                //dire a mon personnage d'attaquer
                Perso.Attaquer(ennemi);
            }
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
