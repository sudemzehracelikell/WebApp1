using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApp1.models;

namespace WebApp1.Data
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext(DbContextOptions<ECommerceContext> options)
            : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<User> Users { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tüm entity'ler için soft delete filter'ı uygula
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var propertyMethodInfo = typeof(EFFilterExtensions).GetMethod("GetIsDeleted");
                    var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter);
                    var falseConstant = Expression.Constant(false);
                    var filter = Expression.Lambda(Expression.Equal(isDeletedProperty, falseConstant), parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }

            // İlişkileri yapılandır
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.UserId)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(p => p.Brand)
                    .WithMany(b => b.Products)
                    .HasForeignKey(p => p.BrandId)
                    .IsRequired(false);

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .IsRequired(false);
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.HasKey(po => new { po.OrderId, po.ProductId });

                entity.HasOne(po => po.Product)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(po => po.ProductId)
                    .IsRequired(false);

                entity.HasOne(po => po.Order)
                    .WithMany(o => o.ProductOrders)
                    .HasForeignKey(po => po.OrderId)
                    .IsRequired(false);
            });
        }
        private void SetQueryFilter<T>(ModelBuilder modelBuilder) where T : BaseEntity
        {
            modelBuilder.Entity<T>().HasQueryFilter(x => !x.IsDeleted);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entity = (BaseEntity)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedDate = DateTime.UtcNow;
                    entity.CreatedBy = "system";
                }
                else
                {
                    entity.ModifiedDate = DateTime.UtcNow;
                    entity.ModifiedBy = "system";
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}