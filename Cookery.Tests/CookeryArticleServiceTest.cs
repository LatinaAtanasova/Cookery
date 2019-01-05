using System;
using System.Collections.Generic;
using System.Text;
using Cookery.Data;
using Cookery.Models;
using Cookery.Services.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Cookery.Tests
{
    public class CookeryArticleServiceTest
    {
        [Fact]
        public void ReturnAllArticles()
        {
            var options = new DbContextOptionsBuilder<CookeryDbContext>()
                .UseInMemoryDatabase(databaseName: "Find_Cookery_Database") // Give a Unique name to the DB
                .Options;

            var count = 0;
            using (var dbContext = new CookeryDbContext(options)) // Initialize Testing Data
            {
                dbContext.CookeryArticles.AddRange(GetArticleData());
                dbContext.SaveChanges();

                var service = new CookeryArticleService(dbContext);
                count = service.AllArticles().Count;

            }
            Assert.Equal(2, count);
        }

        private CookeryArticle[] GetArticleData()
        {
            CookeryArticle[] SampleArticles =
            {
                new CookeryArticle
                {
                    ArticleContent = "Unce Upon a time.......",
                    Author = "James Born",
                    IsPublished = false,
                    IssuedOn = DateTime.UtcNow,
                    Title = "Something"
                },
                new CookeryArticle
                {
                    ArticleContent = "Not so interesting",
                    Author = "Michael Still",
                    IssuedOn = DateTime.UtcNow,
                    Title = "Go go"
                }
            };
            return SampleArticles;
        }

        [Fact]
        public void GetArticleById()
        {
            var options = new DbContextOptionsBuilder<CookeryDbContext>()
                .UseInMemoryDatabase(databaseName: "Find_Cookery_Database") // Give a Unique name to the DB
                .Options;

            CookeryArticle result = null;

            using (var dbContext = new CookeryDbContext(options)) // Initialize Testing Data
            {
                dbContext.CookeryArticles.AddRange(GetArticleData());
                dbContext.SaveChanges();

                var service = new CookeryArticleService(dbContext);
                result = service.GetArticle(1);

            }
            Assert.NotNull(result);


        }
    }
}
