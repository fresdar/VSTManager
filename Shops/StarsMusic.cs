using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;

namespace VSTManager
{
    public class ShopStarsMusic : IShop
    {
        public ShopStarsMusic(WebScraper refScraper)
        {
            m_refScraper = refScraper;
        }

        private WebScraper m_refScraper;
        public async Task<bool> Search(string manufacturer, string model, bool strictSearch)
        {
            string url = string.Format("https://www.stars-music.fr/search?q={0}%20{1}",
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
                var productNodes = root.CssSelect("div.products-grid-item");
                foreach (var productNode in productNodes)
                {
                    string m = string.Empty;
                    string price = string.Empty;
                    string murl = string.Empty;
                    string brand = string.Empty;
                    foreach (HtmlNode infoNode in productNode.CssSelect(".pdt_brand"))
                    {
                        brand = WebScraper.UppercaseFirst(infoNode.InnerHtml.Trim());
                    }
                    foreach (HtmlNode urlNode in productNode.CssSelect("a.title"))
                    {
                        murl = "https://www.stars-music.fr" + urlNode.GetAttributeValue("href");
                    }
                    foreach (HtmlNode nameNode in productNode.CssSelect(".pdt_name"))
                    {
                        m = WebScraper.UppercaseFirst(nameNode.InnerText.Trim());
                    }
                    foreach (HtmlNode priceNode in productNode.CssSelect("div.price"))
                    {
                        price = HttpUtility.HtmlDecode(priceNode.InnerHtml).Trim();
                        price = price.Replace(".00", "");
                    }
                    bool canAdd = culture.CompareInfo.IndexOf(m, model, CompareOptions.IgnoreCase) >= 0;
                    if (strictSearch)
                    {
                        canAdd &= culture.CompareInfo.IndexOf(brand, manufacturer, CompareOptions.IgnoreCase) >= 0;
                    }
                    if (canAdd)
                    {
                        m_refScraper.AddPrice(brand, m, "Star's Music", murl, price);
                    }
                }
            }

            return true;
        }
    }
}
