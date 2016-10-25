using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace test
{
    public class BDService
    {   /*
        User: gofusprog
        Pass: GP994433
         */

        // Pour éviter les problèmes causé par Reader qui met la connection en mode readonly.
        private MySqlConnection BDChat;
        private MySqlConnection BDSelect;
        private MySqlConnection BDInsert;
        private MySqlConnection BDUpdate;

        private string serveur = "420.cstj.qc.ca";
        private string baseDonnee = "420.5a5.a16_gofusprog";
        private string utilisateur = "gofusprog";
        private string motPasse = "GP994433";

        public BDService()
        {
            try
            {
                string connexionString = "server=" + serveur + ";database=" + baseDonnee + ";uid=" + utilisateur + ";password=" + motPasse;


                BDSelect = new MySqlConnection(connexionString);

                BDInsert = new MySqlConnection(connexionString);

                BDChat = new MySqlConnection(connexionString);

                BDUpdate = new MySqlConnection(connexionString);

               
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
                if (ouvrirConnexionINSERT())
                {
                    MySqlCommand cmd = new MySqlCommand(req, BDInsert);

                    cmd.ExecuteNonQuery();
                    fermerConnexionINSERT();
                    retVal = cmd.LastInsertedId;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur d'insertion :" + e.Message);
                fermerConnexionINSERT();
                return -1;
            }
            //aa
            return retVal;
        }

        public bool Update(string req)
        {

            /*
            
    try  
    {  
        //This is my connection string i have assigned the database file address path  
        string MyConnection2 = "datasource=localhost;port=3307;username=root;password=root";  
        //This is my update query in which i am taking input from the user through windows forms and update the record.  
        string Query = "update student.studentinfo set idStudentInfo='" + this.IdTextBox.Text + "',Name='" + this.NameTextBox.Text + "',Father_Name='" + this.FnameTextBox.Text + "',Age='" + this.AgeTextBox.Text + "',Semester='" + this.SemesterTextBox.Text + "' where idStudentInfo='" + this.IdTextBox.Text + "';";  
        //This is  MySqlConnection here i have created the object and pass my connection string.  
        MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);  
        MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);  
        MySqlDataReader MyReader2;  
        MyConn2.Open();  
        MyReader2 = MyCommand2.ExecuteReader();  
        MessageBox.Show("Data Updated");  
        while (MyReader2.Read())  
        {  
        }  
        MyConn2.Close();//Connection closed here  
    }  
    catch (Exception ex)  
    {   
        MessageBox.Show(ex.Message);  
    } 
            
            */
            try
            {
                if (ouvrirConnexionUPDATE())
                {
                    MySqlCommand cmd = new MySqlCommand(req, BDUpdate);

                 if (cmd.ExecuteNonQuery() == 1)
                {
                        return true;
                }
                else
                {
                    MessageBox.Show("Erreur de mise à jour ! ");
                }
                    fermerConnexionUPDATE();
                    return false;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                fermerConnexionUPDATE();
                return false;
            }

            return true;
        }



        public DataSet selectionChat(string req)
        {
            DataSet ds = new DataSet();

            try
            {
                if (ouvrirConnexionCHAT())
                {
                    MySqlDataAdapter adapteur = new MySqlDataAdapter();
                    adapteur.SelectCommand = new MySqlCommand(req, BDSelect);

                    adapteur.Fill(ds);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur de récuperation : {0}", e.Message);
                ds = null;
            }
            finally
            {
                fermerConnexionCHAT();
            }
            return ds;
        }






        public List<string>[] selection(string req)
        {
            List<string> infoBrut = new List<string>();
            int nbLigne = 0;
            int nbChamps = 0;

            try
            {
                if (ouvrirConnexionSELECT())
                {
                    MySqlCommand cmd = new MySqlCommand(req, BDSelect);
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
                    fermerConnexionSELECT();
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

        private bool ouvrirConnexionSELECT()
        {
            try
            {
                BDSelect.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool fermerConnexionSELECT()
        {
            try
            {
                BDSelect.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ouvrirConnexionCHAT()
        {
            try
            {
                BDChat.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool fermerConnexionCHAT()
        {
            try
            {
                BDChat.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }


        private bool ouvrirConnexionUPDATE()
        {
            try
            {
                BDUpdate.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool fermerConnexionUPDATE()
        {
            try
            {
                BDUpdate.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }








        private bool ouvrirConnexionINSERT()
        {
            try
            {
                BDInsert.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool fermerConnexionINSERT()
        {
            try
            {
                BDInsert.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
