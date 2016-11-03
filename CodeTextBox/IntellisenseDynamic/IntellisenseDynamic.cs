using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections;
using System.Linq;

namespace Moonlight.IntellisenseDynamic
{
    internal class IntellisenseDynamics
    {
        #region Members
        private bool compiled = false;
        private Regex chainesRegexp = null;
        private Regex chainesFonctionRegexp = null;
        private Regex tableauRegexp = null;
        private Regex tableauFonctionRegexp = null;
        private Regex simpleVarRegexp = null;
        private Regex simpleVarFonctionRegexp = null;
        private Regex voidFonctionRegexp = null;

        #region gofusRegex

        private Regex ArmeRegexp = null;
        private Regex ArmeFonctionRegexp = null;

        private Regex CaseRegexp = null;
        private Regex CaseFonctionRegexp = null;

        private Regex ClasseRegexp = null;
        private Regex ClasseFonctionRegexp = null;

        private Regex EffetRegexp = null;
        private Regex EffetFonctionRegexp = null;

        private Regex EntiteRegexp = null;
        private Regex EntiteFonctionRegexp = null;

        private Regex EntiteInconnuRegexp = null;
        private Regex EntiteInconnuFonctionRegexp = null;

        private Regex EnvoutementRegexp = null;
        private Regex EnvoutementFonctionRegexp = null;

        private Regex EquipementRegexp = null;
        private Regex EquipementFonctionRegexp = null;

        private Regex ListeRegexp = null;
        private Regex ListeFonctionRegexp = null;

        private Regex PersonnageRegexp = null;
        private Regex PersonnageFonctionRegexp = null;

        private Regex SortRegexp = null;
        private Regex SortFonctionRegexp = null;

        private Regex StatistiqueRegexp = null;
        private Regex StatistiqueFonctionRegexp = null;

        private Regex TerrainRegexp = null;
        private Regex TerrainFonctionRegexp = null;

        private Regex ZoneRegexp = null;
        private Regex ZoneFonctionRegexp = null;


        #endregion

        #endregion

        #region Constructor
        public IntellisenseDynamics()
        {
        }
        #endregion

