using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSTManager
{
    public class ShopPrice
    {
        public ShopPrice()
        {
        }

        public string Price;
        public string Url;
    }
    public class ModelPrices
    {
        public ModelPrices()
        {
        }

        // dictionary of shop, price
        public Dictionary<string, ShopPrice> Prices = new Dictionary<string, ShopPrice>();
    }
    public class ManufacturerProducts
    {
        public ManufacturerProducts()
        {
        }

        // dictionary of model, price
        public Dictionary<string, ModelPrices> Products = new Dictionary<string, ModelPrices>();
    }
}
