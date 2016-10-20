﻿using System.Drawing;
using System.Text.RegularExpressions;

namespace Moonlight.SyntaxHighlight
{
    internal class SyntaxHighlighter
    {
        #region Members
        private bool compiled = false;
        private Regex keywordsRegexp = null;
        private Regex functionsRegexp = null;
        private Regex typeNamesRegexp = null;
        private Regex stringsRegexp = null;
        private Regex commentsRegexp = null;
        private Regex chainesRegexp = null;
        #endregion

        #region Constructor
        public SyntaxHighlighter()
        {
        }
        #endregion

        #region Methods
        public void DoSyntaxHightlight_CurrentLine(CodeTextBox codeTextbox)
        {
            #region Compile regexs if necessary
            if (!compiled)
            {
                Update(codeTextbox);
            }
            #endregion

            string line = RichTextboxHelper.GetCurrentLine(codeTextbox);
            int lineStart = RichTextboxHelper.GetCurrentLineStartIndex(codeTextbox);

            ProcessLine(codeTextbox, line, lineStart);
        }
        public void DoSyntaxHightlight_Selection(CodeTextBox codeTextbox, int selectionStart, int selectionLength)
        {
            #region Compile regexs if necessary
            if (!compiled)
            {
                Update(codeTextbox);
            }
            #endregion

            ProcessSelection(codeTextbox, selectionStart, selectionLength);
        }
        public void DoSyntaxHightlight_AllLines(CodeTextBox codeTextbox)
        {
            #region Compile regexs if necessary
            if (!compiled)
            {
                Update(codeTextbox);
            }
            #endregion

            ProcessAllLines(codeTextbox);
        }
        /// <summary>
        /// Compiles the necessary regexps
        /// </summary>
        /// <param name="syntaxSettings"></param>
        public void Update(CodeTextBox codeTextbox)
        {
            string keywords = string.Empty;
            string functions = string.Empty;
            string typeNames = string.Empty;
            string chaines = string.Empty;
            string comments = string.Empty;

            #region Build the strings above for regexs
            #region Build keywords
            for (int i = 0; i < codeTextbox.CodeWords_Keywords.Count; i++)
            {
                string strKeyword = codeTextbox.CodeWords_Keywords[i];

                if (i == codeTextbox.CodeWords_Keywords.Count - 1)
                    keywords += "\\b" + strKeyword + "\\b";
                else
                    keywords += "\\b" + strKeyword + "\\b|";
            }
            #endregion

            #region Build functions
            for (int i = 0; i < codeTextbox.CodeWords_Functions.Count; i++)
            {
                string strFunction = codeTextbox.CodeWords_Functions[i];

                if (i == codeTextbox.CodeWords_Functions.Count - 1)
                    functions += "\\b" + strFunction + "\\b";
                else
                    functions += "\\b" + strFunction + "\\b|";
            }
            #endregion

            #region Build typeNames
            for (int i = 0; i < codeTextbox.CodeWords_Types.Count; i++)
            {
                string strType = codeTextbox.CodeWords_Types[i];

                if (i == codeTextbox.CodeWords_Types.Count - 1)
                    typeNames += "\\b" + strType + "\\b";
                else
                    typeNames += "\\b" + strType + "\\b|";
            }
            #endregion

            #region Build comments
            for (int i = 0; i < codeTextbox.CodeWords_Comments.Count; i++)
            {
                string strComments = codeTextbox.CodeWords_Comments[i];

                if (i == codeTextbox.CodeWords_Comments.Count - 1)
                    comments += "" + strComments + ".*$";
                else
                    comments += "" + strComments + ".*$|";
            }
            #endregion

            #region Build chaines
            for (int i = 0; i < codeTextbox.CodeWords_Chaines.Count; i++)
            {
                string strChaines = codeTextbox.CodeWords_Chaines[i];

                if (i == codeTextbox.CodeWords_Chaines.Count - 1)
                    chaines += strChaines + "[^" + strChaines + "\\\\]*(?:\\\\.[^" + strChaines + "\\\\]*)*" + strChaines;
                else
                    chaines += strChaines + "[^" + strChaines + "\\\\]*(?:\\\\.[^" + strChaines + "\\\\]*)*" + strChaines + "|";
            }
            #endregion
            #endregion

            keywordsRegexp = new Regex(keywords, RegexOptions.Compiled | RegexOptions.Multiline);
            typeNamesRegexp = new Regex(typeNames, RegexOptions.Compiled | RegexOptions.Multiline);
            functionsRegexp = new Regex(functions, RegexOptions.Compiled | RegexOptions.Multiline);
            chainesRegexp = new Regex(chaines, RegexOptions.Compiled | RegexOptions.Multiline);
            commentsRegexp = new Regex(comments, RegexOptions.Compiled | RegexOptions.Multiline);
            stringsRegexp = new Regex("\"[^\"\\\\\\r\\n]*(?:\\\\.[^\"\\\\\\r\\n]*)*\"", RegexOptions.Compiled | RegexOptions.Multiline);
            
            //commentsRegexp = new Regex(syntaxSettings.CommentString + ".*$", RegexOptions.Compiled | RegexOptions.Multiline);

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
        private void ProcessRegex(CodeTextBox codeTextbox, string line, int lineStart, Regex regexp, Color color)
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
                int nStart = lineStart + regMatch.Index;
                int nLenght = regMatch.Length;
                codeTextbox.SelectionStart = nStart;
                codeTextbox.SelectionLength = nLenght;
                codeTextbox.SelectionColor = color;
            }
        }
        /// <summary>
        /// Processes syntax highlightning for a line.
        /// </summary>
        /// <param name="richTextbox"></param>
        /// <param name="syntaxSettings"></param>
        /// <param name="line"></param>
        /// <param name="lineStart"></param>
        private void ProcessLine(CodeTextBox codeTextbox, string line, int lineStart)
        {
            codeTextbox.EnablePainting = false;

            // Save the position and make the whole line black
            int nPosition = codeTextbox.SelectionStart;
            codeTextbox.SelectionStart = lineStart;
            codeTextbox.SelectionLength = line.Length;
            codeTextbox.SelectionColor = Color.Black;


            // Process the keywords
            ProcessRegex(codeTextbox, line, lineStart, keywordsRegexp, codeTextbox.CodeColor_Keyword);

            // Process cached type names
            ProcessRegex(codeTextbox, line, lineStart, typeNamesRegexp, codeTextbox.CodeColor_Type);

            //process functions
            ProcessRegex(codeTextbox, line, lineStart, functionsRegexp, codeTextbox.CodeColor_Function);

            //process plain strings
            ProcessRegex(codeTextbox, line, lineStart, stringsRegexp, codeTextbox.CodeColor_PlainText);

            //process strings
            ProcessRegex(codeTextbox, line, lineStart, chainesRegexp, codeTextbox.CodeColor_Chaine);

            // Process comments
            if (codeTextbox.CodeWords_Comments.Count>0)
            {
                ProcessRegex(codeTextbox, line, lineStart, commentsRegexp, codeTextbox.CodeColor_Comment);
            }
            

            codeTextbox.SelectionStart = nPosition;
            codeTextbox.SelectionLength = 0;
            codeTextbox.SelectionColor = Color.Black;

            codeTextbox.EnablePainting = true;
        }
        private void ProcessSelection(CodeTextBox codeTextbox, int selectionStart, int selectionLength)
        {
            codeTextbox.EnablePainting = false;

            // Save the position and make the whole line black
            int nPosition = selectionStart;
            
            codeTextbox.SelectionStart = selectionStart;
            codeTextbox.SelectionLength = selectionLength;
            string text = codeTextbox.SelectedText;

            codeTextbox.SelectionColor = Color.Black;


            // Process the keywords
            ProcessRegex(codeTextbox, text, selectionStart, keywordsRegexp, codeTextbox.CodeColor_Keyword);

            // Process cached type names
            ProcessRegex(codeTextbox, text, selectionStart, typeNamesRegexp, codeTextbox.CodeColor_Type);

            //process functions
            ProcessRegex(codeTextbox, text, selectionStart, functionsRegexp, codeTextbox.CodeColor_Function);

            //process strings
            ProcessRegex(codeTextbox, text, selectionStart, stringsRegexp, codeTextbox.CodeColor_PlainText);

            // Process comments
            if (codeTextbox.CodeWords_Comments.Count > 0)
            {
                ProcessRegex(codeTextbox, text, selectionStart, commentsRegexp, codeTextbox.CodeColor_Comment);
            }

            codeTextbox.SelectionStart = nPosition;
            codeTextbox.SelectionLength = 0;
            codeTextbox.SelectionColor = Color.Black;

            codeTextbox.EnablePainting = true;
        }
        public void ProcessAllLines(CodeTextBox codeTextbox)
        {
            codeTextbox.EnablePainting = false;

            // Save the position and make the whole line black
            int nPosition = codeTextbox.SelectionStart;
            codeTextbox.SelectionStart = 0;
            codeTextbox.SelectionLength = codeTextbox.Text.Length;
            codeTextbox.SelectionColor = Color.Black;

            // Process the keywords
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, keywordsRegexp, codeTextbox.CodeColor_Keyword);

            // Process cached type names
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, typeNamesRegexp, codeTextbox.CodeColor_Type);

            //process functions
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, functionsRegexp, codeTextbox.CodeColor_Function);

            // Process plain strings
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, stringsRegexp, codeTextbox.CodeColor_PlainText);

            // Process comments
            if (codeTextbox.CodeWords_Comments.Count>0)
            {
                ProcessRegex(codeTextbox, codeTextbox.Text, 0, commentsRegexp, codeTextbox.CodeColor_Comment);
            }

            // Process strings
            ProcessRegex(codeTextbox, codeTextbox.Text, 0, chainesRegexp, codeTextbox.CodeColor_Chaine);


            codeTextbox.SelectionStart = nPosition;
            codeTextbox.SelectionLength = 0;
            codeTextbox.SelectionColor = Color.Black;


            //suppressHightlighting = false;
            codeTextbox.EnablePainting = true;
        }
        #endregion
    }
}
