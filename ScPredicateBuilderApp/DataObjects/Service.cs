using Sitecore.Data;

namespace ScPredicateBuilderApp.DataObjects
{
	public class Service
	{
		public ID Id { get; set; }
		public string ServiceName { get; set; }
		public string Description { get;  set; }
		public double Rate { get; set; }

		/// <summary>
		/// Default c'tor
		/// </summary>
		public Service()
		{
		}

		/// <summary>
		/// C'tor loading properties from database
		/// </summary>
		/// <param name="itemId"></param>
		public Service(ID itemId)
		{
			ServiceName = string.Empty;
			Description = string.Empty;
			var productItem = Sitecore.Context.Database.GetItem(itemId);
			if (productItem != null)
			{
				ServiceName = productItem["ServiceName"];
				Description = productItem["Description"];
				Rate = double.Parse(productItem["Rate"] ?? "0.0");
			}
		}
	}
}