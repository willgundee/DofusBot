using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour DescItem.xaml
    /// </summary>
    public partial class DescItem : UserControl
    {
        public DescItem(Equipement item)
        {
            InitializeComponent();

            ObservableCollection<string> LstStats;
            ObservableCollection<string> LstConds;
            ObservableCollection<string> LstCaras;

            LstStats = new ObservableCollection<string>();
            LstConds = new ObservableCollection<string>();
            LstCaras = new ObservableCollection<string>();
            // lie les informations de l'équipements au listes et au endroit approprié
            lbxStatsDesc.ItemsSource = LstStats;
            lbxCondDesc.ItemsSource = LstConds;
            lbxCaraDesc.ItemsSource = LstCaras;

            txtDesc2.Text = "Catégorie : " + item.Type + Environment.NewLine + item.Desc;
            imgDesc.Source = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/items/200/" + item.NoImg + ".png"));
            lblNomItem.Content = item.Nom;
            if (item.EstArme)
            {// si c'est un arme
                tbCaraDesc.Visibility = Visibility.Visible;
                foreach (Effet effet in item.LstEffets)
                    LstStats.Add(effet.NomSimplifier + " : " + effet.DmgMin + " à " + effet.DmgMax);
                LstCaras.Add("Pa requis : " + item.Pa);
                LstCaras.Add("Portée : " + item.ZonePortee.Nom + " de " + (item.ZonePortee.PorteeMax == item.ZonePortee.PorteeMin ? item.ZonePortee.PorteeMax.ToString() : item.ZonePortee.PorteeMin.ToString() + " à " + item.ZonePortee.PorteeMax.ToString()));
            }
            else// sinon
                tbCaraDesc.Visibility = Visibility.Hidden;
            //remplis les stats
            foreach (Statistique stat in item.LstStatistiques)
                LstStats.Add(stat.NomSimple + " : " + stat.Valeur.ToString());
            //rempli les conditions
            foreach (Condition cond in item.LstConditions)
                if (cond.Stat.Nom == Statistique.element.experience)
                {
                    lblivItem.Content = "Niv." + cond.Stat.toLevel().ToString();
                    LstConds.Add("Niveau requis : " + cond.Stat.toLevel().ToString());
                }
                else
                    LstConds.Add(cond.Stat.NomSimple + " " + cond.Signe + "  " + cond.Stat.Valeur.ToString());
        }
    }
}
