namespace HomeXplorer.Data.Models.Seeding
{
    using HomeXplorer.Data.Entities;

    using static HomeXplorer.Common.DataConstants.ApplicationUserConstants;

    public class AgentSeeder
    {
        public ICollection<Agent> GenerateAgents()
        {
            var agents = new List<Agent>()
            {
                new Agent()
                {
                    Id = 1,
                    UserId = "6ea2b1f0-3183-4fe5-b2fa-83b765e18e55",
                    CityId = 1,
                    ProfilePictureUrl = DefaultUserProfilePictureUrl
                }
            };

            return agents;
        }
    }
}
