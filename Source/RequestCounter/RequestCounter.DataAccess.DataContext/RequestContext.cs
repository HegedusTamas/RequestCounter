using Microsoft.EntityFrameworkCore;

namespace RequestCounter.DataAccess.DataContext;

public class RequestContext : DbContext
{
    public RequestContext(DbContextOptions<RequestContext> options)
        : base(options)
    {

    }

    public DbSet<RequestLog> RequestLogs { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("RequestLog");
        }
    }
}