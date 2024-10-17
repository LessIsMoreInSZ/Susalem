import { request } from "@/utils/service"
import * as GetAllRes from "../publicTypes/getAllRes"

/**获取所有表格数据*/
export function getAllMesConfigDataApi() {
  return request<GetAllRes.getAllResponseData>({
    url: "/api/MesConfig/GetAllMesConfigData",
    method: "get"
  })
}

/**获取表格数据*/
export function getMesConfigDataApi(params: any) {
  return request({
    url: "/api/MesConfig/GetMesConfigData",
    method: "post",
    data: params
  })
}
/**新增表格数据 */
export function addMesConfigApi(params: any) {
  return request({
    url: "/api/MesConfig/AddMesConfig",
    method: "post",
    data: params
  })
}
/**修改表格数据 */
export function updateMesConfigApi(params: any) {
  return request({
    url: "/api/MesConfig/UpdateMesConfig",
    method: "post",
    data: params
  })
}
/**删除表格数据 */
export function deleteMesConfigApi(params: any) {
  return request({
    url: "/api/MesConfig/DeleteMesConfig",
    method: "post",
    data: params
  })
}
/**批量删除表格数据 */
export function batchDelMesConfigApi(params: any) {
  return request({
    url: "/api/MesConfig/BatchDelMesConfig",
    method: "post",
    data: params
  })
}
