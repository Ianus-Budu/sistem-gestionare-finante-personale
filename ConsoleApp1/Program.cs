using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

class Category
{
    public int Id { get; set; }
    public string Title { get; set; }
}

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
}

class Program
{
    static HttpClient client = new HttpClient
    {
        BaseAddress = new Uri("https://localhost:5001/api/") // 🔴 schimbă cu API-ul tău
    };

    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n=== MENIU ===");
            Console.WriteLine("1. Lista categorii");
            Console.WriteLine("2. Detalii categorie");
            Console.WriteLine("3. Creeaza categorie");
            Console.WriteLine("4. Sterge categorie");
            Console.WriteLine("5. Modifica categorie");
            Console.WriteLine("6. Creeaza produs");
            Console.WriteLine("7. Lista produse din categorie");
            Console.WriteLine("0. Exit");

            Console.Write("Alege: ");
            var opt = Console.ReadLine();

            switch (opt)
            {
                case "1":
                    await GetCategories();
                    break;
                case "2":
                    await GetCategoryById();
                    break;
                case "3":
                    await CreateCategory();
                    break;
                case "4":
                    await DeleteCategory();
                    break;
                case "5":
                    await UpdateCategory();
                    break;
                case "6":
                    await CreateProduct();
                    break;
                case "7":
                    await GetProductsByCategory();
                    break;
                case "0":
                    return;
            }
        }
    }

    static async Task GetCategories()
    {
        var response = await client.GetAsync("categories");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Eroare la fetch categorii");
            return;
        }

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<List<Category>>(json);

        foreach (var c in data)
        {
            Console.WriteLine($"{c.Id} - {c.Title}");
        }
    }

    static async Task GetCategoryById()
    {
        Console.Write("ID categorie: ");
        int id = int.Parse(Console.ReadLine());

        var response = await client.GetAsync($"categories/{id}");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Categoria nu a fost gasita");
            return;
        }

        var json = await response.Content.ReadAsStringAsync();
        var c = JsonSerializer.Deserialize<Category>(json);

        Console.WriteLine($"{c.Id} - {c.Title}");
    }

    static async Task CreateCategory()
    {
        Console.Write("Titlu: ");
        string title = Console.ReadLine();

        var category = new Category { Title = title };

        var json = JsonSerializer.Serialize(category);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("categories", content);

        Console.WriteLine(response.IsSuccessStatusCode
            ? "Categorie creata"
            : "Eroare la creare");
    }

    static async Task DeleteCategory()
    {
        Console.Write("ID categorie: ");
        int id = int.Parse(Console.ReadLine());

        var response = await client.DeleteAsync($"categories/{id}");

        Console.WriteLine(response.IsSuccessStatusCode
            ? "Stearsa cu succes"
            : "Eroare la stergere");
    }

    static async Task UpdateCategory()
    {
        Console.Write("ID categorie: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Titlu nou: ");
        string title = Console.ReadLine();

        var category = new Category { Id = id, Title = title };

        var json = JsonSerializer.Serialize(category);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"categories/{id}", content);

        Console.WriteLine(response.IsSuccessStatusCode
            ? "Actualizat cu succes"
            : "Eroare update");
    }

    static async Task CreateProduct()
    {
        Console.Write("Nume produs: ");
        string name = Console.ReadLine();

        Console.Write("Pret: ");
        double price = double.Parse(Console.ReadLine());

        Console.Write("Category ID: ");
        int categoryId = int.Parse(Console.ReadLine());

        var product = new Product
        {
            Name = name,
            Price = price,
            CategoryId = categoryId
        };

        var json = JsonSerializer.Serialize(product);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("products", content);

        Console.WriteLine(response.IsSuccessStatusCode
            ? "Produs creat"
            : "Eroare creare produs");
    }

    static async Task GetProductsByCategory()
    {
        Console.Write("Category ID: ");
        int id = int.Parse(Console.ReadLine());

        var response = await client.GetAsync($"categories/{id}/products");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Eroare la fetch produse");
            return;
        }

        var json = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<List<Product>>(json);

        foreach (var p in products)
        {
            Console.WriteLine($"{p.Name} - {p.Price}");
        }
    }
}