import { request } from "@/utils/service"
import type * as Login from "./types/login"


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
/** 退出登录接口 POST /api/login/outLogin */
export async function outLogin(options?: { [key: string]: any }) {
  return request<any>( {
    url:'/api/login/outLogin',
    method: 'POST',
    ...(options || {}),
  });
}


/** 登录接口 GET /api/login/account */
export async function loginApi(body: Login.LoginRequestData, options?: { [key: string]: any }) {
  var urlencoded = new URLSearchParams();
  urlencoded.append("client_id", "Susalem_Local_Web");
  urlencoded.append("client_secret", "Susalem321..");
  urlencoded.append("grant_type", "password");
  urlencoded.append("username", body.username || '');
  urlencoded.append("password", body.password || '');

  return request<any>({
    url:'/connect/token',
    method: 'POST',
    data: urlencoded,
    headers: {
      "Content-Type": "application/x-www-form-urlencoded",
    }
  });
}
