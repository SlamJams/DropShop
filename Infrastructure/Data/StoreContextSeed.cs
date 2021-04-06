using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    //Using this class to create seed data for our application.
    public class StoreContextSeed
    {
        public static async Task SeedDataAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                
                //If there is NO data in productBrands table, add seed data for productBrands.
                if(!context.ProductBrands.Any())
                {
                    //@ symbol is to tell the string constructor to ignore escape characters and line breaks.
                    var productBrands = File.ReadAllText(@"..\Infrastructure\Data\SeedData\brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrands);

                    foreach(var brand in brands)
                    {
                        context.ProductBrands.Add(brand);
                    }

                    await context.SaveChangesAsync();
                }

                //If there is NO data in productTypes table, add seed data for productTypes.
                if(!context.ProductTypes.Any())
                {
                    //@ symbol is to tell the string constructor to ignore escape characters and line breaks.
                    var productTypes = File.ReadAllText(@"..\Infrastructure\Data\SeedData\types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(productTypes);

                    foreach(var type in types)
                    {
                        context.ProductTypes.Add(type);
                    }

                    await context.SaveChangesAsync();
                }
                
                //If there is NO data in products table, add seed data for products.
                //NOTE: need to create/populate foreign tables before main products table (otherwise you will get an error).
                if(!context.Products.Any())
                {
                    //@ symbol is to tell the string constructor to ignore escape characters and line breaks.
                    var productsData = File.ReadAllText(@"..\Infrastructure\Data\SeedData\products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach(var product in products)
                    {
                        context.Products.Add(product);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                //logging error if seeding data fails
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, "Error seeding data");
            }
        }
    }
}