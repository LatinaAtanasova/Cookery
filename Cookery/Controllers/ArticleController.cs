using System.Collections.Generic;
using AutoMapper;
using Cookery.Services.Contracts;
using Cookery.Web.Models.Articles;
using Microsoft.AspNetCore.Mvc;

namespace Cookery.Web.Controllers
{
    public class ArticleController : Controller
    {

        private readonly ICookeryArticleService service;
        private readonly IMapper mapper;

        public ArticleController(ICookeryArticleService service,
                                IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET
        public IActionResult AllArticles()
        {
            var articles = this.service.AllArticles();
            var articleModels = new List<ArticleViewModel>();

            foreach (var article in articles)
            {
                var articleModel = this.mapper.Map<ArticleViewModel>(article);
                articleModels.Add(articleModel);
            }
            
            return this.View(articleModels);
        }

        public IActionResult ArticleDetails(int id)
        {
            var article = this.service.GetArticle(id);
            var articleModel = this.mapper.Map<ArticleViewModel>(article);

            return this.View(articleModel);
        }
    }
}