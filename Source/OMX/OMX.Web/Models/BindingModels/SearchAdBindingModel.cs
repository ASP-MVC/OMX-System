namespace OMX.Web.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class SearchAdBindingModel
    {
        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string AdSubstring { get; set; }
    }
}