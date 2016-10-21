﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

namespace test
{
    public class BDService
    {   /*
        User: gofusprog
        Pass: GP994433
         */
        private MySqlConnection BDInterne;
        private string serveur = "420.cstj.qc.ca";
        private string baseDonnee = "420.5a5.a16_gofusprog";
        private string utilisateur = "gofusprog";
        private string motPasse = "GP994433";

        public BDService()
        {
            try
            {
                string connexionString = "server=" + serveur + ";database=" + baseDonnee + ";uid=" + utilisateur + ";password=" + motPasse;

                BDInterne = new MySqlConnection(connexionString);

                // MessageBox.Show("Connexion OK");
            }
            catch (Exception e)
            {
                MessageBox.Show("Connexion defectueuse : " + e.Message);
            }


        }
        private string addCommit(string query)
        { // TODO: à arranger
            string rep = ((query.IndexOf(';') == -1) ? query.Insert(query.Length - 1, " ; COMMIT;") : query.Insert(query.Length - 1, " COMMIT;"));
            return rep;
        }
        public long insertion(string req)
        {
            long retVal = 0;
            try
            {
                if (ouvrirConnexion())
                {
                    MySqlCommand cmd = new MySqlCommand(req, BDInterne);

                    cmd.ExecuteNonQuery();
                    fermerConnexion();
                    retVal = cmd.LastInsertedId;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur d'insertion :" + e.Message);
                fermerConnexion();
                return -1;
            }
            //aa
            return retVal;
        }

        public bool Update(string req)
        {
            try
            {
                if (ouvrirConnexion())
                {
                    MySqlCommand cmd = new MySqlCommand(req, BDInterne);

                 if (cmd.ExecuteNonQuery() == 1)
                {
                        return true;
                }
                else
                {
                    MessageBox.Show("Erreur de mise à jour ! ");
                }
                    fermerConnexion();
                    return false;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                fermerConnexion();
                return false;
            }

            return true;
        }



        public List<string>[] selection(string req)
        {
            List<string> infoBrut = new List<string>();
            int nbLigne = 0;
            int nbChamps = 0;

            try
            {
                if (ouvrirConnexion())
                {
                    MySqlCommand cmd = new MySqlCommand(req, BDInterne);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    nbChamps = dataReader.FieldCount;
                    while (dataReader.Read())
                    {
                        // On a l'enregistrement courant qui contient nbChamps
                        for (int i = 0; i < nbChamps; i++)
                        {
                            infoBrut.Add(dataReader[i].ToString());
                        }
                        nbLigne++;
                    }

                    dataReader.Close();
                    fermerConnexion();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur de selection : {0}", e.Message);
            }

            List<string>[] listEnregistrement;

            if (nbLigne == 0)
            {
                listEnregistrement = new List<string>[1];
                listEnregistrement[0] = new List<string>();
                listEnregistrement[0].Add("rien");
            }
            else
            {
                listEnregistrement = new List<string>[nbLigne];
                for (int i = 0; i < nbLigne; i++)
                {
                    listEnregistrement[i] = new List<string>();
                    for (int j = 0; j < nbChamps; j++)
                    {
                        int indice = j + (i * nbChamps);
                        listEnregistrement[i].Add(infoBrut[indice]);
                    }
                }
            }
            return listEnregistrement;
        }

        private bool ouvrirConnexion()
        {
            try
            {
                BDInterne.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool fermerConnexion()
        {
            try
            {
                BDInterne.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
