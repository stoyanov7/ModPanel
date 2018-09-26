namespace ModPanel.Services.Contracts
{
    using Models.Enums;

    public interface IUserService
    {
        bool Create(string email, string password, PositionType position);
    }
}