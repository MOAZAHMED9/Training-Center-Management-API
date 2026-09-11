namespace Training_Center_Management_API.Services.Auditing
{
    public interface ICurrentUserService
    {
        int? UserId { get; }

        string? UserName { get; }

        bool IsAuthenticated { get; }
    }
}
