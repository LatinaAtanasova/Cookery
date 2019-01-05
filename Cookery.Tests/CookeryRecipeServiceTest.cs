using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using Cookery.Data;
using Cookery.Models;
using Cookery.Models.Enums;
using Cookery.Services.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Cookery.Tests
{
    public class CookeryRecipeServiceTest
    {
        [Fact]
        public void ReturnAllRecipes()
        {
            var options = new DbContextOptionsBuilder<CookeryDbContext>()
                .UseInMemoryDatabase(databaseName: "Find_Cookery_Database") // Give a Unique name to the DB
                .Options;

            var count = 0;
            using (var dbContext = new CookeryDbContext(options)) // Initialize Testing Data
            {
                dbContext.Recipes.AddRange(GetRecipeSample());
                dbContext.SaveChanges();

                var service = new RecipeService(dbContext);
                count = service.AllPublishedRecipes().Count;

            }
            Assert.Equal(2, count);
        }

        private Recipe[] GetRecipeSample()
        {
            Recipe[] sample =
            {
                new Recipe
                {
                    RecipeName = "Bulgarian Tomato Salad (Shopska Salata)",
                    isPublished = true,
                    CookeryCategory = CookeryCategory.Salad,
                    CookingTime = "15 min",
                    Description =
                        "In a large bowl, place tomatoes, cucumber, peppers, onion and parsley, and toss. Place oil, vinegar, salt, and pepper to taste in a screw-top jar. Cover and shake until well blended. Toss dressing with vegetables, turn into a serving bowl and refrigerate until ready to serve. Top with crumbled cheese and portion on chilled plates. Serve with hearty bread and a glass of rakia.",
                    PhotoUrl = "bulgarian-shopska-salata.png",
                    Date = DateTime.Now,
                    Products = new List<RecipeProduct>
                    {
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Tomato",
                                ProductUnit = "4"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Cucumber",
                                ProductUnit = "1"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Onion",
                                ProductUnit = "1"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Parsley",
                                ProductUnit = "2 tablespoons"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "sunflower oil",
                                ProductUnit = "1/2 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "red wine vinegar",
                                ProductUnit = "1/4 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "salt",
                                ProductUnit = "small amount"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "cheese",
                                ProductUnit = "1/2 cup"
                            }
                        },
                    }
                },

                new Recipe
                {
                    RecipeName = "Chicken Salad with Bacon, Lettuce, and Tomato",
                    isPublished = true,
                    CookeryCategory = CookeryCategory.Salad,
                    CookingTime = "1 hour",
                    Description =
                        "Place bacon in a large skillet and cook over medium-high heat, turning occasionally, until evenly browned, about 10 minutes. Drain bacon slices on paper towels; crumble. " +
                        "Stir chicken, bacon, tomato, and celery together in a bowl." +
                        "Whisk mayonnaise, parsley, green onions, lemon juice, Worcestershire sauce, salt, and black pepper together in a bowl until dressing is smooth. Pour dressing over chicken mixture; toss to coat.Refrigerate until chilled, at least 30 minutes." +
                        "Stir chicken mixture and serve over romaine lettuce leaves; garnish with avocado slices.",
                    PhotoUrl = "chicken_salad.png",
                    Date = DateTime.Now,
                    Products = new List<RecipeProduct>
                    {
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Bacon",
                                ProductUnit = "5 slices"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Cooked chicken",
                                ProductUnit = "3 cups diced"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Celery",
                                ProductUnit = "2, thinly sliced"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Mayonnaise",
                                ProductUnit = "3/4 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "sunflower oil",
                                ProductUnit = "1/2 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Green onion",
                                ProductUnit = "2 tablespoons"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Parsley",
                                ProductUnit = "1 tablespoon"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Lemon juice",
                                ProductUnit = "1 teaspoon"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "salt",
                                ProductUnit = "to taste"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "black pepper",
                                ProductUnit = "to taste"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Lettuce",
                                ProductUnit = "12 leaves"
                            }
                        },
                    }
                }
            };

            return sample;
        }

        [Fact]
        public void CreateRecipe()
        {
            var options = new DbContextOptionsBuilder<CookeryDbContext>()
                .UseInMemoryDatabase(databaseName: "Find_Cookery_Database") // Give a Unique name to the DB
                .Options;

            var count = 2;
            using (var dbContext = new CookeryDbContext(options)) // Initialize Testing Data
            {
                var recipe = new Recipe
                {
                    RecipeName = "Bulgarian Tomato Salad (Shopska Salata)",
                    isPublished = true,
                    CookeryCategory = CookeryCategory.Salad,
                    CookingTime = "15 min",
                    Description =
                        "In a large bowl, place tomatoes, cucumber, peppers, onion and parsley, and toss. Place oil, vinegar, salt, and pepper to taste in a screw-top jar. Cover and shake until well blended. Toss dressing with vegetables, turn into a serving bowl and refrigerate until ready to serve. Top with crumbled cheese and portion on chilled plates. Serve with hearty bread and a glass of rakia.",
                    PhotoUrl = "bulgarian-shopska-salata.png",
                    Date = DateTime.Now,
                    Products = new List<RecipeProduct>
                    {
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Tomato",
                                ProductUnit = "4"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Cucumber",
                                ProductUnit = "1"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Onion",
                                ProductUnit = "1"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Parsley",
                                ProductUnit = "2 tablespoons"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "sunflower oil",
                                ProductUnit = "1/2 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "red wine vinegar",
                                ProductUnit = "1/4 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "salt",
                                ProductUnit = "small amount"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "cheese",
                                ProductUnit = "1/2 cup"
                            }
                        },
                    }
                };

                
                var service = new RecipeService(dbContext);
                service.CreateRecipe(recipe);
                count +=1;

            }
            Assert.Equal(3, count);
        }

        [Fact]
        public void GetRecipeById()
        {
            var options = new DbContextOptionsBuilder<CookeryDbContext>()
                .UseInMemoryDatabase(databaseName: "Find_Cookery_Database") // Give a Unique name to the DB
                .Options;

            Recipe result = null;

            using (var dbContext = new CookeryDbContext(options)) // Initialize Testing Data
            {
                var recipe = new Recipe
                {
                    RecipeName = "Bulgarian Tomato Salad (Shopska Salata)",
                    isPublished = true,
                    CookeryCategory = CookeryCategory.Salad,
                    CookingTime = "15 min",
                    Description =
                        "In a large bowl, place tomatoes, cucumber, peppers, onion and parsley, and toss. Place oil, vinegar, salt, and pepper to taste in a screw-top jar. Cover and shake until well blended. Toss dressing with vegetables, turn into a serving bowl and refrigerate until ready to serve. Top with crumbled cheese and portion on chilled plates. Serve with hearty bread and a glass of rakia.",
                    PhotoUrl = "bulgarian-shopska-salata.png",
                    Date = DateTime.Now,
                    Products = new List<RecipeProduct>
                    {
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Tomato",
                                ProductUnit = "4"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Cucumber",
                                ProductUnit = "1"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Onion",
                                ProductUnit = "1"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Parsley",
                                ProductUnit = "2 tablespoons"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "sunflower oil",
                                ProductUnit = "1/2 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "red wine vinegar",
                                ProductUnit = "1/4 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "salt",
                                ProductUnit = "small amount"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "cheese",
                                ProductUnit = "1/2 cup"
                            }
                        },
                    }
                };


                var service = new RecipeService(dbContext);
                service.CreateRecipe(recipe);
                result = service.GetRecipeById(1);

            }
            Assert.NotNull(result);
        }

        [Fact]
        public void UnpublishedRecipes()
        {
            var options = new DbContextOptionsBuilder<CookeryDbContext>()
                .UseInMemoryDatabase(databaseName: "Find_Cookery_Database") // Give a Unique name to the DB
                .Options;

            var count = 0;
            using (var dbContext = new CookeryDbContext(options)) // Initialize Testing Data
            {
                var recipeList = new List<Recipe>();
                var recipe = new Recipe
                {
                    RecipeName = "Bulgarian Tomato Salad (Shopska Salata)",
                    isPublished = false,
                    CookeryCategory = CookeryCategory.Salad,
                    CookingTime = "15 min",
                    Description =
                        "In a large bowl, place tomatoes, cucumber, peppers, onion and parsley, and toss. Place oil, vinegar, salt, and pepper to taste in a screw-top jar. Cover and shake until well blended. Toss dressing with vegetables, turn into a serving bowl and refrigerate until ready to serve. Top with crumbled cheese and portion on chilled plates. Serve with hearty bread and a glass of rakia.",
                    PhotoUrl = "bulgarian-shopska-salata.png",
                    Date = DateTime.Now,
                    Products = new List<RecipeProduct>
                    {
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Tomato",
                                ProductUnit = "4"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Cucumber",
                                ProductUnit = "1"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Onion",
                                ProductUnit = "1"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Parsley",
                                ProductUnit = "2 tablespoons"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "sunflower oil",
                                ProductUnit = "1/2 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "red wine vinegar",
                                ProductUnit = "1/4 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "salt",
                                ProductUnit = "small amount"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "cheese",
                                ProductUnit = "1/2 cup"
                            }
                        },
                    }
                };
                var recipe2 = new Recipe
                {
                    RecipeName = "Bulgarian Tomato Salad (Shopska Salata)",
                    isPublished = false,
                    CookeryCategory = CookeryCategory.Salad,
                    CookingTime = "15 min",
                    Description =
                        "In a large bowl, place tomatoes, cucumber, peppers, onion and parsley, and toss. Place oil, vinegar, salt, and pepper to taste in a screw-top jar. Cover and shake until well blended. Toss dressing with vegetables, turn into a serving bowl and refrigerate until ready to serve. Top with crumbled cheese and portion on chilled plates. Serve with hearty bread and a glass of rakia.",
                    PhotoUrl = "bulgarian-shopska-salata.png",
                    Date = DateTime.Now,
                    Products = new List<RecipeProduct>
                    {
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Tomato",
                                ProductUnit = "4"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Cucumber",
                                ProductUnit = "1"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Onion",
                                ProductUnit = "1"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Parsley",
                                ProductUnit = "2 tablespoons"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "sunflower oil",
                                ProductUnit = "1/2 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "red wine vinegar",
                                ProductUnit = "1/4 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "salt",
                                ProductUnit = "small amount"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "cheese",
                                ProductUnit = "1/2 cup"
                            }
                        },
                    }
                };

                recipeList.Add(recipe);
                recipeList.Add(recipe2);

                dbContext.AddRange(recipeList);
                dbContext.SaveChanges();

                var service = new RecipeService(dbContext);
                count = service.UnpublishedRecipes().Count;

            }
            Assert.Equal(2, count);
        }

        [Fact]
        public void UpdatePublishStatus()
        {
            var options = new DbContextOptionsBuilder<CookeryDbContext>()
                .UseInMemoryDatabase(databaseName: "Find_Cookery_Database") // Give a Unique name to the DB
                .Options;

            bool result = false;

            using (var dbContext = new CookeryDbContext(options)) // Initialize Testing Data
            {
                var recipe = new Recipe
                {
                    RecipeName = "Bulgarian Tomato Salad (Shopska Salata)",
                    isPublished = false,
                    CookeryCategory = CookeryCategory.Salad,
                    CookingTime = "15 min",
                    Description =
                        "In a large bowl, place tomatoes, cucumber, peppers, onion and parsley, and toss. Place oil, vinegar, salt, and pepper to taste in a screw-top jar. Cover and shake until well blended. Toss dressing with vegetables, turn into a serving bowl and refrigerate until ready to serve. Top with crumbled cheese and portion on chilled plates. Serve with hearty bread and a glass of rakia.",
                    PhotoUrl = "bulgarian-shopska-salata.png",
                    Date = DateTime.Now,
                    Products = new List<RecipeProduct>
                    {
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Tomato",
                                ProductUnit = "4"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Cucumber",
                                ProductUnit = "1"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Onion",
                                ProductUnit = "1"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Parsley",
                                ProductUnit = "2 tablespoons"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "sunflower oil",
                                ProductUnit = "1/2 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "red wine vinegar",
                                ProductUnit = "1/4 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "salt",
                                ProductUnit = "small amount"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "cheese",
                                ProductUnit = "1/2 cup"
                            }
                        },
                    }
                };


                var service = new RecipeService(dbContext);
                service.CreateRecipe(recipe);
                bool newStatus = true;
                service.UpdatePublishStatus(recipe, newStatus);
                result = recipe.isPublished;

            }
            Assert.True(result);
        }
    }
}
