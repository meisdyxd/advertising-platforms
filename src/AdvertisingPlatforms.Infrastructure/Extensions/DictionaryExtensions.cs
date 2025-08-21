using System.Collections.Concurrent;
using AdvertisingPlatforms.Infrastructure.PlatformsTree;

namespace AdvertisingPlatforms.Infrastructure.Extensions;

public static class DictionaryExtensions
{
    public static Node GetAndCreateIfNotExistNode(
        this ConcurrentDictionary<string, Node> dictionary, 
        string key)
    {
        if (!dictionary.TryGetValue(key, out var node))
        {
            node = new Node();
            dictionary[key] = node;
        }
        return node;
    }
}
