using Microsoft.AspNetCore.Mvc;
using SharpIco;
using SharpIcoWeb.Model;
using SharpIcoWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.WebHost.UseUrls("http://*:5235");

builder.Services.AddScoped<IFileService, FileService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        opt => opt.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("http://localhost:5173/", "https://localhost:5173/api"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.MapPost("/api/uploadDownload", async ([FromForm] IFormFile file,[FromForm] string? sizes,IFileService fileService, ILogger<Program> logger) =>
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
        var sizesArray = sizes?.Split(',').Select(int.Parse).ToArray();
        if (sizesArray is { Length: > 0 })
            IcoGenerator.GenerateIcon(tempFilePath, outputPath, sizesArray);
        else
            IcoGenerator.GenerateIcon(tempFilePath, outputPath);

        // 读取到内存
        var memoryStream = await fileService.ReadFileToMemoryAsync(outputPath);
        
        // 删除临时文件
        fileService.DeleteFile(outputPath);
        
        logger.LogInformation($"{DateTime.UtcNow} 文件 {file.FileName} 转换成功");
        
        return Results.File(memoryStream, "image/x-icon");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, $"{DateTime.UtcNow} 处理文件时发生错误");
        return Results.Json(new ApiError { Message = ex.Message });
    }
}).DisableAntiforgery();


app.Run();

