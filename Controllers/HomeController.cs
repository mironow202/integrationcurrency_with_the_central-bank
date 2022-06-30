using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        public HomeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public IActionResult Index()
        {
            if (!_memoryCache.TryGetValue("key_currency", out CurrencyConverterModel currencyConverterModel))
            {
                throw new Exception("Ошибка получения данных");
            }
            return View(currencyConverterModel);
        }

    }
}
