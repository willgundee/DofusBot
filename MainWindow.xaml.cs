﻿using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using Forms = System.Windows.Forms;
using System.Windows.Forms;
using GofusSharp;
using CodeBox;

namespace test
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Form1 codeSpace = new Form1();
            codeSpace.Visible = true;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
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
            string richText = new TextRange(code_test.Document.ContentStart, code_test.Document.ContentEnd).Text;
            string finalCode = code.Replace("user_code", richText);
            //initialisation d'un compilateur de code C#
            CSharpCodeProvider provider = new CSharpCodeProvider(new Dictionary<string,string>() { { "CompilerVersion", "v3.5" } });
            //initialisation des paramètres du compilateur de code C#
            CompilerParameters parameters = new CompilerParameters();
            //ajout des lien de bibliothèque dynamique (dll) * pas fini
            //parameters.ReferencedAssemblies.Add("WindowsBase.dll");
            parameters.ReferencedAssemblies.Add("GofusSharp.dll");
            //parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            //compilation du code 
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, finalCode);
            //recherche d'érreurs de compilation * pas fini
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                }

                throw new InvalidOperationException(sb.ToString());
            }
            //mettre la fonction compilé dans une variable
            Type binaryFunction = results.CompiledAssembly.GetType("FonctionUtilisateur.BinaryFunction");
            //invoqué la fonction compilée avec la variable
            binaryFunction.GetMethod("Function").Invoke(null,null);
        }
    }
}
