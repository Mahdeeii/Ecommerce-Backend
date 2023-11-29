using Final.Project.API.Responses;

namespace Final.Project.API;
public interface IMailingService
{
    Task<UserManagerResponse> SendEmailAsync(string email, string subject, string body);
}
