﻿//using System.Text;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using PARTS.DAL.Entities.Item;

//namespace PARTS.DAL.Seeders;

//public class CategorySeeder : ISeeder<Category>
//{    public async void Seed(EntityTypeBuilder<Category> builder)
//    {

//        var titles = new HashSet<string>(); // Set to track unique titles
//        var stringbuilder = new StringBuilder();
//        stringbuilder.Append("generate json and fill them for 1 Category, Category of auto part public class Category : Base\r\n    {\r\n        public string Title { get; set; }\r\n        public string? Description { get; set; }\r\n}  ");
//        stringbuilder.Append(" exept ");
//        while (ls.Count < 20)
//        {
//            try
//            {
//                var category = Task.Run(() => aiseed.GenerateEntityAsync(stringbuilder.ToString())).GetAwaiter().GetResult();
//                category.Id = Guid.NewGuid();
//                // Check if the title is unique before adding
//                if (!titles.Contains(category.Title))
//                {
//                    ls.Add(category);
//                    titles.Add(category.Title); // Add title to the set to track uniqueness
//                    stringbuilder.Append(category.Title + " ");

//                }
//            }
//            catch { }
//        }

//        builder.HasData(.AsReadOnly());
//    }
//}