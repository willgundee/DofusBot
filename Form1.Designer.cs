namespace CodeBox
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            //#############################################################################################################

            System.Windows.Forms.TreeNode[] treeNodeTab_method_chaine = new System.Windows.Forms.TreeNode[] {
                new System.Windows.Forms.TreeNode("Clone()"),
                new System.Windows.Forms.TreeNode("CompareTo()"),
                new System.Windows.Forms.TreeNode("Contains()"),
                new System.Windows.Forms.TreeNode("EndsWith()"),
                new System.Windows.Forms.TreeNode("Equals()"),
                new System.Windows.Forms.TreeNode("GetHashCode()"),
                new System.Windows.Forms.TreeNode("GetType()"),
                new System.Windows.Forms.TreeNode("GetTypeCode()"),
                new System.Windows.Forms.TreeNode("IndexOf()"),
                new System.Windows.Forms.TreeNode("ToLower()"),
                new System.Windows.Forms.TreeNode("ToUpper()"),
                new System.Windows.Forms.TreeNode("Insert()"),
                new System.Windows.Forms.TreeNode("IsNormalized()"),
                new System.Windows.Forms.TreeNode("LastIndexOf()"),
                new System.Windows.Forms.TreeNode("Remove()"),
                new System.Windows.Forms.TreeNode("Replace()"),
                new System.Windows.Forms.TreeNode("Split()"),
                new System.Windows.Forms.TreeNode("StartsWith()"),
                new System.Windows.Forms.TreeNode("Substring()"),
                new System.Windows.Forms.TreeNode("ToCharArray()"),
                new System.Windows.Forms.TreeNode("Trim()")
            };
            foreach (System.Windows.Forms.TreeNode Tnode in treeNodeTab_method_chaine)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "method";
                Tnode.Text = "system";
            }
            System.Windows.Forms.TreeNode[] treeNodeTab_attribut_chaine = new System.Windows.Forms.TreeNode[] {
                new System.Windows.Forms.TreeNode("Length")
            };
            foreach (System.Windows.Forms.TreeNode Tnode in treeNodeTab_attribut_chaine)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }
            System.Windows.Forms.TreeNode[] treeNodeTab_chaine = new System.Windows.Forms.TreeNode[treeNodeTab_method_chaine.Length + treeNodeTab_attribut_chaine.Length];
            treeNodeTab_method_chaine.CopyTo(treeNodeTab_chaine, 0);
            treeNodeTab_attribut_chaine.CopyTo(treeNodeTab_chaine, treeNodeTab_method_chaine.Length);
            //#############################################################################################################
            System.Windows.Forms.TreeNode[] treeNodeTab_simpleVar = new System.Windows.Forms.TreeNode[] {
                new System.Windows.Forms.TreeNode("CompareTo()"),
                new System.Windows.Forms.TreeNode("Equals()"),
                new System.Windows.Forms.TreeNode("GetHashCode()"),
                new System.Windows.Forms.TreeNode("GetType()"),
                new System.Windows.Forms.TreeNode("GetTypeCode()"),
                new System.Windows.Forms.TreeNode("ToString()")
            };
            foreach (System.Windows.Forms.TreeNode Tnode in treeNodeTab_simpleVar)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "class";
                Tnode.Text = "system";
            }
            //############################################################################################################# 
            System.Windows.Forms.TreeNode[] treeNodeTab_method_tab = new System.Windows.Forms.TreeNode[] {
                new System.Windows.Forms.TreeNode("Clone()"),
                new System.Windows.Forms.TreeNode("CopyTo()"),
                new System.Windows.Forms.TreeNode("Equals()"),
                new System.Windows.Forms.TreeNode("GetEnumerator()"),
                new System.Windows.Forms.TreeNode("GetHashCode()"),
                new System.Windows.Forms.TreeNode("GetLength()"),
                new System.Windows.Forms.TreeNode("GetLongLength()"),
                new System.Windows.Forms.TreeNode("GetLowerBound()"),
                new System.Windows.Forms.TreeNode("GetType()"),
                new System.Windows.Forms.TreeNode("GetUpperBound()"),
                new System.Windows.Forms.TreeNode("GetValue()"),
                new System.Windows.Forms.TreeNode("Initialize()"),
                new System.Windows.Forms.TreeNode("SetValue()"),
                new System.Windows.Forms.TreeNode("ToString()")
            };

            foreach (System.Windows.Forms.TreeNode Tnode in treeNodeTab_method_tab)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "method";
                Tnode.Text = "system";
            }

            System.Windows.Forms.TreeNode[] treeNodeTab_attribut_tab = new System.Windows.Forms.TreeNode[] {
                new System.Windows.Forms.TreeNode("IsFixedSize"),
                new System.Windows.Forms.TreeNode("IsReadOnly"),
                new System.Windows.Forms.TreeNode("IsSynchronized"),
                new System.Windows.Forms.TreeNode("Length"),
                new System.Windows.Forms.TreeNode("LongLength"),
                new System.Windows.Forms.TreeNode("Rank"),
                new System.Windows.Forms.TreeNode("SyncRoot")
            };
            foreach (System.Windows.Forms.TreeNode Tnode in treeNodeTab_attribut_tab)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }

            System.Windows.Forms.TreeNode[] treeNodeTab_tab = new System.Windows.Forms.TreeNode[treeNodeTab_method_tab.Length + treeNodeTab_attribut_tab.Length];
            treeNodeTab_method_tab.CopyTo(treeNodeTab_tab, 0);
            treeNodeTab_attribut_tab.CopyTo(treeNodeTab_tab, treeNodeTab_method_tab.Length);

            //#############################################################################################################
            System.Windows.Forms.TreeNode[] treeNodeTab_method_math = new System.Windows.Forms.TreeNode[] {
                new System.Windows.Forms.TreeNode("Abs()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Acos()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Asin()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Atan()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Atan2()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("BigMul()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Ceiling()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Cos()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Cosh()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("DivRem()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Exp()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Floor()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("IEEERemainder()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Log()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Log10()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Max()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Min()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Pow()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Round()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Sign()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Sin()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Sinh()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Sqrt()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Tan()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Tanh()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Truncate()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Sinh()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Sqrt()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Tan()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Tanh()", treeNodeTab_simpleVar),
                new System.Windows.Forms.TreeNode("Truncate()", treeNodeTab_simpleVar)
            };

            foreach (System.Windows.Forms.TreeNode Tnode in treeNodeTab_method_math)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "method";
                Tnode.Text = "system";
            }


            System.Windows.Forms.TreeNode[] treeNodeTab_attribut_math = new System.Windows.Forms.TreeNode[] {
                new System.Windows.Forms.TreeNode("E"),
                new System.Windows.Forms.TreeNode("PI")
            };
            foreach (System.Windows.Forms.TreeNode Tnode in treeNodeTab_attribut_math)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "property";
                Tnode.Text = "system";
            }

            System.Windows.Forms.TreeNode[] treeNodeTab_math = new System.Windows.Forms.TreeNode[treeNodeTab_method_math.Length + treeNodeTab_attribut_math.Length];
            treeNodeTab_method_math.CopyTo(treeNodeTab_math, 0);
            treeNodeTab_attribut_math.CopyTo(treeNodeTab_math, treeNodeTab_method_math.Length);

            //#############################################################################################################
            System.Windows.Forms.TreeNode[] treeNodeTab_keyword = new System.Windows.Forms.TreeNode[] {
                new System.Windows.Forms.TreeNode("abstract"),
                new System.Windows.Forms.TreeNode("as"),
                new System.Windows.Forms.TreeNode("base"),
                new System.Windows.Forms.TreeNode("bool"),
                new System.Windows.Forms.TreeNode("break"),
                new System.Windows.Forms.TreeNode("byte"),
                new System.Windows.Forms.TreeNode("case"),
                new System.Windows.Forms.TreeNode("catch"),
                new System.Windows.Forms.TreeNode("char"),
                new System.Windows.Forms.TreeNode("checked"),
                new System.Windows.Forms.TreeNode("class"),
                new System.Windows.Forms.TreeNode("const"),
                new System.Windows.Forms.TreeNode("continue"),
                new System.Windows.Forms.TreeNode("decimal"),
                new System.Windows.Forms.TreeNode("default"),
                new System.Windows.Forms.TreeNode("delegate"),
                new System.Windows.Forms.TreeNode("do"),
                new System.Windows.Forms.TreeNode("double"),
                new System.Windows.Forms.TreeNode("else"),
                new System.Windows.Forms.TreeNode("enum"),
                new System.Windows.Forms.TreeNode("event"),
                new System.Windows.Forms.TreeNode("explicit"),
                new System.Windows.Forms.TreeNode("extern"),
                new System.Windows.Forms.TreeNode("false"),
                new System.Windows.Forms.TreeNode("finally"),
                new System.Windows.Forms.TreeNode("fixed"),
                new System.Windows.Forms.TreeNode("float"),
                new System.Windows.Forms.TreeNode("for"),
                new System.Windows.Forms.TreeNode("foreach"),
                new System.Windows.Forms.TreeNode("goto"),
                new System.Windows.Forms.TreeNode("if"),
                new System.Windows.Forms.TreeNode("implicit"),
                new System.Windows.Forms.TreeNode("in"),
                new System.Windows.Forms.TreeNode("int"),
                new System.Windows.Forms.TreeNode("interface"),
                new System.Windows.Forms.TreeNode("internal"),
                new System.Windows.Forms.TreeNode("is"),
                new System.Windows.Forms.TreeNode("lock"),
                new System.Windows.Forms.TreeNode("long"),
                new System.Windows.Forms.TreeNode("namespace"),
                new System.Windows.Forms.TreeNode("new"),
                new System.Windows.Forms.TreeNode("null"),
                new System.Windows.Forms.TreeNode("object"),
                new System.Windows.Forms.TreeNode("operator"),
                new System.Windows.Forms.TreeNode("out"),
                new System.Windows.Forms.TreeNode("override"),
                new System.Windows.Forms.TreeNode("params"),
                new System.Windows.Forms.TreeNode("private"),
                new System.Windows.Forms.TreeNode("protected"),
                new System.Windows.Forms.TreeNode("public"),
                new System.Windows.Forms.TreeNode("readonly"),
                new System.Windows.Forms.TreeNode("ref"),
                new System.Windows.Forms.TreeNode("return"),
                new System.Windows.Forms.TreeNode("sbyte"),
                new System.Windows.Forms.TreeNode("sealed"),
                new System.Windows.Forms.TreeNode("short"),
                new System.Windows.Forms.TreeNode("sizeof"),
                new System.Windows.Forms.TreeNode("stackalloc"),
                new System.Windows.Forms.TreeNode("static"),
                new System.Windows.Forms.TreeNode("string"),
                new System.Windows.Forms.TreeNode("struct"),
                new System.Windows.Forms.TreeNode("switch"),
                new System.Windows.Forms.TreeNode("this"),
                new System.Windows.Forms.TreeNode("throw"),
                new System.Windows.Forms.TreeNode("true"),
                new System.Windows.Forms.TreeNode("try"),
                new System.Windows.Forms.TreeNode("typeof"),
                new System.Windows.Forms.TreeNode("uint"),
                new System.Windows.Forms.TreeNode("ulong"),
                new System.Windows.Forms.TreeNode("unchecked"),
                new System.Windows.Forms.TreeNode("unsafe"),
                new System.Windows.Forms.TreeNode("ushort"),
                new System.Windows.Forms.TreeNode("using"),
                new System.Windows.Forms.TreeNode("virtual"),
                new System.Windows.Forms.TreeNode("void"),
                new System.Windows.Forms.TreeNode("volatile"),
                new System.Windows.Forms.TreeNode("while")
            };
            foreach (System.Windows.Forms.TreeNode Tnode in treeNodeTab_keyword)
            {
                Tnode.Name = Tnode.Text;
                Tnode.Tag = "namespace";
                Tnode.Text = "system";
            }
            //#############################################################################################################
            //#############################################################################################################
            System.Windows.Forms.TreeNode treeNode_1 = new System.Windows.Forms.TreeNode("keyword");
            System.Windows.Forms.TreeNode treeNode_2 = new System.Windows.Forms.TreeNode("classGofus");
            System.Windows.Forms.TreeNode treeNode_3 = new System.Windows.Forms.TreeNode("Math", treeNodeTab_math);
            System.Windows.Forms.TreeNode treeNode_4 = new System.Windows.Forms.TreeNode("chaine", treeNodeTab_chaine);
            System.Windows.Forms.TreeNode treeNode_5 = new System.Windows.Forms.TreeNode("simpleVar", treeNodeTab_simpleVar);
            System.Windows.Forms.TreeNode treeNode_6 = new System.Windows.Forms.TreeNode("tab", treeNodeTab_tab);
            System.Windows.Forms.TreeNode treeNode_7 = new System.Windows.Forms.TreeNode("fonctionVoid");

            this.codeTextBox1 = new Moonlight.CodeTextBox();
            this.SuspendLayout();
            // 
            // codeTextBox1
            // 
            this.codeTextBox1.AcceptsTab = true;
            this.codeTextBox1.AutoWordSelection = true;
            this.codeTextBox1.CodeColor_Chaine = System.Drawing.Color.Firebrick;
            this.codeTextBox1.CodeColor_Comment = System.Drawing.Color.Green;
            this.codeTextBox1.CodeColor_Function = System.Drawing.Color.CornflowerBlue;
            this.codeTextBox1.CodeColor_Keyword = System.Drawing.Color.Blue;
            this.codeTextBox1.CodeColor_PlainText = System.Drawing.Color.Black;
            this.codeTextBox1.CodeColor_Type = System.Drawing.Color.CornflowerBlue;
            this.codeTextBox1.CodeImage_Class = ((System.Drawing.Image)(resources.GetObject("codeTextBox1.CodeImage_Class")));
            this.codeTextBox1.CodeImage_Event = ((System.Drawing.Image)(resources.GetObject("codeTextBox1.CodeImage_Event")));
            this.codeTextBox1.CodeImage_Interface = ((System.Drawing.Image)(resources.GetObject("codeTextBox1.CodeImage_Interface")));
            this.codeTextBox1.CodeImage_Method = ((System.Drawing.Image)(resources.GetObject("codeTextBox1.CodeImage_Method")));
            this.codeTextBox1.CodeImage_Namespace = ((System.Drawing.Image)(resources.GetObject("codeTextBox1.CodeImage_Namespace")));
            this.codeTextBox1.CodeImage_Property = ((System.Drawing.Image)(resources.GetObject("codeTextBox1.CodeImage_Property")));
            this.codeTextBox1.CodeWords_Chaines = ((System.Collections.Generic.List<string>)(resources.GetObject("codeTextBox1.CodeWords_Chaines")));
            this.codeTextBox1.CodeWords_Comments = ((System.Collections.Generic.List<string>)(resources.GetObject("codeTextBox1.CodeWords_Comments")));
            this.codeTextBox1.CodeWords_Functions = ((System.Collections.Generic.List<string>)(resources.GetObject("codeTextBox1.CodeWords_Functions")));
            this.codeTextBox1.CodeWords_Keywords = ((System.Collections.Generic.List<string>)(resources.GetObject("codeTextBox1.CodeWords_Keywords")));
            this.codeTextBox1.CodeWords_ScopeOperators = ((System.Collections.Generic.List<string>)(resources.GetObject("codeTextBox1.CodeWords_ScopeOperators")));
            this.codeTextBox1.CodeWords_Types = ((System.Collections.Generic.List<string>)(resources.GetObject("codeTextBox1.CodeWords_Types")));
            this.codeTextBox1.DetectUrls = false;
            this.codeTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeTextBox1.Font = new System.Drawing.Font("Courier New", 8F);
            this.codeTextBox1.IntellisenseKey = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            // 
            // 
            // 
            this.codeTextBox1.IntellisenseTree.LineColor = System.Drawing.Color.Empty;
            this.codeTextBox1.IntellisenseTree.Location = new System.Drawing.Point(0, 0);
            this.codeTextBox1.IntellisenseTree.Name = "";

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

            System.Windows.Forms.TreeNode[] treeNode_root = new System.Windows.Forms.TreeNode[] {
            treeNode_1,
            treeNode_2,
            treeNode_3,
            treeNode_4,
            treeNode_5,
            treeNode_6,
            treeNode_7};

            System.Windows.Forms.TreeNode[] treeNode_Intellisense = new System.Windows.Forms.TreeNode[treeNode_root.Length + treeNodeTab_keyword.Length];
            treeNodeTab_keyword.CopyTo(treeNode_Intellisense, 0);
            treeNode_root.CopyTo(treeNode_Intellisense, treeNodeTab_keyword.Length);

            this.codeTextBox1.IntellisenseTree.Nodes.AddRange(treeNode_Intellisense);
            this.codeTextBox1.IntellisenseTree.TabIndex = 0;
            this.codeTextBox1.Location = new System.Drawing.Point(0, 0);
            this.codeTextBox1.Name = "codeTextBox1";
            this.codeTextBox1.Size = new System.Drawing.Size(632, 455);
            this.codeTextBox1.TabIndex = 2;
            this.codeTextBox1.Text = "";
            this.codeTextBox1.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 455);
            this.Controls.Add(this.codeTextBox1);
            this.Name = "Form1";
            this.Text = "CodeTextBox Sample";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
        }

        #endregion

        private Moonlight.CodeTextBox codeTextBox1;
    }
}

