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

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour Résultat.xaml
    /// </summary>
    
        
    public partial class Résultat : Window
    {
        BDService bd = new BDService();
        public Résultat(double seed)
        {
            InitializeComponent();
            bd.selection("SELECT * FROM Parties WHERE Seed="+seed);
            
        }
    }
}
