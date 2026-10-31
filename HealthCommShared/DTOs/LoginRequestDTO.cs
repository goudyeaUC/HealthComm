namespace HealthCommShared.DTOs
{
	public class LoginRequestDTO
	{
		public string Email { get; set; } = null!;
		public string GivenPassword { get; set; } = null!;
	}
}