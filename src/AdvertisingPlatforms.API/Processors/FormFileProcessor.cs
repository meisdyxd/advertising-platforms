using AdvertisingPlatforms.Contracts.Dtos;

namespace AdvertisingPlatforms.API.Processors;

public class FormFileProcessor: IAsyncDisposable
{
    private FileDto  _file = new();

    public FileDto Process(IFormFile file)
    {
        var ms = new MemoryStream();
        file.CopyTo(ms);
        ms.Position = 0;
        _file = new FileDto(ms, file.FileName);
        return _file;
    }
    
    public async ValueTask DisposeAsync()
    {
        await _file.Stream.DisposeAsync();
    }
}