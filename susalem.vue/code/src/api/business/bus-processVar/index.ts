import { request } from "@/utils/service"
import * as GetAllRes from "../publicTypes/getAllRes"

/**获取所有表格数据*/
export function getAllProcessVarDataApi() {
  return request<GetAllRes.getAllResponseData>({
    url: "/api/ProcessVar/GetAllProcessVarData",
    method: "get"
  })
}
/**获取所有不重复的工序编号 */
export function getProcessIdsDataApi() {
  return request({
    url: "/api/ProcessVar/GetProcessIds",
    method: "get"
  })
}

/**获取表格数据*/
export function getProcessVarDataApi(params: any) {
  return request({
    url: "/api/ProcessVar/GetProcessVarData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addProcessVarApi(params: any) {
  return request({
    url: "/api/ProcessVar/AddProcessVar",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updateProcessVarApi(params: any) {
  return request({
    url: "/api/ProcessVar/UpdateProcessVar",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deleteProcessVarApi(params: any) {
  return request({
    url: "/api/ProcessVar/DeleteProcessVar",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelProcessVarApi(params: any) {
  return request({
    url: "/api/ProcessVar/BatchDelProcessVar",
    method: "post",
    data: params
  })
}

/**批量复制工序变量 */
export function BatchCopyProcessVarApi(params: any) {
  return request({
    url: "/api/ProcessVar/BatchCopyProcessVar",
    method: "post",
    data: params
  })
}
