using Microsoft.EntityFrameworkCore;
using PlatformService.Context;

namespace PlatformService.SeedingData
{
    public static class initialization
    {
        public static void prepareData(IApplicationBuilder app ,bool isProd)
        {

            using(var serviceScope=app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        public static void SeedData(AppDbContext context, bool isProd) {

            if (isProd) context.Database.Migrate();
      
            

                if (!context.Platform.Any())
                {
                    context.Platform.AddRangeAsync(
                        new Platform { Name = "Dot Net", Publisher = "microsoft", Cost = "Free" },
                        new Platform { Name = "java ", Publisher = "java", Cost = "10$" },
                        new Platform { Name = "python ", Publisher = "microsoft", Cost = "20$" },
                        new Platform { Name = "windows", Publisher = "microsoft", Cost = "Free" }

                        );
                    context.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine("Data is aleardy inserted before ");
                }
            
        }
    }
}
