using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RiverTransportAutoschedule.Domain.Entities;
using RiverTransportAutoschedule.Domain.IdentityConfigurations;

namespace RiverTransportAutoschedule.Domain;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<RiverTransport> RiverTransports { get; set; }

    public DbSet<Schedule> Schedules { get; set; }

    public DbSet<RiverPort> RiverPorts { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Schedule>()
            .HasOne(s => s.RiverTransport)
            .WithMany(rt => rt.Schedules)
            .HasForeignKey(s =>  s.RiverTransportId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Schedule>()
            .HasOne(s => s.RiverPort)
            .WithMany(rp => rp.Schedules)
            .HasForeignKey(s => s.RiverPortId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new ApplicationUserRoleConfiguration());
    }
}