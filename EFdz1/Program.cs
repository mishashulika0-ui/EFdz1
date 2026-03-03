using EFdz1.Data;
using EFdz1.Entities;

class Program
{
    static void Main(string[] args)
    {
        using var context = new DataContext();

        while (true)
        {
            Console.WriteLine("1. Get all");
            Console.WriteLine("2. Add");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. Find by ID");
            Console.WriteLine("5. Find by name");
            Console.WriteLine("6. Update");
            Console.WriteLine("7. Exit");
            Console.Write("Choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": GetAll(context); break;
                case "2": Add(context); break;
                case "3": Delete(context); break;
                case "4": FindById(context); break;
                case "5": FindByName(context); break;
                case "6": Update(context); break;
                case "7": return;
            }
        }
    }

    static void GetAll(DataContext context)
    {
        var categories = context.Categories.ToList();
        Console.WriteLine("\nAll categories:");
        foreach (var c in categories)
            Console.WriteLine($"ID: {c.Id}, Name: {c.Name}, Desc: {c.Description ?? "-"}");
    }

    static void Add(DataContext context)
    {
        Console.Write("Name: ");
        var name = Console.ReadLine();

        Console.Write("Description: ");
        var desc = Console.ReadLine();

        var category = new Category
        {
            Name = name,
            Description = string.IsNullOrWhiteSpace(desc) ? null : desc
        };

        context.Categories.Add(category);
        context.SaveChanges();
        Console.WriteLine($"Added with ID: {category.Id}");
    }

    static void Delete(DataContext context)
    {
        Console.Write("ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var category = context.Categories.Find(id);
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
                Console.WriteLine("Deleted");
            }
            else Console.WriteLine("Not found");
        }
    }

    static void FindById(DataContext context)
    {
        Console.Write("ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var category = context.Categories.Find(id);
            if (category != null)
                Console.WriteLine($"ID: {category.Id}, Name: {category.Name}, Desc: {category.Description ?? "-"}");
            else Console.WriteLine("Not found");
        }
    }

    static void FindByName(DataContext context)
    {
        Console.Write("Name: ");
        var name = Console.ReadLine();

        var categories = context.Categories
            .Where(c => c.Name.Contains(name))
            .ToList();

        if (categories.Any())
            foreach (var c in categories)
                Console.WriteLine($"ID: {c.Id}, Name: {c.Name}, Desc: {c.Description ?? "-"}");
        else Console.WriteLine("Not found");
    }

    static void Update(DataContext context)
    {
        Console.Write("ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var category = context.Categories.Find(id);
            if (category != null)
            {
                Console.Write($"New name ({category.Name}): ");
                var name = Console.ReadLine();

                Console.Write($"New desc ({category.Description ?? "-"}): ");
                var desc = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(name)) category.Name = name;
                if (!string.IsNullOrWhiteSpace(desc)) category.Description = desc;

                context.SaveChanges();
                Console.WriteLine("Updated");
            }
            else Console.WriteLine("Not found");
        }
    }
}