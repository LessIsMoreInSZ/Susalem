import { request } from "@/utils/service"
changeProcessStatusApi

/**是否启用工序 */
export function changeProcessStatusApi(params: any) {
  return request({
    url: "/api/Process/ChangeProcessStatus",
    method: "post",
    data: params
  })
}
/**是否跳过工序 */
export function changeProcessSkippedApi(params: any) {
  return request({
    url: "/api/Process/SkipProcess",
    method: "post",
    data: params
  })
}

/**一键启用与停用工序 */
export function changeAllProcessStatusApi(params: any) {
  return request({
    url: "/api/Process/ChangeAllProcessStatus",
    method: "post",
    data: params
  })
}

/**获取所有表格数据*/
export function getAllProcessDataApi() {
  return request({
    url: "/api/Process/GetAllProcessData",
    method: "get"
  })
}

/**获取表格数据*/
export function getProcessDataApi(params: any) {
  return request({
    url: "/api/Process/GetProcessData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addProcessApi(params: any) {
  return request({
    url: "/api/Process/AddProcess",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updateProcessApi(params: any) {
  return request({
    url: "/api/Process/UpdateProcess",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deleteProcessApi(params: any) {
  return request({
    url: "/api/Process/DeleteProcess",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelProcessApi(params: any) {
  return request({
    url: "/api/Process/BatchDelProcess",
    method: "post",
    data: params
  })
}
