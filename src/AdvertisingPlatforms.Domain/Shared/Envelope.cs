namespace AdvertisingPlatforms.Domain.Shared;

public class Envelope
{
    public object? Result { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];
    public DateTime DateTime { get; set; }
}
