import { request } from "@/utils/service"
import type * as User from "./types/sys-user"

/**头像上传 */
export function uploadImageApi(params: any) {
  return request({
    url: "/api/Users/FileupLoad",
    method: "post",
    data: params,
    headers: {
      "Content-Type": "multipart/form-data"
    }
  })
}
/*更新个人信息 */
export function updatePersonanlInfoApi(params: any) {
  return request({
    url: "/api/Users/UpdatePersonanlInfo",
    method: "post",
    data: params
  })
}

/**获取个人信息数据*/
export function getPersonInfoApi(params: any) {
  return request({
    url: "/api/Users/GetPersonInfo",
    method: "get",
    params
  })
}
/**获取用户数据*/
export function getUserDataApi(params: User.getUserRequestData) {
  return request({
    url: "/api/Users/GetUserData",
    method: "post",
    data: params
  })
}
// 新增用户
export function addUserApi(params: any) {
  return request({
    url: "/api/Users/AddUser",
    method: "post",
    data: params
  })
}
// 修改用户
export function updateUserDataApi(params: any) {
  return request({
    url: "/api/Users/UpdateUserData",
    method: "post",
    data: params
  })
}
//删除用户
export function deleteUserApi(params: any) {
  return request({
    url: "/api/Users/DeleteUser",
    method: "post",
    data: params
  })
}
// 批量删除用户
export function batchDelUserApi(params: any) {
  return request({
    url: "/api/Users/BatchDelUser",
    method: "post",
    data: params
  })
}
// 更改用户状态或密码
export function changeStatusOrPasswordApi(params: any) {
  return request({
    url: "/api/Users/ChangeStatusOrPassword",
    method: "post",
    data: params
  })
}
