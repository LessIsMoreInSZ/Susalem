import { request } from "@/utils/service"

/**获取所有表格数据*/
export function getAllRecipeTypeDataApi() {
  return request({
    url: "/api/RecipeType/GetAllRecipeTypeData",
    method: "get"
  })
}

/**获取表格数据*/
export function getRecipeTypeDataApi(params: any) {
  return request({
    url: "/api/RecipeType/GetRecipeTypeData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addRecipeTypeApi(params: any) {
  return request({
    url: "/api/RecipeType/AddRecipeType",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updateRecipeTypeApi(params: any) {
  return request({
    url: "/api/RecipeType/UpdateRecipeType",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deleteRecipeTypeApi(params: any) {
  return request({
    url: "/api/RecipeType/DeleteRecipeType",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelRecipeTypeApi(params: any) {
  return request({
    url: "/api/RecipeType/BatchDelRecipeType",
    method: "post",
    data: params
  })
}
