
namespace Business.Results;
public class ServiceResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; } = null;
}

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; set; } = default;
}
