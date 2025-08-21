using System.Collections.Concurrent;
using AdvertisingPlatforms.Infrastructure.Extensions;

namespace AdvertisingPlatforms.Infrastructure.PlatformsTree;

public class Node
{
    private readonly object _lock = new();
    public HashSet<string> Platforms { get; set; } = [];
    public ConcurrentDictionary<string, Node> Childrens { get; set; } = [];

    public void AddElement(
        string advertisingPlatform,
        string[] segments,
        int index)
    {
        lock (_lock)
        {
            var current = this;
            for (int i = index; i < segments.Length; i++)
                current = current.Childrens.GetAndCreateIfNotExistNode(segments[i]);

            current.Platforms.Add(advertisingPlatform);
        }
    }

    public void GetElements(
        string[] segments, 
        HashSet<string> result,
        int index)
    {
        lock (_lock)
        {
            var current = this;
            foreach (var item in Platforms)
                result.Add(item);

            for (int i = index; i < segments.Length; i++)
            {
                if (!current.Childrens.TryGetValue(segments[i], out current))
                    break;

                foreach (var platform in current.Platforms)
                    result.Add(platform);
            }
        }
    }
}