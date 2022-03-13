using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace VSTManager
{
    public class ShopKeyMusic : IShop
    {
        public ShopKeyMusic(WebScraper refScraper)
        {
            m_refScraper = refScraper;
        }
        private const string ShopName = "KeyMusic";

        private WebScraper m_refScraper;
        public async Task<bool> Search(string manufacturer, string model, bool strictSearch)
        {
            string url = string.Format("https://www.keymusic.com/fr/recherche/?q={0}+{1}",
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
                var productNodes = root.CssSelect("div.product");
                foreach (var productNode in productNodes)
                {
                    string m = string.Empty;
                    string price = string.Empty;
                    string murl = string.Empty;
                    string brand = string.Empty;
                    foreach (HtmlNode urlNode in productNode.CssSelect("a.product-link"))
                    {
                        murl = "https://www.keymusic.com" + urlNode.GetAttributeValue("href");
                    }
                    foreach (HtmlNode nameNode in productNode.CssSelect("div.title"))
                    {
                        m = WebScraper.UppercaseFirst(nameNode.InnerText.Trim());
                        WebScraper.ExtractBrandFromModel(manufacturer, ref m, ref brand);
                    }
                    foreach (HtmlNode priceNode in productNode.CssSelect("span.price-label"))
                    {
                        price = HttpUtility.HtmlDecode(priceNode.InnerHtml).Trim();
                        price = price.Replace(",", ".");
                        price += " €";
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
            else
            {
                m_refScraper.ThrowException(ShopName, response.ReasonPhrase);
            }

            return true;
        }
    }
}
