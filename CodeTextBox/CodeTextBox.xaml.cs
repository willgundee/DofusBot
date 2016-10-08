using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Drawing;
using System.Windows.Input;
using Moonlight.Intellisense;
using Moonlight.SyntaxHighlight;
using Moonlight.IntellisenseDynamic;
using CodeTextBox;
using System.ComponentModel;

namespace Moonlight
{
    /// <summary>
    /// Logique d'interaction pour CodeTextBox.xaml
    /// </summary>
    public partial class CodeTextBox : UserControl
    {
        #region Members

        private SyntaxHighlighter m_SyntaxHighLighter = new SyntaxHighlighter();
        private IntellisenseDynamics m_IntellisenseDynamic = new IntellisenseDynamics();
        private IntellisenseManager m_IntellisenseManager;
        private RichTextBox mp_CodeTextBox;
        private ListBox mp_IntellisenseBox;
        private System.Windows.Forms.TreeView mp_IntellisenseTree;

        #region Drawing
        /// <summary>
        /// Enables or disables control's painting - internal
        /// </summary>
        private bool mp_EnablePainting = true;
        #endregion

        #region Word lists
        private List<string> mp_CodeWords_Keywords;
        private List<string> mp_CodeWords_Types;
        private List<string> mp_CodeWords_Functions;
        private List<string> mp_CodeWords_Comments;
        private List<string> mp_CodeWords_Chaines;

        internal System.Drawing.Point GetPositionFromCharIndex(RichTextBox codeTextbox)
        {
            TextPointer position = codeTextbox.CaretPosition;
            if (position == null)
                return new System.Drawing.Point();
            Rect positionRect = position.GetCharacterRect(LogicalDirection.Forward);
            return new System.Drawing.Point((int)positionRect.X, (int)positionRect.Y);
        }

        private List<string> mp_CodeWords_ScopeOperators;
        #endregion

        #region Syntax highlightning colors
        private System.Windows.Media.SolidColorBrush mp_CodeColor_Keyword = System.Windows.Media.Brushes.Blue;
        private System.Windows.Media.SolidColorBrush mp_CodeColor_Type = System.Windows.Media.Brushes.CornflowerBlue;
        private System.Windows.Media.SolidColorBrush mp_CodeColor_Function = System.Windows.Media.Brushes.CornflowerBlue;
        private System.Windows.Media.SolidColorBrush mp_CodeColor_Chaine = System.Windows.Media.Brushes.Crimson;
        private System.Windows.Media.SolidColorBrush mp_CodeColor_Comment = System.Windows.Media.Brushes.Green;
        private System.Windows.Media.SolidColorBrush mp_CodeColor_PlainText = System.Windows.Media.Brushes.Black;
        #endregion

        #region Intellisense images
        private System.Drawing.Image mp_CodeImage_Class = Resource._class;
        private System.Drawing.Image mp_CodeImage_Event = Resource._event;
        private System.Drawing.Image mp_CodeImage_Interface = Resource._interface;
        private System.Drawing.Image mp_CodeImage_Method = Resource._method;
        private System.Drawing.Image mp_CodeImage_Namespace = Resource._namespace;
        private System.Drawing.Image mp_CodeImage_Property = Resource._property;
        #endregion

        #endregion

        #region Properties
        #region Public properties

        #region Word lists
        /// <summary>
        /// Gets or Sets the keywords used for syntax highlightning and intellisense.
        /// </summary>
        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the keywords used for syntax highlightning and intellisense.")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public List<string> CodeWords_Keywords
        {
            get { return mp_CodeWords_Keywords; }
            set { mp_CodeWords_Keywords = value; }
        }

        /// <summary>
        /// Gets or Sets the types used for syntax highlightning and intellisense.
        /// </summary>
        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the types used for syntax highlightning and intellisense.")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public List<string> CodeWords_Types
        {
            get { return mp_CodeWords_Types; }
            set { mp_CodeWords_Types = value; }
        }

        /// <summary>
        /// Gets or Sets the functions used for syntax highlightning and intellisense.
        /// </summary>
        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the functions used for syntax highlightning and intellisense.")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public List<string> CodeWords_Functions
        {
            get { return mp_CodeWords_Functions; }
            set { mp_CodeWords_Functions = value; }
        }

        /// <summary>
        /// Gets or Sets the comment strings used for syntax highlightning.
        /// </summary>
        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the comment strings used for syntax highlightning.")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public List<string> CodeWords_Comments
        {
            get { return mp_CodeWords_Comments; }
            set { mp_CodeWords_Comments = value; }
        }

