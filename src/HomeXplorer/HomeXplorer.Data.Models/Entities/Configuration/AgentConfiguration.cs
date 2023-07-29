namespace HomeXplorer.Data.Models.Entities.Configuration
{
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Data.Models.Seeding;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AgentConfiguration
        : IEntityTypeConfiguration<Agent>
    {
        private readonly AgentSeeder seeder;

        public AgentConfiguration()
        {
            this.seeder = new AgentSeeder();
        }

        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            //Add into context
            var agents = this.seeder.GenerateAgents();
            builder.HasData(agents);
        }
    }
}
