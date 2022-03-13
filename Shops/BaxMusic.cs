using HtmlAgilityPack;
using Newtonsoft.Json;
using ScrapySharp.Extensions;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;

namespace VSTManager
{
    public class ShopBaxMusic: IShop
    {
        public ShopBaxMusic(WebScraper refScraper)
        {
            m_refScraper = refScraper;
        }
        private const string ShopName = "Bax music";
        private WebScraper m_refScraper;
        public async Task<bool> Search(string manufacturer, string model, bool strictSearch)
        {
            string url = string.Format("https://www.bax-shop.fr/catalogsearch/result/?q={0}+{1}",
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
                var productNode = root.CssSelect(".product-box");
                foreach (var node in productNode)
                {
 
                    string enc = node.GetAttributeValue("data-product");
                    string info = HttpUtility.HtmlDecode(enc);
                    var productInfo = JsonConvert.DeserializeObject<dynamic>(info);
                    string brand = productInfo.brand;
                    string murl = url;
                    string m = productInfo.name;
                    string price = productInfo.price;
                    price = price.Replace(".00", "");
                    string currency = CurrencyTools.GetCurrencySymbol((string)productInfo.currencyCode);
                    
                    WebScraper.ExtractBrandFromModel(manufacturer, ref m, ref brand);

                    foreach (HtmlNode urlNode in node.CssSelect("div.product-thumb > a"))
                    {
                        murl = "https://www.bax-shop.fr" + urlNode.GetAttributeValue("href");
                    }

                    bool canAdd = culture.CompareInfo.IndexOf(m, model, CompareOptions.IgnoreCase) >= 0;
                    if (strictSearch)
                    {
                        canAdd &= culture.CompareInfo.IndexOf(brand, manufacturer, CompareOptions.IgnoreCase) >= 0;
                    }
                    if (canAdd)
                        m_refScraper.AddPrice(brand, m, ShopName, murl, price + " " + currency);
                }
            }
            else
            {
                m_refScraper.ThrowException(ShopName, response.ReasonPhrase);
            }

            return true;
        }
    }
}
