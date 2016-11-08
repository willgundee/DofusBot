using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace Gofus
{
    /// <summary>
    /// Logique d'interaction pour SortDesc.xaml
    /// </summary>
    public partial class SortDesc : UserControl
    {
        public BDService bd = new BDService();

        public SortDesc(Sort s)
        {
            InitializeComponent();
         
         BitmapImage path = new BitmapImage(new Uri("http://staticns.ankama.com/dofus/www/game/spells/55/sort_" + s.NoImage + ".png"));

            Imgsort.Source = path;
            lblNom.Text = s.Nom;
            lblDescription.Text = s.Description;

            lblExperiece.Text = toLevel(s).ToString();

            lblPa.Text = s.PointActionRequis.ToString();
           
            TauxDeRelance(s);

            NomCommunZones(s);

            #region vrai ou faux
            if (s.CelluleLibre)
            {
                lblCellule.Text = "Vrai";
            }
            else
            {
                lblCellule.Text = "Faux";
            }
            if (s.LigneDeVue)
            {
                lblLigne.Text = "Vrai";
            }
            else
            {
                lblLigne.Text = "Faux";
            }
            if (s.PorteeModifiable)
            {
                lblPorte.Text = "Vrai";
            }
            else
            {
                lblPorte.Text = "Faux";
            }
            #endregion
        }

        public Dictionary<int, double> dictLvl = new Dictionary<int, double>()
            #region les levels
        {
            {1,0},
            {2,110},
            {3,650},
            {4,1500},
            {5,2800},
            {6,4800},
            {7,7300},
            {8,10500},
            {9,14500},
            {10,19200},
            {11,25200},
            {12,32600},
            {13,41000},
            {14,50500},
            {15,61000},
            {16,75000},
            {17,91000},
            {18,115000},
            {19,142000},
            {20,171000},
            {21,202000},
            {22,235000},
            {23,270000},
            {24,310000},
            {25,353000},
            {26,398500},
            {27,448000},
            {28,503000},
            {29,561000},
            {30,621600},
            {31,687000},
            {32,755000},
            {33,829000},
            {34,910000},
            {35,1000000},
            {36,1100000},
            {37,1240000},
            {38,1400000},
            {39,1580000},
            {40,1780000},
            {41,2000000},
            {42,2250000},
            {43,2530000},
            {44,2850000},
            {45,3200000},
            {46,3570000},
            {47,3960000},
            {48,4400000},
            {49,4860000},
            {50,5350000},
            {51,5860000},
            {52,6390000},
            {53,6950000},
            {54,7530000},
            {55,8130000},
            {56,8765100},
            {57,9420000},
            {58,10150000},
            {59,10894000},
            {60,11655000},
            {61,12450000},
            {62,13280000},
            {63,14130000},
            {64,15170000},
            {65,16251000},
            {66,17377000},
            {67,18553000},
            {68,19778000},
            {69,21055000},
            {70,22385000},
            {71,23529000},
            {72,25209000},
            {73,26707000},
            {74,28264000},
            {75,29882000},
            {76,31563000},
            {77,33307000},
            {78,35118000},
            {79,36997000},
            {80,38945000},
            {81,40965000},
            {82,43059000},
            {83,45229000},
            {84,47476000},
            {85,49803000},
            {86,52211000},
            {87,54704000},
            {88,57284000},
            {89,59952000},
            {90,62712000},
            {91,65565000},
            {92,68514000},
            {93,71561000},
            {94,74710000},
            {95,77963000},
            {96,81323000},
            {97,84792000},
            {98,88374000},
            {99,92071000},
            {100,95886000},
            {101,99823000},
            {102,103885000},
            {103,108075000},
            {104,112396000},
            {105,116853000},
            {106,121447000},
            {107,126184000},
            {108,131066000},
            {109,136098000},
            {110,141283000},
            {111,146626000},
            {112,152130000},
            {113,157800000},
            {114,163640000},
            {115,169655000},
            {116,175848000},
            {117,182225000},
            {118,188791000},
            {119,195550000},
            {120,202507000},
            {121,209667000},
            {122,217037000},
            {123,224620000},
            {124,232424000},
            {125,240452000},
            {126,248712000},
            {127,257209000},
            {128,265949000},
            {129,274939000},
            {130,284186000},
            {131,293694000},
            {132,303473000},
            {133,313527000},
            {134,323866000},
            {135,334495000},
            {136,345423000},
            {137,356657000},
            {138,368206000},
            {139,380076000},
            {140,392278000},
            {141,404818000},
            {142,417706000},
            {143,430952000},
            {144,444564000},
            {145,458551000},
            {146,472924000},
            {147,487693000},
            {148,502867000},
            {149,518458000},
            {150,534476000},
            {151,551000000},
            {152,567839000},
            {153,585206000},
            {154,603047000},
            {155,621374000},
            {156,640199000},
            {157,659536000},
            {158,679398000},
            {159,699798000},
            {160,720751000},
            {161,742272000},
            {162,764374000},
            {163,787074000},
            {164,810387000},
            {165,834329000},
            {166,858917000},
            {167,884167000},
            {168,910098000},
            {169,936727000},
            {170,964073000},
            {171,992154000},
            {172,1020991000},
            {173,1050603000},
            {174,1081010000},
            {175,1112235000},
            {176,1144298000},
            {177,1177222000},
            {178,1211030000},
            {179,1245745000},
            {180,1281393000},
            {181,1317997000},
            {182,1355584000},
            {183,1404179000},
            {184,1463811000},
            {185,1534506000},
            {186,1616294000},
            {187,1709205000},
            {188,1813267000},
            {189,1928513000},
            {190,2054975000},
            {191,2192686000},
            {192,2341679000},
            {193,2501990000},
            {194,2673655000},
            {195,2856710000},
            {196,3051194000},
            {197,3257146000},
            {198,3474606000},
            {199,3703616000},
            {200,5555424000}
        };
        #endregion
        public int toLevel(Sort s)
        {
            for (int i = 1; i < 200; i++)
            {
                if (s.Exprience >= dictLvl[i] && s.Exprience < dictLvl[i + 1])
                    return i;
            }
            if (s.Exprience >= dictLvl[200])
                return 200;

            return 0;//si tout fucktop
        }

        public void NomCommunZones(Sort s)
        {
            string nomZoneE;
            string nomZoneP;

            switch(s.ZoneEffet.Nom.ToString())
            {
                case "carre":
                    nomZoneE = "Carre";
                    break;
                case "Cercle":
                    nomZoneE = "Cercle";
                    break;
                case "cone":
                    nomZoneE = "Cone";
                    break;
                case "croix":
                    nomZoneE = "Croix";
                    break;
                case "tous":
                    nomZoneE = "Tous";
                    break;
                case "ligne_verticale":
                    nomZoneE = "Ligne Verticale";
                    break;
                case "ligne_horizontale":
                    nomZoneE = "Ligne horizontale";
                    break;
                case "demi_cercle":
                    nomZoneE = "Demi-cercle";
                    break;
                default:
                    nomZoneE = s.ZoneEffet.Nom.ToString();
                    break;
            }

            switch (s.ZonePortee.Nom.ToString())
            {
                case "carre":
                    nomZoneP = "Carre";
                    break;
                case "Cercle":
                    nomZoneP = "Cercle";
                    break;
                case "cone":
                    nomZoneP = "Cone";
                    break;
                case "croix":
                    nomZoneP = "Croix";
                    break;
                case "tous":
                    nomZoneP = "Tous";
                    break;
                case "ligne_verticale":
                    nomZoneP = "Ligne Verticale";
                    break;
                case "ligne_horizontale":
                    nomZoneP = "Ligne horizontale";
                    break;
                case "demi_cercle":
                    nomZoneP = "Demi-cercle";
                    break;
                default:
                    nomZoneP = s.ZonePortee.Nom.ToString();
                    break;
            }
            lblZoneE.Text = nomZoneE;
            lblZoneP.Text = nomZoneP;
        }

        public void TauxDeRelance(Sort s)
        {
            string taux;

            if (Convert.ToInt32(s.TauxDeRelance)>0)
            {
                lblTtaux.Content = "NB lancé par tour :";
                taux=s.TauxDeRelance.ToString();
            }
            else
            {
                lblTtaux.Content = "Tour de relance :";
                taux = (Convert.ToInt32(s.TauxDeRelance.ToString()) * -1).ToString();
            }

            lblTaux.Text = taux;
        }

    }


}
