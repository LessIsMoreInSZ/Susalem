import { request } from "@/utils/service"

/**获取所有表格数据-产线页面*/
export function getAllWorkShopDataApi() {
  return request({
    url: "/api/WorkShop/GetAllWorkShopData",
    method: "get"
  })
}

/**获取表格数据-分页-搜索*/
export function getWorkShopDataApi(params: any) {
  return request({
    url: "/api/WorkShop/GetWorkShopData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addWorkShopApi(params: any) {
  return request({
    url: "/api/WorkShop/AddWorkShop",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updateWorkShopApi(params: any) {
  return request({
    url: "/api/WorkShop/UpdateWorkShop",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deleteWorkShopApi(params: any) {
  return request({
    url: "/api/WorkShop/DeleteWorkShop",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelWorkShopApi(params: any) {
  return request({
    url: "/api/WorkShop/BatchDelWorkShop",
    method: "post",
    data: params
  })
}
