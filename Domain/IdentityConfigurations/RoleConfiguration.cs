using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiverTransportAutoschedule.Domain.IdentityConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "39C5C918-1235-4C2B-8B81-962D26794B1E",
                Name = "admin",
                NormalizedName = "ADMIN",
            },

            new IdentityRole
            {
                Id = "70246A84-AFB1-409A-AEF2-6D020ECD9276",
                Name = "user",
                NormalizedName = "USER"
            }
        );
    }
}
