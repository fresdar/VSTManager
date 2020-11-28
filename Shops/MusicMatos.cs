using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace VSTManager
{
    public class ShopMusicMatos :IShop
    {
        public ShopMusicMatos(WebScraper refScraper)
        {
            m_refScraper = refScraper;
        }

        private WebScraper m_refScraper;
        public async Task<bool> Search(string manufacturer, string model, bool strictSearch)
        {
            string url = string.Format("https://www.musicmatos.com/recherche?controller=search&s={0}+{1}",
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
                var productNodes = root.CssSelect("div.second-third-block");
                foreach (var productNode in productNodes)
                {
                    string m = string.Empty;
                    string price = string.Empty;
                    string murl = string.Empty;
                    string brand = string.Empty;
                    foreach (HtmlNode name in productNode.CssSelect(".product-name > a"))
                    {
                        murl = name.GetAttributeValue("href");
                        int pos = culture.CompareInfo.IndexOf(murl, "-" + model, CompareOptions.IgnoreCase);
                        if (pos ==-1)
                        {
                            pos = murl.LastIndexOf('-');
                        }
                        brand = murl.Substring(0, pos);
                        pos = brand.LastIndexOf('-');
                        brand = UppercaseFirst(brand.Substring(pos + 1));
                        m = UppercaseFirst(HttpUtility.HtmlDecode(name.InnerHtml));
                    }
                    foreach (HtmlNode priceNode in productNode.CssSelect("span.product-price"))
                    {
                        price = HttpUtility.HtmlDecode(priceNode.InnerHtml).Trim();
                        price = price.Replace(",00", "");
                        price = Regex.Replace(price, @"\s+", "");
                        price = price.Insert(price.Length - 1, " ");
                    }
                    bool canAdd = culture.CompareInfo.IndexOf(m, model, CompareOptions.IgnoreCase) >= 0;
                    if (strictSearch)
                    {
                        canAdd &= culture.CompareInfo.IndexOf(brand, manufacturer, CompareOptions.IgnoreCase) >= 0;
                    }
                    if (canAdd)
                    {
                        m_refScraper.AddPrice(brand, m, "Music Matos", murl, price);
                    }
                }
            }

            return true;
        }
    }
}
