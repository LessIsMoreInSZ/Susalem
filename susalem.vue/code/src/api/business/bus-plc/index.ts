import { request } from "@/utils/service"
import * as GetAllRes from "../publicTypes/getAllRes"

/**获取所有表格数据*/
export function getAllPlcDataApi() {
  return request<GetAllRes.getAllResponseData>({
    url: "/api/Plc/GetAllPlcData",
    method: "get"
  })
}

/**获取表格数据*/
export function getPlcDataApi(params: any) {
  return request({
    url: "/api/Plc/GetPlcData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addPlcApi(params: any) {
  return request({
    url: "/api/Plc/AddPlc",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updatePlcApi(params: any) {
  return request({
    url: "/api/Plc/UpdatePlc",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deletePlcApi(params: any) {
  return request({
    url: "/api/Plc/DeletePlc",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelPlcApi(params: any) {
  return request({
    url: "/api/Plc/BatchDelPlc",
    method: "post",
    data: params
  })
}
