using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _19.Products
{
   public class ProductFixed
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }

   public class Program
    {
       public static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, Product>> productsByType = new Dictionary<string, Dictionary<string, Product>>
            {
                { "Food", new Dictionary<string, Product>() },
                { "Electronics", new Dictionary<string, Product>() },
                { "Domestics", new Dictionary<string, Product>() }
            };

            PreLoad(productsByType);

            string inputLine = "";

            while ((inputLine = Console.ReadLine()) != "exit")
            {
                string[] inputParams = inputLine.Split(' ');

                if (inputParams.Length > 1)
                {
                    string productName = inputParams[0];
                    string productType = inputParams[1];
                    decimal productPrice = decimal.Parse(inputParams[2]);
                    int productQuantity = int.Parse(inputParams[3]);

                    if (productsByType[productType].ContainsKey(productName))
                    {
                        productsByType[productType][productName].Price = productPrice;
                        productsByType[productType][productName].Quantity = productQuantity;
                    }
                    else
                    {
                        Product newProduct = new Product();

                        newProduct.Name = productName;
                        newProduct.Type = productType;
                        newProduct.Price = productPrice;
                        newProduct.Quantity = productQuantity;

                        productsByType[productType].Add(newProduct.Name, newProduct);
                    }
                }
                else
                {
                    switch (inputParams[0])
                    {
                        case "stock":
                            File.WriteAllLines(
                                "../../database.prod",
                                productsByType["Domestics"]
                                .Select(x => x.Value)
                                .Select(x => x.Name + "|" + x.Type + "|" + x.Price + "|" + x.Quantity));
                            File.AppendAllLines(
                                "../../database.prod",
                                productsByType["Electronics"]
                                .Select(x => x.Value)
                                .Select(x => x.Name + "|" + x.Type + "|" + x.Price + "|" + x.Quantity));
                            File.AppendAllLines(
                                "../../database.prod",
                                productsByType["Food"]
                                .Select(x => x.Value)
                                .Select(x => x.Name + "|" + x.Type + "|" + x.Price + "|" + x.Quantity));
                            break;
                        case "analyze":
                            if (File.Exists("../../database.prod"))
                            {
                                string[] stockedProducts = File.ReadAllLines("../../database.prod");

                                foreach (var product in stockedProducts)
                                {
                                    string[] productParams = product.Split('|');

                                    Console.WriteLine("{0}, Product: {1}", productParams[1], productParams[0]);
                                    Console.WriteLine("Price: ${0:F2}, Amount Left: {1}", productParams[2], productParams[3]);
                                }
                            }
                            else
                            {
                                Console.WriteLine("No products stocked");
                            }

                            break;
                        case "sales":

                            productsByType
                                .Where(x => x.Value.Any())
                                .ToDictionary(x => x.Key, x => x.Value.Sum(y => (y.Value.Price * y.Value.Quantity)))
                                .OrderByDescending(x => x.Value)
                                .ToList()
                                .ForEach(x => Console.WriteLine("{0}: ${1}", x.Key, x.Value));

                            break;
                    }
                }
            }
        }

        private static void PreLoad(Dictionary<string, Dictionary<string, Product>> productsByType)
        {
            if (File.Exists("../../database.prod"))
            {
                string[] products = File.ReadAllLines("../../database.prod");

                foreach (var product in products)
                {
                    string[] productParams = product.Split('|');

                    string productName = productParams[0];
                    string productType = productParams[1];
                    decimal productPrice = decimal.Parse(productParams[2]);
                    int productQuantity = int.Parse(productParams[3]);

                    Product newProduct = new Product();

                    newProduct.Name = productName;
                    newProduct.Type = productType;
                    newProduct.Price = productPrice;
                    newProduct.Quantity = productQuantity;

                    productsByType[productType].Add(newProduct.Name, newProduct);
                }
            }
        }
    }
}