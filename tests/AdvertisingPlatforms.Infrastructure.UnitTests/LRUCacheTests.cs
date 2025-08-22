using AdvertisingPlatforms.Infrastructure.PlatformsTree;

namespace AdvertisingPlatforms.Infrastructure.UnitTests;

public class LRUCacheTests
{
    [Fact]
    public void Constructor_InvalidCapacity_ThrowsArgumentOutOfRangeException()
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new LRUCache<string, string>(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new LRUCache<string, string>(-1));
    }

    [Fact]
    public void Constructor_ValidCapacity_CreatesEmptyCache()
    {
        // Act
        var cache = new LRUCache<string, string>(5);

        // Assert
        Assert.Equal(0, cache.Count);
    }

    [Fact]
    public void Add_NewItem_AddsToCache()
    {
        // Arrange
        var cache = new LRUCache<string, string>(5);

        // Act
        cache.Add("key1", "value1");

        // Assert
        Assert.True(cache.TryGetValue("key1", out var value));
        Assert.Equal("value1", value);
        Assert.Equal(1, cache.Count);
        Assert.True(cache.ContainsKey("key1"));
    }

    [Fact]
    public void Add_ExistingItem_UpdatesValueAndMovesToFront()
    {
        // Arrange
        var cache = new LRUCache<string, string>(3);
        cache.Add("key1", "value1");
        cache.Add("key2", "value2");
        cache.Add("key3", "value3");

        // Act
        cache.Add("key1", "new_value1");

        // Assert
        Assert.True(cache.TryGetValue("key1", out var value));
        Assert.Equal("new_value1", value);

        var keysInOrder = cache.GetKeysInOrder();
        Assert.Equal("key1", keysInOrder.First());
    }

    [Fact]
    public void Add_ExceedsCapacity_RemovesLeastRecentlyUsed()
    {
        // Arrange
        var cache = new LRUCache<string, string>(3);
        cache.Add("key1", "value1");
        cache.Add("key2", "value2");
        cache.Add("key3", "value3");

        cache.TryGetValue("key1", out _);

        // Act
        cache.Add("key4", "value4");

        // Assert
        Assert.False(cache.ContainsKey("key2")); 
        Assert.True(cache.ContainsKey("key1"));  
        Assert.True(cache.ContainsKey("key3"));  
        Assert.True(cache.ContainsKey("key4")); 
        Assert.Equal(3, cache.Count);
    }

    [Fact]
    public void TryGetValue_ExistingItem_ReturnsTrueAndMovesToFront()
    {
        // Arrange
        var cache = new LRUCache<string, string>(3);
        cache.Add("key1", "value1");
        cache.Add("key2", "value2");
        cache.Add("key3", "value3");

        // Act
        var result = cache.TryGetValue("key2", out var value);

        // Assert
        Assert.True(result);
        Assert.Equal("value2", value);

        var keysInOrder = cache.GetKeysInOrder();
        Assert.Equal("key2", keysInOrder.First());
    }

    [Fact]
    public void TryGetValue_NonExistentItem_ReturnsFalse()
    {
        // Arrange
        var cache = new LRUCache<string, string>(3);
        cache.Add("key1", "value1");

        // Act
        var result = cache.TryGetValue("key2", out var value);

        // Assert
        Assert.False(result);
        Assert.Null(value);
    }

    [Fact]
    public void Clear_RemovesAllItems()
    {
        // Arrange
        var cache = new LRUCache<string, string>(3);
        cache.Add("key1", "value1");
        cache.Add("key2", "value2");

        // Act
        cache.Clear();

        // Assert
        Assert.Equal(0, cache.Count);
        Assert.False(cache.ContainsKey("key1"));
        Assert.False(cache.ContainsKey("key2"));
    }

    [Fact]
    public void LRU_Behavior_CorrectlyMaintainsOrder()
    {
        // Arrange
        var cache = new LRUCache<string, string>(3);

        // Act 
        cache.Add("key1", "value1");
        cache.Add("key2", "value2");
        cache.Add("key3", "value3");

        cache.TryGetValue("key1", out _);
        cache.Add("key4", "value4");

        // Assert
        Assert.False(cache.ContainsKey("key2"));
        Assert.True(cache.ContainsKey("key1")); 
        Assert.True(cache.ContainsKey("key3")); 
        Assert.True(cache.ContainsKey("key4"));  

        var keysInOrder = cache.GetKeysInOrder();
        Assert.Equal(new[] { "key4", "key1", "key3" }, keysInOrder);
    }
}
