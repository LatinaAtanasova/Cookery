using System;
using System.Collections.Generic;
using System.Text;
using Cookery.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cookery.Data
{
    public class CookeryDbContext : IdentityDbContext<CookeryUser>
    {
        public CookeryDbContext(DbContextOptions<CookeryDbContext> options)
            : base(options)
        {
        }

        public DbSet<CookeryArticle> CookeryArticles { get; set; }

        public DbSet<CustomLog> CustomLogs { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<ShoppingItem> ShoppingItems { get; set; }

        public DbSet<RecipeProduct> RecipesProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
          

            builder.Entity<RecipeProduct>()
                .HasKey(x => new { x.ProductId, x.RecipeId });

            builder.Entity<RecipeProduct>()
                .HasOne(x => x.Product)
                .WithMany(x => x.Recipes)
                .HasForeignKey(x => x.ProductId);

            builder.Entity<RecipeProduct>()
                .HasOne(x => x.Recipe)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.RecipeId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(true);
        }
    }

    
}
