import { request } from '../request';

/** 角色分页 */
export function getSysData(params?: any) {
  return request<SystemInfo>({
    url: '/SysData/GetSystemInfo',
    method: 'get',
    params
  });
}
