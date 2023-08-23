using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int,
    IdentityUserClaim<int>, AppUserRole,
    IdentityUserLogin<int>,
    IdentityRoleClaim<int>,
    IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserLike> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Connection> Connections { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(user => user.UserRoles)
                .WithOne(userRole => userRole.User)
                .HasForeignKey(userRole => userRole.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(userRole => userRole.UserRoles)
                .WithOne(role => role.Role)
                .HasForeignKey(role => role.RoleId)
                .IsRequired();

            builder.Entity<UserLike>()
                .HasKey(key => new { key.SourceUserId, key.LikedUserId });

            builder.Entity<UserLike>()
                .HasOne(source => source.SourceUser)
                .WithMany(liked => liked.LikedUsers)
                .HasForeignKey(source => source.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade); // Use NoAction if you're using SqlServer

            builder.Entity<Message>()
                .HasOne(source => source.Recipient)
                .WithMany(message => message.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict); // Use NoAction if you're using SqlServer

            builder.Entity<Message>()
                .HasOne(source => source.Sender)
                .WithMany(message => message.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict); // Use NoAction if you're using SqlServer
        }
    }
}