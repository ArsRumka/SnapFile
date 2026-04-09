namespace SnapFile.Application.Services.Interfaces
{
    public interface IEmailService
    {
        public Task SendCode(string email, string code);
    }
}
