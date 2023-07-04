namespace HomeXplorer.ViewModels.Property.Agent
{
    using HomeXplorer.ViewModels.Property.Agent.Enums;

    public class AllPropertiesViewModel
    {
        public AllPropertiesViewModel()
        {
            this.Properties = new List<IndexAgentPropertiesViewModel>();
        }

        public PropertySorting PropertySorting { get; set; }

        public IEnumerable<IndexAgentPropertiesViewModel> Properties { get; set; }
    }
}
