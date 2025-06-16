using Microsoft.EntityFrameworkCore;
using PARTS.DAL.Data;
using PARTS.DAL.Entities.Item;
using System.Text;

namespace PARTS.DAL.Seeders;

public class ShopSeeder
{
    private readonly PartsDBContext _context;
    private readonly AISeeder<Brand> _brandSeeder;
    private readonly AISeeder<Part> _partSeeder;

    public ShopSeeder(PartsDBContext context)
    {
        _context = context;
        _brandSeeder = new AISeeder<Brand>();
        _partSeeder = new AISeeder<Part>();
    }

    public async Task SeedAsync()
    {
        Console.WriteLine("Starting shop seeding process...");

        await SeedBrandsAsync();
        await SeedCategoriesAsync();
        await SeedPartsNoAIAsync();

        Console.WriteLine("Shop seeding completed successfully!");
    }

    private async Task SeedBrandsAsync()
    {
        if (await _context.Brands.AnyAsync())
        {
            Console.WriteLine("Brands already exist. Skipping brand seeding.");
            return;
        }

        Console.WriteLine("Seeding brands...");

        const int targetBrandCount = 20;
        const int maxRetries = 50;

        var brands = new List<Brand>();
        var uniqueTitles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var excludedTitles = new StringBuilder();

        var basePrompt = "generate json and fill them for 1 Brand, brand of auto part " +
                         "public class Brand : Base { public string Title { get; set; } public string? Description { get; set; } }";

        int attempts = 0;
        while (brands.Count < targetBrandCount && attempts < maxRetries)
        {
            try
            {
                var prompt = $"{basePrompt} except {excludedTitles}";
                var brand = await _brandSeeder.GenerateEntityAsync(prompt);

                if (brand != null && !string.IsNullOrWhiteSpace(brand.Title))
                {
                    brand.Id = Guid.NewGuid();
                    brand.Timestamp = DateTime.UtcNow;

                    if (uniqueTitles.Add(brand.Title))
                    {
                        brands.Add(brand);
                        excludedTitles.Append($"{brand.Title} ");
                        Console.WriteLine($"Generated brand: {brand.Title} ({brands.Count}/{targetBrandCount})");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate brand (attempt {attempts + 1}): {ex.Message}");
            }

            attempts++;
        }

        if (brands.Any())
        {
            await _context.Brands.AddRangeAsync(brands);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Successfully seeded {brands.Count} brands.");
        }
        else
        {
            Console.WriteLine("Failed to generate any brands.");
        }
    }

    private async Task SeedCategoriesAsync()
    {
        if (await _context.Categories.AnyAsync())
        {
            Console.WriteLine("Categories already exist. Skipping category seeding.");
            return;
        }

        Console.WriteLine("Seeding categories...");

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Categories NOCHECK CONSTRAINT FK_Categories_Categories_ParentId");

            // Seed main categories first
            var mainCategories = CategoryData.GetCategories();
            if (mainCategories?.Any() == true)
            {
                foreach (var category in mainCategories)
                {
                    category.Timestamp = DateTime.UtcNow;
                    category.SupCategories = null;
                    await _context.Categories.AddAsync(category);
                    await _context.SaveChangesAsync();
                }


                Console.WriteLine($"Seeded {mainCategories.Count} main categories.");
            }

            await transaction.CommitAsync();
            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Categories CHECK CONSTRAINT FK_Categories_Categories_ParentId");
            await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Failed to seed categories: {ex.Message}");
            throw;
        }
    }
    private async Task SeedPartsNoAIAsync()
    {
        if (await _context.Parts.AnyAsync())
        {
            Console.WriteLine("Parts already exist. Skipping Parts seeding.");
            return;
        }

        Console.WriteLine("Seeding Parts...");

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            //  await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Categories NOCHECK CONSTRAINT FK_Categories_Categories_ParentId");

            // Seed main categories first
            var parts = PartsData.GetParts();
            if (parts?.Any() == true)
            {
                foreach (var part in parts)
                {
                    part.Timestamp = DateTime.UtcNow;
                    await _context.Parts.AddAsync(part);
                    await _context.SaveChangesAsync();
                }


                Console.WriteLine($"Seeded {parts.Count} Parts.");
            }

            await transaction.CommitAsync();
            //    await _context.Database.ExecuteSqlRawAsync("ALTER TABLE Categories CHECK CONSTRAINT FK_Categories_Categories_ParentId");
            await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Failed to seed Parts: {ex.Message}");
            throw;
        }
    }

    private async Task SeedPartsAsync()
    {
        if (await _context.Parts.AnyAsync())
        {
            Console.WriteLine("Parts already exist. Skipping part seeding.");
            return;
        }

        Console.WriteLine("Seeding parts...");

        var categories = await _context.Categories.AsNoTracking().Where(p => p.SupCategories == null || p.SupCategories.Count == 0).ToListAsync();
        var brands = await _context.Brands.AsNoTracking().ToListAsync();

        if (!categories.Any() || !brands.Any())
        {
            Console.WriteLine("No categories or brands found. Cannot seed parts.");
            return;
        }

        var totalCombinations = categories.Count * brands.Count;
        Console.WriteLine($"Generating parts for {totalCombinations} category-brand combinations...");

        const int batchSize = 1;
        var allParts = new List<Part>();
        var processedCount = 0;

        // Process in batches to avoid memory issues
        for (int i = 0; i < categories.Count; i += batchSize)
        {
            var categoryBatch = categories.Skip(i).Take(batchSize);
            var batchParts = await GeneratePartsForCategoriesAsync(categoryBatch, brands);

            if (batchParts.Any())
            {
                await _context.Parts.AddRangeAsync(batchParts);
                await _context.SaveChangesAsync();

                processedCount += batchParts.Count;
                Console.WriteLine($"Saved batch: {processedCount} parts total");
            }
        }

        Console.WriteLine($"Successfully seeded {processedCount} parts.");
    }

