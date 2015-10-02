using System;
using Sitecore.Data;
using Sitecore.Data.Fields;

namespace ScPredicateBuilderApp.DataObjects
{
	public class Product
	{
		public ID Id { get; set; }
		public string ProductName { get; set; }
		public string Description { get;  set; }
		public string SerialNumber { get; set; }
		public string Released { get; set; }
		public double Price { get; set; }
		public bool Available { get; set; }
		public string ImageSrc { get; set; }
		public string ImageAlt { get; set; }

		/// <summary>
		/// Default c'tor
		/// </summary>
		public Product()
		{
		}

		/// <summary>
		/// C'tor loading properties from database
		/// </summary>
		/// <param name="itemId"></param>
		public Product(ID itemId)
		{
			ProductName = string.Empty;
			Description = string.Empty;
			var productItem = Sitecore.Context.Database.GetItem(itemId);
			if (productItem != null)
			{
				ProductName = productItem["ProductName"];
				Description = productItem["Description"];
				SerialNumber = productItem["SerialNumber"];
				Released = productItem["Released"];
				Price = double.Parse(productItem["Price"] ?? "0.0");
				Available = productItem["Available"] == "1";
				ImageSrc = string.Empty;	// Initialize image stuff separately from media item; not implemented here
				ImageAlt = string.Empty;
			}
		}


	}
}