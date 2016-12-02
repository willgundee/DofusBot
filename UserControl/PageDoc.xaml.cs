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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour PageDoc.xaml
    /// </summary>
    public partial class PageDoc : UserControl
    {/// <summary>
    /// Constructeur de base de la page documentation
    /// </summary>
        public PageDoc()
        {
            InitializeComponent();
            Dispatcher.Invoke(new Action(() =>
            {
                //appel la page de documentation Gofus#
                PgGofusSharp.Content = new DocGofusSharp();
                //appel la page de documentation des sorts
                PGSort.Content = new pageSort();
                //appel la page de documentation des types d'armes
                pgArme.Content = new PageArme();
                //appel la page de documentation des monstres
                PgBestiaire.Content = new PageBestiaire();

            }));

        }
    }
}
