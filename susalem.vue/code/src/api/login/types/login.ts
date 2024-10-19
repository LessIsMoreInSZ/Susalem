export interface LoginRequestData {
  /** admin 或 editor */
  username: string
  /** 密码 */
  password: string
  // /** 验证码 */
  // code: string
}

export type LoginCodeResponseData = ApiResponseData<string>

export type LoginResponseData = ApiResponseData<{ access_token: string, expires_in: number, token_type: string }>

export type UserInfoResponseData = ApiResponseData<{ username: string; roles: string[] }>
