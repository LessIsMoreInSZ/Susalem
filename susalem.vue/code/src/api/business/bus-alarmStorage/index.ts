import { request } from "@/utils/service"
import * as GetAllRes from "../publicTypes/getAllRes"
/**获取所有表格数据-工序页面*/
export function getAllAlarmDataApi() {
  return request<GetAllRes.getAllResponseData>({
    url: "/api/AlarmStorage/GetAllAlarmStorageData",
    method: "get"
  })
}

/**获取表格数据-分页-搜索*/
export function getAlarmDataApi(params: any) {
  return request({
    url: "/api/AlarmStorage/GetAlarmStorageData",
    method: "post",
    data: params
  })
}
