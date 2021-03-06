﻿namespace ModPanel.Services.Contracts
{
    using System.Collections.Generic;
    using Models.BindingModels;
    using Models.ViewModels;

    public interface IPostService
    {
        void Create(string title, string content, int userId);

        IEnumerable<PostListingViewModel> AllPost();

        IEnumerable<HomePostViewModel> AllWithDetails(string search = null);

        PostBindingModel GetById(int id);

        void Update(int id, string title, string content);

        string Delete(int id);
    }
}