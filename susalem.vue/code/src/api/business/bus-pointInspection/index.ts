import { request } from "@/utils/service"
import * as GetAllRes from "../publicTypes/getAllRes"
/**获取所有表格数据-工序页面*/
export function getAllPointInspectionDataApi() {
  return request<GetAllRes.getAllResponseData>({
    url: "/api/PointInspection/GetAllPointInspectionData",
    method: "get"
  })
}

/**获取表格数据-分页-搜索*/
export function getPointInspectionDataApi(params: any) {
  return request({
    url: "/api/PointInspection/GetPointInspectionData",
    method: "post",
    data: params
  })
}
