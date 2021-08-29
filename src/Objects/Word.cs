﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sozluk.src.Objects
{
    public class Word
    {
        public string Name { get; }

        public List<string> Definitions { get; internal set; }

        public List<string> References { get; internal set; }

        public string WikipediaArticleLink { get; internal set; }

        public string ArticleLink { get; internal set; }

        /// <summary>
        /// This constructor gets a line of dictionary file and uses the corresponding syntax to select matching fields, initializing a word.
        /// </summary>
        /// <param name="dictionaryFileLine">One line of word entry</param>
        public Word(string dictionaryFileLine)
        {
            Definitions = new List<string>();
            References = new List<string>();
            string[] keyAndValue = Model.Tokenize(dictionaryFileLine);
            Name = keyAndValue[0];
            foreach (var item in keyAndValue.Skip(1).ToArray())
            {
                if (Model.IsUrl(item))
                {
                    if (new Uri(Model.ParseUrl(item)).Host.Contains("wikipedia"))
                    {
                        WikipediaArticleLink = item;
                    }
                    else
                    {
                        ArticleLink = item;
                    }
                }
                else if (item.Contains("<ref="))
                {
                    References.Add(item.Replace("<ref=", "").Replace(">", ""));
                }
                else
                {
                    Definitions.Add(item);
                }
            }
        }

        internal Word(src.Objects.Word word)
        {
            Name = word.Name;
            Definitions = word.Definitions;
            References = word.References;
            WikipediaArticleLink = word.WikipediaArticleLink;
            ArticleLink = word.ArticleLink;
        }

        public override string ToString()
        {
            string key;
            List<string> values = new();

            key = Name;

            if (Definitions.Any())
                values.AddRange(Definitions);
            if (References.Any())
                References.ForEach(x => values.Add($"<ref={x}>"));
            if (ArticleLink is not null)
                values.Add(ArticleLink);
            if (WikipediaArticleLink is not null)
                values.Add(WikipediaArticleLink);

            StringBuilder builder = new();
            builder.AppendFormat("\"{0}\": ", key);
            for (int i = 0; i < values.Count; i++)
            {
                builder.AppendFormat("\"{0}\" ", values[i]);
            }

            return builder.ToString().TrimEnd();
        }
    }
}