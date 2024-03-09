using Microsoft.EntityFrameworkCore;
using learnify.Models;

public class AppDbContext:DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Resource> Resources {get; set;}
    public DbSet<ResourceCollection> ResourceCollections {get; set;}
}