import { request } from "@/utils/service"
import * as GetAllRes from "../publicTypes/getAllRes"
/**获取所有表格数据-工序页面*/
export function getAllWorkOrderDataApi() {
  return request<GetAllRes.getAllResponseData>({
    url: "/api/WorkOrder/GetAllWorkOrderData",
    method: "get"
  })
}

/**获取表格数据-分页-搜索*/
export function getWorkOrderDataApi(params: any) {
  return request({
    url: "/api/WorkOrder/GetWorkOrderData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addWorkOrderApi(params: any) {
  return request({
    url: "/api/WorkOrder/AddWorkOrder",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updateWorkOrderApi(params: any) {
  return request({
    url: "/api/WorkOrder/UpdateWorkOrder",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deleteWorkOrderApi(params: any) {
  return request({
    url: "/api/WorkOrder/DeleteWorkOrder",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelWorkOrderApi(params: any) {
  return request({
    url: "/api/WorkOrder/BatchDelWorkOrder",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function inputString(params: any) {
  return request({
    url: "/api/WorkOrder/InputString",
    method: "get",
    params: {
      input:params
    }
  })
}
