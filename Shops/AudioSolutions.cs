using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace VSTManager
{
    public class ShopAudioSolutions : IShop
    {
        public ShopAudioSolutions(WebScraper refScraper)
        {
            m_refScraper = refScraper;
        }

        private WebScraper m_refScraper;
        public Task<bool> Search(string manufacturer, string model, bool strictSearch)
        {
            string url = string.Format("https://www.audiosolutions.fr/advanced_search_result.php?keywords={0}+{1}",
                manufacturer,
                model);
            return Search(url, strictSearch);
        }
        private async Task<bool> Search(string url, bool strictSearch)
        {
            var response = await m_refScraper.Analyze(new Uri(url));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var html = await response.Content.ReadAsStringAsync();
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

                doc.LoadHtml(html);
                var root = doc.DocumentNode;
                var table = root.CssSelect(".productListing");
                if (table.Count() > 0)
                {
                    bool header = true;
                    foreach (HtmlNode row in table.CssSelect("tr"))
                    {
                        if (header)
                        {
                            header = false;
                        }
                        else
                        {
                            int col = 0;
                            string manufacturer = string.Empty;
                            string model = string.Empty;
                            string price = string.Empty;
                            foreach (HtmlNode cell in row.CssSelect("td"))
                            {
                                switch (col)
                                {
                                    case 1:
                                        manufacturer = WebScraper.UppercaseFirst(HttpUtility.HtmlDecode(cell.InnerText).Trim());
                                        break;
                                    case 2:
                                        model = WebScraper.UppercaseFirst(HttpUtility.HtmlDecode(cell.InnerText).Trim());
                                        break;
                                    case 3:
                                        price = HttpUtility.HtmlDecode(cell.InnerText).Trim();
                                        price = price.Remove(price.Length - 1) + "€";
                                        price = price.Replace(".00","");
                                        price = price.Replace(",", "");
                                        var temp = price.Split();
                                        int last = temp.Length -1;
                                        if( last > 0 )
                                            price = temp[last];
                                        break;
                                }
                                col++;
                            }

                            m_refScraper.AddPrice(manufacturer, model, "AudioSolutions", url, price);
                        }
                    }
                }
            }

            return true;
        }
    }
}