        /// <summary>
        /// Gets or Sets the string strings used for syntax highlightning.
        /// </summary>
        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the string strings used for syntax highlightning.")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public List<string> CodeWords_Chaines
        {
            get { return mp_CodeWords_Chaines; }
            set { mp_CodeWords_Chaines = value; }
        }

        /// <summary>
        /// Gets or Sets the comment strings used for syntax highlightning.
        /// </summary>
        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the object separator strings used for intellisense.")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public List<string> CodeWords_ScopeOperators
        {
            get { return mp_CodeWords_ScopeOperators; }
            set { mp_CodeWords_ScopeOperators = value; }
        }
        #endregion

        #region Syntax highlightning colors
        /// <summary>
        /// Gets or Sets the color of plain texts for syntax highlightning.
        /// </summary>
        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the color of plain texts for syntax highlightning.")]
        public System.Windows.Media.SolidColorBrush CodeColor_PlainText
        {
            get { return mp_CodeColor_PlainText; }
            set { mp_CodeColor_PlainText = value; }
        }

        /// <summary>
        /// Gets or Sets the color of keywords for syntax highlightning.
        /// </summary>
        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the color of keywords for syntax highlightning.")]
        public System.Windows.Media.SolidColorBrush CodeColor_Keyword
        {
            get { return mp_CodeColor_Keyword; }
            set { mp_CodeColor_Keyword = value; }
        }

        /// <summary>
        /// Gets or Sets the color of types for syntax highlightning.
        /// </summary>
        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the color of types for syntax highlightning.")]
        public System.Windows.Media.SolidColorBrush CodeColor_Type
        {
            get { return mp_CodeColor_Type; }
            set { mp_CodeColor_Type = value; }
        }

        /// <summary>
        /// Gets or Sets the color of functions for syntax highlightning.
        /// </summary>
        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the color of functions for syntax highlightning.")]
        public System.Windows.Media.SolidColorBrush CodeColor_Function
        {
            get { return mp_CodeColor_Function; }
            set { mp_CodeColor_Function = value; }
        }

        /// <summary>
        /// Gets or Sets the color of comments for syntax highlightning.
        /// </summary>
        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the color of comments for syntax highlightning.")]
        public System.Windows.Media.SolidColorBrush CodeColor_Comment
        {
            get { return mp_CodeColor_Comment; }
            set { mp_CodeColor_Comment = value; }
        }

        /// <summary>
        /// Gets or Sets the color of strings for syntax highlightning.
        /// </summary>
        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the color of strings for syntax highlightning.")]
        public System.Windows.Media.SolidColorBrush CodeColor_Chaine
        {
            get { return mp_CodeColor_Chaine; }
            set { mp_CodeColor_Chaine = value; }
        }
        #endregion

        #region Intellisense images
        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the intellisense image of keywords.")]
        public System.Drawing.Image CodeImage_Class
        {
            get { return mp_CodeImage_Class; }
            set { mp_CodeImage_Class = value; }
        }

        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the intellisense image of events.")]
        public System.Drawing.Image CodeImage_Event
        {
            get { return mp_CodeImage_Event; }
            set { mp_CodeImage_Event = value; }
        }

        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the intellisense image of interfaces.")]
        public System.Drawing.Image CodeImage_Interface
        {
            get { return mp_CodeImage_Interface; }
            set { mp_CodeImage_Interface = value; }
        }

        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the intellisense image of methods.")]
        public System.Drawing.Image CodeImage_Method
        {
            get { return mp_CodeImage_Method; }
            set { mp_CodeImage_Method = value; }
        }

        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the intellisense image of namespaces.")]
        public System.Drawing.Image CodeImage_Namespace
        {
            get { return mp_CodeImage_Namespace; }
            set { mp_CodeImage_Namespace = value; }
        }

        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the intellisense image of properties.")]
        public System.Drawing.Image CodeImage_Property
        {
            get { return mp_CodeImage_Property; }
            set { mp_CodeImage_Property = value; }
        }
        #endregion

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the intellisense item tree.")]
        public System.Windows.Forms.TreeView IntellisenseTree
        {
            get { return mp_IntellisenseTree; }
            set { mp_IntellisenseTree = value; }
        }
        
        [Browsable(true), Category("CodeTexbox"), Description("Gets or Sets the intellisense item tree.")]
        public RichTextBox CodeTextbox
        {
            get { return mp_CodeTextBox; }
            set { mp_CodeTextBox = value; }
        }
        #endregion

