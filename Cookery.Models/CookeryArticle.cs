using System;
using System.Collections.Generic;
using System.Text;

namespace Cookery.Models
{
    public class CookeryArticle
    {
        // News Collection, Food News

        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ArticleContent { get; set; }

        public DateTime IssuedOn { get; set; }

        public bool IsPublished => false;

    }
}
