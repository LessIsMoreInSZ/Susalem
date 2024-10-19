import type { SysOnlineUserRecord } from '@/typings/res/sys_onlie_user_res.ts';
import { request } from '../request';
// import type { PaginatedData, SysOnlineUserRecord } from '../types';
/** 在线用户 */
export function getOnlineUser(params?: any) {
  return request<SysOnlineUserRecord>({
    url: '/SysOnlineUser/PageList',
    method: 'get',
    params
  });
}

/// 强制下线
export function ForceOffline(params?: string) {
  return request<Boolean>({
    url: '/SysOnlineUser/ForceOffline',
    method: 'get',
    params
  });
}

/// 强制下线
export function SendMsgToOne(data?: any) {
  return request<Boolean>({
    url: '/SysOnlineUser/SendMsgToOne',
    method: 'post',
    data
  });
}
