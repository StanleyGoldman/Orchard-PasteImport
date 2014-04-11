using System;
using Orchard;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Recipes.Models;
using Orchard.Recipes.Services;

namespace Contrib.PasteImport.Services
{
    public interface ICustomRecipeManager : IDependency
    {
        string Execute(Recipe recipe);
    }

    public class CustomRecipeManager : ICustomRecipeManager
    {
        private readonly IRecipeStepQueue _recipeStepQueue;
        private readonly IRecipeJournal _recipeJournal;
        private readonly Lazy<IRecipeStepExecutor> _recipeStepExecutor;

        public CustomRecipeManager(IRecipeStepQueue recipeStepQueue, Lazy<IRecipeStepExecutor> recipeStepExecutor, IRecipeJournal recipeJournal)
        {
            _recipeStepQueue = recipeStepQueue;
            _recipeJournal = recipeJournal;
            _recipeStepExecutor = recipeStepExecutor;

            Logger = NullLogger.Instance;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }
        public ILogger Logger { get; set; }

        public string Execute(Recipe recipe)
        {
            if (recipe == null)
                return null;

            var executionId = Guid.NewGuid().ToString("n");
            _recipeJournal.ExecutionStart(executionId);

            foreach (var recipeStep in recipe.RecipeSteps)
            {
                _recipeStepQueue.Enqueue(executionId, recipeStep);
            }

            bool result;
            do
            {
                try {
                    result = _recipeStepExecutor.Value.ExecuteNextStep(executionId);
                }
                catch (Exception ex) {
                    Logger.Error("Error executing recipe step: " + executionId, ex);
                    result = false;
                }
            } while (result);

            return executionId;
        }
    }
}