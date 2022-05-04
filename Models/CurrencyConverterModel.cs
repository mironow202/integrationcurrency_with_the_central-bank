using Microsoft.Extensions.Caching.Memory;
using System.Runtime.InteropServices;


namespace WebApplication1.Models
{
    public class CurrencyConverterModel
    {
        public decimal EUR { get; set; }
        public decimal ConverterEURO (decimal rubl) => rubl / EUR;

        public decimal USD { get; set; }
        public decimal ConverterUSD(decimal rubl) => rubl / USD;

        public decimal UAH { get; set; }
        public decimal ConverterUAH(decimal rubl) => rubl / UAH;
      
    }
}