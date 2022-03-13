using System.Threading.Tasks;

namespace VSTManager
{
    public interface IShop
    {
        Task<bool> Search(string manufacturer, string model, bool strictSearch);
    }
    public class Shop
    {
        public Shop(string name, WebScraper refScraper)
        {
            switch (name.ToLower())
            {
                case "audiosolutions":
                    _shop = new ShopAudioSolutions(refScraper);
                    break;
                case "baxmusic":
                    _shop = new ShopBaxMusic(refScraper);
                    break;
                case "energyson":
                    _shop = new ShopEnergySon(refScraper);
                    break;
                case "keymusic":
                    _shop = new ShopKeyMusic(refScraper);
                    break;
                case "kitary":
                    _shop = new ShopKitary(refScraper);
                    break;
                case "michenaud":
                    _shop = new ShopMichenaud(refScraper);
                    break;
                case "musicmatos":
                    _shop = new ShopMusicMatos(refScraper);
                    break;
                case "musicstore":
                    _shop = new ShopMusicStore(refScraper);
                    break;
                case "sonovente":
                    _shop = new ShopSonoVente(refScraper);
                    break;
                case "starsmusic":
                    _shop = new ShopStarsMusic(refScraper);
                    break;
                case "thomann":
                    _shop = new ShopThomann(refScraper);
                    break;
                case "universsons":
                    _shop = new ShopUniversSons(refScraper);
                    break;
                case "woodbrass":
                    _shop = new ShopWoodbrass(refScraper);
                    break;
                case "centrechopin":
                    _shop = new ShopCentreChopin(refScraper);
                    break;
                default:
                    break;
            }
        }
        IShop _shop = null;

        public async Task<bool> Search(string manufacturer, string model, bool strictSearch)
        {
            return await _shop.Search(manufacturer, model, strictSearch);
        }
    }
}
