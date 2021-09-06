﻿using System;
using System.Windows.Forms;

namespace sozluk
{
    public partial class AddWordForm : Form
    {
        public Objects.Word ReturnedWord = null; // MainForm will access this field to retrieve entry from user
        
        private string Theme { get; }

        private string Language { get; }

        public AddWordForm(string theme, string langCode)
        {
            InitializeComponent();
            Theme = theme;
            Language = langCode;
        }

        #region Methods
        private void ApplyTheme()
        {
            throw new NotImplementedException();
        }

        private void ApplyLanguage()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Events
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string wordNameInput = TxtWordName.Text.Trim();

            if (!string.IsNullOrWhiteSpace(wordNameInput) && ListDefinitions.Items.Count is not 0)
            {
                Objects.Word word = new(wordNameInput);
                word.WikipediaArticleLink = Model.GrabWikipediaLink(wordNameInput);
                word.Definitions = new string[ListDefinitions.Items.Count];
                ListDefinitions.Items.CopyTo(word.Definitions, 0);
                if (ListReferences.Items.Count is not 0)
                {
                    word.References = new string[ListReferences.Items.Count];
                    ListReferences.Items.CopyTo(word.References, 0);
                }
                if (ListArticles.Items.Count is not 0)
                {
                    word.ArticleLinks = new string[ListArticles.Items.Count];
                    ListArticles.Items.CopyTo(word.ArticleLinks, 0);
                }

                ReturnedWord = word;
                DialogResult = DialogResult.OK;
            }
        }

        private void BtnAddDefinition_Click(object sender, EventArgs e)
        {
            string definitionInput = TxtDefinition.Text.Trim();
            if (!string.IsNullOrWhiteSpace(definitionInput))
            {
                ListDefinitions.Items.Add(definitionInput);
                TxtDefinition.Clear();
                BtnRemoveDefinition.Enabled = true;
            }
        }

        private void BtnRemoveDefinition_Click(object sender, EventArgs e)
        {
            if (ListDefinitions.SelectedIndex is not -1)
                ListDefinitions.Items.RemoveAt(ListDefinitions.SelectedIndex);

            if (ListDefinitions.Items.Count is 0)
                BtnRemoveDefinition.Enabled = false;
        }

        private void BtnAddReference_Click(object sender, EventArgs e)
        {
            string referenceInput = TxtReference.Text.Trim();
            if (!string.IsNullOrWhiteSpace(referenceInput))
            {
                ListReferences.Items.Add(referenceInput);
                TxtReference.Clear();
                BtnRemoveReference.Enabled = true;
            }
        }

        private void BtnRemoveReference_Click(object sender, EventArgs e)
        {
            if (ListReferences.SelectedIndex is not -1)
                ListReferences.Items.RemoveAt(ListReferences.SelectedIndex);

            if (ListDefinitions.Items.Count is 0)
                BtnRemoveReference.Enabled = false;
        }

        private void BtnAddArticle_Click(object sender, EventArgs e)
        {
            string articleInput = TxtArticle.Text.Trim();
            if (!string.IsNullOrWhiteSpace(articleInput))
            {
                ListArticles.Items.Add(articleInput);
                TxtArticle.Clear();
                BtnRemoveArticle.Enabled = true;
            }
        }

        private void BtnRemoveArticle_Click(object sender, EventArgs e)
        {
            if (ListArticles.SelectedIndex is not -1)
                ListArticles.Items.RemoveAt(ListArticles.SelectedIndex);

            if (ListArticles.Items.Count is 0)
                BtnRemoveReference.Enabled = false;
        }
        #endregion
    }
}