using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShorter.Models;

namespace UrlShorter.Repository
{
    public interface IShortUrlRepository
    {
        Task<ShortUrl> GetByCodeAsync(string shortUrlCode);
        Task<ShortUrl> CreateAsync(ShortUrl shortUrl);
    }
}
