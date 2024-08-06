import { request } from "/src/utils/service"

/**获取所有表格数据*/
export function getAllRecipeDataApi() {
  return request({
    url: "/api/Recipe/GetAllRecipeData",
    method: "get"
  })
}

/**获取表格数据*/
export function getRecipeDataApi(params: any) {
  return request({
    url: "/api/Recipe/GetRecipeData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addRecipeApi(params: any) {
  return request({
    url: "/api/Recipe/AddRecipe",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updateRecipeApi(params: any) {
  return request({
    url: "/api/Recipe/UpdateRecipe",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deleteRecipeApi(params: any) {
  return request({
    url: "/api/Recipe/DeleteRecipe",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelRecipeApi(params: any) {
  return request({
    url: "/api/Recipe/BatchDelRecipe",
    method: "post",
    data: params
  })
}
