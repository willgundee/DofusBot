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
    /// Logique d'interaction pour FenetreScript.xaml
    /// </summary>
    public partial class FenetreScript : Window
    {
        MainWindow mW = (Application.Current.Windows.Cast<Window>().First(x => x.GetType() == typeof(MainWindow)) as MainWindow);
        BDService bd = new BDService();
        public FenetreScript()
        {
            InitializeComponent();
            foreach (Script script in mW.Player.LstScripts)
            {
                TabItem onglet = new TabItem();
                onglet.Header = script.Nom;
                onglet.Content = new pageScript(script.Uuid);
                (onglet.Content as pageScript).ctb_main.Text = script.Code;
                tc_Edit.Items.Add(onglet);
            }
            tc_Edit.SelectedIndex = 0;
            if (tc_Edit.Items.Count < 10)
            {
                TabItem onglet = new TabItem();
                onglet.Header = "+";
                tc_Edit.Items.Add(onglet);
            }
        }
        
        private void tc_Edit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.TabControl tab = sender as System.Windows.Controls.TabControl;
            TabItem ti_selected = tab.SelectedItem as TabItem;
            if (ti_selected == null)
                return;
            if (ti_selected.Header.ToString() == "+")
            {
                string codeBase = "EntiteInconnu ennemi = Perso.EnnemiLePlusProche(ListEntites);\nPerso.AvancerVers(ennemi);\nPerso.Attaquer(ennemi);";
                object f = e.Source;
                string nom = "Script" + tc_Edit.Items.Count;
                long id = bd.insertion("INSERT INTO Scripts (contenu, nom, uuid) VALUES ('" + codeBase + "', '" + nom + "', uuid());");
                if (id != 0)
                {
                    if (bd.insertion("INSERT INTO JoueursScripts (idJoueur, idScript) VALUES ((SELECT idJoueur FROM Joueurs WHERE nomUtilisateur ='" + mW.Player.NomUtilisateur + "'), " + id + ");") != 0)
                    {
                        TabItem onglet = ti_selected;
                        string uuid = bd.selection("SELECT uuid FROM Scripts WHERE idScript = " + id + ";").First().First();
                        mW.Player.LstScripts.Add(new Script(new List<string>() { codeBase, nom, uuid }));
                        onglet.Header = nom;
                        onglet.Content = new pageScript(uuid, codeBase);
                        if (tc_Edit.Items.Count < 10)
                        {
                            TabItem ongletPlus = new TabItem();
                            ongletPlus.Header = "+";
                            tc_Edit.Items.Add(ongletPlus);
                        }
                    }
                }

            }
        }
    }
}
