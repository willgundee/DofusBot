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

    public partial class CreationCompteWindow: Window
    {
        private BDService bd = new BDService();
        public CreationCompteWindow()
        {
            InitializeComponent();

        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

        }

       
    }
}
