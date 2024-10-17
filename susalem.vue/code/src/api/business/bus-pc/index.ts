import { request } from "@/utils/service"
import * as GetAllRes from "../publicTypes/getAllRes"

/**获取所有表格数据*/
export function getAllPcDataApi() {
  return request<GetAllRes.getAllResponseData>({
    url: "/api/Pc/GetAllPcData",
    method: "get"
  })
}

/**获取表格数据*/
export function getPcDataApi(params: any) {
  return request({
    url: "/api/Pc/GetPcData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addPcApi(params: any) {
  return request({
    url: "/api/Pc/AddPc",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updatePcApi(params: any) {
  return request({
    url: "/api/Pc/UpdatePc",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deletePcApi(params: any) {
  return request({
    url: "/api/Pc/DeletePc",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelPcApi(params: any) {
  return request({
    url: "/api/Pc/BatchDelPc",
    method: "post",
    data: params
  })
}
