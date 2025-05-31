using Microsoft.EntityFrameworkCore;
using PARTS.DAL.Data;
using PARTS.DAL.Entities.Vehicle;
using System.Text.Json;

namespace PARTS.DAL.Seeders;

public class ModelSplitter
{
    private readonly PartsDBContext _context;

    public ModelSplitter(PartsDBContext context)
    {
        _context = context;
    }

    public async Task<bool> IsDataPresentAsync()
    {
        return await _context.Models.AnyAsync();
    }

    public async Task SeedAsync()
    {
        if (await IsDataPresentAsync())
        {
            Console.WriteLine("Data already exists. Skipping seed.");
            return;
        }

        const string jsonFilePath = "models.json";
        var models = await ReadModelsFromJsonAsync(jsonFilePath);

        if (!models.Any())
        {
            Console.WriteLine("No models found in JSON file.");
            return;
        }

        await ProcessModelsInBatchesAsync(models);
    }

    private async Task ProcessModelsInBatchesAsync(List<Class> models)
    {
        const int batchSize = 100;
        var totalBatches = (int)Math.Ceiling((double)models.Count / batchSize);

        // Кеш для уникнення дублікатів
        var makeCache = new Dictionary<string, Make>();
        var modelCache = new Dictionary<string, Model>();
        var subModelCache = new Dictionary<string, SubModel>();
        var engineCache = new Dictionary<string, Engine>();

        Console.WriteLine($"Processing {models.Count} models in {totalBatches} batches...");

        for (int i = 0; i < totalBatches; i++)
        {
            var batch = models.Skip(i * batchSize).Take(batchSize);
            await ProcessBatchAsync(batch, makeCache, modelCache, subModelCache, engineCache);

            Console.WriteLine($"Processed batch {i + 1}/{totalBatches}");
        }
    }

