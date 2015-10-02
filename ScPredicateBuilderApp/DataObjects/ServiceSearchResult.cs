using Sitecore.ContentSearch.SearchTypes;

namespace ScPredicateBuilderApp.DataObjects
{
	public class ServiceSearchResult : SearchResultItem
	{
		public string ServiceName { get; set; }
		public string Description { get; set; }
		public double Rate { get; set; }
	}
}