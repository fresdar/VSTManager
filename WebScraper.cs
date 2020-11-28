using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace VSTManager
{
    public class WebScraper
    {
        public WebScraper()
        {
        }
        private Task[] _searchTasks;
        private string[] _shops = {
        "AudioSolutions",
        "BaxMusic",
        "KeyMusic",
        "Kitary",
        "Michenaud",
        "MusicMatos",
        "StarsMusic",
        "Thomann",
        "UniversSons",
        "Woodbrass",
        "MusicStore",
        "EnergySon",
        };

        private Dictionary<string, ManufacturerProducts> _manufacturers = new Dictionary<string, ManufacturerProducts>();
        public void AddPrice(string manufacturer, string model, string shop, string url, string price)
        {
            ManufacturerProducts products;
            if (!_manufacturers.TryGetValue(manufacturer, out products))
            {
                products = new ManufacturerProducts();
                var shopPrice = new ShopPrice();
                shopPrice.Price = price;
                shopPrice.Url = url;
                ModelPrices modelPrices = new ModelPrices();
                modelPrices.Prices.Add(shop, shopPrice);
                products.Products.Add(model, modelPrices);
                _manufacturers.Add(manufacturer, products);
            }
            else
            {
                ModelPrices models;
                if (!products.Products.TryGetValue(model, out models))
                {
                    var shopPrice = new ShopPrice();
                    shopPrice.Price = price;
                    shopPrice.Url = url;
                    ModelPrices modelPrices = new ModelPrices();
                    modelPrices.Prices.Add(shop, shopPrice);
                    products.Products.Add(model, modelPrices);
                }
                else
                {
                    ShopPrice shopPrice;
                    if (!models.Prices.TryGetValue(shop, out shopPrice))
                    {
                        shopPrice = new ShopPrice();
                        shopPrice.Price = price;
                        shopPrice.Url = url;
                        models.Prices.Add(shop, shopPrice);
                    }
                    else
                    {
                        shopPrice.Price = price;
                        shopPrice.Url = url;
                    }
                }
            }
        }
        public async Task<HttpResponseMessage> Analyze(Uri url)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(request);

            return response;
        }
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }

        public void Search(string manufacturer, string model, bool strictSearch)
        {
            _manufacturers.Clear();
            int index = 0;
            _searchTasks = new Task[_shops.Length];

            foreach (string shop in _shops)
            {
                Shop shopAudioSolutions = new Shop(shop, this);
                _searchTasks[index++] = shopAudioSolutions.Search(manufacturer, model, strictSearch);
            }
        }

        public async Task<string> getResult()
        {
            await Task.WhenAll(_searchTasks);

            string result = @"<!DOCTYPE html><html lang=""fr""><head><meta charset='utf-8'>
<meta name=""viewport"" content=""width=device-width,initial-scale=1,shrink-to-fit= no"">
<link href=""https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.5.0/css/bootstrap.min.css"" rel=stylesheet>
<link href=""https://cdnjs.cloudflare.com/ajax/libs/datatables/1.10.21/css/dataTables.bootstrap4.min.css"" rel=stylesheet>
</ head><body>
<table class=""table""><thead class=""thead-light""><tr><th>Marque</th><th>Référence</th><th>Magasin</th><th>Prix</th></tr></thead><tbody>";
            foreach (var manufacturer in _manufacturers)
            {
                string row = string.Empty;
                string brand = manufacturer.Key;
                foreach(var model in manufacturer.Value.Products)
                {
                    string mod = model.Key;
                    foreach (var shop in model.Value.Prices)
                    {
                        string sho = shop.Key;
                        string price = shop.Value.Price;
                        string url = shop.Value.Url;
                        row = string.Format("<tr><td>{0}</td><td>{1}</td><td><a href='{4}'>{2}</a></td><td>{3}</td></tr>",
                            brand,mod,sho,price,url);
                        result += row;
                    }
                }
            }
            result += @"</tbody></table>
<script src=""https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js""></script>
<script src=""https://cdnjs.cloudflare.com/ajax/libs/datatables/1.10.21/js/jquery.dataTables.min.js""></script>
<script src=""https://cdnjs.cloudflare.com/ajax/libs/datatables/1.10.21/js/dataTables.bootstrap4.min.js""></script>
<script src=""https://cdn.datatables.net/plug-ins/1.10.21/sorting/currency.js""></script>
<script>
$(document).ready(function() {
  $('.table').DataTable({
        language: { url: 'https://cdn.datatables.net/plug-ins/1.10.21/i18n/French.json' },
        columnDefs: [{ type: 'currency', targets: 3 }]
    });
});
</script>
</body></html>";
            return result;
        }
    }
}
