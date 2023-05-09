using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiverTransportAutoschedule.Domain.IdentityConfigurations;

public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                UserId = "AA11B246-CB97-4E1F-B632-AE8F9AFC4660",
                RoleId = "39C5C918-1235-4C2B-8B81-962D26794B1E"
            },

            new IdentityUserRole<string>
            {
                UserId = "EBFA32C4-032F-4733-843D-E90F50EEC75B",
                RoleId = "70246A84-AFB1-409A-AEF2-6D020ECD9276"
            }
        );
    }
}