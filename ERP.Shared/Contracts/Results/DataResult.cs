namespace ERP.Shared.Contracts.Results;

public class DataResult<T> where T : class // Ensure T is a reference type
{
	public bool IsSuccess { get; set; }
	public string Message { get; set; } = string.Empty;
	public T? Data { get; set; }
}