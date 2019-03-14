using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusApp
{
	public class Order
	{
		private List<Product> _products = new List<Product>();

		public BonusProvider Bonus { get; set; }

		public void AddProduct(Product p)
		{
			_products.Add(p);
		}
		public double GetValueOfProducts()
		{
			return _products.Sum(x => x.Value);
		}
		public double GetValueOfProducts(DateTime date)
		{
			//return _products.Where(x => date <= x.AvailableTo && x.AvailableFrom <= date).Sum(x => x.Value);
			return _products.Sum(x => date <= x.AvailableTo && x.AvailableFrom <= date ? x.Value : 0);
		}
		public double GetBonus()
		{
			return Bonus(GetValueOfProducts());
		}

		public double GetBonus(Func<double, double> b)
		{
			return b(GetValueOfProducts());
		}
		public double GetBonus(DateTime date, Func<double, double> b)
		{
			return b(GetValueOfProducts(date));
		}

		public double GetTotalPrice()
		{
			return GetValueOfProducts()-GetBonus();
		}

		public double GetTotalPrice(Func<double,double> f)
		{
			return GetValueOfProducts() - GetBonus(f);
		}

		public double GetTotalPrice(DateTime date, Func<double, double> f)
		{
			return GetValueOfProducts(date) - GetBonus(date, f);
		}

		public List<Product> SortProductOrderByAvailableTo()
		{
			return _products.OrderBy(x => x.AvailableTo).ToList();
		}
	}
}
