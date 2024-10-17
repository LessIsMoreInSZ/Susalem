import { request } from "@/utils/service"

/**获取所有表格数据*/
export function getAllMaterialDataApi() {
  return request({
    url: "/api/Material/GetAllMaterialData",
    method: "get"
  })
}

/**获取表格数据*/
export function getMaterialDataApi(params: any) {
  return request({
    url: "/api/Material/GetMaterialData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addMaterialApi(params: any) {
  return request({
    url: "/api/Material/AddMaterial",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updateMaterialApi(params: any) {
  return request({
    url: "/api/Material/UpdateMaterial",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deleteMaterialApi(params: any) {
  return request({
    url: "/api/Material/DeleteMaterial",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelMaterialApi(params: any) {
  return request({
    url: "/api/Material/BatchDelMaterial",
    method: "post",
    data: params
  })
}
