using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Cookery.Data;
using Cookery.Models;
using Cookery.Models.Enums;
using Cookery.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;

namespace Cookery.Web.Middlewares
{
    public class SeedDataMiddleware
    {
        private readonly RequestDelegate next;

        public SeedDataMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context,
                                 CookeryDbContext dbContext,
                                 RoleManager<IdentityRole> roleManager)
        {
            if (!dbContext.Roles.Any())
            {
                await this.SeedRoles(roleManager);
            }

            if (!dbContext.ShoppingItems.Any())
            {
                this.SeedShoppingItems(dbContext);
            }

            if (!dbContext.CookeryArticles.Any())
            {
                this.SeedCookeryArticles(dbContext);
            }

            if (!dbContext.Recipes.Any())
            {
                this.SeedRecipes(dbContext);
            }

            await this.next(context);
        }

        private void SeedRecipes(CookeryDbContext dbContext)
        {
            var recipes = new List<Recipe>();

            recipes.AddRange(new List<Recipe>
            {
                new Recipe
                {
                    RecipeName = "Bulgarian Tomato Salad (Shopska Salata)",
                    isPublished = true,
                    CookeryCategory = CookeryCategory.Salad,
                    CookingTime = "15 min",
                    Description = "In a large bowl, place tomatoes, cucumber, peppers, onion and parsley, and toss. Place oil, vinegar, salt, and pepper to taste in a screw-top jar. Cover and shake until well blended. Toss dressing with vegetables, turn into a serving bowl and refrigerate until ready to serve. Top with crumbled cheese and portion on chilled plates. Serve with hearty bread and a glass of rakia.",
                    PhotoUrl = "bulgarian-shopska-salata.png",
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
                    Description = "Place bacon in a large skillet and cook over medium-high heat, turning occasionally, until evenly browned, about 10 minutes. Drain bacon slices on paper towels; crumble. " +
                                  "Stir chicken, bacon, tomato, and celery together in a bowl." +
                                  "Whisk mayonnaise, parsley, green onions, lemon juice, Worcestershire sauce, salt, and black pepper together in a bowl until dressing is smooth. Pour dressing over chicken mixture; toss to coat.Refrigerate until chilled, at least 30 minutes." +
                                  "Stir chicken mixture and serve over romaine lettuce leaves; garnish with avocado slices.",
                    PhotoUrl = "chicken_salad.png",
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
                },
                new Recipe
                {
                    RecipeName = "Summer Corn Salad",
                    isPublished = true,
                    CookeryCategory = CookeryCategory.Salad,
                    CookingTime = "45 min",
                    Description = "Bring a large pot of lightly salted water to a boil. Cook corn in boiling water for 7 to 10 minutes, or until desired tenderness. Drain, cool, and cut kernels off the cob with a sharp knife. " +
                                  "In a large bowl, toss together the corn, tomatoes, onion, basil, oil, vinegar, salt and pepper. Chill until serving.",
                    PhotoUrl = "summer_corn_salad.png",
                    Products = new List<RecipeProduct>
                    {
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Corn",
                                ProductUnit = "2 cans"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Tomato",
                                ProductUnit = "3"
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
                                ProductName = "Fresh basil",
                                ProductUnit = "1/4 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Olive oil",
                                ProductUnit = "1/4 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "White vinegar",
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
                    }
                },
                new Recipe
                {
                    RecipeName = "Lemon Chicken and Potato Bake",
                    isPublished = true,
                    CookeryCategory = CookeryCategory.Meal,
                    CookingTime = "1 hour 10 min",
                    Description = "Preheat oven to 425 degrees F (220 degrees C). Grease a large baking sheet with sides." +
                                  "Place chicken quarters on prepared baking sheet. Stir potatoes, olive oil, juice of 1 lemon, basil, oregano, salt, pepper, and lemon herb seasoning together in a large bowl to coat potatoes. Arrange potatoes around chicken on baking sheet. Pour about 3/4 of oil mixture over chicken, reserving remaining oil; drizzle remaining lemon juice over chicken and potatoes." +
                                  "Bake in the preheated oven for about 30 minutes; shake baking sheet to loosen potatoes, then continue baking for 15 minutes.Place green beans in reserved oil mixture; toss to coat.Remove chicken mixture from oven; pour green bean mixture over chicken and potatoes." +
                                  "Return pan to the oven; bake until green beans are tender with a bite, chicken is no longer pink at the bone and juices run clear, about 15 minutes.An instant-read thermometer inserted near the bone should read 165 degrees F(74 degrees C)",
                    PhotoUrl = "lemon_chicken_potato.png",
                    Products = new List<RecipeProduct>
                    {
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Chicken",
                                ProductUnit = "4 leg quarters"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Potatoes",
                                ProductUnit = "1 small bag"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Olive oil",
                                ProductUnit = "1/2 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Lemon",
                                ProductUnit = "2, juiced"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Dried Basil",
                                ProductUnit = "2 tablespoons"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Herb seasoning",
                                ProductUnit = "2 tablespoons"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Fresh green beans",
                                ProductUnit = "1 package"
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
                    }
                },
                new Recipe
                {
                    RecipeName = "Lemon Chicken and Potato Bake",
                    isPublished = true,
                    CookeryCategory = CookeryCategory.Meal,
                    CookingTime = "40 min",
                    Description = "Season pork chops with salt, pepper, and garlic salt to taste." +
                                  "In a large skillet, brown the chops over medium-high heat. Add the onion and mushrooms, and saute for one minute. Pour cream of mushroom soup over chops.Cover skillet, and reduce temperature to medium - low. Simmer 20 to 30 minutes, or until chops are cooked through.",
                    PhotoUrl = "mushroom_pork_chops.png",
                    Products = new List<RecipeProduct>
                    {
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Pork chops",
                                ProductUnit = "4"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Onion",
                                ProductUnit = "1, sliced"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Fresh Mushroom",
                                ProductUnit = "1/2 kg"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Condensed cream",
                                ProductUnit = "1 can"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Garlic salt",
                                ProductUnit = "to taste"
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
                    }
                },
                new Recipe
                {
                    RecipeName = "Strawberry Angel Food Dessert",
                    isPublished = true,
                    CookeryCategory = CookeryCategory.Dessert,
                    CookingTime = "15 min",
                    Description = "Crumble the cake into a 9x13 inch dish." +
                                  "Beat the cream cheese and sugar in a medium bowl until light and fluffy. Fold in whipped topping. Mash the cake down with your hands and spread the cream cheese mixture over the cake." +
                                  "In a bowl, combine strawberries and glaze until strawberries are evenly coated. Spread over cream cheese layer. Chill until serving.",
                    PhotoUrl = "strawberry_dessert.png",
                    Products = new List<RecipeProduct>
                    {
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Food cake",
                                ProductUnit = "1"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Cream cheese",
                                ProductUnit = "2 packages"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "White sugar",
                                ProductUnit = "1 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Frozen whipped topping",
                                ProductUnit = "1 container"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Fresh strawberries",
                                ProductUnit = "1/4 kg"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Strawberry glaze",
                                ProductUnit = "1 jar"
                            }
                        }
                    }
                },
                new Recipe
                {
                    RecipeName = "Tiramisu",
                    isPublished = true,
                    CookeryCategory = CookeryCategory.Dessert,
                    CookingTime = "3 hours 30 min",
                    Description = "In a medium bowl beat together the egg yolks and 1/3 cup of sugar. Using a wooden spoon stir in mascarpone cheese, beaten egg whites, cream and kirschwasser; stir until smooth. Set aside." +
                                  "Dissolve remaining 2/3 cup sugar in coffee. Quickly, to avoid complete saturation, dip ends of ladyfingers in coffee mixture. Place ladyfingers in a single layer in a 9 x 13 inch glass baking dish. Spread a layer of cheese mixture over the ladyfingers; repeat layers, ending with cheese mixture." +
                                  "Cover and refrigerate for several hours. Sprinkle with cocoa just before serving.",
                    PhotoUrl = "tiramisu.png",
                    Products = new List<RecipeProduct>
                    {
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Egg yolks",
                                ProductUnit = "6"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "White sugar",
                                ProductUnit = "1 cup"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Mascarpone cheese",
                                ProductUnit = "500 g"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Egg whites",
                                ProductUnit = "6, stiffly beaten"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Heavy cream",
                                ProductUnit = "1/4 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Kirschwasser",
                                ProductUnit = "3 tablespoon"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Brewed coffee",
                                ProductUnit = "1 1/4 cups strong, cold"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Ladyfingers",
                                ProductUnit = "25"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Cocoa powder",
                                ProductUnit = "1 tablespoon, unsweetened"
                            }
                        }
                    }
                },
                new Recipe
                {
                    RecipeName = "Breakfast Cups",
                    isPublished = true,
                    CookeryCategory = CookeryCategory.Breakfast,
                    CookingTime = "30 min",
                    Description = "Preheat oven to 400 degrees F (200 degrees C). Grease 18 muffin cups with cooking spray." +
                                  "Roll out biscuit dough on a lightly floured surface to form 5-inch rounds. Place each round in the prepared muffin cups, pressing into the base and sides to form a dough cup." +
                                  "Cook and stir sausage in a skillet over medium-high heat until browned and cooked through, 5 to 10 minutes; drain fat. Spoon sausage into dough cups. Whisk eggs, milk, salt, and pepper together in a bowl until well-beaten.Pour egg mixture into each dough cup, filling each just below the top of the biscuit dough.Sprinkle Cheddar cheese on top of egg mixture." +
                                  "Bake in the preheated oven until eggs are set and biscuit dough is golden, 15 to 18 minutes.",
                    PhotoUrl = "breakfast_cups.png",
                    Products = new List<RecipeProduct>
                    {
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Biscuits",
                                ProductUnit = "18 refrigerated, unbaked"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Sausage",
                                ProductUnit = "3 pieces"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Eggs",
                                ProductUnit = "7"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Milk",
                                ProductUnit = "1/2 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Cheddar cheese",
                                ProductUnit = "1 cup"
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
                        }
                    }
                },
                new Recipe
                {
                    RecipeName = "Irish Breakfast",
                    isPublished = true,
                    CookeryCategory = CookeryCategory.Breakfast,
                    CookingTime = "15 min",
                    Description = "Lay the bacon slices in a single layer in a large skillet. Fry over medium heat until it begins to get tinged with brown. Fry on both sides. Remove from pan, but save grease." +
                                  "Melt butter in skillet. Crack eggs into pan, being careful not to break yolks.Place tomato slices, mushrooms, and bread in pan.Fry gently, stirring mushrooms and tomatoes occasionally. Keep everything separate.Turn bread over to brown on both sides." +
                                  "When egg whites are set, but yolks are still runny, dish half of everything onto each of 2 warmed plates, and serve immediately.",
                    PhotoUrl = "irish_breakfast.png",
                    Products = new List<RecipeProduct>
                    {
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Bacon",
                                ProductUnit = "6 thick slices"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Butter",
                                ProductUnit = "2 tablespoons"
                            },
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Eggs",
                                ProductUnit = "4"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Tomatoes",
                                ProductUnit = "2"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Whole mushrooms",
                                ProductUnit = "1 1/2 cup"
                            }
                        },
                        new RecipeProduct
                        {
                            Product = new Product
                            {
                                ProductName = "Bread",
                                ProductUnit = "4 slices"
                            }
                        }
                    }
                }







            });

            dbContext.Recipes.AddRange(recipes);
            dbContext.SaveChanges();

        }

        private void SeedCookeryArticles(CookeryDbContext dbContext)
        {
            List<CookeryArticle> articles = new List<CookeryArticle>();
            articles.AddRange(new List<CookeryArticle>
            {
                new CookeryArticle
                {
                    Title = "The Value of Cooking",
                    Author = "Andrew Smith",
                    ArticleContent =
                        "Why teach your children to cook? It may seem that it will create more of a mess than it’s worth but there are many good reasons to introduce your children to the joys of cooking. " +
                        "It gives you the opportunity to teach your children valuable lessons. Cooking can be a great way to reinforce subjects being taught at school or introduce new ones. Here are just a few of the skills cooking can help teach your children:" +
                        "Simple math skills – Many recipes can be doubled or halved, which will require math skills such as dividing and multiplying." +
                        "Nutrition – Use your time together in the kitchen to teach your children the importance of good nutrition. Encourage them to try new fruits, vegetables, and other healthy foods." +
                        "Following directions – Recipes need to be put together in a specific order.This can help show your children the importance of following directions, whether they are baking a cake or doing a science experiment." +
                        "Measuring – Using measuring cups to get the appropriate amounts of ingredients teaches about the importance of careful and accurate measuring." +
                        "Sensory awareness – Cooking and baking can expose your child to new textures, tastes, colors, odors, and more." +
                        "Language skills – Reading food labels and recipes can help your children improve their reading skills and learn the meanings of unfamiliar words." +
                        "Art – From making a face out of vegetables on a pizza to decorating cookies with sprinkles and icing, cooking provides endless opportunities for artistic expression!" +
                        "Cultural awareness – Are your children intrigued by the exotic flavors, colors, and aromas found in many ethnic dishes ? Introducing your children to ethnic dishes may encourage them to learn more about the culture and people that inspired the dish." +
                        "Cooking can help channel your children’s natural curiosity.Why not take advantage of your children’s inquisitiveness and introduce them to the fun and pleasures of being in the kitchen ? Since many foods will change dramatically in size, color, and texture during the cooking or baking process, your children will likely be fascinated as they watch these changes take place right before their eyes." +
                        "Learning to cook may help your children overcome picky eating habits.Children who are finicky about what they eat may be more inclined to try foods if they prepared or assisted in making them. They might also develop a sense of pride and accomplishment about the things that they have created." +
                        "Cooking is a great way for the family to spend time together.Families can get so busy that they lose touch, even while living in the same house!So why not take a Saturday afternoon and bake some cookies or a cake together and get some conversation going?" +
                        "Teaching your children to cook may inspire a future career choice. By taking the time to introduce your children to the kitchen, you may be helping them develop an interest in cooking that will become a lifelong passion.And who knows, you just might end up with a professional chef in the family!",
                    IssuedOn = DateTime.ParseExact("24/12/2018", "dd/MM/yyyy", CultureInfo.InvariantCulture)
                },
                new CookeryArticle
                {
                    Title = "8 Secrets For a Moist & Juicy Roast Turkey",
                    Author = "Jennifer Armentrout",
                    ArticleContent =
                        "A perfectly roasted turkey is the holy grail of every cook on Thanksgiving. To help you achieve this goal, we’ve collected some tips from some of our best authors." +
                        "For more tips, recipes, menus, and videos to help you pull off the perfect Thanksgiving, check out our Guide to Thanksgiving Dinner." +
                        "We’ve also got answers to your burning turkey questions and the cure for your pie anxiety.If it’s menu anxiety you’re feeling right now, use our Thanksgiving Menu Maker to select recipes and get a shopping list and timeline to keep you on track." +
                        "1.Choose a fresh turkey instead of a frozen one. Ice crystals that form during freezing damage a turkey’s muscle cells.When the bird thaws and roasts, fluids leak more readily from the damaged cells, drying out the meat." +
                        "2.Roast two small turkeys rather than one large one. Smaller turkeys roast more evenly than large ones, so for feeding a crowd, two small turkeys are a better option.They’ll cook quicker, too" +
                        "3.Brine the turkey. A turkey soaked in a salt - water solution absorbs both the salt and the water, so it’s moister to begin with as well as seasoned on the inside.You can flavor a brine as well.Read here for more on the science behind brining." +
                        "4.Rub soft butter under the skin. As it melts, it bastes the turkey and adds buttery flavor.For even more flavor, you can add herbs and spices to the butter(see, for instance, Smoked Paprika & Fennel Seed Roast Turkey with Onion Gravy)." +
                        "5.Truss loosely, or not at all. Legs tied up tightly against the sides of the turkey take longer to roast, putting the breast meat in jeopardy of overcooking while the legs take their time. For more on how to truss, watch our video." +
                        "6.Roast the turkey upside down at first. Placing the turkey, breast side down, on a V - rack for the first hour or so of roasting essentially allows it to baste itself.Any marks left by the rack will disappear once you flip the turkey over and finish roasting it." +
                        "7.Don’t overcook it. Use a thermometer, either instant - read or probe - style, to monitor the temperature in the thickest part of the thigh(be careful not to hit the bone).You’re aiming for 170°F." +
                        "8.Let the turkey rest before carving. The intense heat of the oven forces the juices into the center of the bird, so after roasting, let the turkey rest for roughly 20 minutes(enough time to make the gravy).The juices will redistribute, and you’ll get moister slices.",
                    IssuedOn = DateTime.ParseExact("13/11/2018", "dd/MM/yyyy", CultureInfo.InvariantCulture)
                },
                new CookeryArticle
                {
                    Title = "How Learning To Cook Can Boost Your Creativity",
                    Author = "Faisal Hoque",
                    ArticleContent =
                        "Like many, I was introduced to cooking when I started college at 17 — to survive. Since then I have traveled many miles, experienced many cuisines, and cooked many meals. Along the way I have learned a few things about food, the process of cooking, and the impact it makes on our mind, body, and soul during good times and bad times. Food is the most fundamental of needs for our survival and almost every major event in our lives revolves around it." +
                        "It plays a vital role in the development of social interactions and social relationships. I find food to be sacred and the process of making food to be awakening and insightful. Although I am not professionally trained, cooking has become a joyful passion. The process of making food has taught me to be mindful, embrace creativity, and push for mastery. Below are a few lessons that might make you think differently the next time you enter your kitchen." +
                        "Ritualistic Cooking Can Enhance Mindfulness. Along with billions of others around the globe, I suffer from the daily grind of life. My affinity with mindful living is not grounded in any kind of scientific research — rather from constant self-analysis. I have found cooking is a means towards that journey of mindfulness. It's been said that the only two jobs of a Zen monk that are more important that sitting zazen (meditation) are cooking and cleaning. Cooking is a great way to practice mindfulness. Mindfulness is a state of active, open attention to the present. It simply means living in the moment and awakening to experience. And it takes practice to be mindful. I have found that when I ritualistically cook on a regular basis it enhances my ability to be mindful about everything else I do. In the 13th century, Japanese Zen master Dogen wrote \"Instructions for the Tenzo\", or head cook. In examining the manners and methods of preparing a meal at the Monastery, he reveals how to \"cook\"—or refine—your whole life. In one such instruction, he says \"When you boil rice, know that the water is your own life.\" How do we cultivate the mind that cares as deeply for an ordinary thing, like water, as it cares for our very own life? Sounds simple — but it's actually pretty hard — go ahead and try it. It comes from putting our entire mind into those simple tasks, concentrating deeply, and doing them intentionally and completely. And when we are mindful, it allows us to better connect with the:" +
                        "Past - What we have completed" +
                        "Present - The task at hand" +
                        "Future - How our task at hand moves us forward" +
                        "I believe, if we consciously think about the ingredients we choose, their preparation, the way we cook and the way we eat, it can contribute towards the development of mindfulness. Conscious Openness Is At The Heart of Any Creative Process. I don't ever follow a recipe for my cooking. I like to experiment, mix and match, and 'design' my meals. I make my decisions based on availability, my eating companions, and the hour of the day." +
                        "Over the years this awareness (during cooking) of resource, audience, and need helped me hone how I think.When I started cooking at the age of 17, just like life, I was unsure of the kitchen. Now I try to 'create' my food with confidence. It is entirely natural for me to mix Japanese mirin with Indian turmeric and Mexican chilies." +
                        "In 2006, chefs Ferran Adria of El Bulli, Heston Blumenthal of The Fat Duck, Thomas Keller of French Laundry and Per Se, and the writer Harold McGee put forward what they termed 'the international agenda for great cooking,' and while its focus is food, it could well serve as a manifesto for anyone who is in the business of creativity:" +
                        "\"We believe that today and in the future, a commitment to excellence requires openness to all resources that can help us give pleasure and meaning to people through the medium of food. In the past, cooks and their dishes were constrained by many factors: the limited availability of ingredients and ways of transforming them, limited understanding of cooking processes, and the necessarily narrow definitions and expectations embodied in local tradition. Today there are many fewer constraints, and tremendous potential for the progress of our craft. We can choose from the entire planet's ingredients, cooking methods, and traditions, and draw on all of human knowledge, to explore what it is possible to do with food and the experience of eating." +
                        "Just like making music or poetry, cooking requires understanding interconnectedness and harmonies. Anyone can mix and match two random sets of ingredients together, but not everyone can cook.Understanding the relationships between the ingredients and their interactions is crucial to creating a successful dish. This conscious openness is precisely what is at the heart of any creative process regardless of what we do and the medium we use." +
                        "Mastery Comes From Enthusiastic and Devoted Practice. Most mornings I prepare my son a balanced breakfast and a lunch pack between 6am and 6:15am.",
                    IssuedOn = DateTime.ParseExact("31/03/2014", "dd/MM/yyyy", CultureInfo.InvariantCulture)
                }
            });

            dbContext.CookeryArticles.AddRange(articles);
            dbContext.SaveChanges();
        }

        private void SeedShoppingItems(CookeryDbContext dbContext)
        {
            List<ShoppingItem> shoppingItems = new List<ShoppingItem>();
            shoppingItems.AddRange(new List<ShoppingItem>
            {
                new ShoppingItem{
                    Author = "Victoria Blashford-Snell",
                    Name = "The Cooking Book",
                    Price = 25.50m,
                    ShortDescription =
                    "The cookbook that really understands what you need in the kitchen, answering all your culinary questions, from what the finished dish should look like and if it can be prepared it ahead, to what to do with leftovers. Over 1, 000 mouth - watering recipes, thousands of explanatory photographs, and superb step - by - step guidance will teach you how to get great home - cooking on the table without fuss.",
                    Picture = "the_cooking_book.png",
                    ShoppingType = ShoppingType.Book
                },
                new ShoppingItem
                {
                    Author = "Sunset Editors",
                    Name = "Sunset Italian cook book",
                    Price = 30,
                    ShortDescription = "Italian cook book",
                    Picture = "italian_cook_book.png",
                    ShoppingType = ShoppingType.Book,
                },
                new ShoppingItem
                {
                    Author = "Nina Timm",
                    Name = "Easy Cooking",
                    Price = 35.50m,
                    ShortDescription = "Easy Cooking from Nina’s Kitchen is a recipe book, yes, but it also reads like a story book, every recipe comes with a memory or story that I have around that particular recipe.",
                    Picture = "Nina_Timm_Easy_Cooking.png",
                    ShoppingType = ShoppingType.Book
                },
                new ShoppingItem()
                {
                    Author = "Editor",
                    Name = "Fine Cooking",
                    Price = 6.35m,
                    ShortDescription = "For those who love to cook!",
                    Picture = "fine_cooking.png",
                    ShoppingType = ShoppingType.Magazine
                },
                new ShoppingItem()
                {
                    Author = "Editor",
                    Name = "Taste of Home",
                    Price = 10,
                    ShortDescription = "In each issue of Taste of Home Magazine, you'll find 100+ family-favorite recipes and tips - recipes from real cooks like you! Enjoy easy, tried-and-proven recipes, everyday ingredients, color photo of every dish, contest winners, 30-minute dishes, mom's best recipes and more.",
                    Picture = "taste_of_home.png",
                    ShoppingType = ShoppingType.Magazine
                },
                new ShoppingItem()
                {
                    Author = "Editor",
                    Name = "Everyday Food",
                    Price = 6.50m,
                    ShortDescription = "Quickly and Delicious",
                    Picture = "everyday-food.png",
                    ShoppingType = ShoppingType.Magazine
                }
            });

            dbContext.ShoppingItems.AddRange(shoppingItems);
            dbContext.SaveChanges();
        }

        private async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var adminRoleResult = await roleManager.CreateAsync(new IdentityRole(CookeryConstants.AdminRole));

            var userRoleResult = await roleManager.CreateAsync(new IdentityRole(CookeryConstants.UserRole));

            if (!adminRoleResult.Succeeded && !userRoleResult.Succeeded)
            {
                throw new ArgumentException("Roles are not created!");
            }
        }
    }
}
