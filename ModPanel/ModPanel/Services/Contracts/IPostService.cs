namespace ModPanel.Services.Contracts
{
    public interface IPostService
    {
        void Create(string title, string content, int userId);
    }
}