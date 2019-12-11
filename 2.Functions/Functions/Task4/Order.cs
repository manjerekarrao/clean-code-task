using System.Collections.Generic;
using System.Linq;
using Functions.Task4.ThirdParty;

namespace Functions.Task4
{
    public class Order
    {
        public IList<IProduct> Products { get; set; }

        public double GetPriceOfAvailableProducts()
        {
            IEnumerator<IProduct> enumerator = Products.ToList().GetEnumerator();
            while (enumerator.MoveNext())
            {
                IProduct product = enumerator.Current;
                if (!product.IsAvailable())
                {
                    Products.Remove(product);
                }
            }

            var orderPrice = 0.0;
            foreach (IProduct product in Products)
            {
                orderPrice += product.GetProductPrice();
            }
            return orderPrice;
        }
    }
}
