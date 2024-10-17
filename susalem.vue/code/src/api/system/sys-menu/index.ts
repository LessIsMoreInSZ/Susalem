import { request } from "@/utils/service"

// 获取菜单数据
export function getMenuDataApi(params: any) {
  return request({
    url: "/api/Menu/GetMenuData",
    method: "post",
    data: params
  })
}
// 新增菜单
export function addMenuApi(params: any) {
  return request({
    url: "/api/Menu/AddMenu",
    method: "post",
    data: params
  })
}
// 修改菜单
export function updateMenuDataApi(params: any) {
  return request({
    url: "/api/Menu/UpdateMenuData",
    method: "post",
    data: params
  })
}
//删除菜单
export function deleteMenuApi(params: any) {
  return request({
    url: "/api/menu/DeleteMenu",
    method: "post",
    data: params
  })
}
// 批量删除菜单
export function batchDelMenuApi(params: any) {
  return request({
    url: "/api/menu/BatchDelMenu",
    method: "post",
    data: params
  })
}
// 更改菜单状态
export function changeMenuStatusApi(params: any) {
  return request({
    url: "/api/menu/ChangeMenuStatus",
    method: "post",
    data: params
  })
}

export function getMenuTreeSelectApi(roleId?: any) {
  return request({
    url: "/api/menu/GetMenuTreeSelect",
    method: "get",
    params: { roleId: roleId }
  })
}

export function getAllMenuTreeSelectApi() {
  return request({
    url: "/api/menu/GetAllMenuTreeSelect",
    method: "get"
  })
}
