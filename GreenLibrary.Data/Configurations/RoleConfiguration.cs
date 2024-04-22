namespace GreenLibrary.Data.Configurations
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static GreenLibrary.Common.ApplicationConstants;

    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
        {
            builder
                .HasData(GenerateRoles());
        }

        private IdentityRole<Guid>[] GenerateRoles()
        {
            var roles = new List<IdentityRole<Guid>>();

            IdentityRole<Guid> role;

            role = new IdentityRole<Guid>()
            {
                Name = AdministratorRoleName,
                NormalizedName = AdministratorRoleName.ToUpper(),
                Id = new Guid(AdministratorRoleId),
                ConcurrencyStamp = AdministratorRoleId.ToUpper(),
            };
            roles.Add(role);

            role = new IdentityRole<Guid>()
            {
                Name = ModeratorRoleName,
                NormalizedName = ModeratorRoleName.ToUpper(),
                Id = new Guid(ModeratorRoleId),
                ConcurrencyStamp = ModeratorRoleId.ToUpper(),
            };
            roles.Add(role);

            role = new IdentityRole<Guid>()
            {
                Name = UserRoleName,
                NormalizedName = UserRoleName.ToUpper(),
                Id = new Guid(UserRoleId),
                ConcurrencyStamp = UserRoleId.ToUpper(),
            };
            roles.Add(role);

            return roles.ToArray();
        }
    }
}
