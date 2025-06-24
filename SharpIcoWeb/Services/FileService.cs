namespace SharpIcoWeb.Services;

public class FileService : IFileService, IDisposable
{
    private readonly List<string> _tempFiles = new();

    public string GetTempDirectory()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), "SharpIcoTemp");
        Directory.CreateDirectory(tempDir);
        return tempDir;
    }
    
    public bool IsFileValid(IFormFile file)
    {
        var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return file.Length is > 0 and < 10 * 1024 * 1024 && // 10MB
               allowedExtensions.Contains(extension);
    }

    // 保存上传的文件
    public async Task<string> SaveUploadedFile(IFormFile file)
    {
        var tempDir = GetTempDirectory();
        var tempFilePath = Path.Combine(tempDir, $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}");
        
        await using var stream = new FileStream(tempFilePath, FileMode.Create);
        await file.CopyToAsync(stream);
        
        _tempFiles.Add(tempFilePath); // 记录待清理文件
        return tempFilePath;
    }
    
    // 安全读取文件到内存流
    public async Task<MemoryStream> ReadFileToMemoryAsync(string filePath)
    {
        var memoryStream = new MemoryStream();
        await using (var fileStream = File.OpenRead(filePath))
        {
            await fileStream.CopyToAsync(memoryStream);
        }
        memoryStream.Position = 0;
        return memoryStream;
    }

    public void DeleteFile(string tempFilePath)
    {
        if (File.Exists(tempFilePath))
            File.Delete(tempFilePath);
    }

    // 实现IDisposable接口自动清理
    public void Dispose()
    {
        foreach (var file in _tempFiles)
        {
            try
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            catch { /* 忽略删除异常 */ }
        }
        GC.SuppressFinalize(this);
    }
}