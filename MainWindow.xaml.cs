using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace test
{
    public enum ScrollBarType : uint
    {
        SbHorz = 0,
        SbVert = 1,
        SbCtl = 2,
        SbBoth = 3
    }
    public enum Message : uint
    {
        WM_VSCROLL = 0x0115
    }
    public enum ScrollBarCommands : uint
    {
        SB_THUMBPOSITION = 4
    }
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ctb_main.CreateTreeView(generateTree());
            ctb_main.UpdateSyntaxHightlight();
            ctb_main.UpdateTreeView();

            tb_lineNumber.Font = new System.Drawing.Font("Courier New", 8);
            tb_lineNumber.ReadOnly = true;
            tb_lineNumber.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            tb_lineNumber.Cursor = System.Windows.Forms.Cursors.Arrow;
            
        }

        //**************************************************************************************************
        private void btn_run_Click(object sender, RoutedEventArgs e)
        {
            //code dynamique 
            string code = @"
                using GofusSharp;
                namespace FonctionUtilisateur
                {                
                    public class BinaryFunction
                    {                
                        public static void Function()
                        {
                            user_code
                        }
                    }
                }
            ";

            //je remplace le mot user_code pour ce qui ce trouve dans la text box
            string richText = ctb_main.Text;
            string finalCode = code.Replace("user_code", richText);
            //initialisation d'un compilateur de code C#
            CSharpCodeProvider provider = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
            //initialisation des paramètres du compilateur de code C#
            CompilerParameters parameters = new CompilerParameters();
            //ajout des lien de bibliothèque dynamique (dll)
            parameters.ReferencedAssemblies.Add("WindowsBase.dll");
            parameters.ReferencedAssemblies.Add("GofusSharp.dll");
            parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parameters.ReferencedAssemblies.Add("Math.dll");
            //System.Windows.Forms.MessageBox.Show(  );
            //compilation du code 
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, finalCode);
            //recherche d'érreurs de compilation
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(string.Format("Erreur (Ligne {0}): {1}", (error.Line - 8).ToString(), error.ErrorText));
                }
                System.Windows.Forms.MessageBox.Show(sb.ToString());
                return;
                //throw new InvalidOperationException(sb.ToString());
            }
            //mettre la fonction compilé dans une variable
            Type binaryFunction = results.CompiledAssembly.GetType("FonctionUtilisateur.BinaryFunction");
            //invoqué la fonction compilée avec la variable
            binaryFunction.GetMethod("Function").Invoke(null, null);
        }

        //**************************************************************************************************
        public int maxLC = 0; //maxLineCount - should be public
        private void ctb_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            int linecount = ctb_main.Lines.Count();
            if (linecount != maxLC)
            {
                tb_lineNumber.Clear();
                for (int i = 1; i < linecount + 1; i++)
                {
                    if (i == 1)
                        tb_lineNumber.AppendText(i.ToString());
                    else
                        tb_lineNumber.AppendText(Environment.NewLine + i.ToString());
                }
                maxLC = linecount;
            }
            ctb_main_VScroll(new object(),new EventArgs());
            //tb_lineNumber.SelectedText = 
            //System.Windows.Forms.MessageBox.Show(ctb_main.GetLineFromCharIndex(ctb_main.SelectionStart).ToString());
        }


        //**************************************************************************************************
        private TreeNode[] generateTree()
        {
            //#############################################################################################################

            TreeNode[] treeNodeTab_method_chaine = new TreeNode[] {
                new TreeNode("Clone()"),
                new TreeNode("CompareTo()"),
                new TreeNode("Contains()"),
                new TreeNode("EndsWith()"),
                new TreeNode("Equals()"),
                new TreeNode("GetHashCode()"),
                new TreeNode("GetType()"),
                new TreeNode("GetTypeCode()"),
                new TreeNode("IndexOf()"),
                new TreeNode("ToLower()"),
                new TreeNode("ToUpper()"),
                new TreeNode("Insert()"),
                new TreeNode("IsNormalized()"),
                new TreeNode("LastIndexOf()"),
                new TreeNode("Remove()"),
                new TreeNode("Replace()"),
                new TreeNode("Split()"),
                new TreeNode("StartsWith()"),
                new TreeNode("Substring()"),
                new TreeNode("ToCharArray()"),
                new TreeNode("Trim()")
            };
            foreach (TreeNode Tnode in treeNodeTab_method_chaine)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "method";
                Tnode.Text = "system";
            }
            TreeNode[] treeNodeTab_attribut_chaine = new TreeNode[] {
                new TreeNode("Length")
            };
            foreach (TreeNode Tnode in treeNodeTab_attribut_chaine)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }
            TreeNode[] treeNodeTab_chaine = new TreeNode[treeNodeTab_method_chaine.Length + treeNodeTab_attribut_chaine.Length];
            treeNodeTab_method_chaine.CopyTo(treeNodeTab_chaine, 0);
            treeNodeTab_attribut_chaine.CopyTo(treeNodeTab_chaine, treeNodeTab_method_chaine.Length);
            //#############################################################################################################
            TreeNode[] treeNodeTab_simpleVar = new TreeNode[] {
                new TreeNode("CompareTo()"),
                new TreeNode("Equals()"),
                new TreeNode("GetHashCode()"),
                new TreeNode("GetType()"),
                new TreeNode("GetTypeCode()"),
                new TreeNode("ToString()")
            };
            foreach (TreeNode Tnode in treeNodeTab_simpleVar)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "class";
                Tnode.Text = "system";
            }
            //############################################################################################################# 
            TreeNode[] treeNodeTab_method_tab = new TreeNode[] {
                new TreeNode("Clone()"),
                new TreeNode("CopyTo()"),
                new TreeNode("Equals()"),
                new TreeNode("GetEnumerator()"),
                new TreeNode("GetHashCode()"),
                new TreeNode("GetLength()"),
                new TreeNode("GetLongLength()"),
                new TreeNode("GetLowerBound()"),
                new TreeNode("GetType()"),
                new TreeNode("GetUpperBound()"),
                new TreeNode("GetValue()"),
                new TreeNode("Initialize()"),
                new TreeNode("SetValue()"),
                new TreeNode("ToString()")
            };

            foreach (TreeNode Tnode in treeNodeTab_method_tab)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "method";
                Tnode.Text = "system";
            }

            TreeNode[] treeNodeTab_attribut_tab = new TreeNode[] {
                new TreeNode("IsFixedSize"),
                new TreeNode("IsReadOnly"),
                new TreeNode("IsSynchronized"),
                new TreeNode("Length"),
                new TreeNode("LongLength"),
                new TreeNode("Rank"),
                new TreeNode("SyncRoot")
            };
            foreach (TreeNode Tnode in treeNodeTab_attribut_tab)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }

            TreeNode[] treeNodeTab_tab = new TreeNode[treeNodeTab_method_tab.Length + treeNodeTab_attribut_tab.Length];
            treeNodeTab_method_tab.CopyTo(treeNodeTab_tab, 0);
            treeNodeTab_attribut_tab.CopyTo(treeNodeTab_tab, treeNodeTab_method_tab.Length);

            //#############################################################################################################
            TreeNode[] treeNodeTab_method_math = new TreeNode[] {
                new TreeNode("Abs()", treeNodeTab_simpleVar),
                new TreeNode("Acos()", treeNodeTab_simpleVar),
                new TreeNode("Asin()", treeNodeTab_simpleVar),
                new TreeNode("Atan()", treeNodeTab_simpleVar),
                new TreeNode("Atan2()", treeNodeTab_simpleVar),
                new TreeNode("BigMul()", treeNodeTab_simpleVar),
                new TreeNode("Ceiling()", treeNodeTab_simpleVar),
                new TreeNode("Cos()", treeNodeTab_simpleVar),
                new TreeNode("Cosh()", treeNodeTab_simpleVar),
                new TreeNode("DivRem()", treeNodeTab_simpleVar),
                new TreeNode("Exp()", treeNodeTab_simpleVar),
                new TreeNode("Floor()", treeNodeTab_simpleVar),
                new TreeNode("IEEERemainder()", treeNodeTab_simpleVar),
                new TreeNode("Log()", treeNodeTab_simpleVar),
                new TreeNode("Log10()", treeNodeTab_simpleVar),
                new TreeNode("Max()", treeNodeTab_simpleVar),
                new TreeNode("Min()", treeNodeTab_simpleVar),
                new TreeNode("Pow()", treeNodeTab_simpleVar),
                new TreeNode("Round()", treeNodeTab_simpleVar),
                new TreeNode("Sign()", treeNodeTab_simpleVar),
                new TreeNode("Sin()", treeNodeTab_simpleVar),
                new TreeNode("Sinh()", treeNodeTab_simpleVar),
                new TreeNode("Sqrt()", treeNodeTab_simpleVar),
                new TreeNode("Tan()", treeNodeTab_simpleVar),
                new TreeNode("Tanh()", treeNodeTab_simpleVar),
                new TreeNode("Truncate()", treeNodeTab_simpleVar),
                new TreeNode("Sinh()", treeNodeTab_simpleVar),
                new TreeNode("Sqrt()", treeNodeTab_simpleVar),
                new TreeNode("Tan()", treeNodeTab_simpleVar),
                new TreeNode("Tanh()", treeNodeTab_simpleVar),
                new TreeNode("Truncate()", treeNodeTab_simpleVar)
            };

            foreach (TreeNode Tnode in treeNodeTab_method_math)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "method";
                Tnode.Text = "system";
            }


            TreeNode[] treeNodeTab_attribut_math = new TreeNode[] {
                new TreeNode("E"),
                new TreeNode("PI")
            };
            foreach (TreeNode Tnode in treeNodeTab_attribut_math)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }

            TreeNode[] treeNodeTab_math = new TreeNode[treeNodeTab_method_math.Length + treeNodeTab_attribut_math.Length];
            treeNodeTab_method_math.CopyTo(treeNodeTab_math, 0);
            treeNodeTab_attribut_math.CopyTo(treeNodeTab_math, treeNodeTab_method_math.Length);

            //#############################################################################################################
            TreeNode[] treeNodeTab_keyword = new TreeNode[] {
                new TreeNode("abstract"),
                new TreeNode("as"),
                new TreeNode("base"),
                new TreeNode("bool"),
                new TreeNode("break"),
                new TreeNode("byte"),
                new TreeNode("case"),
                new TreeNode("catch"),
                new TreeNode("char"),
                new TreeNode("checked"),
                new TreeNode("class"),
                new TreeNode("const"),
                new TreeNode("continue"),
                new TreeNode("decimal"),
                new TreeNode("default"),
                new TreeNode("delegate"),
                new TreeNode("do"),
                new TreeNode("double"),
                new TreeNode("else"),
                new TreeNode("enum"),
                new TreeNode("event"),
                new TreeNode("explicit"),
                new TreeNode("extern"),
                new TreeNode("false"),
                new TreeNode("finally"),
                new TreeNode("fixed"),
                new TreeNode("float"),
                new TreeNode("for"),
                new TreeNode("foreach"),
                new TreeNode("goto"),
                new TreeNode("if"),
                new TreeNode("implicit"),
                new TreeNode("in"),
                new TreeNode("int"),
                new TreeNode("interface"),
                new TreeNode("internal"),
                new TreeNode("is"),
                new TreeNode("lock"),
                new TreeNode("long"),
                new TreeNode("namespace"),
                new TreeNode("new"),
                new TreeNode("null"),
                new TreeNode("object"),
                new TreeNode("operator"),
                new TreeNode("out"),
                new TreeNode("override"),
                new TreeNode("params"),
                new TreeNode("private"),
                new TreeNode("protected"),
                new TreeNode("public"),
                new TreeNode("readonly"),
                new TreeNode("ref"),
                new TreeNode("return"),
                new TreeNode("sbyte"),
                new TreeNode("sealed"),
                new TreeNode("short"),
                new TreeNode("sizeof"),
                new TreeNode("stackalloc"),
                new TreeNode("static"),
                new TreeNode("string"),
                new TreeNode("struct"),
                new TreeNode("switch"),
                new TreeNode("this"),
                new TreeNode("throw"),
                new TreeNode("true"),
                new TreeNode("try"),
                new TreeNode("typeof"),
                new TreeNode("uint"),
                new TreeNode("ulong"),
                new TreeNode("unchecked"),
                new TreeNode("unsafe"),
                new TreeNode("ushort"),
                new TreeNode("using"),
                new TreeNode("virtual"),
                new TreeNode("void"),
                new TreeNode("volatile"),
                new TreeNode("while")
            };
            foreach (TreeNode Tnode in treeNodeTab_keyword)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            TreeNode treeNode_1 = new TreeNode("keyword");
            TreeNode treeNode_2 = new TreeNode("classGofus");
            TreeNode treeNode_3 = new TreeNode("Math", treeNodeTab_math);
            TreeNode treeNode_4 = new TreeNode("chaine", treeNodeTab_chaine);
            TreeNode treeNode_5 = new TreeNode("simpleVar", treeNodeTab_simpleVar);
            TreeNode treeNode_6 = new TreeNode("tab", treeNodeTab_tab);
            TreeNode treeNode_7 = new TreeNode("fonctionVoid");

            treeNode_1.Name = "keyword";
            treeNode_1.Tag = "class";
            treeNode_1.Text = "system";

            treeNode_2.Name = "classGofus";
            treeNode_2.Tag = "class";
            treeNode_2.Text = "system";

            treeNode_3.Name = "Math";
            treeNode_3.Tag = "class";
            treeNode_3.Text = "system";

            treeNode_4.Name = "chaine";
            treeNode_4.Tag = "class";
            treeNode_4.Text = "system";

            treeNode_5.Name = "simpleVar";
            treeNode_5.Tag = "class";
            treeNode_5.Text = "system";

            treeNode_6.Name = "tab";
            treeNode_6.Tag = "class";
            treeNode_6.Text = "system";

            treeNode_7.Name = "fonctionVoid";
            treeNode_7.Tag = "class";
            treeNode_7.Text = "system";

            TreeNode[] treeNode_root = new TreeNode[] {
            treeNode_1,
            treeNode_2,
            treeNode_3,
            treeNode_4,
            treeNode_5,
            treeNode_6,
            treeNode_7};



            TreeNode[] treeNode_Intellisense = new TreeNode[treeNode_root.Length + treeNodeTab_keyword.Length];
            treeNodeTab_keyword.CopyTo(treeNode_Intellisense, 0);
            treeNode_root.CopyTo(treeNode_Intellisense, treeNodeTab_keyword.Length);
            return treeNode_Intellisense;
        }
        //**************************************************************************************************

        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);
        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, UIntPtr wParam, UIntPtr lParam);

        private void ctb_main_VScroll(object sender, EventArgs e)
        {
            int nPos = GetScrollPos(ctb_main.Handle, (int)ScrollBarType.SbVert);
            nPos <<= 16;
            uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
            SendMessage(tb_lineNumber.Handle, (int)Message.WM_VSCROLL, new UIntPtr(wParam), new UIntPtr(0));
        }

        //**************************************************************************************************

    }
}
