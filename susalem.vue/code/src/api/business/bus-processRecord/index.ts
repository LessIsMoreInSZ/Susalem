import { request } from "@/utils/service"

/**获取表格数据*/
export function getProcessRecordDataApi(params: any) {
  return request({
    url: "/api/ProcessRecord/GetProcessRecordData",
    method: "post",
    data: params
  })
}

/**强制报废SN*/
export function updateScrapDataApi(params: any) {
  return request({
    url: "/api/ProcessRecord/UpdateScrapDataApi",
    method: "post",
    data: params
  })
}

/**sn解绑*/
export function deleteUnbindDataApi(params: any) {
  return request({
    url: "/api/ProcessRecord/DeleteDataToUnBind",
    method: "post",
    data: params
  })
}
/**获取导出表格Json数据*/
export function exportExcelDataApi(params: any) {
  return request({
    url: "/api/ProcessRecord/ExportProcessRecordExcel",
    method: "post",
    data: params
  })
}

/**更新*/
export function updateProcessRecordCacheDataApi(params: any) {
  return request({
    url: "/api/ProcessRecord/UpdateProcessRecordCacheDataApi",
    method: "post",
    data: params
  })
}
