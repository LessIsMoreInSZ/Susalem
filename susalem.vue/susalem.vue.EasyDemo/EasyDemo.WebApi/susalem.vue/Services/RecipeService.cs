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
             _db.Add(entity);
        }

        public void DeleteRecipe(int id)
        {

        }

        public List<Recipe> GetAllRecipes()
        {
            return _db.Recipes.Select(x => x).ToList();
        }

        public Recipe GetRecipeById(int id)
        {
            return _db.Recipes.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdateRecipe(Recipe entity)
        {
            _db.Update(entity);
        }
    }
}
