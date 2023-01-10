using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookStore.Data.Context;

public class BookStoreContext : DbContext
{
    protected readonly IConfiguration Configuration;
    
    public BookStoreContext(DbContextOptions<BookStoreContext> options, 
        IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("BookStoreDb"));
        
        optionsBuilder
            .EnableSensitiveDataLogging()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .LogTo(Console.WriteLine, LogLevel.Information);
        
        base.OnConfiguring(optionsBuilder);
    }

    // public DbSet<Book> Books { get; set; }
}