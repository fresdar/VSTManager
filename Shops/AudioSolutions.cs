﻿using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Globalization;
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
        private const string ShopName = "AudioSolutions";
        private WebScraper m_refScraper;
        public async Task<bool> Search(string manufacturer, string model, bool strictSearch)
        {
            string url = string.Format("https://www.audiosolutions.fr/recherche?controller=search&s={0}+{1}",
                manufacturer,
                model);
            return await Search(url, manufacturer, model, strictSearch);
        }
        private async Task<bool> Search(string url, string manufacturer, string model, bool strictSearch)
        {
            try
            {
                CultureInfo culture = new CultureInfo("fr-FR", false);
                var response = await m_refScraper.Analyze(new Uri(url));
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var html = await response.Content.ReadAsStringAsync();
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

                    doc.LoadHtml(html);
                    var root = doc.DocumentNode;
                    var productNodes = root.CssSelect("article.js-product-miniature");
                    foreach (var node in productNodes)
                    {
                        string m = string.Empty;
                        string price = string.Empty;
                        string murl = string.Empty;
                        string brand = string.Empty;
                        foreach (HtmlNode modelNode in node.CssSelect("h2.product-title a"))
                        {
                            m = modelNode.InnerHtml;
                            murl = modelNode.GetAttributeValue("href");
                        }
                        foreach (HtmlNode priceNode in node.CssSelect("span.price"))
                        {
                            price = priceNode.InnerHtml;
                            price = price.Trim().Replace("\u202F", "").Replace(",", ".");
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
            catch (Exception ex)
            {
                m_refScraper.ThrowException(ShopName, ex.Message);
            }

            return true;
        }
    }
}
