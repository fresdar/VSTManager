using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace VSTManager
{
    public static class CurrencyTools
    {
        public static string GetCurrencySymbol(string ISOCurrencyCode)
        {
            string symbol = CultureInfo
                .GetCultures(CultureTypes.AllCultures)
                .Where(c => !c.IsNeutralCulture)
                .Select(culture => {
                    try
                    {
                        if(culture.Name == "")
                        {
                            return new RegionInfo("FR");
                        }
                        return new RegionInfo(culture.Name);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(ri => ri != null && ri.ISOCurrencySymbol == ISOCurrencyCode)
                .Select(ri => ri.CurrencySymbol)
                .FirstOrDefault();
            if (symbol == null)
                return ISOCurrencyCode;
            return symbol;
        }
    }
}
