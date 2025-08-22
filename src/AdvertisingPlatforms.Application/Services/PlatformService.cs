using AdvertisingPlatforms.Application.Interfaces;

namespace AdvertisingPlatforms.Application.Services;

public class PlatformService
{
    private readonly IPlatformTree _platformsTree;

    public PlatformService(IPlatformTree platformsTree)
    {
        _platformsTree = platformsTree;
    }

    public HashSet<string> GetElements(string location)
    {
        return _platformsTree.GetElements(location);
    }

    public void UploadPlatforms(StreamReader reader)
    {
        _platformsTree.Clear();
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (line == null)
                continue;
            var splittedString = line.Split(':');
            if (splittedString.Length != 2)
                continue;
            var locations = splittedString[1].Split(',');
            if (locations.Length < 1)
                continue;
            _platformsTree.AddElement(splittedString[0], locations);
        }
    }

}