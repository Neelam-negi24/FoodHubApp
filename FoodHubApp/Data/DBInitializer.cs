using FoodHubApp.Models;

namespace FoodHubApp.Data
{
    public static class DBInitializer
    {
        public static void Seed(FoodHubAppContext context)
        {
            // Check if the database has any food items
            if (context.Products.Any())
            {
                return; // Database has already been seeded
            }

            // List of food items to seed
            var products = new List<Product>
        {
            new Product { Id=1, Name = "Margherita Pizza", Description = "Classic pizza with tomato sauce, mozzarella, and basil.", Price = 8.99m, Category = "Main Course" },
            new Product { Id=2,Name = "Caesar Salad", Description = "Fresh romaine lettuce with Caesar dressing, croutons, and Parmesan.", Price = 6.50m, Category = "Appetizer" },
            new Product {Id=3, Name = "Spaghetti Carbonara", Description = "Pasta with creamy sauce, pancetta, and Parmesan cheese.", Price = 10.99m, Category = "Main Course" },
            new Product {Id=4, Name = "Garlic Bread", Description = "Toasted bread with garlic butter.", Price = 3.99m, Category = "Appetizer" },
            new Product { Id=5,Name = "Chocolate Lava Cake", Description = "Warm chocolate cake with molten center and vanilla ice cream.", Price = 5.50m, Category = "Dessert" },
            new Product { Id=6,Name = "Grilled Chicken Sandwich", Description = "Grilled chicken breast with lettuce, tomato, and mayo on a toasted bun.", Price = 7.99m, Category = "Main Course" },
            new Product { Id=7,Name = "Tiramisu", Description = "Classic Italian dessert with layers of espresso-soaked ladyfingers and mascarpone.", Price = 6.00m, Category = "Dessert" },
        };

            // Add the products to the database
            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }

}
