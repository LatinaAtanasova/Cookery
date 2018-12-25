using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cookery.Data;
using Cookery.Models;
using Cookery.Services.Contracts;

namespace Cookery.Services.Services
{
    public class CookeryArticleService : ICookeryArticleService
    {
        private readonly CookeryDbContext dbContext;

        public CookeryArticleService(CookeryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<CookeryArticle> AllArticles()
        {
            return dbContext.CookeryArticles.ToList();
        }

        public CookeryArticle GetArticle(int id)
        {
            var article = this.dbContext.CookeryArticles.FirstOrDefault(x => x.Id == id);
            return article;
        }
    }
}
