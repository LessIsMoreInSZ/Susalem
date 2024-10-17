import { request } from "@/utils/service"

/**获取表格数据*/
export function getPartDetailsDataApi(params: any) {
  return request({
    url: "/api/PartDetails/GetPartDetailsData",
    method: "post",
    data: params
  })
}
