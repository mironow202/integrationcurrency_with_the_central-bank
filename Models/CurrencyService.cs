using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;

namespace WebApplication1.Models
{
    public class CurrencyService : BackgroundService
    {
        private readonly IMemoryCache _memoryCache;
        public CurrencyService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           try
           {
               Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
               Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
               XDocument xDocument =  XDocument.Load("https://cbr.ru/scripts/XML_daily.asp?date_req=02/03/2022");
               CurrencyConverterModel converterModel = new CurrencyConverterModel();
               converterModel.EUR = Convert.ToDecimal(xDocument.Elements("ValCurs").Elements("Valute").FirstOrDefault(x => x.Element("NumCode").Value == "978").Elements("Value").FirstOrDefault().Value);
               converterModel.USD = Convert.ToDecimal(xDocument.Elements("ValCurs").Elements("Valute").FirstOrDefault(x => x.Element("NumCode").Value == "840").Elements("Value").FirstOrDefault().Value);
               converterModel.UAH = Convert.ToDecimal(xDocument.Elements("ValCurs").Elements("Valute").FirstOrDefault(x => x.Element("NumCode").Value == "980").Elements("Value").FirstOrDefault().Value);
               _memoryCache.Set("key_currency", converterModel, TimeSpan.FromMinutes(1440));
           }
           catch (Exception)
           {
           
               throw;
           }


            await Task.Delay(4320000);
        }
    }
}