namespace ERP.Shared.Contracts.DTOs.BaseDTOs.Current.Requests;

public abstract class UpdateCurrentRequest
{
	public string Code { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Phone { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
}
