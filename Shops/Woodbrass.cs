using ScrapySharp.Extensions;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace VSTManager
{
    public class ShopWoodbrass : IShop
    {
        public ShopWoodbrass(WebScraper refScraper)
        {
            m_refScraper = refScraper;
        }
        private const string ShopName = "Woodbrass";

        private WebScraper m_refScraper;
        public async Task<bool> Search(string manufacturer, string model, bool strictSearch)
        {
            string url = string.Format("https://www.woodbrass.com/advanced_search_result.php?keywords={0}+{1}",
                manufacturer,
                model);
            return await Search(url, manufacturer, model, strictSearch);
        }
        private string getValue(string data)
        {
            string value = data.Replace("\"", "");
            int pos = value.IndexOf(':');
            return value.Substring(pos + 1);
        }
        private async Task<bool> Search(string url, string manufacturer, string model, bool strictSearch)
        {
            try {
                CultureInfo culture = new CultureInfo("fr-FR", false);
                var response = await m_refScraper.Analyze(new Uri(url));
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var html = await response.Content.ReadAsStringAsync();
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

                    doc.LoadHtml(html);
                    var root = doc.DocumentNode;
                    bool bFound = false;
                    // Search result page
                    var productNodes = root.CssSelect("div.slider-boxes > a");
                    foreach (var productNode in productNodes)
                    {
                        bFound = true;
                        string m = string.Empty;
                        string price = string.Empty;
                        string murl = string.Empty;
                        string brand = string.Empty;

                        murl = "https://www.woodbrass.com/" + productNode.GetAttributeValue("href");
                        string info = productNode.GetAttributeValue("onclick");
                        var data = info.Split(',');
                        m = WebScraper.UppercaseFirst(getValue(data[2]));
                        price = getValue(HttpUtility.HtmlDecode(data[4]));
                        price = price.Replace(".00", "");
                        price += " €";
                        brand = WebScraper.UppercaseFirst(getValue(HttpUtility.HtmlDecode(data[5])));

                        bool canAdd = culture.CompareInfo.IndexOf(m, model, CompareOptions.IgnoreCase) >= 0;
                        if (strictSearch)
                        {
                            canAdd &= culture.CompareInfo.IndexOf(brand, manufacturer, CompareOptions.IgnoreCase) >= 0;
                        }
                        if (canAdd)
                        {
                            m_refScraper.AddPrice(brand, m, "Woodbrass", murl, price);
                        }
                    }
                    if (!bFound)
                    {
                        productNodes = root.CssSelect("script");
                        if (productNodes.Count() > 0)
                        {
                            // Product page
                            string redirect = productNodes.Last().InnerHtml;
                            var data = redirect.Split('\'');
                            url = "https://www.woodbrass.com/" + data[1];

                            response = await m_refScraper.Analyze(new Uri(url));
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                html = await response.Content.ReadAsStringAsync();
                                doc = new HtmlAgilityPack.HtmlDocument();

                                doc.LoadHtml(html);
                                root = doc.DocumentNode;

                                string price = string.Empty;
                                string m = string.Empty;
                                string brand = string.Empty;
                                var modelNodes = root.CssSelect("h1");
                                if (modelNodes.Count() > 0)
                                {
                                    m = WebScraper.UppercaseFirst(modelNodes.First().InnerHtml);
                                }
                                var priceNodes = root.CssSelect("div.clrBleu");
                                if (priceNodes.Count() > 0)
                                {
                                    price = priceNodes.First().InnerHtml;
                                    price = price.Replace(".00", "");
                                    price = price.Insert(price.Length - 1, " ");
                                }

                                var metaNodes = root.CssSelect("meta");
                                foreach (var metaNode in metaNodes)
                                {
                                    if (metaNode.GetAttributeValue("itemprop") == "brand")
                                    {
                                        brand = WebScraper.UppercaseFirst(metaNode.GetAttributeValue("content"));
                                        break;
                                    }
                                }

                                m_refScraper.AddPrice(brand, m, "Woodbrass", url, price);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return true;
        }
    }
}
