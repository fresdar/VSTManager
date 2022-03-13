using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace VSTManager
{
    public class ShopKitary : IShop
    {
        public ShopKitary(WebScraper refScraper)
        {
            m_refScraper = refScraper;
        }
        private const string ShopName = "Kitary";

        private WebScraper m_refScraper;
        public async Task<bool> Search(string manufacturer, string model, bool strictSearch)
        {
            string url = string.Format("https://kytary.fr/Search/?term={0}%20{1}",
                manufacturer,
                model);
            return await Search(url, manufacturer, model, strictSearch);
        }
        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
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
                var productNodes = root.CssSelect("li.product-list__item");
                foreach (var productNode in productNodes)
                {
                    string m = string.Empty;
                    string price = string.Empty;
                    string murl = string.Empty;
                    string brand = string.Empty;
                    foreach (HtmlNode infoNode in productNode.CssSelect("div.test-a > a"))
                    {
                        string info = HttpUtility.HtmlDecode(infoNode.GetAttributeValue("onclick"));
                        var data = info.Split(',');
                        m = data[1].Trim('\'');
                        brand = data[4].Trim('\'');
                    }
                    foreach (HtmlNode urlNode in productNode.CssSelect("h2.delta > a"))
                    {
                        murl = "https://kytary.fr" + urlNode.GetAttributeValue("href");
                    }
                    foreach (HtmlNode priceNode in productNode.CssSelect("p.price"))
                    {
                        price = HttpUtility.HtmlDecode(priceNode.InnerHtml).Trim();
                        price = price.Replace(",00", "");
                        price = Regex.Replace(price, @"\s+", "");
                        price = price.Insert(price.Length - 1, " ");
                        price = price.Replace(",", ".");
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
