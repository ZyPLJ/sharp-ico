using Microsoft.AspNetCore.Mvc;
using SharpIco;
using SharpIcoWeb.Model;
using SharpIcoWeb.Services;

namespace SharpIcoWeb;

public static class IcoEndpoints
{
    public static void MapIcoEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/uploadDownload");

        group.MapPost("/", UploadDownload)
            .DisableAntiforgery();

        // 可以添加其他相关端点
        group.MapPost("/sizes", UploadDownloadSizes)
            .DisableAntiforgery();
    }

    private static async Task<IResult> UploadDownload(
        [FromForm] IFormFile file,
        [FromForm] string? sizes,
        IFileService fileService,
        ILogger<Program> logger)
    {
        try
        {
            // 验证输入
            if (!fileService.IsFileValid(file))
            {
                return Results.Json(new ApiError { Message = "请上传有效文件,文件大小不能超过10MB" });
            }

            // 保存临时文件
            var tempFilePath = await fileService.SaveUploadedFile(file);

            // 生成输出路径
            var outputPath = Path.Combine(fileService.GetTempDirectory(), $"{Guid.NewGuid()}.ico");

            // 执行转换 处理大小参数
            var sizesArray = string.IsNullOrEmpty(sizes) ? null : sizes.Split(',').Select(int.Parse).ToArray();
            if (sizesArray is { Length: > 0 })
                IcoGenerator.GenerateIcon(tempFilePath, outputPath, sizesArray);
            else
                IcoGenerator.GenerateIcon(tempFilePath, outputPath);

            // 读取到内存
            var memoryStream = await fileService.ReadFileToMemoryAsync(outputPath);

            // 删除临时文件
            fileService.DeleteFile(outputPath);

            logger.LogInformation($"{DateTime.Now} 文件 {file.FileName} 转换成功");

            return Results.File(memoryStream, "image/x-icon");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{DateTime.Now} 处理文件时发生错误");
            return Results.Json(new ApiError { Message = ex.Message });
        }
    }

    private static async Task<IResult> UploadDownloadSizes(
        [FromForm] IFormFile file,
        [FromForm] string? sizes,
        IFileService fileService,
        ILogger<Program> logger)
    {
        try
        {
            // 验证输入
            if (!fileService.IsFileValid(file))
            {
                return Results.Json(new ApiError { Message = "请上传有效文件,文件大小不能超过10MB" });
            }

            // 保存临时文件
            var tempFilePath = await fileService.SaveUploadedFile(file);

            // 执行转换 处理大小参数
            var sizesArray = string.IsNullOrEmpty(sizes) ? null : sizes.Split(',').Select(int.Parse).ToArray();
            // 为每个尺寸生成ICO文件
            var filesToZip = new Dictionary<string, string>();

            if (sizesArray is { Length: > 0 })
            {
                foreach (var size in sizesArray)
                {
                    var fileName = $"icon{size}.ico";
                    var outputPathSize = Path.Combine(fileService.GetTempDirectory(), fileName);
                    IcoGenerator.GenerateIcon(tempFilePath, outputPathSize, [size]);
                    filesToZip.Add(fileName, outputPathSize);
                }
            }
            else
            {
                var outputPath = Path.Combine(fileService.GetTempDirectory(), "icon.ico");
                IcoGenerator.GenerateIcon(tempFilePath, outputPath);
                filesToZip.Add("icon.ico", outputPath);
            }

            // 创建ZIP文件
            var zipStream = await fileService.CreateZipFromFilesAsync(filesToZip);

            // 删除原始临时文件
            fileService.DeleteFile(tempFilePath);

            var count = sizesArray is { Length: > 0 } ? sizesArray.Length : 1;
            logger.LogInformation($"{DateTime.Now} 文件 {file.FileName} 转换成功，生成 {count} 个尺寸的ICO文件");

            return Results.File(zipStream, "application/zip", $"{DateTime.Now:yyyyMMddHHmmss}icons.zip");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{DateTime.Now} 处理文件时发生错误");
            return Results.Json(new ApiError { Message = ex.Message });
        }
    }
}