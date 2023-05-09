using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiverTransportAutoschedule.Domain.Entities;

namespace RiverTransportAutoschedule.Domain.IdentityConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasData(
            new ApplicationUser
            {
                Id = "AA11B246-CB97-4E1F-B632-AE8F9AFC4660",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "RiverTransportCompany@gmail.com",
                NormalizedEmail = "RIVERTRANSPORTCOMPANY@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "superpassword"),
                SecurityStamp = string.Empty
            },

            new ApplicationUser
            {
                Id = "EBFA32C4-032F-4733-843D-E90F50EEC75B",
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "User123Test@gmail.com",
                NormalizedEmail = "USER123TEST@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "user"),
                SecurityStamp = string.Empty
            }
        );
    }
}
