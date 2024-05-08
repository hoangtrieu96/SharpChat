using Microsoft.EntityFrameworkCore;

namespace SharpChat.Data;

public static class Migrator
{
    public static void MigrateDatabase(IApplicationBuilder applicationBuilder)
    {
        using IServiceScope serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        AppDbContext? dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>()
            ?? throw new InvalidOperationException($"!!! Failed to get database context for migration");

        dbContext.Database.Migrate();
    }
}
