import { request } from "@/utils/service"

/**获取所有表格数据*/
export function getAllPartDataApi() {
  return request({
    url: "/api/Part/GetAllPartData",
    method: "get"
  })
}

/**获取表格数据*/
export function getPartDataApi(params: any) {
  return request({
    url: "/api/Part/GetPartData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addPartApi(params: any) {
  return request({
    url: "/api/Part/AddPart",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updatePartApi(params: any) {
  return request({
    url: "/api/Part/UpdatePart",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deletePartApi(params: any) {
  return request({
    url: "/api/Part/DeletePart",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelPartApi(params: any) {
  return request({
    url: "/api/Part/BatchDelPart",
    method: "post",
    data: params
  })
}
