using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Task8Collection.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Task8Collection.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Collection> Collections => Set<Collection>();
    public DbSet<Theme> Themes => Set<Theme>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Field> Fields => Set<Field>();
    public DbSet<BoolField> BoolFields => Set<BoolField>();
    public DbSet<IntField> IntFields => Set<IntField>();
    public DbSet<StringField> StringFields => Set<StringField>();
    public DbSet<DateField> DateFields => Set<DateField>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<CollectionFields> CollectionFields => Set<CollectionFields>();
    public DbSet<LikedItems> LikedItems => Set<LikedItems>();
    public DbSet<ItemTag> ItemTag => Set<ItemTag>();
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Field>().UseTpcMappingStrategy().ToTable("Fields");
        builder.Entity<User>()
            .HasMany(u => u.Collections)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);
        builder
            .Entity<Tag>()
            .HasMany(t => t.Items)
            .WithMany(i => i.Tags)
            .UsingEntity<ItemTag>(
                j => j
                    .HasOne(pt => pt.Item)
                    .WithMany(t => t.ItemTags)
                    .HasForeignKey(pt => pt.ItemId),
                j => j
                    .HasOne(pt => pt.Tag)
                    .WithMany(p => p.ItemTags)
                    .HasForeignKey(pt => pt.TagId),
                j =>
                {
                    j.HasKey(t => new { t.ItemId, t.TagId });
                    j.ToTable("ItemTag");
                });
        base.OnModelCreating(builder);
        
    }
}