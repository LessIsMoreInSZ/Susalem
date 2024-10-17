import { request } from "@/utils/service"

/**获取*/
export function getRecipeConfigByRecipeIdApi(value: any) {
  return request({
    url: "/api/RecipeConfig/GetRecipeConfigByRecipeId",
    method: "get",
    params: { recipeId: value }
  })
}

/**获取所有表格数据*/
export function getAllRecipeConfigDataApi() {
  return request({
    url: "/api/RecipeConfig/GetAllRecipeConfigData",
    method: "get"
  })
}

/**获取表格数据*/
export function getRecipeConfigDataApi(params: any) {
  return request({
    url: "/api/RecipeConfig/GetRecipeConfigData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addRecipeConfigApi(params: any) {
  return request({
    url: "/api/RecipeConfig/AddRecipeConfig",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updateRecipeConfigApi(params: any) {
  return request({
    url: "/api/RecipeConfig/UpdateRecipeConfig",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deleteRecipeConfigApi(params: any) {
  return request({
    url: "/api/RecipeConfig/DeleteRecipeConfig",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelRecipeConfigApi(params: any) {
  return request({
    url: "/api/RecipeConfig/BatchDelRecipeConfig",
    method: "post",
    data: params
  })
}
