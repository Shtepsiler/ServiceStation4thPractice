using Bogus;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PARTS.DAL.Entities.Item;

namespace PARTS.DAL.Seeders;

public class PartImageSeeder : ISeeder<PartImage>
{
    public void Seed(EntityTypeBuilder<PartImage> builder)
    {
        var partImageFaker = new Faker<PartImage>()
            .RuleFor(p => p.Id, f => f.Random.Guid())
            .RuleFor(pi => pi.SourceContentType, f => f.Lorem.Word())
            .RuleFor(pi => pi.HashFileContent, f => f.Random.Guid().ToString())
            .RuleFor(pi => pi.Size, f => f.Random.Number(1000, 5000))
            .RuleFor(pi => pi.Path, f => f.Lorem.Word())
            .RuleFor(pi => pi.Title, f => f.Lorem.Word());

        // builder.HasData(partImageFaker.Generate(10));
    }
}