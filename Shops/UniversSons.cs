using Newtonsoft.Json;
using ScrapySharp.Extensions;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace VSTManager
{
    public class ShopUniversSons : IShop
    {
        public ShopUniversSons(WebScraper refScraper)
        {
            m_refScraper = refScraper;
        }

        private WebScraper m_refScraper;
        public async Task<bool> Search(string manufacturer, string model, bool strictSearch)
        {
            string url = string.Format("https://www.univers-sons.com/catalogsearch/?textMotorSearch={0}+{1}",
                manufacturer,
                model);
            return await Search(url, manufacturer, model, strictSearch);
        }
        private async Task<bool> Search(string url, string manufacturer, string model, bool strictSearch)
        {
            CultureInfo culture = new CultureInfo("fr-FR", false);
            var response = await m_refScraper.Analyze(new Uri(url));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var html = await response.Content.ReadAsStringAsync();
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

                doc.LoadHtml(html);
                var root = doc.DocumentNode;
                var script = root.CssSelect("script");
                
                string enc = script.First().InnerHtml;
                string info = HttpUtility.HtmlDecode(enc);
                info = info.Replace("dataLayer = [", "");
                info = info.Replace("'", "\"");
                info = info.Remove(info.Length - 2);
                var productInfo = JsonConvert.DeserializeObject<dynamic>(info);
                var list = productInfo.products;
                foreach(var product in list)
                {
                    string m = product.prdName;
                    string brand = product.prdBrand;
                    string price = product.prdAmount;
                    price = price.Replace(".00", "") + " €";
                    string murl = product.prdUrl;

                    bool canAdd = culture.CompareInfo.IndexOf(m, model, CompareOptions.IgnoreCase) >= 0;
                    if (strictSearch)
                    {
                        canAdd &= culture.CompareInfo.IndexOf(brand, manufacturer, CompareOptions.IgnoreCase) >= 0;
                    }
                    if (canAdd)
                    {
                        m_refScraper.AddPrice(brand, m, "Univers Sons", murl, price);
                    }
                }
            }

            return true;
        }
    }
}
