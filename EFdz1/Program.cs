using EFdz1.Data;
using EFdz1.Entities;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        using var context = new DataContext();

        while (true)
        {
            Console.WriteLine("\n1.All categories 2.Add category 3.Delete category 4.Find category by ID");
            Console.WriteLine("5.Find category by name 6.Update category");
            Console.WriteLine("7.All products 8.Add product 9.Update product 10.Delete product");
            Console.WriteLine("11.Find product by ID 12.Find products by category 0.Exit");
            Console.Write("Choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": GetAllCategories(context); break;
                case "2": AddCategory(context); break;
                case "3": DeleteCategory(context); break;
                case "4": FindCategoryById(context); break;
                case "5": FindCategoryByName(context); break;
                case "6": UpdateCategory(context); break;
                case "7": GetAllProducts(context); break;
                case "8": AddProduct(context); break;
                case "9": UpdateProduct(context); break;
                case "10": DeleteProduct(context); break;
                case "11": FindProductById(context); break;
                case "12": FindProductsByCategory(context); break;
                case "0": return;
            }
        }
    }

    static void GetAllCategories(DataContext context)
    {
        var categories = context.Categories.ToList();
        Console.WriteLine("\nAll categories:");
        foreach (var c in categories)
            Console.WriteLine($"ID: {c.Id}, Name: {c.Name}, Desc: {c.Description}");
    }

    static void AddCategory(DataContext context)
    {
        Console.Write("Name: ");
        var name = Console.ReadLine();
        Console.Write("Description: ");
        var desc = Console.ReadLine();

        context.Categories.Add(new Category { Name = name, Description = desc });
        context.SaveChanges();
        Console.WriteLine("Category added");
    }

    static void DeleteCategory(DataContext context)
    {
        Console.Write("ID to delete: ");
        var id = int.Parse(Console.ReadLine());

        var category = context.Categories.Find(id);
        if (category != null)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
            Console.WriteLine("Deleted");
        }
    }

    static void FindCategoryById(DataContext context)
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine());

        var category = context.Categories.Find(id);
        if (category != null)
            Console.WriteLine($"ID: {category.Id}, Name: {category.Name}, Desc: {category.Description}");
    }

    static void FindCategoryByName(DataContext context)
    {
        Console.Write("Name: ");
        var name = Console.ReadLine();

        var categories = context.Categories.Where(c => c.Name.Contains(name)).ToList();
        foreach (var c in categories)
            Console.WriteLine($"ID: {c.Id}, Name: {c.Name}, Desc: {c.Description}");
    }

    static void UpdateCategory(DataContext context)
    {
        Console.Write("ID to update: ");
        var id = int.Parse(Console.ReadLine());

        var category = context.Categories.Find(id);
        if (category != null)
        {
            Console.Write($"New name ({category.Name}): ");
            var name = Console.ReadLine();
            Console.Write($"New desc ({category.Description}): ");
            var desc = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name)) category.Name = name;
            if (!string.IsNullOrWhiteSpace(desc)) category.Description = desc;

            context.SaveChanges();
            Console.WriteLine("Updated");
        }
    }

    static void GetAllProducts(DataContext context)
    {
        var products = context.Products.Include(p => p.Category).ToList();
        Console.WriteLine("\nAll products:");
        foreach (var p in products)
        {
            var catName = p.Category?.Name ?? "No category";
            Console.WriteLine($"ID: {p.Id}, Name: {p.Name}, Price: {p.Price}, Category: {catName}, Desc: {p.Description}");
        }
    }

    static void AddProduct(DataContext context)
    {
        var categories = context.Categories.ToList();
        Console.WriteLine("Categories:");
        foreach (var c in categories)
            Console.WriteLine($"ID: {c.Id}, Name: {c.Name}");

        Console.Write("Category ID: ");
        var catId = int.Parse(Console.ReadLine());

        Console.Write("Name: ");
        var name = Console.ReadLine();
        Console.Write("Description: ");
        var desc = Console.ReadLine();
        Console.Write("Price: ");
        var price = decimal.Parse(Console.ReadLine());

        context.Products.Add(new Product
        {
            Name = name,
            Description = desc,
            Price = price,
            CategoryId = catId
        });
        context.SaveChanges();
        Console.WriteLine("Product added");
    }

    static void UpdateProduct(DataContext context)
    {
        Console.Write("Product ID: ");
        var id = int.Parse(Console.ReadLine());

        var product = context.Products.Find(id);
        if (product != null)
        {
            Console.Write($"New name ({product.Name}): ");
            var name = Console.ReadLine();
            Console.Write($"New desc ({product.Description}): ");
            var desc = Console.ReadLine();
            Console.Write($"New price ({product.Price}): ");
            var priceStr = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name)) product.Name = name;
            if (!string.IsNullOrWhiteSpace(desc)) product.Description = desc;
            if (!string.IsNullOrWhiteSpace(priceStr)) product.Price = decimal.Parse(priceStr);

            context.SaveChanges();
            Console.WriteLine("Updated");
        }
    }

    static void DeleteProduct(DataContext context)
    {
        Console.Write("Product ID: ");
        var id = int.Parse(Console.ReadLine());

        var product = context.Products.Find(id);
        if (product != null)
        {
            context.Products.Remove(product);
            context.SaveChanges();
            Console.WriteLine("Deleted");
        }
    }

    static void FindProductById(DataContext context)
    {
        Console.Write("Product ID: ");
        var id = int.Parse(Console.ReadLine());

        var product = context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            var catName = product.Category?.Name ?? "No category";
            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Category: {catName}, Desc: {product.Description}");
        }
    }

    static void FindProductsByCategory(DataContext context)
    {
        Console.Write("Category ID: ");
        var catId = int.Parse(Console.ReadLine());

        var products = context.Products.Where(p => p.CategoryId == catId).ToList();
        foreach (var p in products)
            Console.WriteLine($"ID: {p.Id}, Name: {p.Name}, Price: {p.Price}, Desc: {p.Description}");
    }
}