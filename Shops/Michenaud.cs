using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;

namespace VSTManager
{
    public class ShopMichenaud :IShop
    {
        public ShopMichenaud(WebScraper refScraper)
        {
            m_refScraper = refScraper;
        }

        private WebScraper m_refScraper;
        public async Task<bool> Search(string manufacturer, string model, bool strictSearch)
        {
            string url = string.Format("https://www.michenaud.com/page/search.php?isAsearch=1&searchtext={0}+{1}",
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
                var productNodes = root.CssSelect("div.grilleItem");
                foreach (var productNode in productNodes)
                {
                    string m = string.Empty;
                    string price = string.Empty;
                    string murl = string.Empty;
                    string brand = string.Empty;
                    foreach (HtmlNode name in productNode.CssSelect("a.grilleLink"))
                    {
                        murl = name.GetAttributeValue("href");
                    }
                    foreach (HtmlNode name in productNode.CssSelect("span.articleMarque"))
                    {
                        brand = WebScraper.UppercaseFirst(HttpUtility.HtmlDecode(name.InnerHtml));
                    }
                    foreach (HtmlNode name in productNode.CssSelect("div.grilleDes"))
                    {
                        m = WebScraper.UppercaseFirst(HttpUtility.HtmlDecode(name.InnerHtml)).Trim();
                        int pos = m.IndexOf("</span>");
                        if(pos != -1)
                        {
                            m = WebScraper.UppercaseFirst(m.Substring(pos + 8)).Trim();
                        }
                    }
                    foreach (HtmlNode priceNode in productNode.CssSelect("span.price"))
                    {
                        price = HttpUtility.HtmlDecode(priceNode.InnerHtml).Trim();
                        price = price.Replace(".00", "");
                        price = price.Insert(price.Length - 1, " ");
                    }
                    bool canAdd = culture.CompareInfo.IndexOf(m, model, CompareOptions.IgnoreCase) >= 0;
                    if (strictSearch)
                    {
                        canAdd &= culture.CompareInfo.IndexOf(brand, manufacturer, CompareOptions.IgnoreCase) >= 0;
                    }
                    if (canAdd)
                    {
                        m_refScraper.AddPrice(brand, m, "Michenaud", murl, price);
                    }
                }
            }

            return true;
        }
    }
}
