using Microsoft.EntityFrameworkCore;
using susalem.vue.Data;
using susalem.vue.IServices;
using susalem.vue.Models;

namespace susalem.vue.Services
{
    public class RecipeService:IRecipeService
    {
        private UserDbContext _db;
        public RecipeService(UserDbContext db)
        {
            this._db = db;
        }

        public void AddRecipe(Recipe entity)
        {
             this._db.Add(entity);
        }

        public void DeleteRecipe(int id)
        {

        }

        public List<Recipe> GetAllRecipes()
        {
            throw new NotImplementedException();
        }

        public Recipe GetRecipeById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecipe(Recipe entity)
        {
            throw new NotImplementedException();
        }
    }
}
