namespace AdvertisingPlatforms.Contracts.Dtos;

public sealed class FileDto
{
    public FileDto() { }
    
    public FileDto(MemoryStream stream, string filename)
    {
        Stream = stream;
        FileName = filename;
    }
    
    public MemoryStream Stream { get; }
    public string FileName { get; }
}