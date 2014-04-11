using System.ComponentModel.DataAnnotations;
using Orchard.Recipes.Models;

namespace Contrib.PasteImport.ViewModels
{
    public class PasteImportViewModel
    {
        [Required]
        public string RecipeSteps { get; set; }

        public RecipeJournal RecipeJournal { get; set; }
    }
}