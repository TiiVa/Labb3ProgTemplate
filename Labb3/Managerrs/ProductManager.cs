using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.DataModels.Users;
using Labb3ProgTemplate.Enums;

namespace Labb3ProgTemplate.Managerrs;

public static class ProductManager
{
    
    private static readonly IEnumerable<Product>? _products = new List<Product>() 
    {
        new Vegetable("Tomato", 5, ProductTypes.Vegetable) { Name = "Tomato", Price = 5.0 },
        new Vegetable("Cucumber", 4, ProductTypes.Vegetable) { Name = "Cucumber", Price = 4.0 },
        new Vegetable("Lettuce", 7, ProductTypes.Vegetable ){ Name = "Lettuce", Price = 7 },
        new Fruit("Apple", 3, ProductTypes.Fruit) { Name = "Apple", Price = 3 },
        new Fruit("Banana", 8, ProductTypes.Fruit) { Name = "Banana", Price = 8 },
        new Fruit("Orange", 5.5, ProductTypes.Fruit) { Name = "Orange", Price = 5.5 }
    };

    public static IEnumerable<Product>? Products => _products;

    // Skicka detta efter att produktlistan ändrats eller lästs in
    public static event Action ProductListChanged;

    public static void AddProduct(Product product)
    {
        if (_products is List<Product> products)
        {
            products.Add(product);
        }

        SaveProductsToFile();
        ProductListChanged.Invoke();
    }
    public static void RemoveProduct(Product product)
    {
        if (_products is List<Product> products)
        {
            products.Remove(product);
        }

        SaveProductsToFile();
        ProductListChanged.Invoke();
    }

    public static async Task SaveProductsToFile()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb3"); 
        Directory.CreateDirectory(directory);
        var filePath = Path.Combine(directory, "Products.json");
        var jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;
        var json = JsonSerializer.Serialize(Products, jsonOptions);

        await using var sw = new StreamWriter(filePath);
        await sw.WriteLineAsync(json);

    }

    public static async Task LoadProductsFromFile()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb3");
        Directory.CreateDirectory(directory);
        var filePath = Path.Combine(directory, "Products.json");
        var deserializedProductList = new List<Product>();

        if (File.Exists(filePath))
        {
            var text = string.Empty;
            var line = string.Empty;

            using var sr = new StreamReader(filePath);

            while ((line = await sr.ReadLineAsync()) != null)
            {
                text += line;
            }

            using (var jsonDoc = JsonDocument.Parse(text))
            {
                if (jsonDoc.RootElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (var jsonElement in jsonDoc.RootElement.EnumerateArray())
                    {
                        Product? a;
                        switch (jsonElement.GetProperty("Type").GetInt32())
                        {
                            case 0:
                                a = jsonElement.Deserialize<Fruit>();
                                deserializedProductList.Add(a);
                                break;
                            case 1:
                                a = jsonElement.Deserialize<Vegetable>();
                                deserializedProductList.Add(a);
                                break;
                        }
                    }
                }

            }

            foreach (var product in deserializedProductList)
            {
                if (_products is List<Product> products)
                {
                    products.Add(product);
                }
            }

            ProductListChanged.Invoke();
            
        } 
    }

    
}