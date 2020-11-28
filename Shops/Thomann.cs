using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace VSTManager
{
    public class ShopThomann : IShop
    {
        public ShopThomann(WebScraper refScraper)
        {
            m_refScraper = refScraper;
        }

        private WebScraper m_refScraper;
        public async Task<bool> Search(string manufacturer, string model, bool strictSearch)
        {
            string url = string.Format("https://www.thomann.de/fr/search_dir.html?sw={0}+{1}",
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
                var productNodes = root.CssSelect("div.extensible-article");
                foreach (var node in productNodes)
                {
                    string m = string.Empty;
                    string price = string.Empty;
                    string murl = string.Empty;
                    string brand = string.Empty;
                    foreach (HtmlNode brandNode in node.CssSelect("span.manufacturer"))
                    {
                        brand = WebScraper.UppercaseFirst(brandNode.InnerHtml);
                    }
                    foreach (HtmlNode modelNode in node.CssSelect("span.model"))
                    {
                        m = modelNode.InnerHtml;
                    }
                    foreach (HtmlNode priceNode in node.CssSelect("span.article-basketlink"))
                    {
                        price = priceNode.InnerHtml;
                        price = price.Replace(".", "");
                    }
                    foreach (HtmlNode urlNode in node.CssSelect("a.article-link"))
                    {
                        murl = urlNode.GetAttributeValue("href");
                    }

                    bool canAdd = culture.CompareInfo.IndexOf(m, model, CompareOptions.IgnoreCase) >= 0;
                    if (strictSearch)
                    {
                        canAdd &= culture.CompareInfo.IndexOf(brand, manufacturer, CompareOptions.IgnoreCase) >= 0;
                    }
                    if (canAdd)
                    {
                        m_refScraper.AddPrice(brand, m, "Thomann", murl, price);
                    }
                }
            }

            return true;
        }
    }
}
