namespace HomeXplorer.ViewModels.Property.Agent
{
    using HomeXplorer.ViewModels.Property.Enums;

    public class AgentAllPropertiesViewModel
    {
        public AgentAllPropertiesViewModel()
        {
            this.Properties = new List<IndexAgentPropertiesViewModel>();
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; } //properties per page

        public int TotalPages { get; set; }

        public PropertySorting PropertySorting { get; set; }

        public IEnumerable<IndexAgentPropertiesViewModel> Properties { get; set; }
    }
}