        #region Methods
        public void DoIntellisense_CurrentLine(CodeTextBox codeTextbox, TreeView m_IntellisenseTree, TreeView m_IntellisenseTree_Template)
        {
            #region Compile regexs if necessary
            if (!compiled)
            {
                Update(codeTextbox);
            }
            #endregion

            string line = RichTextboxHelper.GetCurrentLine(codeTextbox);
            int lineStart = RichTextboxHelper.GetCurrentLineStartIndex(codeTextbox);

            ProcessLine(codeTextbox, line, lineStart, m_IntellisenseTree, m_IntellisenseTree_Template);
        }
        public void DoIntellisense_Selection(CodeTextBox codeTextbox, int selectionStart, int selectionLength, TreeView m_IntellisenseTree, TreeView m_IntellisenseTree_Template)
        {
            #region Compile regexs if necessary
            if (!compiled)
            {
                Update(codeTextbox);
            }
            #endregion

            ProcessSelection(codeTextbox, selectionStart, selectionLength, m_IntellisenseTree, m_IntellisenseTree_Template);
        }
        public void DoIntellisense_AllLines(CodeTextBox codeTextbox, TreeView m_IntellisenseTree, TreeView m_IntellisenseTree_Template)
        {
            #region Compile regexs if necessary
            if (!compiled)
            {
                Update(codeTextbox);
            }
            #endregion

            ProcessAllLines(codeTextbox, m_IntellisenseTree, m_IntellisenseTree_Template);

        }
        public void RefreshIntellisense(CodeTextBox codeTextbox, TreeView m_IntellisenseTree, TreeView m_IntellisenseTree_Template)
        {
            #region Compile regexs if necessary
            if (!compiled)
            {
                Update(codeTextbox);
            }
            #endregion

            RemoveInexistant(codeTextbox, m_IntellisenseTree, m_IntellisenseTree_Template);

        }
        /// <summary>
        /// Compiles the necessary regexps
        /// </summary>
        /// <param name="syntaxSettings"></param>
        public void Update(CodeTextBox codeTextbox)
        {
            //bool|byte|sbyte|char|decimal|double|float|int|uint|long|ulong|object|short|ushort
            chainesRegexp = new Regex(@"string ([a-z0-9_]*?)[^a-z0-9_\(]", RegexOptions.Compiled | RegexOptions.Multiline);
            chainesFonctionRegexp = new Regex(@"string ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);
            tableauRegexp = new Regex(@"\[\] ([a-z0-9_]*?)[^a-z0-9_\(] ", RegexOptions.Compiled | RegexOptions.Multiline);
            tableauFonctionRegexp = new Regex(@"\[\] ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);
            simpleVarRegexp = new Regex(@"bool([a-z0-9_]*?)[^a-z0-9_\(]|byte ([a-z0-9_]*?)[^a-z0-9_\(]|sbyte ([a-z0-9_]*?)[^a-z0-9_\(]|char ([a-z0-9_]*?)[^a-z0-9_\(]|decimal ([a-z0-9_]*?)[^a-z0-9_\(]|double ([a-z0-9_]*?)[^a-z0-9_\(]|float ([a-z0-9_]*?)[^a-z0-9_\(]|int ([a-z0-9_]*?)[^a-z0-9_\(]|uint ([a-z0-9_]*?)[^a-z0-9_\(]|long ([a-z0-9_]*?)[^a-z0-9_\(]|ulong ([a-z0-9_]*?)[^a-z0-9_\(]|object ([a-z0-9_]*?)[^a-z0-9_\(]|short ([a-z0-9_]*?)[^a-z0-9_\(]|ushort ([a-z0-9_]*?)[^a-z0-9_\(]", RegexOptions.Compiled | RegexOptions.Multiline);
            simpleVarFonctionRegexp = new Regex(@"bool ([a-z0-9_]*\(.*\))|byte ([a-z0-9_]*\(.*\))|sbyte ([a-z0-9_]*\(.*\))|char ([a-z0-9_]*\(.*\))|decimal ([a-z0-9_]*\(.*\))|double ([a-z0-9_]*\(.*\))|float ([a-z0-9_]*\(.*\))|int ([a-z0-9_]*\(.*\))|uint ([a-z0-9_]*\(.*\))|long ([a-z0-9_]*\(.*\))|ulong ([a-z0-9_]*\(.*\))|object ([a-z0-9_]*\(.*\))|short ([a-z0-9_]*\(.*\))|ushort ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);
            voidFonctionRegexp = new Regex(@"void ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);

            ArmeRegexp = new Regex(@"Arme ([a-z0-9_]*?)[^a-z0-9_\(]", RegexOptions.Compiled | RegexOptions.Multiline);
            ArmeFonctionRegexp = new Regex(@"Arme ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);

            CaseRegexp = new Regex(@"Case ([a-z0-9_]*?)[^a-z0-9_\(]", RegexOptions.Compiled | RegexOptions.Multiline);
            CaseFonctionRegexp = new Regex(@"Case ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);

            ClasseRegexp = new Regex(@"Classe ([a-z0-9_]*?)[^a-z0-9_\(]", RegexOptions.Compiled | RegexOptions.Multiline);
            ClasseFonctionRegexp = new Regex(@"Classe ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);

            EffetRegexp = new Regex(@"Effet ([a-z0-9_]*?)[^a-z0-9_\(]", RegexOptions.Compiled | RegexOptions.Multiline);
            EffetFonctionRegexp = new Regex(@"Effet ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);

            EntiteRegexp = new Regex(@"Entite ([a-z0-9_]*?)[^a-z0-9_\(]", RegexOptions.Compiled | RegexOptions.Multiline);
            EntiteFonctionRegexp = new Regex(@"Entite ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);

            EntiteInconnuRegexp = new Regex(@"EntiteInconnu ([a-z0-9_]*?)[^a-z0-9_\(]", RegexOptions.Compiled | RegexOptions.Multiline);
            EntiteInconnuFonctionRegexp = new Regex(@"EntiteInconnu ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);

            EnvoutementRegexp = new Regex(@"Envoutement ([a-z0-9_]*?)[^a-z0-9_\(]", RegexOptions.Compiled | RegexOptions.Multiline);
            EnvoutementFonctionRegexp = new Regex(@"Envoutement ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);

            EquipementRegexp = new Regex(@"Equipement ([a-z0-9_]*?)[^a-z0-9_\(]", RegexOptions.Compiled | RegexOptions.Multiline);
            EquipementFonctionRegexp = new Regex(@"Equipement ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);

            ListeRegexp = new Regex(@"Liste\<[a-z]+\> ([a-z0-9_]*?)[^a-z0-9_\(]", RegexOptions.Compiled | RegexOptions.Multiline);
            ListeFonctionRegexp = new Regex(@"Liste\<[a-z]+\> ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);

            PersonnageRegexp = new Regex(@"Personnage ([a-z0-9_]*?)[^a-z0-9_\(]", RegexOptions.Compiled | RegexOptions.Multiline);
            PersonnageFonctionRegexp = new Regex(@"Personnage ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);

            SortRegexp = new Regex(@"Sort ([a-z0-9_]*?)[^a-z0-9_\(]", RegexOptions.Compiled | RegexOptions.Multiline);
            SortFonctionRegexp = new Regex(@"Sort ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);

            StatistiqueRegexp = new Regex(@"Statistique ([a-z0-9_]*?)[^a-z0-9_\(]", RegexOptions.Compiled | RegexOptions.Multiline);
            StatistiqueFonctionRegexp = new Regex(@"Statistique ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);

            TerrainRegexp = new Regex(@"Terrain ([a-z0-9_]*?)[^a-z0-9_\(]", RegexOptions.Compiled | RegexOptions.Multiline);
            TerrainFonctionRegexp = new Regex(@"Terrain ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);

            ZoneRegexp = new Regex(@"Zone ([a-z0-9_]*?)[^a-z0-9_\(]", RegexOptions.Compiled | RegexOptions.Multiline);
            ZoneFonctionRegexp = new Regex(@"Zone ([a-z0-9_]*\(.*\))", RegexOptions.Compiled | RegexOptions.Multiline);

            //Set compiled flag to true
            compiled = true;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Processes a regex.
        /// </summary>
        /// <param name="richTextbox"></param>
        /// <param name="line"></param>
        /// <param name="lineStart"></param>
        /// <param name="regexp"></param>
        /// <param name="color"></param>
        private void RefreshRegex(CodeTextBox codeTextbox, string line, int lineStart, Regex regexp, string nodeType, string nodeTag, TreeView m_IntellisenseTree)
        {
            if (regexp == null)
            {
                // for uninitialized typename regexp
                return;
            }

            MatchCollection regMatches = regexp.Matches(line);

            foreach (TreeNode TNode in m_IntellisenseTree.Nodes)
            {
                if (TNode.Tag.ToString() == nodeTag && TNode.Text == nodeType)
                {
                    bool exist = false;
                    foreach (Match regMatch in regMatches)
                    {
                        if (regMatch.Groups.Cast<Group>().Count(m => m.Value.Length != 0) > 1)
                        {
                            int i = 1;
                            while (regMatch.Groups[i].Value.Length == 0)
                                i++;
                            string result = regMatch.Groups[i].Value;
                            if (TNode.Name == result)
                            {
                                exist = true;
                                break;
                            }
                        }
                    }
                    if (!exist)
                    {
                        m_IntellisenseTree.Nodes.Remove(TNode);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Processes a regex.
        /// </summary>
        /// <param name="richTextbox"></param>
        /// <param name="line"></param>
        /// <param name="lineStart"></param>
        /// <param name="regexp"></param>
        /// <param name="color"></param>
        private void ProcessRegex(CodeTextBox codeTextbox, string line, int lineStart, Regex regexp, string nodeType, string nodeTag, TreeView m_IntellisenseTree, TreeView m_IntellisenseTree_Template)
        {
            if (regexp == null)
            {
                // for uninitialized typename regexp
                return;
            }
            

            Match regMatch;

            for (regMatch = regexp.Match(line); regMatch.Success; regMatch = regMatch.NextMatch())
            {
                // Process the words
                if (regMatch.Groups.Cast<Group>().Count(m => m.Value.Length != 0) > 1)
                {
                    int i = 1;
                    while (regMatch.Groups[i].Value.Length == 0)
                        i++;
                    string result = regMatch.Groups[i].Value;

                    if (m_IntellisenseTree.Nodes.Find(result, false).Length != 0)
                        break;

                    TreeNode newNode = new TreeNode(result);
                    newNode = (TreeNode)m_IntellisenseTree_Template.Nodes.Find(nodeType, false)[0].Clone();
                    newNode.Name = result;
                    newNode.Tag = nodeTag;
                    newNode.Text = nodeType;
                    m_IntellisenseTree.Nodes.Add(newNode);
                }
            }
        }
        /// <summary>
        /// Processes syntax highlightning for a line.
        /// </summary>
        /// <param name="richTextbox"></param>
        /// <param name="syntaxSettings"></param>
        /// <param name="line"></param>
        /// <param name="lineStart"></param>
        private void ProcessLine(CodeTextBox codeTextbox, string line, int lineStart, TreeView m_IntellisenseTree, TreeView m_IntellisenseTree_Template)
        {
            // Process the simpleVar
            ProcessRegex(codeTextbox, line, lineStart, simpleVarRegexp, "simpleVar", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, simpleVarFonctionRegexp, "simpleVar", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            // Process the string
            ProcessRegex(codeTextbox, line, lineStart, chainesRegexp, "chaine", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, chainesFonctionRegexp, "chaine", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            // Process the simpleVar
            ProcessRegex(codeTextbox, line, lineStart, tableauRegexp, "tab", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, tableauFonctionRegexp, "tab", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            // Process the string
            ProcessRegex(codeTextbox, line, lineStart, voidFonctionRegexp, "fonctionVoid", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            #region process gofus
            ProcessRegex(codeTextbox, line, lineStart, ArmeRegexp, "Arme", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, ArmeFonctionRegexp, "Arme", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, line, lineStart, CaseRegexp, "Case", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, CaseFonctionRegexp, "Case", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, line, lineStart, ClasseRegexp, "Classe", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, ClasseFonctionRegexp, "Classe", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, line, lineStart, EffetRegexp, "Effet", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, EffetFonctionRegexp, "Effet", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, line, lineStart, EntiteRegexp, "Entite", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, EntiteFonctionRegexp, "Entite", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, line, lineStart, EntiteInconnuRegexp, "EntiteInconnu", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, EntiteInconnuFonctionRegexp, "EntiteInconnu", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, line, lineStart, EnvoutementRegexp, "Envoutement", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, EnvoutementFonctionRegexp, "Envoutement", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, line, lineStart, EquipementRegexp, "Equipement", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, EquipementFonctionRegexp, "Equipement", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, line, lineStart, ListeRegexp, "Liste", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, ListeFonctionRegexp, "Liste", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, line, lineStart, PersonnageRegexp, "Personnage", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, PersonnageFonctionRegexp, "Personnage", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, line, lineStart, SortRegexp, "Sort", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, SortFonctionRegexp, "Sort", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, line, lineStart, StatistiqueRegexp, "Statistique", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, StatistiqueFonctionRegexp, "Statistique", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, line, lineStart, TerrainRegexp, "Terrain", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, TerrainFonctionRegexp, "Terrain", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, line, lineStart, ZoneRegexp, "Zone", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, line, lineStart, ZoneFonctionRegexp, "Zone", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            #endregion
        }
        private void ProcessSelection(CodeTextBox codeTextbox, int selectionStart, int selectionLength, TreeView m_IntellisenseTree, TreeView m_IntellisenseTree_Template)
        {
            string text = codeTextbox.SelectedText;

            // Process the simpleVar
            ProcessRegex(codeTextbox, text, selectionStart, simpleVarRegexp, "simpleVar", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, text, selectionStart, simpleVarFonctionRegexp, "simpleVar", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            // Process the string
            ProcessRegex(codeTextbox, text, selectionStart, chainesRegexp, "chaine", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, text, selectionStart, chainesFonctionRegexp, "chaine", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            // Process the simpleVar
            ProcessRegex(codeTextbox, text, selectionStart, tableauRegexp, "tab", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, text, selectionStart, tableauFonctionRegexp, "tab", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            // Process the string
            ProcessRegex(codeTextbox, text, selectionStart, voidFonctionRegexp, "fonctionVoid", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            #region process gofus
            ProcessRegex(codeTextbox, text, selectionStart, ArmeRegexp, "Arme", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, ArmeFonctionRegexp, "Arme", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, CaseRegexp, "Case", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, CaseFonctionRegexp, "Case", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, ClasseRegexp, "Classe", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, ClasseFonctionRegexp, "Classe", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, EffetRegexp, "Effet", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, EffetFonctionRegexp, "Effet", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, EntiteRegexp, "Entite", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, EntiteFonctionRegexp, "Entite", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, EntiteInconnuRegexp, "EntiteInconnu", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, EntiteInconnuFonctionRegexp, "EntiteInconnu", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, EnvoutementRegexp, "Envoutement", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, EnvoutementFonctionRegexp, "Envoutement", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, EquipementRegexp, "Equipement", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, EquipementFonctionRegexp, "Equipement", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, ListeRegexp, "Liste", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, ListeFonctionRegexp, "Liste", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, PersonnageRegexp, "Personnage", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, PersonnageFonctionRegexp, "Personnage", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, SortRegexp, "Sort", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, SortFonctionRegexp, "Sort", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, StatistiqueRegexp, "Statistique", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, StatistiqueFonctionRegexp, "Statistique", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, TerrainRegexp, "Terrain", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, TerrainFonctionRegexp, "Terrain", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, ZoneRegexp, "Zone", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, text, selectionStart, ZoneFonctionRegexp, "Zone", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            #endregion
        }
        private void ProcessAllLines(CodeTextBox codeTextbox, TreeView m_IntellisenseTree, TreeView m_IntellisenseTree_Template)
        {
            // Process the simpleVar
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, simpleVarRegexp, "simpleVar", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, codeTextbox.Text, 0, simpleVarFonctionRegexp, "simpleVar", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            // Process the string
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, chainesRegexp, "chaine", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, codeTextbox.Text, 0, chainesFonctionRegexp, "chaine", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            // Process the simpleVar
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, tableauRegexp, "tab", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);

            ProcessRegex(codeTextbox, codeTextbox.Text, 0, tableauFonctionRegexp, "tab", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            // Process the string
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, voidFonctionRegexp, "fonctionVoid", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);

            #region process gofus
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, ArmeRegexp, "Arme", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, ArmeFonctionRegexp, "Arme", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, CaseRegexp, "Case", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, CaseFonctionRegexp, "Case", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, ClasseRegexp, "Classe", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, ClasseFonctionRegexp, "Classe", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, EffetRegexp, "Effet", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, EffetFonctionRegexp, "Effet", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, EntiteRegexp, "Entite", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, EntiteFonctionRegexp, "Entite", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, EntiteInconnuRegexp, "EntiteInconnu", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, EntiteInconnuFonctionRegexp, "EntiteInconnu", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, EnvoutementRegexp, "Envoutement", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, EnvoutementFonctionRegexp, "Envoutement", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, EquipementRegexp, "Equipement", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, EquipementFonctionRegexp, "Equipement", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, ListeRegexp, "Liste", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, ListeFonctionRegexp, "Liste", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, PersonnageRegexp, "Personnage", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, PersonnageFonctionRegexp, "Personnage", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, SortRegexp, "Sort", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, SortFonctionRegexp, "Sort", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, StatistiqueRegexp, "Statistique", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, StatistiqueFonctionRegexp, "Statistique", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, TerrainRegexp, "Terrain", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, TerrainFonctionRegexp, "Terrain", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, ZoneRegexp, "Zone", "Property", m_IntellisenseTree, m_IntellisenseTree_Template);
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, ZoneFonctionRegexp, "Zone", "Method", m_IntellisenseTree, m_IntellisenseTree_Template);
            #endregion
        }
        private void RemoveInexistant(CodeTextBox codeTextbox, TreeView m_IntellisenseTree, TreeView m_IntellisenseTree_Template)
        {
            // Process the simpleVar
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, simpleVarRegexp, "simpleVar", "Property", m_IntellisenseTree);

            RefreshRegex(codeTextbox, codeTextbox.Text, 0, simpleVarFonctionRegexp, "simpleVar", "Method", m_IntellisenseTree);

            // Process the string
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, chainesRegexp, "chaine", "Property", m_IntellisenseTree);

            RefreshRegex(codeTextbox, codeTextbox.Text, 0, chainesFonctionRegexp, "chaine", "Method", m_IntellisenseTree);

            // Process the simpleVar
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, tableauRegexp, "tab", "Property", m_IntellisenseTree);

            RefreshRegex(codeTextbox, codeTextbox.Text, 0, tableauFonctionRegexp, "tab", "Method", m_IntellisenseTree);

            // Process the string
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, voidFonctionRegexp, "fonctionVoid", "Method", m_IntellisenseTree);

            #region Refresh gofus
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, ArmeRegexp, "Arme", "Property", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, ArmeFonctionRegexp, "Arme", "Method", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, CaseRegexp, "Case", "Property", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, CaseFonctionRegexp, "Case", "Method", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, ClasseRegexp, "Classe", "Property", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, ClasseFonctionRegexp, "Classe", "Method", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, EffetRegexp, "Effet", "Property", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, EffetFonctionRegexp, "Effet", "Method", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, EntiteRegexp, "Entite", "Property", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, EntiteFonctionRegexp, "Entite", "Method", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, EntiteInconnuRegexp, "EntiteInconnu", "Property", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, EntiteInconnuFonctionRegexp, "EntiteInconnu", "Method", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, EnvoutementRegexp, "Envoutement", "Property", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, EnvoutementFonctionRegexp, "Envoutement", "Method", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, EquipementRegexp, "Equipement", "Property", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, EquipementFonctionRegexp, "Equipement", "Method", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, ListeRegexp, "Liste", "Property", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, ListeFonctionRegexp, "Liste", "Method", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, PersonnageRegexp, "Personnage", "Property", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, PersonnageFonctionRegexp, "Personnage", "Method", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, SortRegexp, "Sort", "Property", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, SortFonctionRegexp, "Sort", "Method", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, StatistiqueRegexp, "Statistique", "Property", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, StatistiqueFonctionRegexp, "Statistique", "Method", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, TerrainRegexp, "Terrain", "Property", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, TerrainFonctionRegexp, "Terrain", "Method", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, ZoneRegexp, "Zone", "Property", m_IntellisenseTree);
            RefreshRegex(codeTextbox, codeTextbox.Text, 0, ZoneFonctionRegexp, "Zone", "Method", m_IntellisenseTree);
            #endregion
        }
        #endregion
    }
}
