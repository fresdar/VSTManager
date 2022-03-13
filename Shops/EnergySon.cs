using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace VSTManager
{
    public class ShopEnergySon : IShop
    {
        public ShopEnergySon(WebScraper refScraper)
        {
            m_refScraper = refScraper;
        }

        private const string ShopName = "EnergySon";

        private WebScraper m_refScraper;
        public async Task<bool> Search(string manufacturer, string model, bool strictSearch)
        {
            string url = string.Format("https://www.energyson.fr/recherche?search={0}+{1}",
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
                var productNodes = root.CssSelect("div.search_product_item");
                foreach (var productNode in productNodes)
                {
                    string m = string.Empty;
                    string price = string.Empty;
                    string murl = string.Empty;
                    string brand = string.Empty;
                    foreach (HtmlNode urlNode in productNode.CssSelect("a"))
                    {
                        murl = "https://www.energyson.fr/" + urlNode.GetAttributeValue("href");
                    }
                    foreach (HtmlNode nameNode in productNode.CssSelect("div.name"))
                    {
                        m = WebScraper.UppercaseFirst(nameNode.InnerHtml.Trim());
                        brand = m.Split().FirstOrDefault();
                        if (String.IsNullOrEmpty(brand))
                        {
                            brand = string.Empty;
                        }
                    }
                    foreach (HtmlNode priceNode in productNode.CssSelect("div.price"))
                    {
                        price = HttpUtility.HtmlDecode(priceNode.InnerHtml).Trim();
                        price = price.Replace(",00", "");
                    }
                    bool canAdd = culture.CompareInfo.IndexOf(m, model, CompareOptions.IgnoreCase) >= 0;
                    if (strictSearch)
                    {
                        canAdd &= culture.CompareInfo.IndexOf(brand, manufacturer, CompareOptions.IgnoreCase) >= 0;
                    }
                    if (canAdd)
                    {
                        m_refScraper.AddPrice(brand, m, ShopName, murl, price);
                    }
                }
                if (productNodes.Count() == 0)
                {
                    productNodes = root.CssSelect("#product_zoom");
                    if (productNodes.Count() == 1)
                    {
                        // Direct to product page
                        string m = string.Empty;
                        string price = string.Empty;
                        string murl = string.Empty;
                        string brand = string.Empty;
                        var urlNode = root.CssSelect("div.fb-share-button");
                        if (urlNode.Count() == 1)
                        {
                            murl = urlNode.First().GetAttributeValue("data-href");
                        }
                        foreach (HtmlNode dataNode in productNodes.CssSelect("h1"))
                        {
                            brand = dataNode.ChildNodes.First().InnerText;
                            m = dataNode.ChildNodes.Last().InnerText;
                        }

                        foreach (HtmlNode priceNode in productNodes.CssSelect("span.product_zoom_price_vente > span"))
                        {
                            price = HttpUtility.HtmlDecode(priceNode.InnerText).Trim();
                            price = price.Replace(",00", "");
                            break;
                        }
                        bool canAdd = culture.CompareInfo.IndexOf(m, model, CompareOptions.IgnoreCase) >= 0;
                        if (strictSearch)
                        {
                            canAdd &= culture.CompareInfo.IndexOf(brand, manufacturer, CompareOptions.IgnoreCase) >= 0;
                        }
                        if (canAdd)
                        {
                            m_refScraper.AddPrice(brand, m, ShopName, murl, price);
                        }
                    }
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
