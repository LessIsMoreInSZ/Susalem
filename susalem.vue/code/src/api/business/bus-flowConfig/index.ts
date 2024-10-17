import { request } from "@/utils/service"
import * as GetAllRes from "../publicTypes/getAllRes"
/**获取所有表格数据*/
export function getAllFlowConfigDataApi() {
  return request<GetAllRes.getAllResponseData>({
    url: "/api/FlowConfig/GetAllFlowConfigData",
    method: "get"
  })
}

/**获取表格数据*/
export function getFlowConfigDataApi(params: any) {
  return request({
    url: "/api/FlowConfig/GetFlowConfigData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addFlowConfigApi(params: any) {
  return request({
    url: "/api/FlowConfig/AddFlowConfig",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updateFlowConfigApi(params: any) {
  return request({
    url: "/api/FlowConfig/UpdateFlowConfig",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deleteFlowConfigApi(params: any) {
  return request({
    url: "/api/FlowConfig/DeleteFlowConfig",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelFlowConfigApi(params: any) {
  return request({
    url: "/api/FlowConfig/BatchDelFlowConfig",
    method: "post",
    data: params
  })
}
