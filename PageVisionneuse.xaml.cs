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

            #region Mick/invité;

            InitializeComponent();
            lstScripts = new ObservableCollection<string>();
            lstAdversaire = new ObservableCollection<Entite>();
            lstPerso = new Dictionary<int, string>();
            lstTypeAdver = new ObservableCollection<string>();

            bd = new BDService();
          

           
            
            btnQuitter.Visibility = Visibility.Visible;
         
            dataGrid.ItemsSource = lstAdversaire;

        }

        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            Authentification au = new Authentification();
            au.Show();
            if (System.Windows.Application.Current.Windows.Cast<Window>().FirstOrDefault(x => x.GetType() == typeof(PageVisionneuse)) != null)
                System.Windows.Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(PageVisionneuse)).Close();

        }
        #endregion


    }
}
