using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace snglrtycrvtureofspce_YandexCup.Classes;

internal static class ElamArchives
{
    private static readonly HttpClient Client = new HttpClient();
    private const string DbUrl = "http://localhost:80/db/";

    private static readonly List<string> CacheUrls = new List<string>
    {
        "http://localhost:80/cache/1/",
        "http://localhost:80/cache/2/"
    };

    private static readonly Dictionary<string, string> CacheMap = new Dictionary<string, string>();
    private static readonly HashSet<string> UnavailableCaches = new HashSet<string>();

    private static async Task<string?> GetFromCacheOrDb(string key)
    {
        var tasks = CacheUrls
            .Where(url => !UnavailableCaches.Contains(url))
            .Select(url => GetFromCache(url, key));

        var cacheResponses = await Task.WhenAll(tasks);
        var cacheResult = cacheResponses.FirstOrDefault(response => response != null);

        if (cacheResult != null)
        {
            return cacheResult;
        }

        var dbResponse = await GetFromDb(key);
        if (dbResponse != null)
        {
            await WriteToCaches(key, dbResponse);
        }

        return dbResponse;
    }

    private static async Task<string?> GetFromCache(string cacheUrl, string key)
    {
        try
        {
            var response = await Client.GetAsync($"{cacheUrl}/{key}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            if (response.StatusCode == System.Net.HttpStatusCode.BadGateway)
            {
                UnavailableCaches.Add(cacheUrl);
            }
        }
        catch
        {
            UnavailableCaches.Add(cacheUrl);
        }

        return null;
    }

    private static async Task<string?> GetFromDb(string key)
    {
        try
        {
            var response = await Client.GetAsync($"{DbUrl}/{key}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
        catch
        {
            throw new Exception("Ошибка при запросе к базе данных.");
        }

        return null;
    }

    private static async Task WriteToCaches(string key, string value)
    {
        var tasks = CacheUrls
            .Where(url => !UnavailableCaches.Contains(url))
            .Select(async cacheUrl =>
            {
                try
                {
                    var content = new StringContent(value);
                    await Client.PutAsync($"{cacheUrl}/{key}", content);
                }
                catch
                {
                    UnavailableCaches.Add(cacheUrl);
                }
            });

        await Task.WhenAll(tasks);
    }

    public static async Task ElamArchivesMain(string[] args)
    {
        string input;
        while ((input = Console.ReadLine()) != null)
        {
            var key = input.Trim();

            if (CacheMap.TryGetValue(key, out var cachedValue))
            {
                Console.WriteLine(cachedValue);
            }
            else
            {
                var value = await GetFromCacheOrDb(key);
                if (value != null)
                {
                    CacheMap[key] = value;
                    Console.WriteLine(value);
                }
            }
        }
    }
}