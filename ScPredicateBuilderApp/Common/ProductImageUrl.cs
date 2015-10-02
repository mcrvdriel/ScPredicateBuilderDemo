// "Compute" (return) the URL of a product image for storage in index

using System;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;

namespace ScPredicateBuilderApp.Common
{
	public class ProductImageUrl : ComputedFieldBase
	{
		/// <summary>
		/// Checks and calculations for product image URL
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		protected override object Compute(Item item)
		{
			if (!IsProduct(item)) return null;
			var imageFieldObject = item.Fields["ProductImage"];
			// Check if we actually have a field "ProductImage"
			if (imageFieldObject == null) return null;
			var imageField = (Sitecore.Data.Fields.ImageField) imageFieldObject;
			if (imageField == null || imageField.MediaItem == null) return null;
			return MediaManager.GetMediaUrl(imageField.MediaItem);
		}

		/// <summary>
		/// Check the item is a product item by checking it's template
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		private bool IsProduct(Item item)
		{
			return item.TemplateID.ToString().Equals(Constants.ProductTemplateId, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}