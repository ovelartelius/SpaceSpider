using System;
using System.Collections.Generic;

namespace SeoSpider
{
	public class PageExtLinkComparer : IEqualityComparer<PageExtLink>
	{
		// Products are equal if their names and product numbers are equal.
		public bool Equals(PageExtLink x, PageExtLink y)
		{

			//Check whether the compared objects reference the same data.
			if (Object.ReferenceEquals(x, y)) return true;

			//Check whether any of the compared objects is null.
			if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
				return false;

			//Check whether the products' properties are equal.
			return x.PageUrl == y.PageUrl && x.ExtLinkUrl == y.ExtLinkUrl;
		}

		// If Equals() returns true for a pair of objects 
		// then GetHashCode() must return the same value for these objects.

		public int GetHashCode(PageExtLink product)
		{
			//Check whether the object is null
			if (Object.ReferenceEquals(product, null)) return 0;

			//Get hash code for the Name field if it is not null.
			int hashProductName = product.PageUrl == null ? 0 : product.PageUrl.GetHashCode();

			//Get hash code for the Code field.
			//int hashProductCode = product.Code.GetHashCode();
			int hashProductExtLink = product.ExtLinkUrl == null ? 0 : product.ExtLinkUrl.GetHashCode();

			//Calculate the hash code for the product.
			return hashProductName ^ hashProductExtLink;
		}

	}
}
