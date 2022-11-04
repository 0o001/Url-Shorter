using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShorter.Models;
using UrlShorter.Services;

namespace UrlShorter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private readonly IShortUrlService _shortUrlService;

        public ShortUrlController(IShortUrlService shortUrlService)
        {
            _shortUrlService = shortUrlService;
        }

        [HttpGet("{shortUrlCode}")]
        public async Task<IActionResult> Get(string shortUrlCode)
        {
            var shortUrl = await _shortUrlService.GetByCodeAsync(shortUrlCode).ConfigureAwait(false);
            if (shortUrl == null)
            {
                return NotFound();
            }

            return Ok(shortUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Uri longUrl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var shortUrl = new ShortUrl(longUrl);
            await _shortUrlService.CreateAsync(shortUrl).ConfigureAwait(false);

            return Ok(shortUrl);
        }
    }
}
