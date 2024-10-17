import { request } from "@/utils/service"
import * as GetAllRes from "../publicTypes/getAllRes"
/**获取所有表格数据-工序页面*/
export function getAllAlarmConfigDataApi() {
  return request<GetAllRes.getAllResponseData>({
    url: "/api/AlarmConfig/GetAllAlarmConfigData",
    method: "get"
  })
}

/**获取表格数据-分页-搜索*/
export function getAlarmConfigDataApi(params: any) {
  return request({
    url: "/api/AlarmConfig/GetAlarmConfigData",
    method: "post",
    data: params
  })
}
/**新增*/
export function AddAlarmConfigApi(params: any) {
  return request({
    url: "/api/AlarmConfig/AddAlarmConfig",
    method: "post",
    data: params
  })
}
// 批量添加
export function BatchAddAlarmConfigApi(params: any) {
  return request({
    url: "/api/AlarmConfig/BatchAddAlarmConfig",
    method: "post",
    data: params
  })
}
// 修改
export function UpdateAlarmConfigApi(params: any) {
  return request({
    url: "/api/AlarmConfig/UpdateAlarmConfig",
    method: "post",
    data: params
  })
}
// 删除
export function DeleteAlarmConfigApi(params: any) {
  return request({
    url: "/api/AlarmConfig/DeleteAlarmConfig",
    method: "post",
    data: params
  })
}
// 批量删除
export function BatchDelAlarmConfigApi(params: any) {
  return request({
    url: "/api/AlarmConfig/BatchDelAlarmConfig",
    method: "post",
    data: params
  })
}

