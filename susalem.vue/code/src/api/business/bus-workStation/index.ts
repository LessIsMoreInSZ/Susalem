import { request } from "@/utils/service"
import * as GetAllRes from "../publicTypes/getAllRes"
/**获取所有表格数据-工序页面*/
export function getAllWorkStationDataApi() {
  return request<GetAllRes.getAllResponseData>({
    url: "/api/WorkStation/GetAllWorkStationData",
    method: "get"
  })
}

/**获取表格数据-分页-搜索*/
export function getWorkStationDataApi(params: any) {
  return request({
    url: "/api/WorkStation/GetWorkStationData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addWorkStationApi(params: any) {
  return request({
    url: "/api/WorkStation/AddWorkStation",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updateWorkStationApi(params: any) {
  return request({
    url: "/api/WorkStation/UpdateWorkStation",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deleteWorkStationApi(params: any) {
  return request({
    url: "/api/WorkStation/DeleteWorkStation",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelWorkStationApi(params: any) {
  return request({
    url: "/api/WorkStation/BatchDelWorkStation",
    method: "post",
    data: params
  })
}
