using ReversedService.Model.Entities;

namespace ReversedService.Model.Configs
{
    public class AuthorizationPairConfiguration : IEntityTypeConfiguration<AuthorizationPair>
    {

        public void Configure(EntityTypeBuilder<AuthorizationPair> authorizationPairBuilder)
        {
            authorizationPairBuilder.HasKey(p => p.Id);

            authorizationPairBuilder.Property(p => p.Login).IsRequired();
            authorizationPairBuilder.Property(p => p.PasswordHash).IsRequired();
        }

    }
}
