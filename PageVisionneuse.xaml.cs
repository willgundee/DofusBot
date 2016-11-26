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
using System.Windows.Shapes;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour PageVisionneuse.xaml
    /// </summary>




    public partial class PageVisionneuse : Window
    {
        public BDService bd;
        public ObservableCollection<string> lstScripts;
        public ObservableCollection<string> lstTypeAdver;
        public Dictionary<int, string> lstPerso;
        public ObservableCollection<Entite> lstAdversaire;
        public PageVisionneuse()
        {
            InitializeComponent();
            item.Items.Add(new pageArchive());
        }

        private void btnAtt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
