namespace HomeXplorer.Data.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class AgentProperty
    {
        [ForeignKey(nameof(Agent))]
        public int AgentId { get; set; }

        public virtual Agent Agent { get; set; } = null!;

        [ForeignKey(nameof(Property))]
        public int PropertyId { get; set; }

        public virtual Property Property { get; set; } = null!;
    }
}
