import { request } from "@/utils/service"

/**获取所有表格数据*/
export function getAllProductionLineDataApi() {
  return request({
    url: "/api/ProductionLine/GetAllProductionLineData",
    method: "get"
  })
}

/**获取表格数据-分页-搜索*/
export function getProductionLineDataApi(params: any) {
  return request({
    url: "/api/ProductionLine/GetProductionLineData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addProductionLineApi(params: any) {
  return request({
    url: "/api/ProductionLine/AddProductionLine",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updateProductionLineApi(params: any) {
  return request({
    url: "/api/ProductionLine/UpdateProductionLine",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deleteProductionLineApi(params: any) {
  return request({
    url: "/api/ProductionLine/DeleteProductionLine",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelProductionLineApi(params: any) {
  return request({
    url: "/api/ProductionLine/BatchDelProductionLine",
    method: "post",
    data: params
  })
}
