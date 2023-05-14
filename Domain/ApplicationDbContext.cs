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

        builder.Entity<RiverTransport>().HasData(new RiverTransport
        {
            RiverTransportId = new Guid("BB4DC5AC-C22D-435A-9E28-F8B64DCDA5C0"),
            Name = "Титаник-230",
            TransportType = RiverTransportType.Passenger,
            Route = "Самара-Тольятти",
            Capacity = 150
        });

        builder.Entity<RiverPort>().HasData(new RiverPort
        {
            RiverPortId = new Guid("B51ECE33-F9BA-4BD8-949C-C619FCAF55B4"),
            Name = "Тольяттинский речной порт",
            Location = "г. Тольятти, ул. Коммунистическая 96"
        });

        builder.Entity<Schedule>().HasData(new Schedule
        {
            ScheduleId = new Guid("73A3B6DA-7614-4B46-BB78-8CD5F8AF6121"),
            DepartureTime = new DateTime(2023, 4, 22, 19, 0, 0),
            ArrivalTime = new DateTime(2023, 4, 22, 21, 30, 0),
            RiverTransportId = new Guid("BB4DC5AC-C22D-435A-9E28-F8B64DCDA5C0"),
            RiverPortId = new Guid("B51ECE33-F9BA-4BD8-949C-C619FCAF55B4")
        });

        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new ApplicationUserRoleConfiguration());
    }
}