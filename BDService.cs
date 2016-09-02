using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace test
{
    //test comment the real test
        public class cBDService
        {
            private MySqlConnection BDInterne;
            private string serveur = "localhost";
            private string baseDonnee = "BDExemple";
            private string utilisateur = "root";
            private string motPasse = "";

            public cBDService()
            {
                try
                {
                    string connexionString = "server=" + serveur + ";database=" + baseDonnee + ";uid=" + utilisateur + ";password=" + motPasse;

                    BDInterne = new MySqlConnection(connexionString);

                    MessageBox.Show("Connexion OK");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Connexion defectueuse : " + e.Message);
                }


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
                }

                return retVal;
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
                    listEnregistrement[0].Add("");
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
