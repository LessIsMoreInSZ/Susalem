import { request } from "@/utils/service"

/**获取所有表格数据*/
export function getAllUserRecipeDataApi() {
  return request({
    url: "/api/UserRecipe/GetAllUserRecipeData",
    method: "get"
  })
}

/**获取表格数据*/
export function getUserRecipeDataApi(params: any) {
  return request({
    url: "/api/UserRecipe/GetUserRecipeData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addUserRecipeApi(params: any) {
  return request({
    url: "/api/UserRecipe/AddUserRecipe",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updateUserRecipeApi(params: any) {
  return request({
    url: "/api/UserRecipe/UpdateUserRecipe",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deleteUserRecipeApi(params: any) {
  return request({
    url: "/api/UserRecipe/DeleteUserRecipe",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelUserRecipeApi(params: any) {
  return request({
    url: "/api/UserRecipe/BatchDelUserRecipe",
    method: "post",
    data: params
  })
}
