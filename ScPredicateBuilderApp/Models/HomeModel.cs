using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using ScPredicateBuilderApp.Common;
using ScPredicateBuilderApp.DataObjects;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.ContentSearch;

namespace ScPredicateBuilderApp.Models
{
	/// <summary>
	/// Home model class
	/// </summary>
	public class HomeModel
	{
		public string Title { get; private set; }
		public string Description { get; private set; }

		public List<Product> Products { get; private set; }
		public List<Service> Services { get; private set; }

		/// <summary>
		/// C'tor
		/// </summary>
		public HomeModel()
		{
			Title = string.Empty;
			Description = string.Empty;
			var homeItem = Sitecore.Context.Database.GetItem(new ID(Constants.HomeItemId));
			if (homeItem != null)
			{
				Title = homeItem["Title"];
				Description = homeItem["Description"];
			}
		}

		///// <summary>
		///// Get the products from Sitecore, traditional way
		///// </summary>
		//public void GetProducts()
		//{
		//	var productsFolder = Sitecore.Context.Database.GetItem(new ID("1FBE62E6-57F1-4A2F-895C-3BF329B53487"));
		//	var productItems = productsFolder.GetChildren(ChildListOptions.None);
		//	Products = productItems.Select(p => new Product()
		//	{
		//		Id=p.ID,
		//		ProductName = p["ProductName"],
		//		Description = p["Description"],
		//		SerialNumber = p["SerialNumber"]
		//	}).ToList();
		//}


		/// <summary>
		/// Get the products from Sitecore, using search
		/// </summary>
		public void GetProducts()
		{
			var availableProductExpression = GetAvailableProductsExpression();
			var products = SearchIndexContent<ProductSearchResult>(Constants.ProductTemplateId, availableProductExpression);
			Products = products.Select(p => new Product()
			{
				ProductName = p.ProductName,
				Description = p.Description,
				SerialNumber = p.SerialNumber,
				Released = p.ReleaseDate.ToString(CultureInfo.InvariantCulture),
				Price = p.Price,
				ImageSrc = p.ProductImageUrl,
				ImageAlt = p.ProductImageAlt
			}).ToList();
		}

		/// <summary>
		/// Get the services from Sitecore, using search
		/// </summary>
		public void GetServices()
		{
			var services = SearchIndexContent<ServiceSearchResult>(Constants.ServiceTemplateId);
			Services = services.Select(s => new Service()
			{
				ServiceName = s.ServiceName,
				Description = s.Description,
				Rate = s.Rate
			}).ToList();
		}

		/// <summary>
		/// Get the index contents using search
		/// </summary>
		public List<T> SearchIndexContent<T>(string templateId, Expression<Func<T, bool>> expression = null)
			where T : SearchResultItem
		{
			ISearchIndex myIndex = ContentSearchManager.GetIndex(Constants.IndexName);
			using (var context = myIndex.CreateSearchContext())
			{
				var tId = new ID(templateId);
				var exp = expression ?? PredicateBuilder.False<T>().Or(p => true); // Use a dummy predicate if no expression passed
				var query = context.GetQueryable<T>().Filter(t => t.TemplateId == tId).Filter(exp);
				// Filter instead of Where since we don't use Lucene scorings
				var result = query.ToList();
				return result;
			}
		}

		/// <summary>
		/// Return an expression for filtering on available products only
		/// </summary>
		/// <returns></returns>
		public Expression<Func<ProductSearchResult, bool>> GetAvailableProductsExpression()
		{
			var predicate = PredicateBuilder.True<ProductSearchResult>();	//True for "And"
			predicate = predicate.And(p => p.Available == true);
			return predicate;
		}


	}

}