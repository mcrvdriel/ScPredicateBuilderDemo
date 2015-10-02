using System;
using System.ComponentModel;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;

namespace ScPredicateBuilderApp.DataObjects
{
	public class ProductSearchResult : SearchResultItem
	{
		public string ProductName { get; set; }
		public string Description { get; set; }
		public string SerialNumber { get; set; }
		public double Price { get; set; }
		public bool Available { get; set; }
		public string ProductImageUrl { get; set; }

		[IndexField("productimage")]
		public string ProductImageAlt { get; set; }

		[IndexField("released")]
		[TypeConverter(typeof(DateTimeConverter))]
		public DateTime ReleaseDate { get; set; }
	}
}