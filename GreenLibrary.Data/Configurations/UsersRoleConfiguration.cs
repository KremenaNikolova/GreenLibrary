namespace GreenLibrary.Data.Configurations
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static GreenLibrary.Common.ApplicationConstants;

    public class UsersRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder
                .HasKey(iur=>new {iur.RoleId, iur.UserId});

            builder
                .HasData(GenerateUsersRoles());
        }

        private IdentityUserRole<Guid>[] GenerateUsersRoles()
        {
            var roles = new List<IdentityUserRole<Guid>>();

            IdentityUserRole<Guid> role;

            role = new IdentityUserRole<Guid>()
            {
                RoleId = new Guid(AdministratorRoleId),
                UserId = new Guid(AdminId)
            };
            roles.Add(role);

            role = new IdentityUserRole<Guid>()
            {
                RoleId = new Guid(ModeratorRoleId),
                UserId = new Guid(ModeratorId)
            };
            roles.Add(role);

            role = new IdentityUserRole<Guid>()
            {
                RoleId = new Guid(UserRoleId),
                UserId = new Guid(UserId)
            };
            roles.Add(role);

            return roles.ToArray();
        }
    }
}
