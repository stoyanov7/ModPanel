namespace ModPanel.Models.ViewModels
{
    using System;

    public class HomePostViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}