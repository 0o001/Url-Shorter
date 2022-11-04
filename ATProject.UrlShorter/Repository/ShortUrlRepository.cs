using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UrlShorter.Models;

namespace UrlShorter.Repository
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly IMongoCollection<ShortUrl> _collection;
        private readonly DbConfiguration _settings;

        public ShortUrlRepository(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;

            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);

            _collection = database.GetCollection<ShortUrl>(_settings.CollectionName);
        }

        public Task<ShortUrl> GetByCodeAsync(string shortUrlCode)
        {
            return _collection.Find(u => u.ShortUrlCode == shortUrlCode).FirstOrDefaultAsync();
        }

        public async Task<ShortUrl> CreateAsync(ShortUrl shortUrl)
        {
            await _collection.InsertOneAsync(shortUrl).ConfigureAwait(false);
            return shortUrl;
        }
    }
}
