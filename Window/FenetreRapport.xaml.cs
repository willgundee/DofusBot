using System.Collections.Generic;
using System.Windows;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour FenetreRapport.xaml
    /// </summary>
    public partial class FenetreRapport : Window
    {

        Dictionary<int, string> typeRapport;


        BDService bdInsert;

        private int idJoueur;

        public FenetreRapport(int id)
        {
            InitializeComponent();

            idJoueur = id;
            bdInsert = new BDService();
            typeRapport = new Dictionary<int, string>();
            typeRapport.Add(1, "Rédiger une plainte");
            typeRapport.Add(2, "Donner une suggestion");
            typeRapport.Add(3, "Signaler un problème");
            typeRapport.Add(4, "Autre");

            cboType.ItemsSource = typeRapport;
            cboType.SelectedIndex = 0;
        }

        private void btnEnvoyer_Click(object sender, RoutedEventArgs e)
        {
            if (txtCommentaire.Text != "")
            {
                string message = txtCommentaire.Text;
                string titre = txtTitre.Text;

                message = message.Replace("'", @"\'");
                titre = titre.Replace("'", @"\'");

                int typeRapportText = cboType.SelectedIndex + 1;
                long envoie = 0;

                string inser = "INSERT INTO Rapports(idJoueur,temps,contenu,uuid,idTypeRapport,titre)VALUES(" + idJoueur + ",NOW(),'" +
                                    message + "',UUID()," + typeRapportText + ", '" + titre + "')";
                envoie = bdInsert.insertion(inser);
                if (envoie == -1)
                {
                    System.Windows.MessageBox.Show("Une erreur s'est produite lors de l'envoie du rapport.", "Erreur");
                }
                else
                {
                    System.Windows.MessageBox.Show("Merci de votre collaboration pour le projet Gofus.", "Remerciments");
                }

            }
            else
            {
                System.Windows.MessageBox.Show("Veuillez entrer un commentaire avant d'envoyer ! ", "Erreur");
            }


        }
    }
}
