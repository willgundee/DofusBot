using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace test
{
    /// <summary>
    /// Logique d'interaction pour PageEquipement.xaml
    /// </summary>
    public partial class PageEquipement : Window
    {
        public BDService bd = new BDService();
        private string TypeEQ;
        public PageEquipement(string TypeEquipement, string NomJoueur)
        {
            TypeEQ = TypeEquipement;
            bool valide;
            InitializeComponent();
            valide = afficherEquipementDispo(TypeEquipement, NomJoueur);
            foreach (Window Page in Application.Current.Windows)
            {
                if (Page.GetType() == typeof(MainWindow))
                {
                    (Page as MainWindow).pgperso.First().validePg = valide;
                }
            }

        }

        private bool afficherEquipementDispo(string TypeEquipement, string NomJoueur)
        {
            bool valide = false;
            List<string>[] NoImg = null;
            int col = 0;
            int row = 0;
            if (TypeEquipement == "Arme")
            {
                NoImg = bd.selection("SELECT e.noImage, je.quantite FROM Equipements e INNER JOIN JoueursEquipements je ON je.idEquipement = e.idEquipement  INNER JOIN Joueurs j ON j.idJoueur = je.idJoueur WHERE idZonePorte IS NOT NULL AND j.nomUtilisateur = '" + NomJoueur + "'");
            }
            else
            {
                NoImg = bd.selection("SELECT e.noImage, je.quantite FROM equipements e INNER JOIN JoueursEquipements je ON je.idEquipement = e.idEquipement INNER JOIN TypesEquipements t ON t.idTypeEquipement = e.idTypeEquipement INNER JOIN Joueurs j ON j.idJoueur = je.idJoueur WHERE j.nomUtilisateur = '" + NomJoueur + "'AND t.nom = '" + TypeEquipement + "'");
            }
            if (NoImg[0][0] == "rien")
            {
                return valide;
            }

            foreach (List<string> item in NoImg)
            {
                for (int i = 0; i < Convert.ToInt32(item[1]); i++)
                {
                    Image img = CreateImg(item[0]);
                    if (col == 5)
                    {
                        col = 0;
                        row++;
                    }
                    Grid.SetRow(img, row);
                    Grid.SetColumn(img, col);
                    col++;
                    grdItem.Children.Add(img);
                }
                valide = true;
            }

            return valide;
        }

        private Image CreateImg(string Noimg)
        {
            Image img = new Image();
            ImageSource path = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/items/200/" + Noimg + ".png"));
            img.Width = 100;
            img.Height = 100;
            img.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            img.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            img.MouseUp += image_MouseUp;
            img.Source = path;
            return img;
        }

        private void image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            foreach (Window Page in Application.Current.Windows)
            {
                if (Page.GetType() == typeof(MainWindow))
                {     
                    switch (TypeEQ)
                    {
                        case "Chapeau":
                            (Page as MainWindow).pgperso.First().imageCasque.Source = (sender as Image).Source;
                            break;
                        case "Cape":
                            (Page as MainWindow).pgperso.First().imageCape.Source = (sender as Image).Source;
                            break;
                        case "Arme":
                            (Page as MainWindow).pgperso.First().imageArme.Source = (sender as Image).Source;
                            break;
                        case "Ceinture":
                            (Page as MainWindow).pgperso.First().imageCeinture.Source = (sender as Image).Source;
                            break;
                        case "Anneau":
                            (Page as MainWindow).pgperso.First().imageAnneau1.Source = (sender as Image).Source;
                            break;
                        case "Botte":
                            (Page as MainWindow).pgperso.First().imageBotte.Source = (sender as Image).Source;
                            break;
                        case "Amulette":
                            (Page as MainWindow).pgperso.First().imageAmulette.Source = (sender as Image).Source;
                            break;
                    }
                }
            }
        }
    }
}

