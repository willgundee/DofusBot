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
        public PageEquipement(string TypeEquipement, string NomJoueur)
        {
            InitializeComponent();
            afficherEquipementDispo(TypeEquipement, NomJoueur);

           
        }

        private void  afficherEquipementDispo(string TypeEquipement,string NomJoueur)
        {
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

            foreach (List<string> item in NoImg)
            {
                for (int i= 0; i < Convert.ToInt32(item[1]);i++)
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

            }
        }

        private Image CreateImg(string Noimg)
        {
            Image img = new Image();
            ImageSource path = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/items/200/" + Noimg + ".png"));
            img.Width = 100;
            img.Height = 100;
            img.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            img.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            img.Source = path;
            return img;
        }

    }
}

