using System;
using System.Collections.Generic;
using System.Linq;

namespace Amazon
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }

    internal class Program
    {
        static List<Product> inventory = new List<Product>();
        static List<Product> shoppingCart = new List<Product>();

        static void Main(string[] args)
        {
            // Initial inventory items
            inventory.Add(new Product("Laptop", 1000.00m, 10));
            inventory.Add(new Product("Phone", 500.00m, 20));
            inventory.Add(new Product("Headphones", 100.00m, 30));

            char choice;
            do
            {
                DisplayMenu();
                string? input = Console.ReadLine()?.ToUpper();
                choice = input?[0] ?? 'Q';

                switch (choice)
                {
                    case 'C':
                        CreateItem();
                        break;
                    case 'R':
                        ReadItems();
                        break;
                    case 'U':
                        UpdateItem();
                        break;
                    case 'D':
                        DeleteItem();
                        break;
                    case 'A':
                        AddToCart();
                        break;
                    case 'V':
                        ViewCart();
                        break;
                    case 'Q':
                        Checkout();
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            } while (choice != 'Q');
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n--- Inventory Management System ---");
            Console.WriteLine("Inventory Operations:");
            Console.WriteLine("C. Create new inventory item");
            Console.WriteLine("R. Read inventory items");
            Console.WriteLine("U. Update inventory item");
            Console.WriteLine("D. Delete inventory item");
            Console.WriteLine("\nShopping Cart:");
            Console.WriteLine("A. Add item to cart");
            Console.WriteLine("V. View cart");
            Console.WriteLine("Q. Checkout and Quit");
            Console.Write("Enter your choice: ");
        }

        static void CreateItem()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();
            Console.Write("Enter product price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Enter product quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            inventory.Add(new Product(name, price, quantity));
            Console.WriteLine($"Product {name} added to inventory.");
        }

        static void ReadItems()
        {
            Console.WriteLine("\n--- Inventory Items ---");
            foreach (var product in inventory)
            {
                Console.WriteLine($"Name: {product.Name}, Price: ${product.Price}, Quantity: {product.Quantity}");
            }
        }

        static void UpdateItem()
        {
            Console.Write("Enter product name to update: ");
            string name = Console.ReadLine();
            var product = inventory.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (product != null)
            {
                Console.Write("Enter new price (press enter to skip): ");
                string priceInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(priceInput))
                    product.Price = decimal.Parse(priceInput);

                Console.Write("Enter new quantity (press enter to skip): ");
                string quantityInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(quantityInput))
                    product.Quantity = int.Parse(quantityInput);

                Console.WriteLine("Product updated successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        static void DeleteItem()
        {
            Console.Write("Enter product name to delete: ");
            string name = Console.ReadLine();
            var product = inventory.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (product != null)
            {
                inventory.Remove(product);
                Console.WriteLine($"Product {name} deleted from inventory.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        static void AddToCart()
        {
            Console.Write("Enter product name to add to cart: ");
            string name = Console.ReadLine();
            Console.Write("Enter quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            var inventoryProduct = inventory.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (inventoryProduct != null && inventoryProduct.Quantity >= quantity)
            {
                // Update inventory
                inventoryProduct.Quantity -= quantity;

                // Add to cart or update existing cart item
                var cartProduct = shoppingCart.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (cartProduct != null)
                {
                    cartProduct.Quantity += quantity;
                }
                else
                {
                    shoppingCart.Add(new Product(name, inventoryProduct.Price, quantity));
                }

                Console.WriteLine($"{quantity} {name}(s) added to cart.");
            }
            else
            {
                Console.WriteLine("Product not found or insufficient quantity.");
            }
        }

        static void ViewCart()
        {
            Console.WriteLine("\n--- Shopping Cart ---");
            foreach (var product in shoppingCart)
            {
                Console.WriteLine($"Name: {product.Name}, Price: ${product.Price}, Quantity: {product.Quantity}");
            }
        }

        static void Checkout()
        {
            decimal subtotal = 0;
            const decimal TAX_RATE = 0.07m;

            Console.WriteLine("\n--- Receipt ---");
            Console.WriteLine("{0,-20} {1,10} {2,10} {3,15}", "Product", "Price", "Quantity", "Total");
            Console.WriteLine(new string('-', 55));

            foreach (var product in shoppingCart)
            {
                decimal itemTotal = product.Price * product.Quantity;
                subtotal += itemTotal;
                Console.WriteLine("{0,-20} ${1,9:F2} {2,10} ${3,14:F2}", 
                    product.Name, product.Price, product.Quantity, itemTotal);
            }

            decimal tax = subtotal * TAX_RATE;
            decimal total = subtotal + tax;

            Console.WriteLine(new string('-', 55));
            Console.WriteLine("{0,-20} ${1,9:F2}", "Subtotal", subtotal);
            Console.WriteLine("{0,-20} ${1,9:F2}", "Tax (7%)", tax);
            Console.WriteLine("{0,-20} ${1,9:F2}", "Total", total);

            // Clear the shopping cart after checkout
            shoppingCart.Clear();
        }
    }
}