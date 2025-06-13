namespace Data.Results;
public class RepositoryResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; } = null;
}

public class RepositoryResult<T> : RepositoryResult
{
    public T? Data { get; set; } = default;
}
