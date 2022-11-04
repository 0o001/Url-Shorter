using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShorter.Models;
using UrlShorter.Repository;

namespace UrlShorter.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _shortUrlRepository;

        public ShortUrlService(IShortUrlRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public Task<ShortUrl> GetByCodeAsync(string shortUrlCode)
        {
            return _shortUrlRepository.GetByCodeAsync(shortUrlCode);
        }

        public Task<ShortUrl> CreateAsync(ShortUrl shortUrl)
        {
            return _shortUrlRepository.CreateAsync(shortUrl);
        }
    }
}
