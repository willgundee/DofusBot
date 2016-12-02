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
using System.Threading;

namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour DocGofusSharp.xaml
    /// </summary>
    public partial class DocGofusSharp : UserControl
    {
        BDService BD = new BDService();


        public DocGofusSharp()
        {
            InitializeComponent();
            Thread init = new Thread(() =>
            {
                List<string>[] classes = BD.selection("SELECT * FROM ClassesGofus");
                Dispatcher.Invoke(new Action(() =>
                {
                    TreeView tv_classes = new TreeView();
                }));
                Dispatcher.Invoke(new Action(() => { 
                foreach (List<string> classe in classes)
                {
                    TreeViewItem tvi_classe = new TreeViewItem();
                    tvi_classe.Header = classe[1];
                    Label nomClasse = new Label();
                    nomClasse.Content = classe[2];
                    tvi_classe.Items.Add(nomClasse);
                    List<string>[] proprietes = BD.selection("SELECT idType, nom, description FROM ProprietesGofus WHERE idClasseGofus = " + classe[0]);
                    if (proprietes[0].Count() > 1)
                    {
                        TreeViewItem tvi_proprietes = new TreeViewItem();
                        tvi_proprietes.Header = "Propriété";
                        foreach (List<string> propriete in proprietes)
                        {
                            List<string> proprieteType = BD.selection("SELECT nom FROM TypesGofus WHERE idTypeGofus = " + propriete[0])[0];
                            TreeViewItem tvi_propriete = new TreeViewItem();
                            tvi_propriete.Header = proprieteType[0] + " " + propriete[1];
                            Label proprieteDesc = new Label();
                            proprieteDesc.Content = propriete[2];
                            tvi_propriete.Items.Add(proprieteDesc);
                            tvi_proprietes.Items.Add(tvi_propriete);
                        }
                        tvi_classe.Items.Add(tvi_proprietes);
                    }
                    List<string>[] methodes = BD.selection("SELECT * FROM MethodesGofus WHERE idClasseGofus = " + classe[0]);
                    if (methodes[0].Count() > 1)
                    {
                        TreeViewItem tvi_methodes = new TreeViewItem();
                        tvi_methodes.Header = "Méthode";
                        foreach (List<string> methode in methodes)
                        {
                            List<string> methodeType = BD.selection("SELECT nom FROM TypesGofus WHERE idTypeGofus = " + methode[2])[0];
                            TreeViewItem tvi_methode = new TreeViewItem();
                            List<string>[] parametres = BD.selection("SELECT * FROM parametresgofus WHERE idMethodeGofus = " + methode[0]);
                            TextBlock sb_parametres = new TextBlock();
                            sb_parametres.Inlines.Add(methodeType[0] + " " + methode[3] + "(");
                            if (parametres[0].Count() > 1)
                            {
                                foreach (List<string> parametre in parametres)
                                {
                                    if (parametre[4] == "1")
                                        sb_parametres.Inlines.Add("[");
                                    List<string>[] parametreTypes = BD.selection("SELECT idTypeGofus FROM ParametresTypesGofus WHERE idParametreGofus = " + parametre[0]);
                                    foreach (List<string> parametreTypeId in parametreTypes)
                                    {
                                        string parametreType = BD.selection("SELECT nom FROM TypesGofus WHERE idTypeGofus = " + parametreTypeId[0])[0][0];
                                        sb_parametres.Inlines.Add(parametreType);
                                        if (parametreTypeId != parametreTypes.Last())
                                            sb_parametres.Inlines.Add("|");
                                    }
                                    sb_parametres.Inlines.Add(" ");
                                    Run run_parametre = new Run(parametre[2]);
                                    run_parametre.ToolTip = parametre[3];
                                    ToolTipService.SetShowDuration(run_parametre, int.MaxValue);
                                    sb_parametres.Inlines.Add(run_parametre);
                                    if (parametre != parametres.Last())
                                        sb_parametres.Inlines.Add(", ");
                                    if (parametre[4] == "1")
                                        sb_parametres.Inlines.Add("]");
                                }
                            }
                            sb_parametres.Inlines.Add(")");
                            tvi_methode.Header = sb_parametres;
                            Label methodeDesc = new Label();
                            methodeDesc.Content = methode[4];
                            tvi_methode.Items.Add(methodeDesc);
                            tvi_methodes.Items.Add(tvi_methode);
                        }
                        tvi_classe.Items.Add(tvi_methodes);
                    }
                    sp_main.Children.Add(tvi_classe);
                }
                }));
            });
            init.Start();
            Thread.Yield();
        }
    }
}
