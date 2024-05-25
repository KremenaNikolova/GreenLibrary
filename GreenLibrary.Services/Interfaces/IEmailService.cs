namespace GreenLibrary.Services.Interfaces
{
    using GreenLibrary.Services.Dtos.Email;

    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}
