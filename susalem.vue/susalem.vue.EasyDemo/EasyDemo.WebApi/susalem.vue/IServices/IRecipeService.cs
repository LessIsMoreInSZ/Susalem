using Microsoft.Data.SqlClient;
using susalem.vue.Models;

namespace susalem.vue.IServices
{
    public interface IRecipeService
    {
        List<Recipe> GetAllRecipes();

        Recipe GetRecipeById(int id);

        void AddRecipe(Recipe entity);

        void UpdateRecipe(Recipe entity);

        void DeleteRecipe(int id);


    }
}
