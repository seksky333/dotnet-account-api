namespace AccountAPI.Network.Requests;

public record UserRoleRequest
{
    public required string UserId { get; set; }
    public required string RoleName { get; set; }
}