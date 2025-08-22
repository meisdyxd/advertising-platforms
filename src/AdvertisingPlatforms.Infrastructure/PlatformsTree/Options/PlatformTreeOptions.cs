using System.ComponentModel.DataAnnotations;

namespace AdvertisingPlatforms.Infrastructure.PlatformsTree.Options;

public class PlatformTreeOptions
{
    public const string SectionName = "PlatformTree";

    [Required]
    public int CacheCapacity { get; set; }
}
