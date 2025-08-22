using System.Collections.Concurrent;

namespace AdvertisingPlatforms.Infrastructure.PlatformsTree;

public class LRUCache<TKey, TValue> where TKey : notnull
{
    private readonly int _capacity;
    private readonly ConcurrentDictionary<TKey, LinkedListNode<CacheItem>> _cache;
    private readonly LinkedList<CacheItem> _list;
    private readonly object _lock = new();

    public LRUCache(int capacity = 100)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Invalid capacity");

        _capacity = capacity;
        _cache = new ConcurrentDictionary<TKey, LinkedListNode<CacheItem>>();
        _list = new LinkedList<CacheItem>();
    }

    public void Add(TKey key, TValue value)
    {
        lock (_lock)
        {
            if (_cache.TryGetValue(key, out var existingNode))
            {
                _list.Remove(existingNode);
                _list.AddFirst(existingNode);
                existingNode.Value.Value = value;
            }
            else
            {
                if (_cache.Count >= _capacity)
                {
                    var last = _list.Last;
                    if (last != null)
                    {
                        _cache.TryRemove(last.Value.Key, out _);
                        _list.RemoveLast();
                    }
                }

                var newNode = new LinkedListNode<CacheItem>(new CacheItem(key, value));
                _list.AddFirst(newNode);
                _cache[key] = newNode;
            }
        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        value = default;

        if (_cache.TryGetValue(key, out var node))
        {
            lock (_lock)
            {
                _list.Remove(node);
                _list.AddFirst(node);
            }

            value = node.Value.Value;
            return true;
        }

        return false;
    }

    internal class CacheItem
    {
        public TKey Key { get; }
        public TValue Value { get; set; }

        public CacheItem(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    public void Clear()
    {
        lock (_lock)
            _list.Clear();
        _cache.Clear();
    }

    public int Count => _cache.Count;

    public IReadOnlyList<TKey> GetKeysInOrder()
    {
        lock (_lock)
        {
            return [.. _list.Select(node => node.Key)];
        }
    }

    public bool ContainsKey(TKey key) => _cache.ContainsKey(key);

    public IReadOnlyList<TKey> GetAllKeys() => [.. _cache.Keys];

    public IReadOnlyDictionary<TKey, TValue> GetCurrentState()
    {
        lock (_lock)
        {
            return _cache.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Value.Value);
        }
    }
}