using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShorter.Models;

namespace UrlShorter.Services
{
    public interface IShortUrlService
    {
        Task<ShortUrl> GetByCodeAsync(string shortUrlCode);
        Task<ShortUrl> CreateAsync(ShortUrl shortUrl);
    }
}
