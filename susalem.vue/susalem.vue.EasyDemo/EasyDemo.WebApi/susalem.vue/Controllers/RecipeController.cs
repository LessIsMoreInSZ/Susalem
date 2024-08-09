using Microsoft.AspNetCore.Mvc;
using susalem.vue.Data;
using susalem.vue.Helper;
using susalem.vue.IServices;
using susalem.vue.Models;
using susalem.vue.Services;
using System.Reflection.Metadata.Ecma335;

namespace susalem.vue.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController: ControllerBase
    {
        private readonly IRecipeService _recipeService;
        //public RecipeController(IRecipeService recipeService)
        //{
        //    this._recipeService = recipeService;
        //}

        [HttpPost("AddRecipe")]
        public void AddRecipe([FromBody] Recipe recipe)
        {
            _recipeService.AddRecipe(recipe) ;
            //AddRecipe
        }

        [HttpGet("GetAllRecipes")]
        public ApiResponseData GetAllRecipes()
        {

            //return Ok(new List<Recipe>()
            //{
            //    new Recipe { Id =1,RecipeName="Recipe1",Level=1,State = 1},
            //    new Recipe { Id =2,RecipeName="Recipe2",Level=1,State = 1},
            //    new Recipe { Id =3,RecipeName="Recipe3",Level=1,State = 1},
            //}) ;

            List<Recipe> models = new List<Recipe>()
            {
                new Recipe { Id =1,RecipeName="Recipe1",Level=1,State = 1},
                new Recipe { Id =2,RecipeName="Recipe2",Level=1,State = 1},
                new Recipe { Id =3,RecipeName="Recipe3",Level=1,State = 1}
            };

            return ResultHelper.Success(new ReturnData() { total = 3, dataList = models });
            //AddRecipe
        }

        // TODO 增删改
    }
}
