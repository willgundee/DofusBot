namespace test
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.codeTextBox1 = new Moonlight.CodeTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 466);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // codeTextBox1
            // 
            this.codeTextBox1.AcceptsTab = true;
            this.codeTextBox1.AutoWordSelection = true;
            this.codeTextBox1.CodeColor_Chaine = System.Drawing.Color.Crimson;
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
            this.codeTextBox1.Font = new System.Drawing.Font("Courier New", 8F);
            this.codeTextBox1.IntellisenseKey = System.Windows.Forms.Keys.None;
            // 
            // 
            // 
            this.codeTextBox1.IntellisenseTree.LineColor = System.Drawing.Color.Empty;
            this.codeTextBox1.IntellisenseTree.Location = new System.Drawing.Point(0, 0);
            this.codeTextBox1.IntellisenseTree.Name = "";
            this.codeTextBox1.IntellisenseTree.TabIndex = 0;
            this.codeTextBox1.Location = new System.Drawing.Point(12, 12);
            this.codeTextBox1.Name = "codeTextBox1";
            this.codeTextBox1.Size = new System.Drawing.Size(739, 442);
            this.codeTextBox1.TabIndex = 0;
            this.codeTextBox1.Text = "";
            this.codeTextBox1.WordWrap = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 501);
            this.Controls.Add(this.codeTextBox1);
            this.Controls.Add(this.button1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private Moonlight.CodeTextBox codeTextBox1;
    }
}