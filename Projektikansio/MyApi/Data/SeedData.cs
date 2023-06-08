
using Microsoft.EntityFrameworkCore;

namespace MyApi.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MydbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MydbContext>>()))
            {
                if (context == null)
                {
                    throw new ArgumentNullException("Null QuotesContext");
                }

                context.Database.EnsureCreated();





            }
        }
    }
}