    private async Task<List<Part>> GeneratePartsForCategoriesAsync(
        IEnumerable<Category> categories,
        List<Brand> brands)
    {
        var parts = new List<Part>();
        var semaphore = new SemaphoreSlim(5); // Limit concurrent AI requests

        var tasks = new List<Task>();

        foreach (var category in categories)
        {
            foreach (var brand in brands)
            {
                tasks.Add(GeneratePartAsync(category, brand, parts, semaphore));
            }
        }

        await Task.WhenAll(tasks);
        return parts;
    }

    private async Task GeneratePartAsync(
        Category category,
        Brand brand,
        List<Part> parts,
        SemaphoreSlim semaphore)
    {
        await semaphore.WaitAsync();

        try
        {
            const int maxRetries = 3;

            for (int attempt = 0; attempt < maxRetries; attempt++)
            {
                try
                {
                    var prompt = BuildPartPrompt(category.Title, brand.Title);
                    var part = await _partSeeder.GenerateEntityAsync(prompt);

                    if (part != null)
                    {
                        part.Id = Guid.NewGuid();
                        part.Timestamp = DateTime.UtcNow;
                        part.CategoryId = category.Id;
                        part.BrandId = brand.Id;

                        // Ensure required fields are set
                        part.PartTitle ??= $"{brand.Title} {category.Title} Part";
                        part.PartName ??= part.PartTitle;
                        part.Count ??= Random.Shared.Next(1, 100);
                        part.IsUniversal ??= Random.Shared.NextDouble() > 0.7;
                        part.IsMadeToOrder ??= Random.Shared.NextDouble() > 0.8;

                        lock (parts)
                        {
                            parts.Add(part);
                        }

                        break; // Success, exit retry loop
                    }
                }
                catch (Exception ex)
                {
                    if (attempt == maxRetries - 1)
                    {
                        Console.WriteLine($"Failed to generate part for {category.Title} - {brand.Title}: {ex.Message}");
                    }
                }
            }
        }
        finally
        {
            semaphore.Release();
        }
    }

    private static string BuildPartPrompt(string categoryTitle, string brandTitle)
    {
        return $@"generate json and fill them for 1 Part from category {categoryTitle} and brand {brandTitle}, 
                     part is auto part public class Part : Base 
                     {{ 
                         public string? PartNumber {{ get; set; }}
                         public string? ManufacturerNumber {{ get; set; }}
                         public string? Description {{ get; set; }}
                         public string? PartName {{ get; set; }}
                         public bool? IsUniversal {{ get; set; }}
                         public decimal? PriceRegular {{ get; set; }}
                         public string? PartTitle {{ get; set; }}
                         public string? PartAttributes {{ get; set; }}
                         public bool? IsMadeToOrder {{ get; set; }}
                         public string? FitNotes {{ get; set; }}
                         public int? Count {{ get; set; }}
                     }}";
    }

    // Backup method to seed with static data if AI fails
    public async Task SeedWithStaticDataAsync()
    {
        Console.WriteLine("Seeding with static data as fallback...");

        if (!await _context.Brands.AnyAsync())
        {
            await SeedStaticBrandsAsync();
        }

        if (!await _context.Categories.AnyAsync())
        {
            await SeedCategoriesAsync();
        }

        if (!await _context.Parts.AnyAsync())
        {
            await SeedStaticPartsAsync();
        }
    }

    private async Task SeedStaticBrandsAsync()
    {
        var staticBrands = new List<Brand>
        {
            new() { Id = Guid.NewGuid(), Title = "Bosch", Description = "German automotive parts manufacturer", Timestamp = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Title = "Denso", Description = "Japanese automotive components supplier", Timestamp = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Title = "Continental", Description = "German automotive parts and tires", Timestamp = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Title = "Valeo", Description = "French automotive supplier", Timestamp = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Title = "Delphi", Description = "Automotive parts and technology", Timestamp = DateTime.UtcNow }
        };

        await _context.Brands.AddRangeAsync(staticBrands);
        await _context.SaveChangesAsync();
        Console.WriteLine($"Seeded {staticBrands.Count} static brands.");
    }

    private async Task SeedStaticPartsAsync()
    {
        var brands = await _context.Brands.Take(5).ToListAsync();
        var categories = await _context.Categories.Take(10).ToListAsync();

        if (!brands.Any() || !categories.Any()) return;

        var staticParts = new List<Part>();
        var random = new Random();

        foreach (var brand in brands)
        {
            foreach (var category in categories.Take(2)) // Limit to 2 categories per brand
            {
                staticParts.Add(new Part
                {
                    Id = Guid.NewGuid(),
                    PartNumber = $"P{random.Next(10000, 99999)}",
                    ManufacturerNumber = $"MN{random.Next(1000, 9999)}",
                    Description = $"Quality {category.Title.ToLower()} part from {brand.Title}",
                    PartName = $"{brand.Title} {category.Title} Component",
                    PartTitle = $"{brand.Title} {category.Title} Part",
                    PriceRegular = random.Next(50, 500),
                    Count = random.Next(10, 100),
                    IsUniversal = random.NextDouble() > 0.5,
                    IsMadeToOrder = random.NextDouble() > 0.8,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        await _context.Parts.AddRangeAsync(staticParts);
        await _context.SaveChangesAsync();
        Console.WriteLine($"Seeded {staticParts.Count} static parts.");
    }
}