namespace SharpIcoWeb.Services;

public interface IFileService
{
    // 检查文件是否有效
    bool IsFileValid(IFormFile file);
    // 保存上传的文件
    Task<string> SaveUploadedFile(IFormFile file);
    // 创建临时目录
    string GetTempDirectory();
    // 读取文件到内存流
    Task<MemoryStream> ReadFileToMemoryAsync(string filePath);
    // 删除临时文件
    void DeleteFile(string tempFilePath);
    // 将多个文件打包为ZIP
    Task<MemoryStream> CreateZipFromFilesAsync(Dictionary<string, string> filePaths);
    // 在wwwroot目录创建生成后的文件目录
    string GetRootDirectory();
}