        #region Internal Properties
        /// <summary>
        /// Enables or disables the control's paint event.
        /// </summary>
        internal bool EnablePainting
        {
            get { return mp_EnablePainting; }
            set { mp_EnablePainting = value; }
        }
        /// <summary>
        /// Gets the intellisense's ImageListBox
        /// </summary>
        internal ListBox IntellisenseBox
        {
            get { return mp_IntellisenseBox; }
        }

        #endregion
        #endregion

        #region Constructors
        public CodeTextBox()
        {
            mp_CodeTextBox = new RichTextBox();
            //Set some defaults...
            AddChild(mp_CodeTextBox);
            mp_CodeTextBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            mp_CodeTextBox.VerticalAlignment = VerticalAlignment.Stretch;
            mp_CodeTextBox.AcceptsTab = true;
            mp_CodeTextBox.FontFamily = new System.Windows.Media.FontFamily("Courier New");
            mp_CodeTextBox.FontSize = 13;

            // TODO
            //
            //Do not enable drag and dropping text
            //The same problem, as paste - the onDragDrop event fires, BEFORE the text is written into the textbox
            //Need to be handled in WndPrc
            //this.EnableAutoDragDrop = false;

            //this.DetectUrls = false;
            //this.WordWrap = false;
            mp_CodeTextBox.AutoWordSelection = true;

            #region Instantiate Syntax highlightning and Intellisense members
            //Instantiate word lists
            mp_CodeWords_Keywords = new List<string>();
            mp_CodeWords_Types = new List<string>();
            mp_CodeWords_Functions = new List<string>();
            mp_CodeWords_Comments = new List<string>();
            mp_CodeWords_ScopeOperators = new List<string>();
            mp_CodeWords_Chaines = new List<string>();

            //Instantiate intellisense manager
            m_IntellisenseManager = new IntellisenseManager(this);

            //Instantiate the intellisense box
            mp_IntellisenseBox = new ListBox();

            //Instantiate intellisense tree
            mp_IntellisenseTree = new System.Windows.Forms.TreeView();
            #endregion

            #region Setup intellisense box
            //Setup intellisense box
            AddLogicalChild(mp_IntellisenseBox);
            mp_IntellisenseBox.Width = 250;
            mp_IntellisenseBox.Width = 150;
            mp_IntellisenseBox.Visibility = Visibility.Hidden;
            mp_IntellisenseBox.KeyDown += new KeyEventHandler(mp_IntellisenseBox_KeyDown);
            mp_IntellisenseBox.MouseDoubleClick += new MouseButtonEventHandler(mp_IntellisenseBox_DoubleClick);
            #endregion
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Force a full update of syntax highlightning
        /// </summary>
        public void UpdateSyntaxHightlight()
        {
            m_SyntaxHighLighter.Update(this);
            m_SyntaxHighLighter.DoSyntaxHightlight_AllLines(this);
        }

        /// <summary>
        /// Force a full update of syntax highlightning
        /// </summary>
        public void UpdateTreeView()
        {
            m_IntellisenseDynamic.Update(this);
            m_IntellisenseDynamic.DoIntellisense_AllLines(this, mp_IntellisenseTree);
        }

        /// <summary>
        /// Create the treeview from a treenode array
        /// </summary>
        public void CreateTreeView(System.Windows.Forms.TreeNode[] treeNode_arr_main)
        {
            mp_IntellisenseTree.Nodes.AddRange(treeNode_arr_main);
        }
        #endregion

        #region Private methods
        private void InlineUIContainer_Unloaded(object sender, RoutedEventArgs e)
        {
            (sender as InlineUIContainer).Unloaded -= new RoutedEventHandler(InlineUIContainer_Unloaded);

            TextBlock tb = new TextBlock();
            tb.FontFamily = new System.Windows.Media.FontFamily("Courier New");
            tb.FontSize = 8.5;
            tb.Text = "This line of text is not editable.";

            TextPointer tp = mp_CodeTextBox.CaretPosition.GetInsertionPosition(LogicalDirection.Forward);
            InlineUIContainer iuic = new InlineUIContainer(tb, tp);
            iuic.Unloaded += new RoutedEventHandler(InlineUIContainer_Unloaded);
        }

        private void rtb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var newPointer = mp_CodeTextBox.Selection.Start.InsertLineBreak();
                mp_CodeTextBox.Selection.Select(newPointer, newPointer);

                e.Handled = true;
            }
        }
        #endregion

