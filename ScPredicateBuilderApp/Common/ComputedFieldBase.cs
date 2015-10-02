// Base class for computed fields to hold common checks and conversions
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Items;

namespace ScPredicateBuilderApp.Common
{
	public abstract class ComputedFieldBase : IComputedIndexField
	{
		public string FieldName { get; set; }
		public string ReturnType { get; set; }

		/// <summary>
		/// Return null if item is not valid. Nulls aren't indexed.
		/// </summary>
		/// <param name="indexable"></param>
		/// <returns></returns>
		public object ComputeFieldValue(IIndexable indexable)
		{
			var item = CheckItemIsValid(indexable);
			return (item == null) ? null : Compute(item);
		}

		/// <summary>
		/// Field specific computations
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		protected abstract object Compute(Item item);


		/// <summary>
		/// Check and convert 
		/// </summary>
		/// <param name="indexable"></param>
		/// <returns></returns>
		private Item CheckItemIsValid(IIndexable indexable)
		{
			var scIndexable = (SitecoreIndexableItem) indexable;
			if (scIndexable == null) return null;
			var item = (Item) scIndexable;
			return item ?? null;
		}
	}
}