using System.Text.Json;
using AspireSample.ApiService.Models;
using Microsoft.EntityFrameworkCore;

namespace AspireSample.ApiService.Data
{
    public static class DataSetInitializer
    {
        public static void InitializePostalCodes(this FedDbContext context)
        {
            if (context.PostalCodes.Count() == 0)
            {
                JsonDocument jsonDoc = JsonDocument.Parse(System.IO.File.ReadAllText("Data/sqlserver/apeldoorn.json"));
                int id = 1;
                var nextId = 1;
                jsonDoc.RootElement.GetProperty("features").EnumerateArray().ToList().ForEach(je =>
                {
                    if (id > 1000)
                    {
                        return;
                    }

                    var properties = je.GetProperty("properties");
                    nextId = id;
                    var postalCode = new PostalCode
                    {
                        Id = nextId,
                        Code = properties.GetProperty("postcode").GetString(),
                        City = properties.GetProperty("woonplaatsnaam").GetString(),
                        HouseNumber = properties.GetProperty("huisnummer").GetInt16().ToString(),
                        StreetName = properties.GetProperty("openbareruimtenaam").GetString()
                    };
                    context.PostalCodes.Add(postalCode);

                    context.SaveChanges();
                    id++;
                });
            }
        }

        public static void InitializeRecipes(this FedDbContext context)
        {
            if (context.Recipes.Count() == 0)
            {
                var recipes = new List<Recipe>
                {
                    new Recipe { Id = 1, Name = "Chocolate Cake", Ingredients = "Flour, Sugar, Cocoa, Baking Powder, Eggs, Milk, Butter", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 2, Name = "Vanilla Cake", Ingredients = "Flour, Sugar, Baking Powder, Eggs, Milk, Butter, Vanilla Extract", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 3, Name = "Red Velvet Cake", Ingredients = "Flour, Sugar, Cocoa, Baking Powder, Eggs, Milk, Butter, Red Food Coloring", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 4, Name = "Carrot Cake", Ingredients = "Flour, Sugar, Baking Powder, Eggs, Milk, Butter, Carrots", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 5, Name = "Lemon Cake", Ingredients = "Flour, Sugar, Baking Powder, Eggs, Milk, Butter, Lemon Zest", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 6, Name = "Cheesecake", Ingredients = "Cream Cheese, Sugar, Eggs, Graham Crackers, Butter", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 7, Name = "Pineapple Upside-Down Cake", Ingredients = "Flour, Sugar, Baking Powder, Eggs, Milk, Butter, Pineapple", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 8, Name = "Black Forest Cake", Ingredients = "Flour, Sugar, Cocoa, Baking Powder, Eggs, Milk, Butter, Cherries", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 9, Name = "Coffee Cake", Ingredients = "Flour, Sugar, Baking Powder, Eggs, Milk, Butter, Coffee", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 10, Name = "Banana Cake", Ingredients = "Flour, Sugar, Baking Powder, Eggs, Milk, Butter, Bananas", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 11, Name = "Strawberry Shortcake", Ingredients = "Flour, Sugar, Baking Powder, Eggs, Milk, Butter, Strawberries", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 12, Name = "Pumpkin Cake", Ingredients = "Flour, Sugar, Baking Powder, Eggs, Milk, Butter, Pumpkin", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 13, Name = "Coconut Cake", Ingredients = "Flour, Sugar, Baking Powder, Eggs, Milk, Butter, Coconut", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 14, Name = "Almond Cake", Ingredients = "Flour, Sugar, Baking Powder, Eggs, Milk, Butter, Almond Extract", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 15, Name = "Blueberry Cake", Ingredients = "Flour, Sugar, Baking Powder, Eggs, Milk, Butter, Blueberries", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 16, Name = "Raspberry Cake", Ingredients = "Flour, Sugar, Baking Powder, Eggs, Milk, Butter, Raspberries", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 17, Name = "Peach Cake", Ingredients = "Flour, Sugar, Baking Powder, Eggs, Milk, Butter, Peaches", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 18, Name = "Apple Cake", Ingredients = "Flour, Sugar, Baking Powder, Eggs, Milk, Butter, Apples", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 19, Name = "Mango Cake", Ingredients = "Flour, Sugar, Baking Powder, Eggs, Milk, Butter, Mango", Preparations = "Mix ingredients and bake at 350°F for 30 minutes.", IsSecret = false },
                    new Recipe { Id = 20, Name = "Secret Chocolate Cake", Ingredients = "Secret Ingredients", Preparations = "Secret Preparation", IsSecret = true },
                    new Recipe { Id = 21, Name = "Secret Vanilla Cake", Ingredients = "Secret Ingredients", Preparations = "Secret Preparation", IsSecret = true },
                    new Recipe { Id = 22, Name = "Secret Red Velvet Cake", Ingredients = "Secret Ingredients", Preparations = "Secret Preparation", IsSecret = true },
                    new Recipe { Id = 23, Name = "Secret Carrot Cake", Ingredients = "Secret Ingredients", Preparations = "Secret Preparation", IsSecret = true },
                    new Recipe { Id = 24, Name = "Secret Lemon Cake", Ingredients = "Secret Ingredients", Preparations = "Secret Preparation", IsSecret = true },
                    new Recipe { Id = 25, Name = "Secret Cheesecake", Ingredients = "Secret Ingredients", Preparations = "Secret Preparation", IsSecret = true },
                    new Recipe { Id = 26, Name = "Secret Pineapple Upside-Down Cake", Ingredients = "Secret Ingredients", Preparations = "Secret Preparation", IsSecret = true },
                    new Recipe { Id = 27, Name = "Secret Blueberry Cake", Ingredients = "Secret Ingredients", Preparations = "Secret Preparation", IsSecret = true },
                    new Recipe { Id = 28, Name = "Secret Raspberry Cake", Ingredients = "Secret Ingredients", Preparations = "Secret Preparation", IsSecret = true },
                    new Recipe { Id = 29, Name = "Secret Peach Cake", Ingredients = "Secret Ingredients", Preparations = "Secret Preparation", IsSecret = true }
                };

                context.Recipes.AddRange(recipes);
                context.SaveChanges();
            }
        }
    }
}