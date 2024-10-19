import type { SysConfigRes } from '@/typings/res/sysconfig';
import { request } from '../request';

export function getConfigPage(params?: Api.SystemManage.UserSearchParams) {
  return request<SysConfigRes>({
    url: '/SysConfig/PageList',
    method: 'get',
    params
  });
}

// 修改用户
export function upConfig(data?: any) {
  return request<boolean>({
    url: '/SysConfig/Update',
    method: 'post',
    data
  });
}

// 添加p
export function addConfig(data: any) {
  return request<boolean>({
    url: `/SysConfig/Add`,
    method: 'post',
    data
  });
}

// 删除用户
export function deleteConfig(id?: number) {
  return request<boolean>({
    url: `/SysConfig/Delete/${id}`,
    method: 'post'
  });
}
