using System;
using System.Security.Permissions;
using MongoDB.Bson.Serialization.Attributes;

namespace UrlShorter.Models
{
    public class ShortUrl : Base
    {
        public Uri LongUrl { get; protected set; }
        [BsonElement]
        public string ShortUrlCode => Id.Substring(18);
        public ShortUrl(Uri longUrl)
        {
            LongUrl = longUrl;
        }
    }
}
