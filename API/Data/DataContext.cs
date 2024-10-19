using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, 
        AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<WorkOrderStatus> WorkOrderStatuses { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuoteItem> QuoteItems { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            // // Configure one-to-one relationship
            // builder.Entity<WorkOrder>()
            //     .HasOne(w => w.Quote)             // WorkOrder has one Quote
            //     .WithOne(q => q.WorkOrder)       // Quote has one WorkOrder
            //     .HasForeignKey<Quote>(q => q.WorkOrderId);
        }
    }
}