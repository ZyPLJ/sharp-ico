namespace SharpIcoWeb.Model;

public class ApiError
{
    public int StatusCode { get; set; } = 200;
    public bool Successful { get; set; } = true;
    public string? Message { get; set; }
    public object? Data { get; set; }
}