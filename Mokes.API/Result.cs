namespace Mokes.API;

public class Result<T>
{
    public T? Value { get; set; }
    public string? Error { get; set; }
    public int? StatusCode { get; set; }
    public bool IsSuccess => Error == null;
}
