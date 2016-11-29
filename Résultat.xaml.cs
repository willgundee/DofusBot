using Newtonsoft.Json;
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
    public partial class Resultat : Window
    {
        BDService bd = new BDService();
        private long IdPartie { get; set; }

        public Resultat(long idPartie)
        {
            InitializeComponent();
            IdPartie = idPartie;
            string selectPartie = "SELECT * FROM Parties WHERE idPartie = " + idPartie;
            List<string>[] lstPartieBd = bd.selection(selectPartie);
            if (lstPartieBd[0][0] != "rien")
            {
                double gain;
                double exp;
                foreach (List<string> p in lstPartieBd)
                {

                    List<Entite> lstAtt = new List<Entite>();
                    List<Entite> lstDef = new List<Entite>();


                    if (p[3] != "infoEntites")
                    {
                        List<List<Entite>> infoJson = JsonConvert.DeserializeObject<List<List<Entite>>>(p[3]);
                        lstAtt = infoJson[0];
                        lstDef = infoJson[1];

                        exp = lstDef[0].Niveau * lstAtt[0].Niveau * 32;

                        if (p[4] == "")
                        {
                            NomPersoG.Content = lstAtt[0].Nom;
                            NomPersoP.Content = lstDef[0].Nom;

                            if (lstAtt[0].EstPersonnage)
                            {
                                BitmapImage path = new BitmapImage(new Uri("../resources/GofusSharp/" + lstAtt[0].ClasseEntite.Nom + ".png", UriKind.Relative));
                                imgG.Source = path;
                                lblKamasG.Content += " 1 $";
                                lblexpG.Content += " 1 XP";
                                barexpG(lstAtt[0]);
                            }
                            else
                            {
                                BitmapImage path = new BitmapImage(new Uri("../resources/" + lstAtt[0].ClasseEntite.Nom + ".jpg", UriKind.Relative));
                                imgG.Source = path;
                                CacherG();
                            }
                            if (lstDef[0].EstPersonnage)
                            {
                                BitmapImage path = new BitmapImage(new Uri("../resources/GofusSharp/" + lstDef[0].ClasseEntite.Nom + ".png", UriKind.Relative));
                                imgP.Source = path;
                                lblKamasP.Content += " 1 $";
                                lblexpP.Content += " 1 XP";
                                barexpP(lstDef[0]);
                            }
                            else
                            {
                                BitmapImage path = new BitmapImage(new Uri("../resources/" + lstDef[0].ClasseEntite.Nom + ".jpg", UriKind.Relative));
                                imgP.Source = path;
                                CacherP();
                            }

                        }
                        else if (p[4] == "True")
                        {
                            NomPersoG.Content = lstAtt[0].Nom;
                            NomPersoP.Content = lstDef[0].Nom;
                            gain = lstDef[0].Niveau * 100;

                            if (lstAtt[0].EstPersonnage)
                            {
                                BitmapImage path = new BitmapImage(new Uri("../resources/GofusSharp/" + lstAtt[0].ClasseEntite.Nom + ".png", UriKind.Relative));
                                imgG.Source = path;
                                lblKamasG.Content += " " + gain + "$";
                                lblexpG.Content += " " + exp;
                                barexpG(lstAtt[0]);
                            }
                            else
                            {
                                BitmapImage path = new BitmapImage(new Uri("../resources/" + lstAtt[0].ClasseEntite.Nom + ".jpg", UriKind.Relative));
                                imgG.Source = path;
                                CacherG();
                            }
                            if (lstDef[0].EstPersonnage)
                            {
                                BitmapImage path = new BitmapImage(new Uri("../resources/GofusSharp/" + lstDef[0].ClasseEntite.Nom + ".png", UriKind.Relative));
                                imgP.Source = path;
                                lblKamasP.Content += " " + (gain / 10) + "$";
                                lblexpP.Content += " " + exp / 10;
                                barexpP(lstDef[0]);
                            }
                            else
                            {
                                BitmapImage path = new BitmapImage(new Uri("../resources/" + lstDef[0].ClasseEntite.Nom + ".jpg", UriKind.Relative));
                                imgP.Source = path;
                                CacherP();
                            }
                        }
                        else
                        {
                            NomPersoP.Content = lstAtt[0].Nom;
                            NomPersoG.Content = lstDef[0].Nom;
                            gain = lstAtt[0].Niveau * 100;

                            if (lstDef[0].EstPersonnage)
                            {
                                BitmapImage path = new BitmapImage(new Uri("../resources/GofusSharp/" + lstDef[0].ClasseEntite.Nom + ".png", UriKind.Relative));
                                imgG.Source = path;
                                lblKamasG.Content += " " + gain + "$";
                                lblexpG.Content += " " + exp;
                                barexpG(lstDef[0]);
                            }
                            else
                            {
                                BitmapImage path = new BitmapImage(new Uri("../resources/" + lstDef[0].ClasseEntite.Nom + ".jpg", UriKind.Relative));
                                imgG.Source = path;
                                CacherG();
                            }
                            if (lstAtt[0].EstPersonnage)
                            {
                                BitmapImage path = new BitmapImage(new Uri("../resources/GofusSharp/" + lstAtt[0].ClasseEntite.Nom + ".png", UriKind.Relative));
                                imgP.Source = path;
                                lblKamasP.Content += " " + (gain / 10) + "$";
                                lblexpP.Content += " " + exp / 10;
                                barexpP(lstAtt[0]);
                            }
                            else
                            {
                                BitmapImage path = new BitmapImage(new Uri("../resources/" + lstAtt[0].ClasseEntite.Nom + ".jpg", UriKind.Relative));
                                imgP.Source = path;
                                CacherP();
                            }
                        }
                    }
                }
            }
        }


        public void barexpG(Entite ent)
        {
            if (ent.Niveau < 200)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    pgbExp.Maximum = Statistique.dictLvl[ent.Niveau + 1];
                    pgbExp.Minimum = Statistique.dictLvl[ent.Niveau];
                    pgbExp.ToolTip = ent.LstStats.First(x => x.Nom == Statistique.element.experience).Valeur.ToString() + " sur " + Statistique.dictLvl[ent.LstStats.First(x => x.Nom == Statistique.element.experience).toLevel() + 1].ToString() + " exp";
                }));

            }
            else
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    pgbExp.Maximum = ent.LstStats.First(x => x.Nom == Statistique.element.experience).Valeur;
                    pgbExp.Minimum = ent.LstStats.First(x => x.Nom == Statistique.element.experience).Valeur;
                    pgbExp.ToolTip = "IT'S OVER 9000!!!";
                }));
            }
            Dispatcher.Invoke(new Action(() =>
            {
                pgbExp.Value = ent.LstStats.First(x => x.Nom == Statistique.element.experience).Valeur;
            }));
        }

        public void barexpP(Entite ent)
        {
            if (ent.Niveau < 200)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    pgbExpP.Maximum = Statistique.dictLvl[ent.Niveau + 1];
                    pgbExpP.Minimum = Statistique.dictLvl[ent.Niveau];
                    pgbExpP.ToolTip = ent.LstStats.First(x => x.Nom == Statistique.element.experience).Valeur.ToString() + " sur " + Statistique.dictLvl[ent.LstStats.First(x => x.Nom == Statistique.element.experience).toLevel() + 1].ToString() + " exp";
                }));

            }
            else
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    pgbExpP.Maximum = ent.LstStats.First(x => x.Nom == Statistique.element.experience).Valeur;
                    pgbExpP.Minimum = ent.LstStats.First(x => x.Nom == Statistique.element.experience).Valeur;
                    pgbExpP.ToolTip = "IT'S OVER 9000!!!";
                }));
            }
            Dispatcher.Invoke(new Action(() =>
            {
                pgbExpP.Value = ent.LstStats.First(x => x.Nom == Statistique.element.experience).Valeur;
            }));
        }



        public void CacherG()
        {
            GainG.Visibility = Visibility.Hidden;
            lblexpG.Visibility = Visibility.Hidden;
            pgbExp.Visibility = Visibility.Hidden;
            lblLevelEntite.Visibility = Visibility.Hidden;
            lblKamasG.Visibility = Visibility.Hidden;

        }
        public void CacherP()
        {
            GainP.Visibility = Visibility.Hidden;
            lblexpP.Visibility = Visibility.Hidden;
            pgbExpP.Visibility = Visibility.Hidden;
            lblLevelEntiteP.Visibility = Visibility.Hidden;
            lblKamasP.Visibility = Visibility.Hidden;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Application.Current.Windows.Cast<Window>().Where(x => x is GofusSharp.Combat).FirstOrDefault(x => (x as GofusSharp.Combat).IdPartie == IdPartie) != null)
            {
                Application.Current.Windows.Cast<Window>().Where(x => x is GofusSharp.Combat).FirstOrDefault(x => (x as GofusSharp.Combat).IdPartie == IdPartie).Close();
            }
        }

 
    }
}
