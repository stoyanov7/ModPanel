namespace ModPanel.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreatePostBindingModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}