    private async Task ProcessBatchAsync(
        IEnumerable<Class> batch,
        Dictionary<string, Make> makeCache,
        Dictionary<string, Model> modelCache,
        Dictionary<string, SubModel> subModelCache,
        Dictionary<string, Engine> engineCache)
    {
        var makesToAdd = new List<Make>();
        var modelsToAdd = new List<Model>();
        var subModelsToAdd = new List<SubModel>();
        var enginesToAdd = new List<Engine>();

        foreach (var inputModel in batch)
        {
            try
            {
                var (make, model, subModel, engine) = CreateEntitiesFromInput(inputModel);

                // Обробка Make
                if (!makeCache.ContainsKey(make.Title))
                {
                    makeCache[make.Title] = make;
                    makesToAdd.Add(make);
                }
                else
                {
                    make = makeCache[make.Title];
                }

                // Обробка Model
                var modelKey = $"{make.Title}_{model.Title}";
                if (!modelCache.ContainsKey(modelKey))
                {
                    model.Make = make;
                    model.MakeId = make.Id;
                    modelCache[modelKey] = model;
                    modelsToAdd.Add(model);
                }
                else
                {
                    model = modelCache[modelKey];
                }

                // Обробка SubModel
                var subModelKey = $"{modelKey}_{subModel.Title}";
                if (!subModelCache.ContainsKey(subModelKey))
                {
                    subModel.Model = model;
                    subModel.ModelId = model.Id;
                    subModelCache[subModelKey] = subModel;
                    subModelsToAdd.Add(subModel);
                }
                else
                {
                    subModel = subModelCache[subModelKey];
                }

                // Обробка Engine
                var engineKey = CreateEngineKey(engine, make.Title, subModel.Title);
                if (!engineCache.ContainsKey(engineKey))
                {
                    engine.SubModel = subModel;
                    engine.SubModelId = subModel.Id;
                    engine.Make = make;
                    engine.MakeId = make.Id;
                    engineCache[engineKey] = engine;
                    enginesToAdd.Add(engine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process model: {ex.Message}");
            }
        }

        // Збереження в правильному порядку
        await SaveEntitiesBatchAsync(makesToAdd, modelsToAdd, subModelsToAdd, enginesToAdd);
    }

    private async Task SaveEntitiesBatchAsync(
        List<Make> makes,
        List<Model> models,
        List<SubModel> subModels,
        List<Engine> engines)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            if (makes.Any())
            {
                await _context.Makes.AddRangeAsync(makes);
                await _context.SaveChangesAsync();
            }

            if (models.Any())
            {
                await _context.Models.AddRangeAsync(models);
                await _context.SaveChangesAsync();
            }

            if (subModels.Any())
            {
                await _context.SubModels.AddRangeAsync(subModels);
                await _context.SaveChangesAsync();
            }

            if (engines.Any())
            {
                await _context.Engines.AddRangeAsync(engines);
                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private (Make, Model, SubModel, Engine) CreateEntitiesFromInput(Class inputModel)
    {
        var make = new Make
        {
            Id = Guid.NewGuid(),
            Title = inputModel.make_display?.Trim() ?? "Unknown Make",
            Description = null,
            Сountry = inputModel.make_country?.Trim(),
            Year = null,
            Timestamp = DateTime.UtcNow
        };

        var model = new Model
        {
            Id = Guid.NewGuid(),
            Title = inputModel.model_name?.Trim() ?? "Unknown Model",
            Description = inputModel.model_id?.Trim(),
            Seats = inputModel.model_seats?.Trim(),
            Doors = inputModel.model_doors?.Trim(),
            Year = TryParseDateTime(inputModel.model_year),
            Timestamp = DateTime.UtcNow
        };

        var subModel = new SubModel
        {
            Id = Guid.NewGuid(),
            Title = inputModel.model_trim?.Trim() ?? "Base SubModel",
            Description = inputModel.model_trim?.Trim(),
            Transmission = inputModel.model_transmission_type?.Trim(),
            Year = TryParseDateTime(inputModel.model_year),
            Weight = TryParseInt(inputModel.model_weight_kg),
            Timestamp = DateTime.UtcNow
        };

        var engine = new Engine
        {
            Id = Guid.NewGuid(),
            Fuel = inputModel.model_engine_fuel?.Trim(),
            Model = $"{inputModel.model_engine_position?.Trim()} {inputModel.model_engine_type?.Trim()}".Trim(),
            Cylinders = TryParseInt(inputModel.model_engine_cyl),
            CC = TryParseInt(inputModel.model_engine_cc),
            HP = TryParseInt(inputModel.model_engine_power_hp),
            Year = TryParseDateTime(inputModel.model_year),
            Timestamp = DateTime.UtcNow
        };

        return (make, model, subModel, engine);
    }

    private static string CreateEngineKey(Engine engine, string makeName, string subModelName)
    {
        return $"{makeName}_{subModelName}_{engine.Cylinders}_{engine.CC}_{engine.HP}_{engine.Model}_{engine.Fuel}";
    }

    private static DateTime? TryParseDateTime(string? input)
    {
        return DateTime.TryParse(input, out var result) ? result : null;
    }

    private static int? TryParseInt(string? input)
    {
        return int.TryParse(input, out var result) ? result : null;
    }

    private async Task<List<Class>> ReadModelsFromJsonAsync(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"JSON file not found: {filePath}");
            }

            var jsonString = await File.ReadAllTextAsync(filePath);

            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return new List<Class>();
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true
            };

            return JsonSerializer.Deserialize<List<Class>>(jsonString, options) ?? new List<Class>();
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Failed to parse JSON: {ex.Message}");
            return new List<Class>();
        }
    }

    public async Task SeedVehiclesAsync()
    {
        if (await _context.Vehicles.AnyAsync())
        {
            Console.WriteLine("Vehicles already exist. Skipping seed.");
            return;
        }

        var sampleMakes = await _context.Makes
            .Include(m => m.Models)
            .ThenInclude(m => m.SubModels)
            .ThenInclude(sm => sm.Engines)
            .Take(3)
            .ToListAsync();

        if (!sampleMakes.Any())
        {
            Console.WriteLine("No makes found. Cannot seed vehicles.");
            return;
        }

        var vehicles = CreateSampleVehicles(sampleMakes);

        await _context.Vehicles.AddRangeAsync(vehicles);
        await _context.SaveChangesAsync();

        Console.WriteLine($"Seeded {vehicles.Count} vehicles.");
    }

    private List<Vehicle> CreateSampleVehicles(List<Make> makes)
    {
        var vehicles = new List<Vehicle>();
        var vehicleIds = new[]
        {
            Guid.Parse("dc238098-d210-44f3-778e-08dc7b9965a3"),
            Guid.Parse("dc238098-d410-44f3-778e-08dc7b9965a3"),
            Guid.Parse("88C2A122-9E71-4A7A-A52D-9F82A6610D87")
        };

        var vins = new[] { "asd1w1vvcve1e1ew", "asd1w1we1e1e1ew", "asd1w1we1e1ennfdew" };

        for (int i = 0; i < Math.Min(makes.Count, 3); i++)
        {
            var make = makes[i];
            var model = make.Models?.FirstOrDefault();
            var subModel = model?.SubModels?.FirstOrDefault();
            var engine = subModel?.Engines?.FirstOrDefault();

            if (model != null)
            {
                vehicles.Add(new Vehicle
                {
                    Id = vehicleIds[i],
                    VIN = vins[i],
                    Year = DateTime.UtcNow.AddYears(-10),
                    Timestamp = DateTime.UtcNow,
                    MakeId = make.Id,
                    ModelId = model.Id,
                    SubModelId = subModel?.Id,
                    EngineId = engine?.Id,
                    FullModelName = $"{make.Title} {model.Title} {subModel?.Title}".Trim()
                });
            }
        }

        return vehicles;
    }
}