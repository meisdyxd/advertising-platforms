namespace AdvertisingPlatforms.Application.Interfaces;

public interface IPlatformsTree
{
    void AddElement(string advertisingPlatform, string[] locations);
    HashSet<string> GetElements(string location);
    void Clear();
}