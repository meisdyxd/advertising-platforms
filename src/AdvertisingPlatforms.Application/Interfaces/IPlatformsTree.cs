namespace AdvertisingPlatforms.Application.Interfaces;

public interface IPlatformTree
{
    void AddElement(string advertisingPlatform, string[] locations);
    HashSet<string> GetElements(string location);
    void Clear();
}