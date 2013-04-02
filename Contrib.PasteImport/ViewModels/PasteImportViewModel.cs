using System.ComponentModel.DataAnnotations;

namespace Contrib.PasteImport.ViewModels
{
    public class PasteImportViewModel
    {
        [Required]
        public string RecipeSteps { get; set; }
    }
}