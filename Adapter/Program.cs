// See https://aka.ms/new-console-template for more information
using Adapter;

ICache cache = new RedisCacheAdapter(new RedisCache());
cache.Set("username", "john_doe");
Console.WriteLine(cache.Get("username"));

cache = new InMemoryCacheAdapter(new InMemoryCache());
cache.Set("cookie", "21kk1h!!#4");
Console.WriteLine(cache.Get("cookie"));
