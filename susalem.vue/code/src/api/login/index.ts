import { request } from "@/utils/service"
import type * as Login from "./types/login"

/** 登录并返回 Token */
export function loginApi(data: Login.LoginRequestData) {
  return request<Login.LoginResponseData>({
    url: "/api/Login/GetToken",
    method: "post",
    data
  })
}

/** 获取用户详情 */
export function getUserInfoApi(params: any) {
  return request<Login.UserInfoResponseData>({
    url: "/api/Login/Info",
    method: "post",
    data: params
  })
}

/** 获取后端路由 */
export function getDynamicRoutesApi() {
  return request<Login.UserInfoResponseData>({
    url: "/api/Login/GetDynamicRoutes",
    method: "get"
  })
}
