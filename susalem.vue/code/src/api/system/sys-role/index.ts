import { request } from "@/utils/service"

export function getRoleDataApi(parms: any) {
  return request({
    url: "/api/Role/GetRoleData",
    method: "post",
    data: parms
  })
}
// 新增角色
export function addRoleApi(params: any) {
  return request({
    url: "/api/Role/AddRole",
    method: "post",
    data: params
  })
}
// 修改角色
export function updateRoleDataApi(params: any) {
  return request({
    url: "/api/Role/UpdateRoleData",
    method: "post",
    data: params
  })
}
//删除角色
export function deleteRoleApi(params: any) {
  return request({
    url: "/api/Role/DeleteRole",
    method: "post",
    data: params
  })
}
// 批量删除角色
export function batchDelRoleApi(params: any) {
  return request({
    url: "/api/Role/BatchDelRole",
    method: "post",
    data: params
  })
}
// 更改角色状态或密码
export function changeRoleEnableApi(params: any) {
  return request({
    url: "/api/Role/ChangeRoleEnable",
    method: "post",
    data: params
  })
}
/** 更改角色状态或密码*/
export function updataRoleMenuScopeApi(params: any) {
  return request({
    url: "/api/Role/UpdataRoleMenuScope",
    method: "post",
    data: params
  })
}
// 新增-获取启用角色信息
export function getRoleListApi() {
  return request({
    url: "/api/Role/GetRoleList",
    method: "get"
  })
}
// 编辑-获取角色信息
export function getRoleByIdApi(userId: any) {
  return request({
    url: "/api/Role/GetRoleListAndsingle",
    method: "get",
    params: { id: userId }
  })
}