        #region Overridden methods
        protected override void OnMouseRightButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            m_IntellisenseManager.HideIntellisenseBox();
            base.OnMouseRightButtonDown(e);
        }

        protected override void OnTextInput(System.Windows.Input.TextCompositionEventArgs e)
        {
            //Syntax Highlight the current line... :)
            m_SyntaxHighLighter.DoSyntaxHightlight_CurrentLine(this);

            m_IntellisenseDynamic.DoIntellisense_CurrentLine(this, mp_IntellisenseTree);

            m_IntellisenseDynamic.RefreshIntellisense(this, mp_IntellisenseTree);

            base.OnTextInput(e);
        }
        void mp_IntellisenseBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //Let the textbox handle keypresses inside the intellisense box
            this.OnKeyDown(e);
        }
        void mp_IntellisenseBox_DoubleClick(object sender, EventArgs e)
        {
            m_IntellisenseManager.ConfirmIntellisense();
        }
        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            #region Show Intellisense
            if (e.SystemKey >= System.Windows.Input.Key.A && e.SystemKey <= System.Windows.Input.Key.Z)
            {
                m_IntellisenseManager.ShowIntellisenseBox();
                e.Handled = true;
                Focus();
                return;
            }
            #endregion

            if (mp_IntellisenseBox.IsVisible)
            {
                #region ESCAPE and SPACE - Hide Intellisense
                if (e.SystemKey == System.Windows.Input.Key.Escape)
                {
                    m_IntellisenseManager.HideIntellisenseBox();
                    e.Handled = true;
                }
                else if (e.SystemKey == System.Windows.Input.Key.Space)
                {
                    m_IntellisenseManager.HideIntellisenseBox();
                    e.Handled = true;
                }
                #endregion

                #region Navigation - Up, Down, PageUp, PageDown, Home, End
                else if (e.SystemKey == System.Windows.Input.Key.Up)
                {
                    m_IntellisenseManager.NavigateUp(1);
                    e.Handled = true;
                }
                else if (e.SystemKey == System.Windows.Input.Key.Down)
                {
                    m_IntellisenseManager.NavigateDown(1);
                    e.Handled = true;
                }
                else if (e.SystemKey == System.Windows.Input.Key.PageUp)
                {
                    m_IntellisenseManager.NavigateUp(10);
                    e.Handled = true;
                }
                else if (e.SystemKey == System.Windows.Input.Key.PageDown)
                {
                    m_IntellisenseManager.NavigateDown(10);
                    e.Handled = true;
                }
                else if (e.SystemKey == System.Windows.Input.Key.Home)
                {
                    m_IntellisenseManager.NavigateHome();
                    e.Handled = true;
                }
                else if (e.SystemKey == System.Windows.Input.Key.End)
                {
                    m_IntellisenseManager.NavigateEnd();
                    e.Handled = true;
                }
                #endregion

                #region Typing - Back
                else if (e.SystemKey == System.Windows.Input.Key.Back)
                {
                    m_IntellisenseManager.TypeBackspace();
                }
                #endregion

                #region Typing - Brackets
                else if (e.SystemKey == System.Windows.Input.Key.D9)
                {
                    // Trap the open bracket key, displaying a cheap and
                    // cheerful tooltip if the word just typed is in our tree
                    // (the parameters are stored in the tag property of the node)
                }
                else if (e.SystemKey == System.Windows.Input.Key.D8)
                {
                    // Close bracket key, hide the tooltip textbox
                }
                #endregion

                #region Typing - TAB and Enter
                else if (e.SystemKey == System.Windows.Input.Key.Tab)
                {
                    m_IntellisenseManager.ConfirmIntellisense();
                    e.Handled = true;
                    //e.SuppressKeyPress = true;
                }
                else if (e.SystemKey == System.Windows.Input.Key.Enter)
                {
                    m_IntellisenseManager.ConfirmIntellisense();
                    e.Handled = true;
                }
                #endregion
            }

            Focus();
            base.OnKeyDown(e);
        }
        private void ProcessKeyDownWndPrc(uint wParam, uint lParam)
        {
            //We process the keys here instead of the OnKeyDown event, because the OnKeyDown has made
            //some mistakes in converting e.KeyValue to char...

            //Convert wParam to char...
            char c = (char)wParam;


            if (!char.IsLetterOrDigit(c))
            {
                //Char is not alphanumerical
                m_IntellisenseManager.TypeNonAlphaNumerical(c);
            }
            else
            {
                //Char is alphanumerical
                m_IntellisenseManager.TypeAlphaNumerical(c);
            }
        }
        #endregion
    }
}
