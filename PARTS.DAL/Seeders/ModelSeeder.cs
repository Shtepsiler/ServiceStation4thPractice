using Bogus;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PARTS.DAL.Entities.Vehicle;

namespace PARTS.DAL.Seeders;

public class ModelSeeder : ISeeder<Model>
{
    public void Seed(EntityTypeBuilder<Model> builder)
    {
        var modelFaker = new Faker<Model>()
                .RuleFor(p => p.Id, f => f.Random.Guid())
                .RuleFor(m => m.Title, f => f.Vehicle.Model())
                .RuleFor(m => m.Description, f => f.Lorem.Sentence())
                .RuleFor(m => m.Year, f => f.Date.Past())
/*                .RuleFor(m => m.Make, f => f.PickRandom<Make>())
*/                ;

        // builder.HasData(modelFaker.Generate(10));
    }
}