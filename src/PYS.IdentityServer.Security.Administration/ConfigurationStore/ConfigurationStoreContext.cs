using Microsoft.EntityFrameworkCore;

namespace PYS.IdentityServer.Security.Administration.ConfigurationStore
{
    public class ConfigurationStoreContext : DbContext
    {
        public ConfigurationStoreContext(DbContextOptions<ConfigurationStoreContext> options) : base(options)
        { }

        //public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<ApiResourceEntity> ApiResources { get; set; }
        //public DbSet<IdentityResourceEntity> IdentityResources { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<ClientEntity>().HasKey(m => m.ClientId);
            builder.Entity<ApiResourceEntity>().HasKey(m => m.Name);
            //builder.Entity<IdentityResourceEntity>().HasKey(m => m.IdentityResourceName);
            base.OnModelCreating(builder);
        }
    }
}