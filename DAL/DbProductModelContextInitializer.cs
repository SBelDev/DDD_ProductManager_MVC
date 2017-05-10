using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DbProductModelContextInitializer : DropCreateDatabaseIfModelChanges<ProductModelContext>
    {
        protected override void Seed(ProductModelContext context)
        {
            IList<Category> Categories = new List<Category>();

            Categories.Add(new Category() { Name = "Category 1" });
            Categories.Add(new Category() { Name = "Category 1" });
            Categories.Add(new Category() { Name = "Category 1" });

            foreach (Category category in Categories)
                context.Categories.Add(category);

            IList<Tag> Tags = new List<Tag>();

            Tags.Add(new Tag() { Name = "Green" , Color = -16744448 });
            Tags.Add(new Tag() { Name = "Red" , Color = -65536 });
            Tags.Add(new Tag() { Name = "Yellow", Color = -10496 });

            foreach (Tag tag in Tags)
                context.Tags.Add(tag);

            context.SaveChanges();
        }
    }
}
