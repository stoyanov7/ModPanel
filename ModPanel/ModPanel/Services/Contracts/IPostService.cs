namespace ModPanel.Services.Contracts
{
    using System.Collections.Generic;
    using Models.ViewModels;

    public interface IPostService
    {
        void Create(string title, string content, int userId);

        IEnumerable<PostListingViewModel> AllPost();
    }
}