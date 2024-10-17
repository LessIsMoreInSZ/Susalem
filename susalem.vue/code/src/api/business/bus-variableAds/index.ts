import { request } from "@/utils/service"
import * as GetAllRes from "../publicTypes/getAllRes"

/**获取所有表格数据*/
export function getAllVariableAdsDataApi() {
  return request<GetAllRes.getAllResponseData>({
    url: "/api/VariableAds/GetAllVariableAdsData",
    method: "get"
  })
}

/**获取表格数据*/
export function getVariableAdsDataApi(params: any) {
  return request({
    url: "/api/VariableAds/GetVariableAdsData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addVariableAdsApi(params: any) {
  return request({
    url: "/api/VariableAds/AddVariableAds",
    method: "post",
    data: params
  })
}
/**excel批量添加数据 */
export function batchAddVariableAdsApi(params: any) {
  return request({
    url: "/api/VariableAds/BatchAddVariableAds",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updateVariableAdsApi(params: any) {
  return request({
    url: "/api/VariableAds/UpdateVariableAds",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deleteVariableAdsApi(params: any) {
  return request({
    url: "/api/VariableAds/DeleteVariableAds",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelVariableAdsApi(params: any) {
  return request({
    url: "/api/VariableAds/BatchDelVariableAds",
    method: "post",
    data: params
  })
}
