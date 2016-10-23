using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace test
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

            lbxStatsDesc.ItemsSource = LstStats;
            lbxCondDesc.ItemsSource = LstConds;
            lbxCaraDesc.ItemsSource = LstCaras;

            imgDesc.Visibility = Visibility.Visible;
            lblNomItem.Visibility = Visibility.Visible;
            lblivItem.Visibility = Visibility.Visible;
            tabControlStatsDesc.Visibility = Visibility.Visible;
            scrDesc.Visibility = Visibility.Visible;

            txtDesc2.Text = "Catégorie : " + item.Type + Environment.NewLine + item.Desc;
            imgDesc.Source = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/items/200/" + item.NoImg + ".png"));
            lblNomItem.Text = item.Nom;
            if (item.EstArme)
            {
                tbCaraDesc.Visibility = Visibility.Visible;
                foreach (Effet effet in item.LstEffets)
                    LstStats.Add(effet.NomSimplifier + " : " + effet.DmgMin + " à " + effet.DmgMax);
                LstCaras.Add("Pa requis : " + item.Pa);
            }
            else
                tbCaraDesc.Visibility = Visibility.Hidden;

            foreach (Statistique stat in item.LstStatistiques)
                LstStats.Add(stat.NomSimple + " : " + stat.Valeur.ToString());

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
